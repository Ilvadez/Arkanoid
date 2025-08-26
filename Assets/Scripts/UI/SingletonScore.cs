using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SingletonScore : MonoBehaviour
{
    public static SingletonScore Instant { get; private set; }
    [SerializeField] private ScriptableLivesEvent m_events;
    [SerializeField] private TextMeshProUGUI m_textScore;
    [SerializeField] private TextMeshProUGUI m_bestTextScore;
    private int m_score;
    private SaveScore m_save;
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
        m_events.EndLives += SaveScore;
    }
    void OnDisable()
    {
        m_events.EndLives -= SaveScore;
    }
    void Start()
    {
        UpdateText();
        int bestScore = m_save.GetScore("Best_Score");
        m_bestTextScore.text = $"{bestScore}";
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
