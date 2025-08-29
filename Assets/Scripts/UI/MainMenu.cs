using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_objects = new List<GameObject>();
    [SerializeField] private ScriptableSingleEvent m_restartMenu;
    [SerializeField] private ScriptableSingleEvent m_endLives;
    void Awake()
    {
        m_restartMenu.Event += RestartMenu;
        m_endLives.Event += RestartMenu;
    }
    private void RestartMenu()
    {
        foreach (var item in m_objects)
        {
            item.gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
    }
    public void OnQuit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        m_restartMenu.Event -= RestartMenu;
        m_endLives.Event -= RestartMenu;

    }
}
