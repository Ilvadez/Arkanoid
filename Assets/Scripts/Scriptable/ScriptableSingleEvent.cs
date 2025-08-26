using System;
using UnityEditor.PackageManager;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/Single")]
public class ScriptableSingleEvent : ScriptableObject
{
    public event Action Event;

    public void InvokeEvent()
    {
        Event?.Invoke();
    }
}
