using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Transform carTransform;

	public void DoInit ()
	{
		carTransform = GameController.Current.car.transform;
		//transform.SetParent (carTransform);
	}

	public void DoUpdate ()
	{
		transform.position = carTransform.position + Vector3.back * 10f;

	}
}
