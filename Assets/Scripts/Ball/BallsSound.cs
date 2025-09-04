using Unity.VisualScripting;
using UnityEngine;

public class BallsSound : BaseAudioEffect
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        PlaySound(collision.gameObject.tag);
    }
        protected override void PlaySound(string tag)
    {
         switch (tag)
        {
            case string r when r == "Paddle" || r == "Wall":
                m_audioSource.PlayOneShot(m_effectClip, m_volume);
                break;
            
        }
    }

}
