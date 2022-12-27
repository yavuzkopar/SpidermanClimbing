using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTextMove : MonoBehaviour
{
    [SerializeField] float _speed, _range;
    [SerializeField] bool isUpDown;

    void Update()
    {
        if (!isUpDown)
            transform.localPosition = new Vector3(Mathf.Sin(Time.time * _speed) * _range, transform.localPosition.y, transform.localPosition.z);
        else
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Sin(Time.time * _speed) * _range, transform.localPosition.z);

    }
}
