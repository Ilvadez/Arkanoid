using System;
using UnityEngine;

public class SlowUp : PowerUp
{
    [SerializeField]
    private EventForSpeedWithTime m_takeSlowPowerUp;
    [SerializeField]
    private float m_multipliSpeed;
    [SerializeField]
    private float m_effectContinueTime;
    public override void TakePowerUp(TakePowerUps taker)
    {
        m_takeSlowPowerUp?.InvokeEvent(m_multipliSpeed, m_effectContinueTime);
        Destroy(gameObject);
    }
}
