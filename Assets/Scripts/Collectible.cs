using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

    public int pointValue;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player")){
            GameManager.instance.addPoints(pointValue);
            Destroy(this.gameObject);
        }
    }
}
