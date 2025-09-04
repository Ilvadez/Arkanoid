using UnityEditor.Rendering;
using UnityEngine;

public abstract class Brick : MonoBehaviour, IInitializeBrick , IHitBrick
{
    [SerializeField]
    protected DataBrick m_dataBrick;
    protected BrickCounter m_counterBricks;
    private BaseAudioEffect m_soundEffect;
    protected const int m_numberOfDestroy = 0;
    protected int m_countHits;
    void Awake()
    {
        m_countHits = m_dataBrick.CountHits;
        m_soundEffect = GetComponent<BrickSound>();
    }
    public virtual void Initialization(BrickCounter counter)
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
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, m_soundEffect.LengthClip);
    }
}
