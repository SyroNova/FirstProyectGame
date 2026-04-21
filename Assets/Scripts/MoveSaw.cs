using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSaw : MonoBehaviour
{
    public Vector2 posIni;
    public Transform posFin;
    private Rigidbody2D rb;
    public float velocity = 3;
    private bool llego = false;

    private void Start()
    {
        posIni = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float dist = Vector2.Distance(rb.position, posFin.position);
        float dist2 = Vector2.Distance(rb.position, posIni);
        if(dist == 0)
        {
            llego = true;
        }
        if(dist2 == 0)
        {
            llego = false;
        }
        if (llego)
        {
            rb.position = Vector2.MoveTowards(rb.position, posIni, velocity * Time.deltaTime);
        }
        if (!llego)
        {
            rb.position = Vector2.MoveTowards(rb.position, posFin.position, velocity * Time.deltaTime);
        }


    }
}
