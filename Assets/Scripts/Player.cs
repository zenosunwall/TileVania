using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const string IS_RUNNING = "isRunning";
    const string IS_ClIMING = "isClimbing";
    const string INTERACTABLE_LAYER = "Ladder";
    const string STAGE_LAYER = "Stage";
    const string ENEMY_LAYER = "Enemy";
    const string HAZARDS_LAYER = "Hazards";

    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float climbSpeed = 1f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(0, 25f);

    bool isRunning = false;
    bool isCliming = false;
    bool isJumping = false;
    bool isAlive = true;

    float physicsGravity;

    SpriteRenderer playerSprite;
    Animator playerAnimation;
    Rigidbody2D physics;
    Collider2D feetCollider;
    Collider2D bodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerAnimation = GetComponent<Animator>();
        physics = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        physicsGravity = physics.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Death();
            Move();
        }
    }

    private void Move()
    {
        Climb();
        Jump();
        Run();
    }

    
    private void Run()
    {
        float runSpeed = Input.GetAxis("Horizontal") * moveSpeed;
        Vector2 playVelocity = new Vector2(runSpeed, physics.velocity.y);
        physics.velocity = playVelocity;

        isRunning = Mathf.Abs(physics.velocity.x) > Mathf.Epsilon;
        playerSprite.flipX = physics.velocity.x < 0;
        playerAnimation.SetBool(IS_RUNNING, isRunning);
    }

    private void Climb()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask(INTERACTABLE_LAYER)))
        {
            playerAnimation.SetBool(IS_ClIMING, false);
            physics.gravityScale = physicsGravity;
            return;
        }

        float climbSpeed = Input.GetAxis("Vertical") * this.climbSpeed;
        physics.gravityScale = 0;
        Vector2 playVelocity = new Vector2(physics.velocity.x, climbSpeed);
        physics.velocity = playVelocity;
        isCliming = Mathf.Abs(physics.velocity.y) > Mathf.Epsilon;
        playerAnimation.SetBool(IS_ClIMING, isCliming);
    }


    private void Jump()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask(STAGE_LAYER)))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            physics.velocity += jumpVelocity;
        }
    }

    private void Death()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask(ENEMY_LAYER)) ||
            bodyCollider.IsTouchingLayers(LayerMask.GetMask(HAZARDS_LAYER)))
        {
            playerAnimation.SetTrigger("Die");
            physics.velocity += deathKick;
            isAlive = false;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
