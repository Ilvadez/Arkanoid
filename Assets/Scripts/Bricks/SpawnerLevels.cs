using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerLevels : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_listLevels = new List<GameObject>(12);
    [SerializeField] private TextMeshProUGUI m_textCountBricks;
    [SerializeField] private TextMeshProUGUI m_textCurrentLevel;
    [SerializeField] private ScriptableLivesEvent m_events;
    [SerializeField] private ScriptableSingleEvent m_endLevels;
    private int m_currentIndex;
    private BrickCounter m_counter;
    private GameObject m_currentLevel;
    void Awake()
    {
        m_currentIndex = 0;
        SetCountBricks(m_currentIndex);
        UpdateTextLevel();
    }

    void NextLevel()
    {
        m_counter.EndBricks -= NextLevel;
        m_counter = null;
        m_events.InvokeEventEndBricks();
        if (m_currentIndex + 1 < m_listLevels.Count)
        {
            m_currentIndex++;
            SetCountBricks(m_currentIndex);
            UpdateTextLevel();
        }
        else if (m_currentIndex + 1 >= m_listLevels.Count)
        {
            m_endLevels.InvokeEvent();
        }
    }

    private void SetCountBricks(int indexLevel)
    {
        if (m_currentLevel != null)
        {
            Destroy(m_currentLevel);
        }
        m_currentLevel = Instantiate(m_listLevels[indexLevel], transform.position, Quaternion.identity);
        m_counter = m_currentLevel.GetComponent<BrickCounter>();
        m_counter.InitCountBricks(m_textCountBricks);
        m_counter.EndBricks += NextLevel;
    }
    private void UpdateTextLevel()
    {
        m_textCurrentLevel.text = $"{m_currentIndex + 1}";
    } 
}
