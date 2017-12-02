using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController Current;
	public CarController car;
	public CameraController camera;
	public bool isRunning;
	public GameObject map;

	void Start ()
	{
		Current = this;
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
}
