using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{


    public void StartGame()
    {
        //If we have already played a game
        if(GameManager.instance != null)
        {
            //Destroy(GameManager.instance);
            //GameManager.instance.Awake();
            //GameManager.instance.boardScript.SendMessage("Start");
            GameManager.instance.RestartGame();
        }
        SceneManager.LoadScene("MainScene");
    }
}
