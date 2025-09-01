using System;
using UnityEditor.PackageManager;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/Single")]
public class EventWithoutParametr : ScriptableObject
{
    public event Action Event;

    public void InvokeEvent()
    {
        Event?.Invoke();
    }
}
