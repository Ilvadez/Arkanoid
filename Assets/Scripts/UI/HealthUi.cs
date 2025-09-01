using System;
using TMPro;
using UnityEngine;

public class UiHealth : MonoBehaviour
{

    [SerializeField]
    private DataHealth m_data;
    [SerializeField]
    private TextMeshProUGUI m_uiHealth;
    [SerializeField]
    private EventWithoutParametr m_eventsBall;
    [SerializeField]
    private EventWithoutParametr m_endLives;
    [SerializeField]
    private EventWithoutParametr m_eventTakeHealth;
    private const int m_numberGameOver = 0;
    private const int m_numberChangeLive = 1;
    private float m_countLives;
    void OnEnable()
    {
        m_eventsBall.Event += DecreseLives;
        m_eventTakeHealth.Event += IncreaseLives;
        m_countLives = m_data.CountLive;
        UpdateText();
    }
        void OnDisable()
    {
        m_eventsBall.Event -= DecreseLives;
        m_eventTakeHealth.Event -= IncreaseLives;
    }
    public void DecreseLives()
    {
        m_countLives -= m_numberChangeLive;
        if (m_countLives <= m_numberGameOver)
        {
            m_endLives.InvokeEvent();
        }
        UpdateText();
    }
    void IncreaseLives()
    {
        m_countLives += m_numberChangeLive;
        UpdateText();
    }
    void UpdateText()
    {
        m_uiHealth.text = $"{m_countLives}";
    }
}
