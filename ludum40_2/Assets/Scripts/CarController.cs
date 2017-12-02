using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	private int damage = 0;
	private int coins = 0;
	private int fameLevel = 0;
	private int lap = 0;
	private float velocity = 0.2f;

	private GameObject camera;
	public  GameObject car;

	// Use this for initialization
	public void DoInit () {
		
	}
	
	// Update is called once per frame
	public void DoUpdate () {

		//Vector3 position = this.transform.position;
		//position.x += velocity;
		//car.transform.position = position;
		
		if (Input.GetKey(KeyCode.RightArrow)){
			print("right");
			Vector3 newPosition = this.transform.position;
			newPosition.x += velocity;
			car.transform.position = newPosition;

		}
		if (Input.GetKey(KeyCode.LeftArrow)){
			print("left");

			Vector3 newPosition = this.transform.position;
			newPosition.x -= velocity;
			car.transform.position = newPosition;

		}
		if (Input.GetKey(KeyCode.UpArrow)){
			print("up");

			Vector3 newPosition = this.transform.position;
			newPosition.y += velocity;
			car.transform.position = newPosition;
		}
		if (Input.GetKey(KeyCode.DownArrow)){
			print("down");

			Vector3 newPosition = this.transform.position;
			newPosition.y -= velocity;
			car.transform.position = newPosition;
		}
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
