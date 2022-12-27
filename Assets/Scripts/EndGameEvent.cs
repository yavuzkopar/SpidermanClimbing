using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameEvent : MonoBehaviour
{

    [SerializeField] UnityEvent winEvent;

    void WinEventOn()
    {
        winEvent?.Invoke();
    }
}
