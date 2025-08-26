using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] protected float m_speed;
    protected Rigidbody2D m_rb;
    void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MoveDown();
    }
    protected virtual void MoveDown()
    {
        Vector2 velocity = Vector2.down * m_speed;
        m_rb.MovePosition(m_rb.position + velocity * Time.fixedDeltaTime);
    }
    public abstract void TakePowerUp(TakePowerUps taker);
}
