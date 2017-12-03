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
		GameController.Current.camera.GetComponent<AudioSource> ().Play ();
		killBeggart();
		GameController.Current.car.changeKeyboardBlock ();
	}

	public void killBeggart()
	{
		Destroy (this.gameObject);
	}
}
