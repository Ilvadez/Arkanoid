using UnityEngine;

public abstract class IHitBrick : MonoBehaviour, IBrick , IHitBricks
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
        Destroy(gameObject);
    }
}
