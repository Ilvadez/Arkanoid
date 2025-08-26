using System;
using UnityEngine;

public class SlowUp : PowerUp
{
    [SerializeField] private ScriptableSingleEventFloat m_takeSlowPowerUp;
    [SerializeField] private float m_multipliSpeed;
    [SerializeField] private float m_delay;
    public override void TakePowerUp(TakePowerUps taker)
    {
        m_takeSlowPowerUp?.InvokeEvent(m_multipliSpeed, m_delay);
        Destroy(gameObject);
    }
}
