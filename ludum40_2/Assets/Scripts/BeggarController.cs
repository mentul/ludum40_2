using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeggarController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}


	void OnMouseUp()
	{
		print ("OnMouseUp");

		GameController.Current.car.changeKeyboardBlock (false);
		Destroy (this.gameObject);
	}


}
