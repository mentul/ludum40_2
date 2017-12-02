using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController Current;
	public CarController car;
	public CameraController camera;
	public int loopCount;
	public bool isRunning;
	public GameObject map;

	void Start ()
	{
		Current = this;
		camera.DoInit ();
		CreateMap ();
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

	void CreateMap ()
	{
		for (int i=0; i < loopCount; i++)
		{
			Instantiate (map, new Vector3 (i * 42f, 0f), map.transform.rotation);
		}
	}
}
