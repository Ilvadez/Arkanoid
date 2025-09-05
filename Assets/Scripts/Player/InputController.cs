using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    [SerializeField]
    private EventWithoutParametr m_endLives;
    private InputSystem_Actions m_action;
    public event Action Jump;
    public Vector2 Move { get; private set; }
    [SerializeField]
    private float m_censative;
    void Awake()
    {
        m_action = new InputSystem_Actions();
    }
    public void OnEnable()
    {
        m_action.Player.Enable();
        m_action.Player.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        m_action.Player.Move.canceled += ctx => Move = Vector2.zero;
        m_action.Player.Look.performed += ctx => Move = ctx.ReadValue<Vector2>() * m_censative;
        m_action.Player.Look.canceled += ctx => Move = Vector2.zero;
        m_action.Player.Jump.performed += HandleJump;
        m_action.Player.Attack.performed += HandleJump;
        m_endLives.Event += OnDisable;

    }
    public void OnDisable()
    {
        m_action.Player.Jump.performed -= HandleJump;
        m_action.Player.Attack.performed -= HandleJump;
        m_action.Player.Disable();
        m_endLives.Event -= OnDisable;
    }
    private void HandleJump(InputAction.CallbackContext ctx)
    {
        Jump?.Invoke();
    }
}
