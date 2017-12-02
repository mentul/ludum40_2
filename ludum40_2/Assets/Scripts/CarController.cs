using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

	static int maxDamage = 10;

	private int damage = 0;
	private int coins = 0;
	private int fameLevel = 0;
	private int lap = 0;
	public float velocity = 2f;
	public float currentVelocity = 0f;
	public float angle = 30;

	public GameObject checkPoint;
	public GameObject car;
	private Rigidbody2D myRigidBody;
	// Use this for initialization



	public void DoInit () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		currentVelocity = velocity;
	}

	// Update is called once per frame
	public void DoUpdate ()
	{



		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D))
		{
			car.transform.Rotate (0, 0, -angle * Time.deltaTime);

		}
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A))
		{
			car.transform.Rotate (0, 0, angle * Time.deltaTime);

		}
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W))
		{
			currentVelocity += velocity * Time.deltaTime;
			if (currentVelocity > 2 * velocity)
				currentVelocity = 2 * velocity;
		}
		else
		{
			currentVelocity -= velocity * Time.deltaTime;
			if (currentVelocity < velocity)
				currentVelocity = velocity;
		}
		myRigidBody.velocity = car.transform.up * currentVelocity;

	}


	void OnTriggerEnter2D (Collider2D other)
	{
		print ("OnTriggerEnter2D" + other.tag);

		if (other.CompareTag ("coin"))
		{
			this.coins++;
			this.UpdateFameLevel ();
		}
		else if (other.CompareTag("checkPoint")) 
		{
			this.checkPoint = other.gameObject;
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		print ("OnCollisionEnter2D" + other.gameObject.tag);

		if (other.gameObject.CompareTag ("box"))
		{
			this.damage += 10;
		}
		else if (other.gameObject.CompareTag ("barrel"))
		{
			this.damage += 15;
		}
		else if (other.gameObject.CompareTag ("barrier"))
		{
			this.damage += 20;
		}
		print ("DAMAGE " + this.damage);
		this.UpdateGameStatus ();

	}



	private void UpdateGameStatus ()
	{
		if (damage >= maxDamage) {
			print ("DAMAGE " + this.checkPoint.transform.position);

			this.transform.position = this.checkPoint.transform.position;
			this.transform.rotation = this.checkPoint.transform.rotation;
			myRigidBody.velocity = Vector2.zero;

		}	
	}

	public void UpdateFameLevel ()
	{
		fameLevel = coins / 10 * lap;
	}

	public void UpdateLapLevel (int lapCount)
	{
		this.lap = lapCount;
		this.UpdateFameLevel ();
	}

}
