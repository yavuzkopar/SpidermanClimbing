using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initiliation : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("LastScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("LastScene"));
        }
        else
            SceneManager.LoadScene(1);
    }
}
