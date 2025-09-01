using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleMovement : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private InputController m_input;
    private const float m_yPosition = 0f;
    [SerializeField]
    private float m_speed;
    private Vector2 m_movementInput;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_input = GetComponent<InputController>();
    }
    void Update()
    {
        m_movementInput = m_input.Move;
        
    }
    void FixedUpdate()
    {
        MovePosition(m_movementInput);
    }
    private void MovePosition(Vector2 input)
    {
        float velocity = input.x * m_speed;
        m_rigidbody.MovePosition(m_rigidbody.position + new Vector2(velocity, m_yPosition) * Time.fixedDeltaTime);
    }

}
