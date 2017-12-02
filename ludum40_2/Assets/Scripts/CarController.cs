using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	private int damage = 0;
	private int coins = 0;
	private int fameLevel = 0;
	private int lap = 0;

	// Use this for initialization
	public void DoInit () {
		
	}
	
	// Update is called once per frame
	public void DoUpdate () {
		
	}


	void OnTriggerEnter2D( Collider2D other )
	{
		if (other.CompareTag("box")) 
		{
			this.damage += 10;
		}
		else if (other.CompareTag("barrel"))
		{
			this.damage += 15;
		}
		else if (other.CompareTag("barrier"))
		{
			this.damage += 20;
		}
		else if (other.CompareTag("coins")) 
		{
			this.coins++;
			this.UpdateFameLevel ();
		};
		this.UpdateGameStatus ();
	}



	private void UpdateGameStatus()
	{

	}

	public void UpdateFameLevel()
	{
		fameLevel = coins / 10 * lap;
	}

	public void UpdateLapLevel(int lapCount)
	{
		this.lap = lapCount;
		this.UpdateFameLevel ();
	}

}
