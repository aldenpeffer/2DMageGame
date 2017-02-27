using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public float damage = 20;
    public float speed = 5f;
    Vector2 direction = new Vector2(1, 0);
    Vector2 startPosition;
    public float distanceBeforeDelete = 30;
    public float collisionForce = 200f;

    
    // Use this for initialization
	void Start () {
        startPosition = this.transform.position;
        direction = transform.right;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed, direction.y * speed);
        if (Vector2.Distance(startPosition, transform.position) > distanceBeforeDelete) Destroy(this.gameObject);
    }

    public Vector2 getDirection()
    {
        return direction;
    }
}
