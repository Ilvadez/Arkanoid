using System;
using TMPro;
using UnityEngine;

public class UiHealth : MonoBehaviour
{
    [SerializeField] private float m_countLives;
    [SerializeField] private TextMeshProUGUI m_uiHealth;
    [SerializeField] private ScriptableLivesEvent m_eventBall;
    [SerializeField] private ScriptableSingleEvent m_eventTakeHealth;
    void OnEnable()
    {
        m_eventBall.BallOffSide += DecreseLives;
        m_eventTakeHealth.Event += IncreaseLives;
    }
    void Start()
    {
        UpdateText();
    }
    public void DecreseLives()
    {
        m_countLives -= 1;
        if (m_countLives <= 0)
        {
            m_eventBall.InvokeEventEndLives();
        }
        UpdateText();
    }
    void IncreaseLives()
    {
        m_countLives += 1;
        UpdateText();
    }
    void UpdateText()
    {
        m_uiHealth.text = $"{m_countLives}";
    }
    void OnDisable()
    {
        m_eventBall.BallOffSide -= DecreseLives;
        m_eventTakeHealth.Event -= IncreaseLives;
    }
}
