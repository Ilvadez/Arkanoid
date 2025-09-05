using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Fade : MonoBehaviour
{
    public UnityEvent m_inFade = new UnityEvent();
    public UnityEvent m_endedFade = new UnityEvent();
    [SerializeField]
    private CanvasGroup m_canvas;
    [SerializeField]
    private float m_duration = 1f;

    public void StartFade()
    {
        StartCoroutine(FadeEffect());
    }

    IEnumerator FadeEffect()
    {
        float t = 0;
        while (t < m_duration)
        {
            t += Time.deltaTime;
            m_canvas.alpha = t / m_duration;
            yield return null;
        }
        m_inFade?.Invoke();
        t = m_duration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            m_canvas.alpha = t / m_duration;
            yield return null;
        }
        m_endedFade?.Invoke();
    }
}
