using UnityEngine;

public class PlayersSound : BaseAudioEffect
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlaySound(collision.tag);
    }
    protected override void PlaySound(string tagObject)
    {
        switch (tagObject)
        {
            case "PowerUp":
                m_audioSource.PlayOneShot(m_effectClip, m_volume);
                break;
        }
    }
}
