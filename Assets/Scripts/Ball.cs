using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnWonRoundUnityEvent : UnityEvent<PaddleType>
{ }

public class Ball : MonoBehaviour
{
    public OnWonRoundUnityEvent onWonRound;

    [SerializeField]
    float speed = 10;

    bool isActive;
    float radius;
    Vector3 direction;

    public void Init()
    {
        isActive = true;
        direction = new Vector3(1, 1, 0).normalized;
        transform.position = Vector3.zero;
    }

    void Awake()
    {
        if (onWonRound == null)
            onWonRound = new OnWonRoundUnityEvent();

        radius = GetComponentInChildren<Collider>().bounds.size.y / 2;
    }

    void Update()
    {
        if (isActive)
        {
            transform.Translate(direction * speed * Time.deltaTime);

            // invert direction if the ball reaches top or bottom boundaries
            if ((transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0) ||
                (transform.position.y > GameManager.topRight.y - radius && direction.y > 0))
            {
                direction.y = -direction.y;
            }

            // announce winner if the ball reaches left or right boundaries
            if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0)
            {
                isActive = false;
                onWonRound.Invoke(PaddleType.Right);
            }
            else if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
            {
                isActive = false;
                onWonRound.Invoke(PaddleType.Left);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var paddle = other.GetComponentInChildren<Paddle>();
        if (paddle != null)
        {
            if ((paddle.paddleType == PaddleType.Left && direction.x < 0) ||
                (paddle.paddleType == PaddleType.Right && direction.x > 0))
            {
                direction.x = -direction.x;
            }
        }
    }
}
