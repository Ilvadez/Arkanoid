using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    [SerializeField] private ScriptableSingleEvent m_endLives;
    private InputSystem_Actions m_action;
    public Vector2 Move { get; private set;}
    public event Action Jump;
    void Awake()
    {
        m_action = new InputSystem_Actions();
    }
    void OnEnable()
    {
        m_action.Player.Enable();
        m_action.Player.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        m_action.Player.Move.canceled += ctx => Move = Vector2.zero;
        m_action.Player.Jump.performed += HandleJump;
        m_endLives.Event += OnDisable;

    }
    void OnDisable()
    {
        m_action.Player.Jump.performed -= HandleJump;
        m_action.Player.Disable();
        m_endLives.Event -= OnDisable;
    }
    private void HandleJump(InputAction.CallbackContext ctx)
    {
        Jump.Invoke();
    }
}
