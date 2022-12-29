using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMove : MonoBehaviour
{
    [SerializeField] Transform mid,last;
    [HideInInspector]public Transform hedef;
    Vector3 v1, v2, v3;
    private void Awake()
    {
        v2 = mid.position;
        v3 = last.position;
    }
    void OnEnable()
    {
        transform.position = Camera.main.WorldToScreenPoint(hedef.position);
        v1 = transform.position;
        StartCoroutine(Scaling());
    }
    
    IEnumerator Scaling() 
    {
        
        float time = 0;
        bool a = false;
        while (true)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero,Vector3.one,time);
            time += Time.deltaTime;
            if (time >= 0.5f && !a)
            {
                //StopAllCoroutines();
                StartCoroutine(Moving());
                a = true;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator Moving()
    {
        float time = 0;
        
        while (true)
        {
            Vector3 a = Vector3.Lerp(v1, v2, time);
            Vector3 b = Vector3.Lerp(v2, v3, time);
            transform.position = Vector3.Lerp(a, b, time);
            time += Time.deltaTime;
            if (time >= 1)
            {
                StopAllCoroutines();
                gameObject.SetActive(false);
                GameManager.Instance.ScoreUp(1);
                   
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
