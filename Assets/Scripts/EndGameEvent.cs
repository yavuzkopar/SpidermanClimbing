using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class EndGameEvent : MonoBehaviour
{

    [SerializeField] UnityEvent winEvent;

    void WinEventOn()
    {
        winEvent?.Invoke();
        GameManager.Instance.SetScore();
        string a = SceneManager.GetActiveScene().name;
        TinySauce.OnGameFinished(true,PlayerPrefs.GetInt("Score") , a);
    }
}
