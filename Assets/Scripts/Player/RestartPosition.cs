using System;
using UnityEngine;

public class Position
{
    private Transform m_transform;
    public Position(Transform transform)
    {
        m_transform = transform;
    }
    public void RestartPosition(Vector3 position)
    {
        m_transform.position = position;
    }
}