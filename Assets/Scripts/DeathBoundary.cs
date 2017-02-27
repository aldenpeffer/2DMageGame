using UnityEngine;
using System.Collections;

public class DeathBoundary : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player"))
        {
            GameManager.instance.restart();
        }
    }
}
