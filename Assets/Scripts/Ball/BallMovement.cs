
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    private EventWithoutParametr m_wentFromScene;
    [SerializeField]
    private EventWithoutParametr m_endedBricksOnLevel;
    [SerializeField]
    private EventForSpeedWithTime m_tookSlowPowerUp;
    private ReflectBall m_reflect;
    private LimitSpeed m_limitSpeed;
    private Rigidbody2D m_rigidBody;
    private Transform m_transform;
    private Coroutine m_slowCoroutine;
    private const int m_powerHit = 1;
    private const float m_restartPosition = -10f;
    [SerializeField]
    private float m_startSpeed;
    [SerializeField]
    private float m_maxSpeed;
    [SerializeField]
    private float m_multipleSpeed;
    private Vector2 m_velocity;
    private Vector2 m_direction;
    private bool m_isTouch = false;
    void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_transform = GetComponent<Transform>();
        m_reflect = new ReflectBall();
        m_limitSpeed = new LimitSpeed();
    }
    void OnEnable()
    {
        m_endedBricksOnLevel.Event += SettupBall;
        m_tookSlowPowerUp.Event += TakeSlowPowerUp;
        SettupBall();
    }
    void Update()
    {
        RestartBall();
    }
    void FixedUpdate()
    {
        MovePosition(m_velocity);
    }
    void OnDisable()
    {
        m_endedBricksOnLevel.Event -= SettupBall;
        m_tookSlowPowerUp.Event -= TakeSlowPowerUp;
    }
    private void MovePosition(Vector2 velocity)
    {
        m_rigidBody.MovePosition(new Vector2(m_transform.position.x, m_transform.position.y) + velocity * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactWithEnviroment(collision);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            m_isTouch = false;
        }
    }
    private void RestartBall()
    {
        if (m_transform.position.y <= m_restartPosition)
        {
            m_wentFromScene.InvokeEvent();
            SettupBall();
        }
    }
    private void StartBall(Vector2 direction)
    {
        BallLaunch.Instant.StartedBallAction -= StartBall;
        m_rigidBody.simulated = true;
        m_direction = direction.normalized;
        m_velocity = m_direction * m_startSpeed;
    }
    private void SettupBall()
    {
        m_velocity = Vector2.zero;
        m_rigidBody.simulated = false;
        BallLaunch.Instant.StartedBallAction += StartBall;
        BallLaunch.Instant.SettupBall(transform);
    }
    private void ContactWithEnviroment(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            if (!m_isTouch)
            {
                m_direction = m_reflect.GetDirectionFromBrick(collision, m_direction);
                TakeSpeedAfterContact();
            }
            m_isTouch = true;
            collision.gameObject.GetComponent<IHitBrick>().MinusLive( m_powerHit);
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            m_direction = m_reflect.GetDirectionFromPaddle(collision, m_transform);
            m_velocity = m_direction * (m_velocity.magnitude * m_multipleSpeed);
            m_velocity = m_limitSpeed.LimitVelocity(m_velocity, m_maxSpeed);
        }
        else
        {
            m_direction = m_reflect.GetDirectionReflect(m_direction, collision.GetContact(0).normal);
            TakeSpeedAfterContact();
        }
    }
    private void TakeSpeedAfterContact()
    {
         m_velocity = m_direction * m_velocity.magnitude;
    }
    private void TakeSlowPowerUp(float parametr, float delay)
    {
        if (m_slowCoroutine != null)
        {
            StopCoroutine(m_slowCoroutine);
        }
        m_slowCoroutine = StartCoroutine(SlowUp(parametr, delay));
    }

    private IEnumerator SlowUp(float parametr, float delay)
    {
        float tempSpeed = m_velocity.magnitude;
        m_velocity = m_direction * (tempSpeed * parametr);
        yield return new WaitForSeconds(delay);
        m_velocity = m_direction * tempSpeed;
        m_slowCoroutine = null;
    }

}
