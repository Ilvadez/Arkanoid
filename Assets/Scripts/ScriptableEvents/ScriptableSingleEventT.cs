using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SingleT")]
public class EventForSpeedWithTime : ScriptableObject
{
    public event Action<float, float> Event;

    public void InvokeEvent(float parametr, float delay)
    {
        Event?.Invoke(parametr, delay);
    }

}
