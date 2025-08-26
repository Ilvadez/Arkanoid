using UnityEngine;

public class ExpandUp : PowerUp
{
    [SerializeField] private float m_expandScale;
    [SerializeField] private float m_delay;
    public override void TakePowerUp(TakePowerUps taker)
    {
        taker.ExpandUp(m_delay, m_expandScale);
        Destroy(gameObject);
    }
}
