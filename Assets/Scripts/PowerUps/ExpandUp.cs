using UnityEngine;

public class ExpandUp : PowerUp
{
    [SerializeField]
    private float m_expandScale;
    [SerializeField]
    private float m_effectContinueTime;
    public override void TakePowerUp(TakePowerUps taker)
    {
        taker.ExpandPaddle(m_effectContinueTime, m_expandScale);
        Destroy(gameObject);
    }
}
