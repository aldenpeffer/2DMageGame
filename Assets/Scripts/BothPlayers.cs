using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothPlayers : MonoBehaviour {
	public float health = 1000;
	public float currentHealth;
	public float mana = 1000;
	public float manaRechargeRate = 5;
	public float currentMana;

	public Player player1;
	public Player player2;
	public int configuration = 3; 	// 0 is player1, 1 is player2, 2 is player1 as normal and player 2 as mirror
								// 3 is player1 as normal, player 3 as reverse	
	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Selection1")) {
			configurePlayers (0);  
		}
		else if(Input.GetButtonDown ("Selection2")){
			configurePlayers (1); 
		}
		else if(Input.GetButtonDown ("Selection3")){
			toggleBoth ();
		}
	}

	void toggleBoth(){
		switch (configuration) {
		//if player 1 is the active player, make player 1 the normal movement of the both configuration
		case 0:
			configurePlayers (2);
			break;
		//if player 2 is the active player, make player 2 the normal movement of the both configuration
		case 1:
			configurePlayers (3);
			break;
		//if player 1 normal movement, and player 2 is mirror movement, reverse
		case 2:
			configurePlayers (3);
			break;
		//if player 2 normal movement, and player 1 is mirror movement, reverse
		case 3:
			configurePlayers (2);
			break;
		}
	}

	void configurePlayers(int n){
		switch (n) {
		case 0:
			player1.setMain ();
			player2.setOff ();
			configuration = 0;
			break;
		case 1:
			player1.setOff ();
			player2.setMain ();
			configuration = 1;
			break;
		case 2:
			player1.setMain ();
			player2.setReverse ();
			configuration = 2;
			break;
		case 3:
			player1.setReverse ();
			player2.setMain ();
			configuration = 3;
			break;
		}
	}
}
