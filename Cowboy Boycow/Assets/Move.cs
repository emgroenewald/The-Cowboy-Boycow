using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float jumpForce = 3;
    [SerializeField] ContactFilter2D groundFilter;
    [SerializeField] Rigidbody2D rb;
    Vector2 inputDir;
    CapsuleCollider2D coll;
    SpriteRenderer sprite;
    bool grounded = false;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rb.GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = new Vector2(inputDir.x * speed, rb.velocity.y);
        rb.velocity = velocity;
        grounded = coll.IsTouching(groundFilter);


        if (jump)
        {
            Jump();
        }
    }

    public void SetInputDir(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();

        if (inputDir.x > 0 && sprite.flipX)
        {
            sprite.flipX = false;
        }
        if (inputDir.x < 0 && !sprite.flipX)
        {
            sprite.flipX = true;
        }
    }

    public void ActivateJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jump = true;
        }
        if (context.performed || context.canceled)
        {
            jump = false;
        }
    }

    private void Jump()
    {
        if (grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump = false;
        }
    }
}