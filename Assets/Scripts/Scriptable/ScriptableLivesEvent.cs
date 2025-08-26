using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Event/Ball")]
public class ScriptableLivesEvent : ScriptableObject
{
    public event Action BallOffSide;
    public event Action EndLives;
    public event Action EndBricks;
    public void InvokeEventBallOffside()
    {
        BallOffSide?.Invoke();
    }
    public void InvokeEventEndLives()
    {
        EndLives?.Invoke();
    }
    public void InvokeEventEndBricks()
    {
        EndBricks?.Invoke();
    }
}
