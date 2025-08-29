using Unity.VisualScripting;
using UnityEngine;

public class PowerUpBrick : Brick
{
    [SerializeField] private GameObject m_powerUpPrefab;
    public override void MinusLive(int power)
    {
        m_countHits -= power;
        if (m_countHits <= 0)
        {
            DestroyObject();
        }
    }
    public override void DestroyObject()
    {
        GameObject powerup = Instantiate(m_powerUpPrefab, transform.position, Quaternion.identity);
        powerup.transform.SetParent(m_counter.transform);
        base.DestroyObject();
    }
}
