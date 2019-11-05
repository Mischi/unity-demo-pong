using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    float speed = 50;

    internal PaddleType paddleType;
    float height;

    private void Awake()
    {
        height = GetComponentInChildren<Collider>().bounds.size.y;
    }

    public void Init(PaddleType paddleType)
    {
        this.paddleType = paddleType;

        switch (this.paddleType)
        {
            case PaddleType.Right:
                transform.position = new Vector3(GameManager.topRight.x, 0, 0);
                transform.position += Vector3.left * transform.localScale.x * 2;
                transform.name = "PlayerRight";
                break;
            case PaddleType.Left:
                transform.position = new Vector3(GameManager.bottomLeft.x, 0, 0);
                transform.position += Vector3.right * transform.localScale.x * 2;
                transform.name = "PlayerLeft";
                break;
            default:
                throw new UnityException("Invalid playerType");
        }
    }

    void Update()
    {
        var move = GetInput() * Time.deltaTime * speed;

        if ((transform.position.y < GameManager.bottomLeft.y + height / 2 && move < 0) ||
            (transform.position.y > GameManager.topRight.y - height / 2 && move > 0))
        {
            move = 0;
        }

        transform.Translate(move * Vector3.up);
    }

    float GetInput()
    {
        switch (paddleType)
        {
            case PaddleType.Left:
                return Input.GetAxis("PaddleLeft");
            case PaddleType.Right:
                return Input.GetAxis("PaddleRight");
            default:
                throw new UnityException("Invalid playerType");
        }
    }
}
