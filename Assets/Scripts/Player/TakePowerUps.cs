using System.Collections;
using UnityEngine;

public class TakePowerUps : MonoBehaviour
{
    private Coroutine m_expandCoroutine;
    private Vector3 m_startScale;
    void Awake()
    {
        m_startScale = transform.localScale;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            PowerUp powerUp = collision.gameObject.GetComponent<PowerUp>();
            powerUp.TakePowerUp(this);
        }
    }
    public void ExpandPaddle(float delay, float expandScale)
    {
        if (m_expandCoroutine != null)
        {
            StopCoroutine(m_expandCoroutine);
        }
        m_expandCoroutine = StartCoroutine(ExpandEffect(delay,expandScale));
    }

    private IEnumerator ExpandEffect(float delay, float expandScale)
    {
        transform.localScale = new Vector3(m_startScale.x * expandScale, m_startScale.y, m_startScale.z);
        yield return new WaitForSeconds(delay);
        transform.localScale = m_startScale;
        m_expandCoroutine = null;
    }
}
