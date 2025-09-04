using UnityEngine;

public abstract class BaseAudioEffect : MonoBehaviour
{
    [SerializeField]
    protected AudioClip m_effectClip;
    protected AudioSource m_audioSource;
    public float LengthClip => m_effectClip.length;
    [SerializeField]
    protected float m_volume = 0.5f;
    protected void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    protected abstract void PlaySound(string tag);

}
