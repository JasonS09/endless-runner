using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private Animator anim;
    [SerializeField] private UIController uiController;
    [SerializeField] private GameObject shield;
    [SerializeField] private float jumpForce = 1;
    [SerializeField] private float jumpForceAcceleration = 0.0007f;
    [SerializeField] private float maxJumpTime = 0.2f;
    [SerializeField] private float movementSpeed = 3;
    [SerializeField] private bool isGrounded;  
    [SerializeField] private bool isShieldActive;
    [SerializeField] private bool isAirJumpActive;
    [SerializeField] private bool isJumping;
    private SFXManager sfxManager;
    private float startJumpTime;    
    private float lastYPos;
    private float leftLimit;
    private float rightLimit;

    public float DistanceTraveled { get; private set; }
    public int CollectedCoins { get; private set; }

    private void Start()
    {
        sfxManager = SFXManager.GetInstance();
        lastYPos = transform.position.y;
        rightLimit = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0f, 0f)).x;
        leftLimit = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
    }

    private void Update()
    {
        DistanceTraveled += Time.deltaTime;
        CheckForJumpInput();
        AnimateIfLanding();
        DestroyPlayerOnLeftLimit();
    }

    private void FixedUpdate()
    {
        CheckForMovement();
        CheckIfLanded();
        CheckForJump();
    }

    private void CheckForJumpInput()
    {
        if (isGrounded || isAirJumpActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isAirJumpActive && !isGrounded)
                {
                    sfxManager.PlaySFX("Double Jump");
                    isAirJumpActive = false;
                }

                startJumpTime = Time.time;
                isJumping = true;
                sfxManager.PlaySFX("Jump");
                anim.SetTrigger("Jump");
            }
        }

        if (isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            jumpForce += jumpForceAcceleration;
        }

        if (isJumping && Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    private void CheckForMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (transform.position.x < rightLimit || h == -1)
        {
            rb.velocity = new Vector2(h * movementSpeed, rb.velocity.y);
        }    
    }

    private void CheckIfLanded()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, Vector2.down);

        if (hit.collider != null)
        {
            if (hit.distance < 0.1f)
            {
                if (!isGrounded) 
                    sfxManager.PlaySFX("Land");

                isGrounded = true;
                anim.SetBool("IsGrounded", true);
            }
            else
            {
                isGrounded = false;
                anim.SetBool("IsGrounded", false);
            }
        }
        else
        {
            isGrounded = false;
            anim.SetBool("IsGrounded", false);
        }
        //Debug.Log(isGrounded);
        //Debug.DrawRay(raycastOrigin.position, Vector2.down, Color.green);
    }

    private void AnimateIfLanding()
    {
        if (transform.position.y < lastYPos)
        {
            anim.SetBool("IsFalling", true);
        }
        else
        {
            anim.SetBool("IsFalling", false);
        }

        lastYPos = transform.position.y;
    }

    private void CheckForJump()
    {
        if (isJumping)
        {
            if (Time.time - startJumpTime >= maxJumpTime) 
            {
                isJumping = false;
            }
            
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            if (isShieldActive)
            {
                sfxManager.PlaySFX("Shield Break");
                Destroy(collision.gameObject);
                shield.SetActive(false);
                isShieldActive = false;
            }
            else
            {
                sfxManager.PlaySFX("Game Over Hit");
                uiController.ShowGameOverScreen();
                gameObject.SetActive(false);
            }
        }

        else if (collision.transform.CompareTag("DeathBox"))
        {
            uiController.ShowGameOverScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            CollectedCoins++;
            sfxManager.PlaySFX("Coin");
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("AirJump"))
        {
            sfxManager.PlaySFX("Power Up Double Jump");
            isAirJumpActive = true;
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("Shield"))
        {
            sfxManager.PlaySFX("Power Up Shield");
            isShieldActive = true;
            shield.SetActive(true);
            Destroy(collision.gameObject);
        }
    }

    private void DestroyPlayerOnLeftLimit()
    {
        if (transform.position.x < leftLimit)
        {
            uiController.ShowGameOverScreen();
            gameObject.SetActive(false);
        }
    }
}
