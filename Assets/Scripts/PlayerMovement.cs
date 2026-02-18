using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 3f;
    public Rigidbody2D rb;
    public bool isGround;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float move = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Vertical");
        animator.SetBool("isGround", isGround);
        animator.SetFloat("movement", move);
        if(move != 0)
        {
            rb.velocity = new Vector2(move*speed, rb.velocity.y);
        }
        if(jump != 0 && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGround = true;
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGround = false;
        }
    }
}
