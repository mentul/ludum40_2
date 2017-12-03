using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
	public static GameController Current;
	public CarController car;
	public CameraController camera;
	public bool isRunning;
	public GameObject map;
	public GameObject coin;
	public int coinsOnMapNumber = 453;
	public List<Vector3> coinsPositions;

	void Start ()
	{
		Current = this;

		getCoinsPosition ();
		addRandomCoins ();

		camera.DoInit ();
	}

	void Update ()
	{
		if (isRunning)
		{
			car.DoUpdate ();
			camera.DoUpdate ();
		}
		else
		{
			if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKey (KeyCode.W))
			{
				isRunning = true;
				car.DoInit ();
			}
		}
	}

	public void getCoinsPosition()
	{
		foreach (GameObject coin in GameObject.FindGameObjectsWithTag("coin").ToList())
		{
			coinsPositions.Add (coin.transform.position);
		}
	}

	public void addRandomCoins()
	{
		foreach (GameObject coin in GameObject.FindGameObjectsWithTag("coin").ToList())
		{
			Destroy (coin);
		}

		int[] listOfNumbers = new int[coinsOnMapNumber];
		for (int i = 0; i < coinsOnMapNumber; i++) {
			int rand = Random.Range (0, coinsPositions.Count - 1);
			while (checkNumberInArray(listOfNumbers, rand))
			{
				rand = Random.Range (0, coinsPositions.Count - 1);
			}

			listOfNumbers [i] = rand;
			Instantiate (coin, new Vector3 (coinsPositions[rand].x, coinsPositions[rand].y), GameController.Current.car.transform.rotation);

		}

	}

	bool checkNumberInArray(int[] array, int number)
	{
		bool isThere = false;
		for (int i = 0; i < 20; i++) {
			if (array [i] == number) {
				isThere = true;
				break;
			}
		}
		return isThere;
	}


}
