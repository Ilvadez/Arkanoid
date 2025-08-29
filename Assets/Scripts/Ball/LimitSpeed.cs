using UnityEngine;

public class LimitSpeed 
{
    public Vector2 LimitVelocity(Vector2 currentVelocity, float maxSpeed)
    {
        if (currentVelocity.magnitude >= maxSpeed)
        {
            return currentVelocity.normalized * maxSpeed;
        }
        return currentVelocity;
    }
}
