using System.Collections;
using UnityEngine;

public class BallPowerUp
{
    private BallMovement m_movement;
    private Coroutine m_coroutine;

    public BallPowerUp(BallMovement movement)
    {
        m_movement = movement;
    }

    public void ApplySlowPowerUp(float parametr, float delay)
    {
        if (m_coroutine != null)
        {
            m_movement.StopCoroutine(m_coroutine);
        }
        m_coroutine = m_movement.StartCoroutine(SlowUp(parametr, delay));
    }
    private IEnumerator SlowUp(float parametr, float delay)
    {
        float tempSpeed = m_movement.Velocity.magnitude;
        m_movement.SetSpeed(tempSpeed * parametr);
        yield return new WaitForSeconds(delay);
        m_movement.SetSpeed(tempSpeed);
        m_coroutine = null;
    }
}
