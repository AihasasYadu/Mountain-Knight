using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 6;
    [SerializeField] private bool facingRight = true;
    [SerializeField] private float moveDirection = 1;
    [SerializeField] private float jumpSpeed = 300;
    [SerializeField] private bool doubleJumped = false;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.22f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float swordAttackSpeed = 600;
    [SerializeField] private Transform swordSpawn;
    [SerializeField] private Rigidbody swordPrefab;
    private Rigidbody swordClone;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        SetDirection();
        Jump();
        Attack();
    }

    private void SetDirection()
    {
        moveDirection = Input.GetAxis("Horizontal");
    }

    private void Movement()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if(isGrounded)
        {
            doubleJumped = false;
        }
        rb.velocity = new Vector2(moveDirection * maxSpeed, rb.velocity.y);
        if(moveDirection > 0.0f && !facingRight)
        {
            Flip();
        }
        else if(moveDirection < 0.0f && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    private void Jump()
    {
        if((isGrounded || !doubleJumped) && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0, jumpSpeed));
            if(!doubleJumped && !isGrounded)
            {
                doubleJumped = true;
            }
        }
    }

    private void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            swordClone = Instantiate(swordPrefab, swordSpawn.position, swordSpawn.rotation) as Rigidbody;
            swordClone.AddForce(swordSpawn.transform.right * swordAttackSpeed);
        }
    }
}
