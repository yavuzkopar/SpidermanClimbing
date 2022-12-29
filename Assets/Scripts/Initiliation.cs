using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initiliation : MonoBehaviour
{
    private void Awake()
    {
        int a;
        string level;
        //if (PlayerPrefs.HasKey("LastScene"))
        //{
        //    SceneManager.LoadScene(PlayerPrefs.GetInt("LastScene"));
        //    a = PlayerPrefs.GetInt("LastScene");
        //    a = a - 1;
        //}
       
            SceneManager.LoadScene(1);
            a = 0;
        

        level = a.ToString();
        TinySauce.OnGameStarted("1");
    }
}
