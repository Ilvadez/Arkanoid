using System;
using TMPro;
using UnityEngine;

public class UiHealth : MonoBehaviour
{

    [SerializeField] private DataHealth m_data;
    [SerializeField] private TextMeshProUGUI m_uiHealth;
    [SerializeField] private ScriptableSingleEvent m_eventsBall;
    [SerializeField] private ScriptableSingleEvent m_endLives;
    [SerializeField] private ScriptableSingleEvent m_eventTakeHealth;
    private float m_countLives;
    void OnEnable()
    {
        m_eventsBall.Event += DecreseLives;
        m_eventTakeHealth.Event += IncreaseLives;
        m_countLives = m_data.CountLive;
        UpdateText();
    }
    public void DecreseLives()
    {
        m_countLives -= 1;
        if (m_countLives <= 0)
        {
            m_endLives.InvokeEvent();
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
        m_eventsBall.Event -= DecreseLives;
        m_eventTakeHealth.Event -= IncreaseLives;
    }
}
