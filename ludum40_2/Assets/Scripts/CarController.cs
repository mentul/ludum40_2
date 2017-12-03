using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class CarController : MonoBehaviour
{

	static int coolDownValue = 1;
	public int maxLap = 5;

	private int lives = 3;
	private int coins = 0;
	private int fameLevel = 0;
	public float currentVelocity = 0f;
	public float baseVelocity = 4f;
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
		startRound ();
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
				currentVelocity = baseVelocity + fameLevel;
			}
		}
		myRigidBody.velocity = car.transform.up * currentVelocity;
	}

	void startRound()
	{
		myRigidBody = GetComponent<Rigidbody2D> ();
		currentVelocity = baseVelocity + fameLevel/2;
		keyboardBlocked = false;
		beggarList = new  List<GameObject>();
		restartButton.gameObject.SetActive (false);
		coolDown = -1;
		coolDownCheckpoint = -1;

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
				GameController.Current.addRandomCoins ();
			}
			this.checkPoint = other.gameObject;
			UpdateFameLevel ();

			if (lapCount > maxLap) {
				lapLabel.text = "YOU WIN";
				Invoke ("StopAuto",0.9f);
			} else {
				lapLabel.text = "Lap " + lapCount + "/" + maxLap;
				currentVelocity = baseVelocity + fameLevel/2;
			}
		}
	}

	void StopAuto()
	{
		currentVelocity = 0;
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
			currentVelocity = 0;
			restartButton.gameObject.GetComponentInChildren<Text> ().text = "Restart";
			restartButton.gameObject.SetActive (true);
		} else {
			this.transform.position = this.checkPoint.transform.position;
			this.transform.rotation = this.checkPoint.transform.rotation;

		}

	}

	public void UpdateFameLevel ()
	{
		fameLevel = coins / 10 + lapCount - 1;
		coinsLabel.text = "Coins " + coins;
		fameLabel.text = "Fame " + fameLevel;
	}
		


	void showBeggaer()
	{
		if (fameLevel > 1)
		{
			float x = car.transform.position.x;
			float y = car.transform.position.y;
			print ("POS " + x + "    " + y);
			beggarList.Clear ();

			for (int i = 0; i < fameLevel/2; i++)
			{
				keyboardBlocked = true;
				float randX = Random.Range (x - 7, x + 7);
				float randY = Random.Range (y - 4, y + 4);
				print ("rand " + randX + "    " + randY);
				GameObject test = Instantiate (beggar, new Vector3 (randX, randY), GameController.Current.camera.transform.rotation);
				test.transform.SetParent (GameController.Current.camera.transform);
				beggarList.Add (test);
			}
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
		if (lives <= 0) {
			Application.LoadLevel (Application.loadedLevel);
		} else {
			startRound ();
			currentVelocity = baseVelocity + fameLevel;
			GameController.Current.isRunning = true;
		}
	}

}
