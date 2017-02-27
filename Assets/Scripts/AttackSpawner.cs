using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpawner : MonoBehaviour {

    public GameObject attack;
    public float fireWait = 1.0f;
    float currentWait;

    // Use this for initialization
    void Start () {
        currentWait = fireWait;
	}
	
	// Update is called once per frame
	void Update () {
        currentWait -= Time.deltaTime;
        if (currentWait <= 0) 
        {
            currentWait = fireWait;
            GameObject attackInstance = Instantiate(attack, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            attackInstance.transform.rotation = this.transform.rotation;
            attackInstance.transform.Rotate(Vector3.forward * 90);
        }
	}
}
