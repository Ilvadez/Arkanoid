using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    protected Rigidbody2D m_rigidbody2d;
    [SerializeField]
    protected float m_speed;
    void Awake()
    {
        m_rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MoveDown();
    }
    protected virtual void MoveDown()
    {
        Vector2 velocity = Vector2.down * m_speed;
        m_rigidbody2d.MovePosition(m_rigidbody2d.position + velocity * Time.fixedDeltaTime);
    }
    public abstract void TakePowerUp(TakePowerUps taker);
}
