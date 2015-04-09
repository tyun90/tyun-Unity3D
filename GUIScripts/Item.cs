using UnityEngine;
using System.Collections;

public class Item {

	public GameObject obj;
	public string itemName;
	public string itemDescription;
	public Quaternion examineRot;
	public Sprite displayImg;

	public Item(GameObject o, string n, Quaternion q, string msg, Sprite img) {
		obj = o;
		itemName = n;
		examineRot = q;
		itemDescription = msg;
		displayImg = img;
	}

	public Item() {
		obj = null;
	}

	public string name {
		get {
			return itemName;
		}
		set {
			itemName = value;
		}
	}

	public string description {
		get {
			return itemDescription;
		}
		set {
			itemDescription = value;
		}
	}

	public GameObject prefab {
		get {
			return obj;
		}
		set {
			obj = value;
		}
	}

	public Sprite sprite {
		get {
			return displayImg;
		}
		set {
			displayImg = value;
		}
	}
}
