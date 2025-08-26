using TMPro;
using System;
using UnityEngine;

public class BrickCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_textCount;
    public int Count { get; private set; }
    public event Action EndBricks;
    void Awake()
    {

    }
    public void InitCountBricks(TextMeshProUGUI text)
    {
        m_textCount = text;
        Count = transform.childCount;
        UpdateText();
        foreach (IBrick i in transform.GetComponentsInChildren<IBrick>())
        {
            i.Init(this);
        } 
    }
    public void UpdateCount()
    {
        Count -= 1;
        UpdateText();
        if (Count <= 0)
        {
            EndBricks?.Invoke();
        }
    }

    private void UpdateText()
    {
        m_textCount.text = $"{Count}";
    }
    
}
