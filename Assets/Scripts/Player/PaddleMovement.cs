using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private const float m_yPosition = 0f;
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private Vector3 m_restartPosition;
    private Rigidbody2D m_rigidbody;
    private InputController m_input;
    private Position m_position;
    private Vector2 m_movementInput;
    [SerializeField]
    private EventWithoutParametr m_goneNextLevel;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_input = GetComponent<InputController>();
        m_position = new Position(transform);
    }
    void OnEnable()
    {
        m_goneNextLevel.Event += () => m_position.RestartPosition(m_restartPosition);
    }
    void Update()
    {
        m_movementInput = m_input.Move;
    }
    void FixedUpdate()
    {
        MovePosition(m_movementInput);
    }
    void OnDisable()
    {
        m_goneNextLevel.Event -= () => m_position.RestartPosition(m_restartPosition);
    }
    private void MovePosition(Vector2 input)
    {
        float velocity = input.x * m_speed;
        m_rigidbody.MovePosition(m_rigidbody.position + new Vector2(velocity, m_yPosition) * Time.fixedDeltaTime);
    }

}
