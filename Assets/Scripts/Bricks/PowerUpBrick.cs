using Unity.VisualScripting;
using UnityEngine;

public class PowerUpBrick : Brick
{
    [SerializeField] private GameObject m_powerUpPrefab;
    Transform m_prefabParent;

    public override void Initialization(BrickCounter counter)
    {
        base.Initialization(counter);
        m_prefabParent = counter.transform;
    }
    public override void DestroyObject()
    {
        GameObject powerup = Instantiate(m_powerUpPrefab, transform.position, Quaternion.identity, m_prefabParent);
        powerup.transform.SetParent(m_counterBricks.transform);
        base.DestroyObject();
    }
}
