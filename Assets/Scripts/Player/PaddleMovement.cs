using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private float m_speed;

    private Vector2 m_movementInput;
    private Rigidbody2D m_rigidbody;
    private InputController m_input;

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
        MoveObject(m_movementInput);
    }
    private void MoveObject(Vector2 input)
    {
        float velocity = input.x * m_speed;
        m_rigidbody.MovePosition(m_rigidbody.position + new Vector2(velocity, 0f) * Time.fixedDeltaTime);
    }

}
