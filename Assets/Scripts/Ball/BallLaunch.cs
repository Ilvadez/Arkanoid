using System;
using UnityEngine;

public class BallLaunch : MonoBehaviour
{
    public event Action<Vector2> StartedBallAction;
    public static BallLaunch Instant { get; private set; }
    [SerializeField]
    private InputController m_input;
    private Transform m_positionSpawnBall;
    private readonly Vector3 m_positionBall = new Vector3(0.15f, 0.25f);
    [SerializeField]
    private Vector2 m_startVectorForBall;
    private bool m_isStarted = false;
    void Awake()
    {
        if (Instant == null && Instant != this)
        {
            Instant = this;
        }
        else
        {
            Destroy(this);
        }
        m_positionSpawnBall = transform;
    }
    void OnEnable()
    {
        if (Instant == null)
        {
            Instant = this;
        }
        m_input.Jump += StartBall;
    }
    void Update()
    {
        MovePosition();
    }
    void OnDisable()
    {
        Instant = null;
    }
    private void StartBall()
    {
        StartedBallAction?.Invoke(m_startVectorForBall);
        m_isStarted = false;
    }
    public void SettupBall(Transform settupTransform)
    {
        m_isStarted = true;
        settupTransform.position = m_positionSpawnBall.position;
        settupTransform.SetParent(m_positionSpawnBall);
    }

    private void MovePosition()
    {
        if (m_isStarted)
        {
            transform.position = m_input.gameObject.transform.position + m_positionBall;
        }
    }
}
