using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 1f;
    private Rigidbody2D rb;
    public bool isGround;
    private Animator animator;
    private Transform t;
    private bool isFacingRight = false;
    private float horizontal;
    private int quantityJumps = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
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
        if(jump != 0 && !isGround && Input.GetButtonDown("Jump") && quantityJumps == 1)
        {
            animator.SetTrigger("doubleJump");
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            quantityJumps = 0;
        }
        Flip();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGround = true;
        }

        quantityJumps = 1;

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

    private void Flip()
    {
        if(isFacingRight && horizontal > 0f || !isFacingRight && horizontal < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localscale = transform.localScale;
            localscale.x *= -1;
            transform.localScale = localscale;
        }
    }
}
