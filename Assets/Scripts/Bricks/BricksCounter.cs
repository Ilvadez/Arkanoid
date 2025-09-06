using TMPro;
using System;
using UnityEngine;

public class BrickCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_textCount;
    public int Count { get; private set; }
    public event Action EndedBricks;

    public void InitCountBricks(TextMeshProUGUI text)
    {
        m_textCount = text;
        Count = transform.childCount;
        UpdateText();
        foreach (IInitializeBrick i in transform.GetComponentsInChildren<IInitializeBrick>())
        {
            i.Initialization(this);
        }
    }
    public void UpdateCount()
    {
        Count -= 1;
        UpdateText();
        if (Count <= 0)
        {
            EndedBricks?.Invoke();
        }
    }

    private void UpdateText()
    {
        m_textCount.text = $"{Count}";
    }
    
}
