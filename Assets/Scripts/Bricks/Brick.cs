using UnityEngine;

public abstract class Brick : MonoBehaviour, IInitializeBrick , IHitBrick
{
    [SerializeField]
    protected DataBrick m_dataBrick;
    protected BrickCounter m_counterBricks;
    protected const int m_numberOfDestroy = 0;
    protected int m_countHits;
    void Awake()
    {
        m_countHits = m_dataBrick.CountHits;
    }
    public void Initialization(BrickCounter counter)
    {
        m_counterBricks = counter;
    }
    public virtual void MinusLive(int power)
    {
        m_countHits -= power;
        if (m_countHits <= m_numberOfDestroy)
        {
            DestroyObject();
        }
    }

    public virtual void DestroyObject()
    {
        m_counterBricks.UpdateCount();
        SingletonScore.Instant.UpdateScore(m_dataBrick.Score);
        Destroy(gameObject);
    }
}
