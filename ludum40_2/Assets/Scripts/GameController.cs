using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController Current;
	public CarController car;
	public CameraController camera;
	public int loopCount;

	void Start ()
	{
		Current = this;
		car.DoInit ();
		camera.DoInit ();
	}

	void Update ()
	{
		print("UPDATE");
		car.DoUpdate ();
		camera.DoUpdate ();
	}
}
