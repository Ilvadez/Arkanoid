using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    private EventWithoutParametr m_wentFromScene;
    [SerializeField]
    private EventWithoutParametr m_endedBricksOnLevel;
    [SerializeField,
    Tooltip("Event for activite a slow action")]
    private EventForSpeedWithTime m_tookSlowPowerUp;
    private ReflectBall m_reflect;
    private LimitSpeed m_limitSpeed;
    private Rigidbody2D m_rigidBody;
    private Transform m_transform;
    private BallPowerUp m_ballPowerUp;
    private const int m_powerHit = 1;
    private const float m_restartPosition = -10f;
    public Vector2 Velocity { get; private set; }
    [SerializeField,
    Tooltip("Speed when you launch the ball")]
    private float m_startSpeed;
    [SerializeField,
    Tooltip("Max speed for ball")]
    private float m_maxSpeed;
    [SerializeField,
    Range(1, 2),
    Tooltip("Multiplication factor when touching to paddle")]
    private float m_multipleSpeed;
    private Vector2 m_direction;
    private bool m_isTouch = false;
    void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_transform = GetComponent<Transform>();
        m_reflect = new ReflectBall();
        m_limitSpeed = new LimitSpeed();
        m_ballPowerUp = new BallPowerUp(this);
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
        MovePosition(Velocity);
    }
    void OnDisable()
    {
        m_endedBricksOnLevel.Event -= SettupBall;
        m_tookSlowPowerUp.Event -= TakeSlowPowerUp;
    }
    private void MovePosition(Vector2 velocity)
    {
        m_rigidBody.MovePosition(m_rigidBody.position + velocity * Time.fixedDeltaTime);
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
        SetSpeed(m_startSpeed);
    }
    private void SettupBall()
    {
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
                SetSpeed(Velocity.magnitude);
            }
            m_isTouch = true;
            collision.gameObject.GetComponent<IHitBrick>().MinusLive(m_powerHit);
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            m_direction = m_reflect.GetDirectionFromPaddle(collision, m_transform);
            SetSpeed(Velocity.magnitude * m_multipleSpeed);
            Velocity = m_limitSpeed.LimitVelocity(Velocity, m_maxSpeed);
        }
        else
        {
            m_direction = m_reflect.GetDirectionReflect(m_direction, collision.GetContact(0).normal);
            SetSpeed(Velocity.magnitude);
        }
    }
    public void SetSpeed(float speed)
    {
        Velocity = m_direction * speed;
    }
    private void TakeSlowPowerUp(float parametr, float delay)
    {
        m_ballPowerUp.ApplySlowPowerUp(parametr,delay);
    }
}
