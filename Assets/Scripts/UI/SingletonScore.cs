using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SingletonScore : MonoBehaviour
{
    public static SingletonScore Instant { get; private set; }
    [SerializeField]
    private EventWithoutParametr m_endLives;
    [SerializeField]
    private TextMeshProUGUI m_textScore;
    [SerializeField]
    private TextMeshProUGUI m_bestTextScore;
    private SaveScore m_save;
    private const int m_startScore = 0;
    private int m_score;
    void Awake()
    {
        if (Instant != this && Instant != null)
        {
            Destroy(this);
        }
        else
        {
            Instant = this;
        }
        m_save = new SaveScore();
    }
    void OnEnable()
    {
        m_score = m_startScore;
        UpdateText();
        m_endLives.Event += SaveScore;
    }
    void Start()
    {
        int bestScore = m_save.GetScore("Best_Score");
        m_bestTextScore.text = $"{bestScore}";
    }
    void OnDisable()
    {
        m_endLives.Event -= SaveScore;

    }
    public void UpdateScore(int score)
    {
        m_score += score;
        UpdateText();
    }

    private void UpdateText()
    {
        m_textScore.text = $"{m_score}";
    }
    private void SaveScore()
    {
        int bestScore = m_save.GetScore("Best_Score");
        if (m_score > bestScore)
        {
            m_save.Save("Best_Score", m_score);
        }
    }
}
