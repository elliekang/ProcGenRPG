﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Chests can hold subclasses of Item
 */
public class Chest : Interactable {

	/**
	 * The number of items all chests can store
	 */
	private static readonly int SLOTS = 10;
	private static readonly int minBytes = 10000;
	private static readonly int maxBytes = 1000000;

	private bool dropItems = false;
	/**
	 * List of items in the chest. Cannot be more than SLOTS
	 */
	private List<Item> items;

	private static GameObject byteObject;
	// Use this for initialization
	void Start () {
		if(byteObject == null) {
			byteObject = Resources.Load<GameObject>("Info/Byte");
		}
		items = new List<Item>(SLOTS);
	}
	
	// Update is called once per frame
	void Update () {
		transform.GetChild(0).rotation = Quaternion.RotateTowards(transform.GetChild(0).rotation, Quaternion.Euler(new Vector3(transform.GetChild(0).eulerAngles.x, FollowPlayer.rotate, transform.GetChild(0).eulerAngles.z)), Time.deltaTime*50f);

		if (this.CanInteract()) {
			transform.GetChild(0).gameObject.SetActive(true);
			transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Press " + Player.useKey + " to open";
		} else {
			transform.GetChild(0).gameObject.SetActive(false);
		}
	}

	// KARTIK do the thing!
	protected override void Interact() {
		if (Player.useKey != KeyCode.None && Input.GetKeyDown(Player.useKey)) {
			int tempByteVal = (int)(Random.value*(maxBytes-minBytes) + minBytes);
			int curByteVal = 0;
			int byteVal = Mathf.Max(tempByteVal/5, 5000);
			while (curByteVal < tempByteVal) {
				GameObject tmp = (GameObject)Instantiate(byteObject, transform.position, Quaternion.identity);
				tmp.GetComponent<Byte>().val = byteVal;
				curByteVal += byteVal;
			}
			Destroy(this.gameObject);
		}
	}

	/**
	 * Adds an item to the chest and returns true if there are enough slots.
	 * returns false otherwise.
	 */
	public bool AddToChest(Item i) {
		if (items.Count < SLOTS) {
			items.Add(i);
			return true;
		} else {
			return false;
		}
	}

	/**
	 * Removes an item to the chest and returns true if item was there.
	 * returns false otherwise.
	 */
	public bool RemoveFromChest(Item i) {
		return items.Remove(i);
	}

	/**
	 * Gets the Collections.Generic.List of items in the chest
	 */
	public List<Item> GetList() {
		return items;
	}
}
