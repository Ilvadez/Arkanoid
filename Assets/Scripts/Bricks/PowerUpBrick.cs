using Unity.VisualScripting;
using UnityEngine;

public class PowerUpBrick : IHitBrick
{
    [SerializeField] private GameObject m_powerUpPrefab;
    public override void MinusLive(int power)
    {
        m_countHits -= power;
        if (m_countHits <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        Instantiate(m_powerUpPrefab,transform.position, Quaternion.identity);
        m_counter.UpdateCount();
        SingletonScore.Instant.UpdateScore(m_data.Score);
    }
}
