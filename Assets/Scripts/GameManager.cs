using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static Vector3 bottomLeft;
    public static Vector3 topRight;

    public UnityEvent onGameOver;

    [SerializeField]
    Ball ballPrefab;

    [SerializeField]
    Paddle paddlePrefab;

    Game game;
    Player player1 = new Player { name = "Foo" };
    Player player2 = new Player { name = "Baz" };

    Paddle paddleLeft;
    Paddle paddleRight;
    Ball ball;

    private void Awake()
    {
        if (onGameOver == null)
            onGameOver = new UnityEvent();
    }

    void NewGame(Player playerLeft, Player playerRight)
    {
        Debug.Log("Starting new Game");
        game = new Game(playerLeft, playerRight);
        paddleLeft.Init(PaddleType.Left);
        paddleRight.Init(PaddleType.Right);
        ball.Init();
    }

    void Start()
    {
        var zPositionInWorldView = Mathf.Abs(Camera.main.transform.position.z);
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zPositionInWorldView));
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, zPositionInWorldView));
        Debug.Log($"GameManager bottomLeft: {bottomLeft}, topRight: {topRight}");

        paddleLeft = Instantiate(paddlePrefab);
        paddleRight = Instantiate(paddlePrefab);
        ball = Instantiate(ballPrefab);
        ball.onWonRound.AddListener((PaddleType paddleType) =>
        {
            switch (paddleType)
            {
                case PaddleType.Left:
                    game.AddPoint(game.playerLeft);
                    break;
                case PaddleType.Right:
                    game.AddPoint(game.playerRight);
                    break;
                default:
                    break;
            }

            if (game.IsGameOver())
            {
                Debug.Log($"Game is over {game.playerLeftPoints}:{game.playerRightPoints}");
                onGameOver.Invoke();
            }
            else
            {
                Debug.Log($"Player won round {game.playerLeftPoints}:{game.playerRightPoints}");
                ball.Init();
            }
        });

        StartDemoGame();
    }

    public void StartDemoGame()
    {
        NewGame(player1, player2);
    }
}