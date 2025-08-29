
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float m_startSpeed;
    [SerializeField] private float m_maxSpeed;
    [SerializeField] private float m_MultypleSpeed;
    [SerializeField] private ScriptableSingleEvent m_ballOffside;
    [SerializeField] private ScriptableSingleEvent m_endBricksOnLevel;
    [SerializeField] private ScriptableSingleEventFloat m_takeSlowPowerUp;
    private Vector2 m_velocity;
    private Vector2 m_direction;
    private bool m_isTouch = false;
    private Rigidbody2D m_rigidBody;
    private Transform m_transform;
    private ReflectBall m_reflect;
    private LimitSpeed m_limitSpeed;
    void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_transform = GetComponent<Transform>();
        m_reflect = new ReflectBall();
        m_limitSpeed = new LimitSpeed();
    }
    void OnEnable()
    {
        m_endBricksOnLevel.Event += SettupBall;
        m_takeSlowPowerUp.Event += TakeSlowPowerUp;
        SettupBall();
    }
    void Update()
    {
        if (m_transform.position.y <= -10f)
        {
            m_ballOffside.InvokeEvent();
            SettupBall();
        }
        Debug.Log(m_direction);
        Debug.Log(m_maxSpeed);
    }
    void FixedUpdate()
    {
        MoveBall(m_velocity);
    }
    void OnDisable()
    {
        m_endBricksOnLevel.Event -= SettupBall;
        m_takeSlowPowerUp.Event -= TakeSlowPowerUp;
    }
    private void MoveBall(Vector2 velocity)
    {
        m_rigidBody.MovePosition(new Vector2(m_transform.position.x, m_transform.position.y) + velocity * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            if (!m_isTouch)
            {
                m_direction = m_reflect.GetDirectionFromBrick(collision, m_direction);
                m_velocity = m_direction * m_velocity.magnitude;
            }
            m_isTouch = true;
            collision.gameObject.GetComponent<IHitBricks>().MinusLive(1);
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            m_direction = m_reflect.GetDirectionFromPaddle(collision, m_transform);
            m_velocity = m_direction * (m_velocity.magnitude * m_MultypleSpeed);
            m_velocity = m_limitSpeed.LimitVelocity(m_velocity, m_maxSpeed);
        }
        else
        {
            m_direction = m_reflect.GetDirectionReflect(m_direction, collision.GetContact(0).normal);
            m_velocity = m_direction * m_velocity.magnitude;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
          m_isTouch = false;  
        }
    }
    private void StartBall(Vector2 direction)
    {
        BallLaunch.Instant.StartBallAction -= StartBall;
        m_rigidBody.simulated = true;
        m_direction = direction.normalized;
        m_velocity = m_direction * m_startSpeed;
    }
    private void SettupBall()
    {
        m_velocity = Vector2.zero;
        m_rigidBody.simulated = false;
        BallLaunch.Instant.StartBallAction += StartBall;
        BallLaunch.Instant.SettupBall(transform);
    }

    private void TakeSlowPowerUp(float parametr, float delay)
    {
        StartCoroutine(SlowUp(parametr, delay));
    }

    private IEnumerator SlowUp(float parametr, float delay)
    {
        float tempSpeed = m_velocity.magnitude;
        m_velocity = m_direction * (tempSpeed * parametr);
        yield return new WaitForSeconds(delay);
        m_velocity = m_direction * tempSpeed;
    }
}
