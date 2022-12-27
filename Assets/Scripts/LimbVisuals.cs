using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbVisuals : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    Limb limb;
    private void Awake()
    {
        limb = GetComponentInParent<Limb>();
        spriteRenderer= GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        
    }
}
