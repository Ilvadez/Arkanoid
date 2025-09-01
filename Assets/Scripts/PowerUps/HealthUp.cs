using UnityEngine;

public class HealthUp : PowerUp
{
    [SerializeField]
    EventWithoutParametr m_event;
    public override void TakePowerUp(TakePowerUps taker)
    {
        m_event?.InvokeEvent();
        Destroy(gameObject);
    }
}
