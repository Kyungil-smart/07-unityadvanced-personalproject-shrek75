using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    PlayerInput playerInput;
    BoxCollider2D coll;
    SpriteRenderer spriteRenderer;
    Animator anim;

    InputAction moveAction;
    InputAction jumpAction;

    Vector2 moveInput;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    void OnEnable()
    {
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        jumpAction.performed += OnJump;
    }

    void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        jumpAction.performed -= OnJump;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (anim.GetBool("isJump")) return;
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    
    void Update()
    {
        if (moveInput.x > 0) spriteRenderer.flipX = false;
        else if(moveInput.x < 0) spriteRenderer.flipX = true;
        if (rigid.linearVelocityX == 0) anim.SetBool("isWalking", false);
        else anim.SetBool("isWalking", true);

    }

    void FixedUpdate()
    {
        rigid.linearVelocity = new Vector2(moveInput.x * moveSpeed, rigid.linearVelocity.y);

        Debug.DrawRay(rigid.position, Vector2.down);
        var c = Physics2D.OverlapBox(rigid.position + Vector2.down * (coll.size.y / 2f), new Vector2(coll.size.x * 0.9f, 0.01f), 0, LayerMask.GetMask("Platform"));
        if (c != null)
        {
            //Debug.Log(c.name);
            anim.SetBool("isJump", false);
        }
        else anim.SetBool("isJump", true);
    }
}
