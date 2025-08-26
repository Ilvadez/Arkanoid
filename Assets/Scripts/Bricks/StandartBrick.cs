using UnityEngine;

public class StandartBrick : IHitBrick
{
    public override void MinusLive(int power)
    {
        m_countHits -= power;
        if (m_countHits <= 0)
        {
            DestroyObject();
        }
    }
    private void OnDestroy()
    {
        m_counter.UpdateCount();
        SingletonScore.Instant.UpdateScore(m_data.Score);
    }
}
