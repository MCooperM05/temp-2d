using System.Numerics;
using UnityEngine;

public class GeiserMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float pauseTime = 0.25f;

    private UnityEngine.Vector2 startPosition = new UnityEngine.Vector2(0.14f,-9.1f);
    private UnityEngine.Vector2 topPosition = new UnityEngine.Vector2(0.14f,-2.4f);

    private enum MoveState
    {
        Idle,
        MovingUp,
        PausingAtTop,
        MovingDown
    }

    private MoveState state = MoveState.Idle;
    private float pauseTimer;

    void Start()
    {
        startPosition = transform.position;
        topPosition = startPosition + UnityEngine.Vector2.up * moveDistance;
    }

    void Update()
    {
        switch (state)
        {
            case MoveState.MovingUp:
                transform.position = UnityEngine.Vector2.MoveTowards(transform.position, topPosition, moveSpeed * Time.deltaTime);

                if (UnityEngine.Vector2.Distance(transform.position, topPosition) <= 0.001f)
                {
                    transform.position = topPosition;
                    state = MoveState.PausingAtTop;
                    pauseTimer = pauseTime;
                }
                break;

            case MoveState.PausingAtTop:
                pauseTimer -= Time.deltaTime;
                if (pauseTimer <= 0f)
                {
                    state = MoveState.MovingDown;
                }
                break;

            case MoveState.MovingDown:
                transform.position = UnityEngine.Vector2.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

                if (UnityEngine.Vector2.Distance(transform.position, startPosition) <= 0.001f)
                {
                    transform.position = startPosition;
                    state = MoveState.Idle;
                }
                break;
        }
    }

    public void GoUp()
    {
        startPosition = transform.position;
        topPosition = startPosition + UnityEngine.Vector2.up * moveDistance;
        state = MoveState.MovingUp;
    }
}