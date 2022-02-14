using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == true)
        {
            return;
        }
        else if (GateManager.gateCurrentHP <= 0)
        {
            GameOver();
        }
    }

    // Indicates that the player has won
    public void Victory()
    {
        // TODO: Display victory screen and redirect to main menu

        isGameOver = true;
        Debug.Log("Victory");
    }

    // Indicates that the player has lost
    public void GameOver()
    {
        // TODO: Display game over screen and redirect to main menu

        isGameOver = true;
        Debug.Log("Game Over");
    }
}
