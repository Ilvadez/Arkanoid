
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float m_startSpeed;
    [SerializeField] private float m_maxSpeed;
    [SerializeField] private float m_MultypleSpeed;
    [SerializeField] private ScriptableLivesEvent m_globalEvent;
    [SerializeField] private ScriptableSingleEventFloat m_takeSlowPowerUp;
    private Vector2 m_velocity;
    private Vector2 m_direction;
    private Rigidbody2D m_rigidBody;
    private Transform m_transform;
    private ReflectBall m_reflect;
    void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_transform = GetComponent<Transform>();
        m_reflect = new ReflectBall();
    }
    void OnEnable()
    {
        m_globalEvent.EndLives += OverGame;
        m_globalEvent.EndBricks += SettupBall;
        m_takeSlowPowerUp.Event += TakeSlowUp;
    }
    void Start()
    {
        SettupBall();
    }
    void Update()
    {
        if (m_transform.position.y <= -10f)
        {
            m_globalEvent.InvokeEventBallOffside();
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
        m_globalEvent.EndLives -= OverGame;
        m_globalEvent.EndBricks -= SettupBall;
        m_takeSlowPowerUp.Event -= TakeSlowUp;
    }
    private void MoveBall(Vector2 velocity)
    {
        m_rigidBody.MovePosition(new Vector2(m_transform.position.x, m_transform.position.y) + velocity * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            m_direction = m_reflect.GetDirectionFromBrick(collision, m_direction);
            m_direction = m_reflect.FixedReflect(m_direction, 0.2f);
            m_velocity = m_direction * m_velocity.magnitude;
            collision.gameObject.GetComponent<IHitBricks>().MinusLive(1);
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            m_direction = m_reflect.GetDirectionFromPaddle(collision, m_transform);
            m_direction = m_reflect.FixedReflect(m_direction, 0.2f);
            m_velocity = m_direction * (m_velocity.magnitude * m_MultypleSpeed);
            m_velocity = LimitVelocity(m_velocity, m_maxSpeed);
        }
        else
        {
            m_direction = m_reflect.GetDirectionReflect(m_direction, -collision.collider.transform.right);
            m_direction = m_reflect.FixedReflect(m_direction, 0.2f);
            m_velocity = m_direction * m_velocity.magnitude;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        float time = 0.1f;
        if (collision.gameObject.CompareTag("Wall"))
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                m_direction.x = m_direction.x * -1;
            
            }
        }
    }
    private Vector2 LimitVelocity(Vector2 currentVelocity, float maxSpeed)
    {
        if (currentVelocity.magnitude >= maxSpeed)
        {
            return currentVelocity.normalized * maxSpeed;
        }
        return currentVelocity;
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
    private void OverGame()
    {
        gameObject.SetActive(false);
    }
    private void TakeSlowUp(float parametr, float delay)
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
