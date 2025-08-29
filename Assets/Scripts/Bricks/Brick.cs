using UnityEngine;

public abstract class Brick : MonoBehaviour, IBrick , IHitBricks
{
    [SerializeField] protected int m_countHits;
    [SerializeField] protected int m_score;
    [SerializeField] protected DataBrick m_data;
    
    protected BrickCounter m_counter;

    void Awake()
    {
        m_countHits = m_data.CountHits;
    }
    public void Init(BrickCounter counter)
    {
        m_counter = counter;
    }
    public abstract void MinusLive(int power);

    public virtual void DestroyObject()
    {
        m_counter.UpdateCount();
        SingletonScore.Instant.UpdateScore(m_data.Score);
        Destroy(gameObject);
    }
}
