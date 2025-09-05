using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerLevels : MonoBehaviour
{
    [SerializeField]
    private EventWithoutParametr m_endedLives;
    [SerializeField]
    private EventWithoutParametr m_endedBricks;
    [SerializeField]
    private EventWithoutParametr m_endedLevels;
    private BrickCounter m_counter;
    [SerializeField]
    private TextMeshProUGUI m_textCountBricks;
    [SerializeField]
    private TextMeshProUGUI m_textCurrentLevel;
    [SerializeField]
    private List<GameObject> m_listLevels = new List<GameObject>(12);
    private GameObject m_currentLevel;
    [SerializeField]
    private Fade m_fadeBetweenLevel;
    private const int m_startIndex = 0;
    private int m_currentIndex;
    void Awake()
    {
        m_currentIndex = m_startIndex;
    }
    void OnEnable()
    {
        m_endedLives.Event += DestroyObject;
        m_fadeBetweenLevel.m_inFade.AddListener(NextLevel);
        SetCountBricks(m_currentIndex);
        UpdateTextLevel();
    }
    void OnDisable()
    {

        m_endedLives.Event -= DestroyObject;
    }
    void NextLevel()
    {
        m_counter = null;
        m_endedBricks.InvokeEvent();
        if (m_currentIndex + 1 < m_listLevels.Count)
        {
            m_currentIndex++;
            SetCountBricks(m_currentIndex);
            UpdateTextLevel();
        }
        else if (m_currentIndex + 1 >= m_listLevels.Count)
        {
            m_endedLevels.InvokeEvent();
        }
    }
    public void MoveNextLevel()
    {
        m_fadeBetweenLevel.StartFade();
        m_counter.EndedBricks -= MoveNextLevel;
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
        m_counter.EndedBricks += MoveNextLevel;
    }
    private void UpdateTextLevel()
    {
        m_textCurrentLevel.text = $"{m_currentIndex + 1}";
    }
    private void DestroyObject()
    {
        Destroy(m_currentLevel);
        m_currentIndex = m_startIndex;
    }
}
