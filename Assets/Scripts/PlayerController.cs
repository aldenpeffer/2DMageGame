using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float maxSpeed = 10f;
    public float jumpForce = 700;
    public float maxHealth = 100;
    public float currentHealth;

    public float leftRightFactor = 0.3f;
    public float knockUpFactor = 0.9f;

    public bool dead = false;
    bool facingRight = true;
    bool hpBarShown = false;

    public float applyForceTime = 0.5f;
    float currentApplyForceTime;
    bool recentlyCollided = false;
    float collisionForce;
    Vector2 collisionDirection;

    public GameObject healthBar;
    public GameObject healthBarParent;

    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    Rigidbody2D rigidBody;

    // Use this for initialization
    void Start()
    {
        healthBarParent.SetActive(false);
        currentHealth = maxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vertical movementS
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //Horizontal movement
        float move = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(move * maxSpeed, rigidBody.velocity.y);

        //If move direction doesn't match facing direction, flip the sprite
        if (move > 0 && !facingRight || move < 0 && facingRight)
        {
            Flip();
        }
    }

    //Handle jump in update, as Fixed Update may miss the spacebar key input
    void Update()
    {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
        }

        if (recentlyCollided)
        {
            //REMOVE ALL PLAYER CONTROL, THEN KNOCK BACK + INVULN FRAMES
            
            //DIFFERENT WAYS TO HANDLE COLLISIONS
            //Note: In visual studio: Ctrl K -> Ctrl C to comment highlighted, Ctrl K -> Ctrl D to uncomment

            //-----Knockup on collision, regardless of direction-----
            //rigidBody.AddForce(new Vector2(0, collisionForce));

            //-----Push in the direction of collision-----
            //Vector2 forceDirection = attackScript.getDirection().normalized;
            //rigidBody.AddForce(forceDirection * collisionForce * 3);

            //-----Knock slightly upwards on collision regardless of direction, and slightly in left/right depending on direction of collision, with static values-----
            /*if (collisionDirection.x >= 0)
            {
                rigidBody.AddForce(new Vector2(collisionForce * leftRightFactor, collisionForce * knockUpFactor));
            }
            else
            {
                rigidBody.AddForce(new Vector2(-collisionForce * leftRightFactor, collisionForce * knockUpFactor));
            }

            currentApplyForceTime -= Time.deltaTime;
            if (currentApplyForceTime <= 0)
            {
                recentlyCollided = false;
            }*/

            //-----Same as above (partially left/right, but mainly up) with non-static values based on force direction-----
            //Vector2 forceDirection = attackScript.getDirection().normalized;
            //rigidBody.AddForce(new Vector2(forceDirection.x * leftRightFactor, forceDirection.y * knockUpFactor));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!dead) //Don't consume attacks if already dead
        {
            if (!hpBarShown) healthBarParent.SetActive(true);

            GameObject attackObject = other.gameObject;
            if (attackObject.tag == "Attack")
            {
                recentlyCollided = true;
                Attack attackScript = attackObject.GetComponent<Attack>();
                collisionForce = attackScript.collisionForce;
                collisionDirection = attackScript.getDirection();
                currentApplyForceTime = applyForceTime;

                currentHealth -= attackScript.damage;
                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                    //die here
                    dead = true;
                }

                Vector2 healthScale = healthBar.transform.localScale;
                healthScale.x = currentHealth / maxHealth;
                healthBar.transform.localScale = healthScale;

                //Particle effect here

                Destroy(attackObject);
            }
        }
    }

    void Flip()
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        facingRight = !facingRight;
    }
}
