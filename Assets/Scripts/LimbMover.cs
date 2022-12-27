using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbMover : MonoBehaviour
{
    Vector3 offset;
    float zCoord;
    [SerializeField] LayerMask limbLayer;
    Limb currentLimb;
    private void Start()
    {
        StartCoroutine(HitUpdate());

    }
    IEnumerator HitUpdate()
    {
        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit, 1000f, limbLayer);
            if (hasHit)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    offset = hit.transform.position - hit.point;
                    currentLimb = hit.transform.GetComponent<Limb>();
                }
                if (Input.GetMouseButton(0))
                {
                    currentLimb.transform.position = offset + hit.point;
                    currentLimb.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, 0);
                }
            }
            yield return null;
        }
    }
}
