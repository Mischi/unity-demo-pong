using UnityEngine;
using UnityEditor;

public class Game
{ 
    public Player playerLeft;
    public Player playerRight;

    public int playerLeftPoints;
    public int playerRightPoints;

    public Game(Player playerLeft, Player playerRight)
    {
        this.playerLeft = playerLeft;
        this.playerRight = playerRight;
        playerLeftPoints = 0;
        playerRightPoints = 0;
    }

    public void AddPoint(Player player)
    {
        if (player == playerLeft)
        {
            playerLeftPoints++;
        }
        else if (player == playerRight)
        {
            playerRightPoints++;
        }
    }

    public bool IsGameOver()
    {
        return playerLeftPoints >= 3 || playerRightPoints >= 3;
    }
}