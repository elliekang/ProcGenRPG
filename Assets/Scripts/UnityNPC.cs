﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnityNPC : Interactable {

	private bool talking = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (this.CanInteract()) {
			transform.GetChild(2).gameObject.SetActive(true);
			transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Press " + Player.useKey + " to talk";
		} else {
			transform.GetChild(2).gameObject.SetActive(false);
		}

		PlayerControl.immobile = talking;

		if(talking) {
			transform.GetChild(0).GetComponent<Camera>().enabled = true;
			transform.GetChild(1).GetComponent<Camera>().enabled = true;
			transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "";
			transform.GetChild(2).GetChild(0).GetChild(0).gameObject.SetActive(true);
			transform.GetChild(2).eulerAngles = new Vector3(0,180f,0f);
			PlayerCanvas.cinematicMode = true;
		} else {
			transform.GetChild(0).GetComponent<Camera>().enabled = false;
			transform.GetChild(1).GetComponent<Camera>().enabled = false;
			transform.GetChild(2).GetChild(0).GetChild(0).gameObject.SetActive(false);
			transform.GetChild(2).rotation = Quaternion.RotateTowards(transform.GetChild(2).rotation, Quaternion.Euler(new Vector3(transform.GetChild(2).eulerAngles.x, FollowPlayer.rotate, transform.GetChild(2).eulerAngles.z)), Time.deltaTime*50f);
			PlayerCanvas.cinematicMode = false;
		}

	}

	protected override void Interact ()
	{
		talking = !talking;
		Debug.Log(talking);
		return;
	}
}
