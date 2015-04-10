using UnityEngine;
using System.Collections;

public class Door {

	private bool isOpen;
	private Transform thisDoor;
	private Transform reqKey;
	private string ID;

	public Door() {
	}

	public Door(Transform door, Transform key, string keyID) {
		thisDoor = door;
		reqKey = key;
		ID = keyID;
	}

	public void setState(bool b) {
		isOpen = b;
	}

	public bool getState() {
		return isOpen;
	}


}
