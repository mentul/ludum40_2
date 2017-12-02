using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CarController : MonoBehaviour
{

	static int maxHP = 150;
	static int barrierCoolDownValue = 1;
	static int maxLap = 5;

	private int hp = 150;
	private int coins = 0;
	private int fameLevel = 0;
	public float velocity = 2f;
	public float currentVelocity = 0f;
	public float angle = 30;
	public float coolDown = 1;
	public int lapCount = 1;
	public GameObject checkPoint;
	public GameObject car;
	public GameObject beggar;

	private Rigidbody2D myRigidBody;

	public Text coinsLabel;
	public Text fameLabel;
	public Text damageLabel;
	public Text lapLabel;

	// Use this for initialization
	private bool keyboardBlocked;


	public void DoInit () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		currentVelocity = velocity;
		keyboardBlocked = false;
	}

	// Update is called once per frame
	public void DoUpdate ()
	{
		coolDown -= Time.deltaTime;

		if (!keyboardBlocked) {
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				car.transform.Rotate (0, 0, -angle * Time.deltaTime);

			}
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				car.transform.Rotate (0, 0, angle * Time.deltaTime);

			}
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
				currentVelocity += velocity * Time.deltaTime;
				if (currentVelocity > 2 * velocity)
					currentVelocity = 2 * velocity;
			} else {
				currentVelocity -= velocity * Time.deltaTime;
				if (currentVelocity < velocity)
					currentVelocity = velocity;
			}
		}



		myRigidBody.velocity = car.transform.up * currentVelocity;

	}


	void OnTriggerEnter2D (Collider2D other)
	{
		print ("OnTriggerEnter2D" + other.tag);

		if (other.CompareTag ("coin"))
		{
			this.coins++;
			Destroy(other.gameObject);
			this.showBeggaer ();
			this.UpdateFameLevel ();
		}
		else if (other.CompareTag("checkPoint")) 
		{
			this.checkPoint = other.gameObject;
		}
		else if (other.CompareTag("finish")) 
		{
			if (this.checkPoint.tag != "finish")
			{
				lapCount += 1;
			}

			this.checkPoint = other.gameObject;
			if (lapCount > 5) {
				lapLabel.text = "FINISHED";

			}
			UpdateFameLevel ();
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		print ("OnCollisionEnter2D" + other.gameObject.tag);

		if (other.gameObject.CompareTag ("box"))
		{
			this.hp -= 10;
		}
		else if (other.gameObject.CompareTag ("barrel"))
		{
			this.hp -= 15;
		}
		else if (other.gameObject.CompareTag ("barrier") && coolDown < 0)
		{
			this.hp -= 20;
			coolDown = barrierCoolDownValue;
		}

		this.UpdateGameStatus ();

	}



	private void UpdateGameStatus ()
	{
		if (hp <= 0) {
			this.transform.position = this.checkPoint.transform.position;
			this.transform.rotation = this.checkPoint.transform.rotation;
			myRigidBody.velocity = Vector2.zero;
			this.hp = 150;
			coolDown = barrierCoolDownValue;
		}	

		damageLabel.text = ("HP " + this.hp + "/" + maxHP);

	}

	public void UpdateFameLevel ()
	{
		fameLevel = coins / 10 + lapCount;

		coinsLabel.text = "Coins " + coins;
		fameLabel.text = "Fame " + fameLevel;

	}
		
	public void changeKeyboardBlock(bool block)
	{
		keyboardBlocked = block;
	}

	public void showBeggaer()
	{
		float x = car.transform.position.x;
		float y = car.transform.position.y;

		for (int i = 0; i <= fameLevel; i++)
		{
			keyboardBlocked = true;
			float randX = Random.Range (x - 5, x + 5);
			float randY = Random.Range (y - 5, y + 5);

			Instantiate (beggar, new Vector3 (randX, randY), beggar.transform.rotation).transform.SetParent (GameController.Current.camera.transform);
		}


	}

}
