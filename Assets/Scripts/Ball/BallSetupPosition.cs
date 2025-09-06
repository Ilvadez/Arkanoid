using UnityEngine;

public class BallSetupPosition
{
    private const float m_restartPosition = -5.5f;
    private Rigidbody2D m_rigidBody;
    private BallMovement m_movement;
    public BallSetupPosition(BallMovement movement, Rigidbody2D rigidbody)
    {
        m_movement = movement;
        m_rigidBody = rigidbody;
    }
    public void SetupBall()
    {
        m_rigidBody.simulated = false;
        BallLaunch.Instant.StartedBallAction += StartBall;
        BallLaunch.Instant.SettupBall(m_rigidBody.gameObject.transform);
    }
    public void StartBall(Vector2 direction)
    {
        BallLaunch.Instant.StartedBallAction -= StartBall;
        m_rigidBody.simulated = true;
        m_movement.SetDirection(direction.normalized);
        m_movement.SetSpeed(m_movement.StartSpeed);
    }
    public void RestartBall(bool isActiveValue, EventWithoutParametr wentFromScene)
    {
        if (m_movement.transform.position.y <= m_restartPosition)
        {
            SetupBall();
            if (isActiveValue)
            {
                wentFromScene.InvokeEvent();
            }
        }
    }
}