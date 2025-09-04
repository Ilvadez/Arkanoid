using UnityEngine;

public class BrickSound : BaseAudioEffect
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlaySound(collision.gameObject.tag);
    }

    protected override void PlaySound(string tag)
    {
        if (tag.Equals("Ball"))
        {
            m_audioSource.PlayOneShot(m_effectClip, m_volume);
        }
    }
}
