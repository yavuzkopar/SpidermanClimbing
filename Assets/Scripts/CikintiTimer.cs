using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CikintiTimer : MonoBehaviour
{
    public bool isCounting;
    public float timer;
    [SerializeField] float timerr;
  //  [SerializeField] TextMesh textMesh;
    public bool isEnded;
    public bool isTaken;
    private void Update()
    {
        if (isEnded) return;
        if(!isEnded && timer <= 0)
        {
            GameManager.Instance.GameOverEvent?.Invoke();
            isEnded = true;
        }
        if (isCounting)
        {
            timer -= Time.deltaTime;
            float rounded = Mathf.RoundToInt(timer);
         //   textMesh.text = rounded.ToString();
            return;
        }
        else
        {
            timer = timerr;
        }
        

    }
    public void SetTextMeshEnable(bool v)
    {
      //  textMesh.gameObject.SetActive(v);
        timer = timerr;
      //  textMesh.text = timerr.ToString();
    }
}
