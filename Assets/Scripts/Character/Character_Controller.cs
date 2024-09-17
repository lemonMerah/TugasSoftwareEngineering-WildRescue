using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Character_Controller : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private enum State {
        Normal,
        Dashing
    }

    [Header("Character Movement")]
    public float speed;
    float horizontal;
    float vertical;
    Vector2 moveDirection;
    private State state;

    [Header("Dash")]
    public float dashSpeed;
    float dashSpeedCounter;
    public float dashCooldown;
    Vector2 dashDirection;
    public float dashSpeedDropMultiplier;
    public float minimumDashSpeed;
    float lastDash;

    [Header("Gun")]
    public Gun_Controller gun;
    Vector2 mousePosition;
    private Transform aim;
    public float shotDelay;
    float lastShot;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();                 // Used for animating
        spriteRenderer = GetComponent<SpriteRenderer>();     // Used for flipping the sprite
        state = State.Normal;
        dashSpeedCounter = dashSpeed;

        aim = transform.Find("Aim");
    }

    
    void Update()
    {
        switch(state)
        {
            case State.Normal:
                 // Movement
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");

                moveDirection = new Vector2(horizontal, vertical).normalized;

                if (horizontal > 0)
                {
                    animator.SetFloat("speed", 8);
                    spriteRenderer.flipX = false;
                }
                if (horizontal < 0)
                {
                    animator.SetFloat("speed", 8);
                    spriteRenderer.flipX = true;
                }
                if (vertical == 0 || vertical > 0 || vertical < 0)
                {
                    animator.SetFloat("speed", 8);
                }

                if (horizontal == 0 && vertical == 0)
                {
                    animator.SetFloat("speed", 0);
                }

                // Aim
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Shoot
                if (Input.GetMouseButtonDown(0))
                {
                    if (Time.time - lastShot < shotDelay){
                        return;
                    }

                    lastShot = Time.time;
                    gun.Fire();
                }

                // Dash
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (Time.time - lastDash < dashCooldown){
                        return;
                    }
                    
                    lastDash = Time.time;
                    dashDirection = moveDirection;
                    state = State.Dashing;
                }

                break;

            case State.Dashing:
                dashSpeed -= dashSpeed * dashSpeedDropMultiplier * Time.deltaTime;

                if (dashSpeed <= minimumDashSpeed)
                {
                    dashSpeed = dashSpeedCounter;
                    state = State.Normal;
                }
                
                break;
        }
    }

    private void FixedUpdate()
    {  
        switch (state)
        {
            case State.Normal:
                rb2d.velocity = new Vector2(horizontal * speed, vertical * speed);

                Vector2 aimDirection = (mousePosition - rb2d.position).normalized;
                float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                aim.eulerAngles = new Vector3(0, 0, aimAngle);
                break;

            case State.Dashing:
                rb2d.velocity = dashDirection * dashSpeed;
                state = State.Dashing;
                break;
        }
        
    }
}
