using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] 
    private float maxSpeed = 6;

    [SerializeField] 
    private bool facingRight = true;

    [SerializeField] 
    private float moveDirection = 1;

    [SerializeField] 
    private float jumpSpeed = 300;

    [SerializeField] 
    private bool doubleJumped = false;

    [SerializeField] 
    private bool isGrounded = false;

    [SerializeField] 
    private Transform groundCheck;

    [SerializeField] 
    private float groundRadius = 0.22f;

    [SerializeField] 
    private LayerMask whatIsGround = default(LayerMask);

    [SerializeField] 
    private float swordAttackSpeed = 600;

    [SerializeField] 
    private Transform swordSpawn = null;

    [SerializeField] 
    private Rigidbody swordPrefab = null;

    [SerializeField]
    private Animator anim = null;
    public Animator CharacterAnim => anim;

    [SerializeField] 
    private GameObject deadCharPrefab = null;

    [SerializeField]
    private Animator deadAnim = null;
    public Animator DeadAnim => deadAnim;

    private Rigidbody swordClone = null;

    private GameObject deadCharClone = null;
    
    private Rigidbody rb = null;

    private List<Renderer> renderers = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0;
        renderers = GetComponentsInChildren<Renderer>().ToList();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        SetDirection();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    private void SetDirection()
    {
        moveDirection = Input.GetAxis("Horizontal");
    }

    private void Movement()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        rb.velocity = new Vector2(moveDirection * maxSpeed, rb.velocity.y);
        anim.SetFloat ( "speed", Mathf.Abs(moveDirection) );
        anim.SetFloat ( "vSpeed", rb.velocity.y );
        anim.SetBool ( "isGrounded", isGrounded );
        if(isGrounded)
        {
            doubleJumped = false;
        }
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
        anim.SetBool ( "facingRight", facingRight );
    }

    private void Jump()
    {
        if((isGrounded || !doubleJumped))
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
        anim.SetTrigger ( "attacking" );
    }

    public void FireProjectile ()
    {
        swordClone = Instantiate(swordPrefab, swordSpawn.position, swordSpawn.rotation) as Rigidbody;
        swordClone.AddForce(swordSpawn.transform.right * swordAttackSpeed);
    }

    public void FallApartCharacter ()
    {
        deadCharClone = Instantiate ( deadCharPrefab, transform.position, transform.rotation );
        for ( int i = 0 ; i < renderers.Count ; i++ )
        {
            renderers [i].enabled = false;
        }
    }
}
