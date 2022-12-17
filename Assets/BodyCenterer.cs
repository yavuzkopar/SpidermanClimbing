using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCenterer : MonoBehaviour
{
    [SerializeField] Transform[] limbs = new Transform[4];

    Collider[] colliders;
    Rigidbody[] rbs;
    private void Start()
    {
        colliders = GetComponentsInChildren<Collider>();
        rbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody item in rbs)
        {
            item.isKinematic = true;
        }
        foreach (var item in colliders)
        {
            item.enabled = false;
        }
    }
    public void EnableColliders()
    {
        foreach (var item in colliders)
        {
            item.enabled = true;
        }
        foreach (Rigidbody item in rbs)
        {
            item.isKinematic = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CenterPoint();
    }
    Vector3 CenterPoint()
    {
        Vector3 total = Vector3.zero;
        for (int i = 0; i < limbs.Length; i++)
        {
            total += limbs[i].position;
        }
        return (total / 4) + Vector3.forward * 1.5f;
    }
    void MoveForward()
    {
        StartCoroutine(Move());
        StartCoroutine(Turn());
    }
    IEnumerator Move()
    {
        while (transform.localPosition.y > -2f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 2, Space.World);
          //  transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(-90, 180, 0), Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    float timer = 0;
    IEnumerator Turn()
    {
        while (timer<2)
        {
            timer += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(-90, 180, 0), Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
