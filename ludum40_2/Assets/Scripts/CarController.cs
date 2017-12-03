using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/* What is working list
 * 1) Checkpoint detect, level count, lives lost, game over blocade, checkpoint respawn
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */

public class CarController : MonoBehaviour
{

	static int coolDownValue = 1;
	static int maxLap = 5;

	private int lives = 3;
	private int coins = 0;
	private int fameLevel = 0;
	public float velocity = 2f;
	public float currentVelocity = 0f;
	public float angle = 30;
	public float coolDown = 1;
	public float coolDownCheckpoint = -1;

	public int lapCount = 1;
	public GameObject checkPoint;
	public GameObject car;
	public GameObject beggar;
	public List<GameObject> beggarList;

	private Rigidbody2D myRigidBody;

	public Text coinsLabel;
	public Text fameLabel;
	public Text damageLabel;
	public Text lapLabel;
	public Button restartButton;

	// Use this for initialization
	private bool keyboardBlocked;


	public void DoInit () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		currentVelocity = velocity;
		keyboardBlocked = false;
		beggarList = new  List<GameObject>();
		restartButton.gameObject.SetActive (false);
		coolDown = -1;
		coolDownCheckpoint = -1;
	}

	// Update is called once per frame
	public void DoUpdate ()
	{
		coolDown -= Time.deltaTime;
		coolDownCheckpoint -= Time.deltaTime;

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
		if (other.CompareTag ("coin"))
		{
			this.coins++;
			Destroy(other.gameObject);
			this.UpdateFameLevel ();
			this.showBeggaer ();
		}
		else if (other.CompareTag("checkPoint")) 
		{
			this.checkPoint = other.gameObject;
		}
		else if (other.CompareTag("finish") && coolDownCheckpoint < 0) 
		{
			coolDownCheckpoint = coolDownValue;

			if (this.checkPoint.tag != "finish")
			{
				lapCount += 1;
			}
			this.checkPoint = other.gameObject;
			if (lapCount > 5) {
				lapLabel.text = "FINISHED";
			} else {
				lapLabel.text = "Lap " + lapCount + "/" + maxLap;
				currentVelocity++;
				UpdateFameLevel ();
			}
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.CompareTag ("barrier") && coolDown < 0)
		{
			this.lives -= 1;
			coolDown = coolDownValue;
			this.MoveToCheckPoint ();
		}
	}



	private void MoveToCheckPoint ()
	{
		this.transform.position = this.checkPoint.transform.position;
		this.transform.rotation = this.checkPoint.transform.rotation;
		myRigidBody.velocity = Vector2.zero;
		damageLabel.text = ("Lives " + lives);

		foreach (GameObject beg in GameObject.FindGameObjectsWithTag("beggar").ToList())
		{
			Destroy (beg);
		}

		beggarList.Clear ();
		keyboardBlocked = false;


		if (lives <= 0) {
			lapLabel.text = ("Game Over");
			keyboardBlocked = true;
			velocity = 0;
			currentVelocity = 0;
			restartButton.gameObject.SetActive (true);
		}

	}

	public void UpdateFameLevel ()
	{
		fameLevel = coins / 10 + lapCount;
		coinsLabel.text = "Coins " + coins;
		fameLabel.text = "Fame " + fameLevel;
	}
		


	void showBeggaer()
	{
		float x = car.transform.position.x;
		float y = car.transform.position.y;
		print ("POS " + x + "    " + y);
		beggarList.Clear ();

		for (int i = 0; i < 4; i++)
		{
			keyboardBlocked = true;
			float randX = Random.Range (x - 5, x + 5);
			float randY = Random.Range (y - 3, y + 3);
			print ("rand " + randX + "    " + randY);
			GameObject test = Instantiate (beggar, new Vector3 (randX, randY), GameController.Current.car.transform.rotation);
			test.transform.SetParent (GameController.Current.camera.transform);
			beggarList.Add (test);
		}
	}

	public void changeKeyboardBlock()
	{
		int begCount = GameObject.FindGameObjectsWithTag ("beggar").Length;
		if (begCount == 1)
		{
			keyboardBlocked = false;
		}
	}

	public void RestartOnClick()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

}
