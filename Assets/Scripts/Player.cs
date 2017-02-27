using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {
	public float health = 1000;
	public float currentHealth;
	public float mana = 1000;
	public float manaRechargeRate = 5;
	public float currentMana;
    public float speed = 5;
    public float jumpPower = 5;
	public int direction; //0 for right, 1 for left
    Rigidbody2D rb2D;
    private bool canJump;
    public float maxVelocity;
	public bool facingRight = true;
	public Transform[] projectileSpawnLocations;

	private Animator animator;
	private float prevXInput = 0;
	// Use this for initialization

	public GameObject attack1;
	public GameObject attack2;
	public GameObject attack3;

	public float attack1CoolDown;
	public float attack1CurrentCoolDown;
	public float attack2CoolDown;
	public float attack2CurrentCoolDown;
	public float attack3CoolDown;
	public float attack3CurrentCoolDown;

	public float fireCost = 100;
	public float jumpCost = 10;
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();
		currentHealth = health;
		currentMana = mana;
	}
		

	void Update(){

		if (attack1CurrentCoolDown > 0) {
			attack1CurrentCoolDown -= Time.deltaTime;
		}

		GainMana ();

	}

	void GainMana(){
		if (currentMana < mana) {
			currentMana += manaRechargeRate * Time.deltaTime;
		}
		if (currentMana > mana) {
			currentMana = mana;
		}
		UpdateVitalsUI ();
	}

	void LoseMana(float value){
		currentMana -= value;
		if (currentMana < 0) {
			currentMana = 0;
		}
		UpdateVitalsUI ();
	}
	void UpdateVitalsUI(){
		GameManager.instance.updateVitals (currentHealth, currentMana);
	}
	void FixedUpdate () {
		Move ();

		if (Input.GetButtonDown ("Fire1")) {
			Attack1 ();
		}
        if (Input.GetButtonDown("Jump")) {
			Jump ();

        }

	}
	void Attack1(){
		if (attack1CurrentCoolDown > 0) {
			return;
		}
		FireBall ();
	}

	void FireBall(){
		if (currentMana < fireCost) {
			return;
		}
		LoseMana (fireCost);
		animator.SetTrigger ("Attack");
		GameObject gO = (GameObject)(Instantiate (attack1, projectileSpawnLocations[0]));
		gO.SetActive (true);
		gO.transform.parent = null;
		gO.GetComponent<ProjectileAttack> ().facingRight = facingRight;
		attack1CurrentCoolDown = attack1CoolDown;
	}
	void Jump(){
		//Normal Jumping 
		if (canJump)
		{
			animator.SetBool("Jump", true);
			rb2D.velocity = new Vector3(rb2D.velocity.x, jumpPower, 0);
			return;
		}

		//Second Jumping, costs mana
		else if (currentMana < jumpCost) {
			return;
		}
		LoseMana (jumpCost);
		animator.SetBool("Jump", true);
		rb2D.velocity = new Vector3(rb2D.velocity.x, jumpPower, 0);

	}
	void Move(){
		float xInput = Input.GetAxisRaw ("Horizontal");
		float xMovement = speed * xInput;
		//Moving right
		//print(prevXInput+","+ input);
		animator.SetFloat("Speed",Math.Abs(xMovement));
		if (xMovement > 0) {
			if (!facingRight) {//if facing left,then flip
				Flip();
			}
		} 
		//Moving left
		else if (xMovement < 0) {
			if (facingRight) {//if facing right,then flip
				Flip();
			}


		} 

		Vector2 totalMovement = new Vector3(xMovement,0);
		if (rb2D.velocity.x < maxVelocity && rb2D.velocity.x > -maxVelocity) { 
			rb2D.velocity = new Vector3(xMovement, rb2D.velocity.y, 0);

		}

	}

	void Flip (){
		facingRight = !facingRight;
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,transform.localScale.z);
	}


	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag.Equals("Floor")) {
			animator.SetBool("Jump", false);
		}
	}
	//Note: you need oncollisionstay as opposed to oncollisionenter if you have tiles right next to 
	//each other. Talk about this with alden
    void OnCollisionStay2D(Collision2D col) {
        if (col.gameObject.tag.Equals("Floor")) {
            canJump = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Floor"))
        {
            canJump = false;
        }
    }
}
