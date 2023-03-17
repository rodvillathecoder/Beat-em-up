using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class OrdenPj : MonoBehaviour
{
    SpriteRenderer sr;

    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    //Cuanto más bajemos en la cordenada y menos profundidad tendrá el personaje.
    void Update()
    {
        sr.sortingOrder = -(int)(transform.position.y * 100);
    }
}
