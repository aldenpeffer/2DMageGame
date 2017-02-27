using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour {
	public bool facingRight = true; 
	public float xSpeed;
	public float ySpeed = 0;
	public float lifeSpan = 0.5f; 

	private Rigidbody2D rb2D;
	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();

		if (facingRight) {
			rb2D.velocity = new Vector2 (xSpeed, ySpeed);
		} else {
			rb2D.velocity = new Vector2 (-xSpeed, ySpeed);
		}


	}
	
	// Update is called once per frame
	void Update () {
		lifeSpan -= Time.deltaTime;
		if (lifeSpan < 0) {
			Explode ();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Floor")) {
			Explode(other.gameObject);
		}
	}

	void Explode(){
		Destroy (gameObject);
	}
	void Explode(GameObject other){
		//Check what thing you are exploding on, and affect it.
		Destroy (gameObject);
	}
}
