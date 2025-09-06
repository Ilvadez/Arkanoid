using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private const int m_powerHit = 1;
    private const float m_restartPosition = -5.5f;
    [SerializeField] private EventWithoutParametr m_endedBricks;
    [SerializeField] private EventWithoutParametr m_wentFromScene;
    [SerializeField] private EventWithoutParametr m_goneToNextLevel;
    [SerializeField,
    Tooltip("Event for activite a slow action")] private EventForSpeedWithTime m_tookSlowPowerUp;
    [SerializeField,
    Tooltip("Speed when you launch the ball")] private float m_startSpeed;
    [SerializeField,
    Tooltip("Max speed for ball")] private float m_maxSpeed;
    [SerializeField,
    Range(1, 2),
    Tooltip("Multiplication factor when touching to paddle")] private float m_multipleSpeed;
    private ReflectBall m_reflect;
    private LimitSpeed m_limitSpeed;
    private Rigidbody2D m_rigidBody;
    private Transform m_transform;
    private BallPowerUp m_ballPowerUp;
    private BallSetupPosition m_setupPosition;
    private Vector2 m_direction;
    private bool m_isTouch = false;
    private bool m_isActiveValue = true;
    public float StartSpeed => m_startSpeed;
    public Vector2 Velocity { get; private set; }

    void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_transform = GetComponent<Transform>();
        m_reflect = new ReflectBall();
        m_limitSpeed = new LimitSpeed();
        m_setupPosition = new BallSetupPosition(this,m_rigidBody);
        m_ballPowerUp = new BallPowerUp(this);
    }
    void OnEnable()
    {
        m_endedBricks.Event += ChangeActiveValue;
        m_goneToNextLevel.Event += m_setupPosition.SetupBall;
        m_goneToNextLevel.Event += ChangeActiveValue;
        m_tookSlowPowerUp.Event += TakeSlowPowerUp;
        m_setupPosition.SetupBall();
    }
    void Update()
    {
        m_setupPosition.RestartBall(m_isActiveValue, m_wentFromScene);
    }
    void FixedUpdate()
    {
        MovePosition(Velocity);
    }
    void OnDisable()
    {
        m_endedBricks.Event -= ChangeActiveValue;
         m_goneToNextLevel.Event -= ChangeActiveValue;
        m_goneToNextLevel.Event -= m_setupPosition.SetupBall;
        m_tookSlowPowerUp.Event -= TakeSlowPowerUp;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            m_isTouch = false;
        }
    }
    public void SetSpeed(float speed)
    {
        Velocity = m_direction * speed;
    }
    public void SetDirection(Vector2 direction)
    {
        m_direction = direction;
    }
    private void MovePosition(Vector2 velocity)
    {
        m_rigidBody.MovePosition(m_rigidBody.position + velocity * Time.fixedDeltaTime);
    }
    private void ChangeActiveValue()
    {
        m_isActiveValue = !m_isActiveValue;
    }
    private void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            if (!m_isTouch)
            {
                SetDirection(m_reflect.GetDirectionFromBrick(collision, m_direction));
                SetSpeed(Velocity.magnitude);
            }
            m_isTouch = true;
            collision.gameObject.GetComponent<IHitBrick>().MinusLive(m_powerHit);
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            SetDirection(m_reflect.GetDirectionFromPaddle(collision, m_transform));
            SetSpeed(Velocity.magnitude * m_multipleSpeed);
            Velocity = m_limitSpeed.LimitVelocity(Velocity, m_maxSpeed);
        }
        else
        {
            SetDirection(m_reflect.GetDirectionReflect(m_direction, collision.GetContact(0).normal));
            SetSpeed(Velocity.magnitude);
        }
    }
    private void TakeSlowPowerUp(float parametr, float delay)
    {
        m_ballPowerUp.ApplySlowPowerUp(parametr, delay);
    }
}
