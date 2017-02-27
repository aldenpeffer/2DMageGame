using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    int currentPoints = 0;
    public Text pointsText;
    [HideInInspector] public static GameManager instance;
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addPoints(int pointsToAdd) {
        currentPoints = currentPoints + pointsToAdd;
        pointsText.text = "Points: " + currentPoints;
    }

	public void updateVitals(float currentHealth, float currentMana){
		pointsText.text = "Health: " + (int)currentHealth;
		pointsText.text += "\nMana: " + (int)currentMana;
	}

    public void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
