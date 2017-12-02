using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	private int damage = 0;
	private int coins = 0;
	private int fameLevel = 0;
	private int lap = 0;
	public float velocity = 0.2f;
	public float angle = 30;

	public  GameObject car;
	private Rigidbody2D myRigidBody;
	// Use this for initialization
	public void DoInit () {
		myRigidBody = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	public void DoUpdate () {

		//Vector3 position = this.transform.position;
		//position.x += velocity;
		//myRigidBody.transform.position = position;

		if (Input.GetKey(KeyCode.RightArrow)){
			car.transform.Rotate (0, 0, -angle * Time.deltaTime);

		}
		if (Input.GetKey(KeyCode.LeftArrow)){
			car.transform.Rotate (0, 0, angle * Time.deltaTime);

		}
		if (Input.GetKey(KeyCode.UpArrow)){
			myRigidBody.velocity = car.transform.up*velocity;
		}
		if (Input.GetKey(KeyCode.DownArrow)){
			//Vector3 newPosition = this.transform.position;
			//newPosition.y -= velocity;
			//car.transform.position = newPosition;
			myRigidBody.velocity = Vector2.down;

		}
	}


	void OnTriggerEnter2D( Collider2D other )
	{
		print ("OnTriggerEnter2D" + other.tag);

		if (other.CompareTag("coin")) 
		{
			this.coins++;
			this.UpdateFameLevel ();
		};
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		print ("OnCollisionEnter2D" + other.gameObject.tag);

		if (other.gameObject.CompareTag("box")) 
		{
			this.damage += 10;
		}
		else if (other.gameObject.CompareTag("barrel"))
		{
			this.damage += 15;
		}
		else if (other.gameObject.CompareTag("barrier"))
		{
			this.damage += 20;
		}
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
