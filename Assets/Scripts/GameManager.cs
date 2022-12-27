using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations.Rigging;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UnityEvent GameOverEvent;
    public UnityEvent WinEvent;
    public Rig[] rigs;
    public LayerMask layerMask;
    public int counter;
    [SerializeField] Transform man;
    public Transform final;
    [SerializeField] GameObject FailPanel;
    private void Awake()
    {
        Instance = this;
        GameOverEvent.AddListener(InvokedFail);
    }
    
    void OpenFailPanel()
    {
        FailPanel.SetActive(true); 
    }
    void InvokedFail()
    {
        Invoke("OpenFailPanel",2f);
    }

    public void DisableRigs()
    {
        foreach (var item in rigs)
        {
            item.weight = 0;
        }
        foreach (CikintiTimer item in FindObjectsOfType(typeof(CikintiTimer)))
        {
            item.SetTextMeshEnable(false);
            item.isEnded = true;
        }
        foreach (SpriteRenderer item in FindObjectsOfType(typeof(SpriteRenderer)))
        {
            item.gameObject.SetActive(false);
        }

    }
    public void TransformDuzelt()
    {
        man.localEulerAngles = new Vector3(90, 0, 0);
        man.localPosition += Vector3.up * 4.5f;
        man.localPosition = new Vector3(man.localPosition.x, man.localPosition.y, 5.5f);
    }
    private void Update()
    {
        if (counter == 2)
        {
            WinEvent?.Invoke();
            Debug.Log("Wind");
            counter++;
        }
    }
}
