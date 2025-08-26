using System.Collections;
using UnityEngine;

public class TakePowerUps : MonoBehaviour
{
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
    public void ExpandUp(float delay, float expandScale)
    {
        StartCoroutine(ExpandEffect(delay,expandScale));
    }

    private IEnumerator ExpandEffect(float delay, float expandScale)
    {
        transform.localScale = new Vector3(m_startScale.x * expandScale, m_startScale.y, m_startScale.z);
        yield return new WaitForSeconds(delay);
        transform.localScale = m_startScale;
    }
}
