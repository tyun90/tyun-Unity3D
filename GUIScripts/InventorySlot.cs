using UnityEngine;
using System.Collections;

public class InventorySlot {

	private bool isSelected;
	private GameObject slot;
	private GameObject item;
	private int index;


	public InventorySlot(GameObject obj) {
		slot = obj;
	}

	public void triggerOn() {
		if (true) {
			slot.GetComponent<TweenScale> ().PlayForward ();
			isSelected = true;
		}
	}

	public void triggerOff() {
		slot.GetComponent<TweenScale> ().PlayReverse ();
		isSelected = false;
	}

	public void triggerState() {
		if (isSelected) {
			slot.GetComponent<TweenScale> ().PlayReverse ();
			isSelected = false;
		} else if (true) {
			slot.GetComponent<TweenScale> ().PlayForward ();
			isSelected = true;;
		}
	}

	public void showObj() {
		item.SetActive (true);
		foreach (MonoBehaviour c in item.transform.GetComponents<MonoBehaviour>()) {
			c.enabled = false;
		}
		item.GetComponent<BoxCollider> ().enabled = false;
		item.transform.localScale = Vector3.one;
		//Set to UI Layer
		item.layer = 5;
		//item.transform.SetParent (slot.transform);
		//item.transform.localPosition = Vector3.zero;
	}

	public void hideObj() {
		item.SetActive (false);
	}

	public GameObject getObj() {
		if (item != null) {
			return item;
		}
		return null;
	}

	public void setSlot() {
		slot.transform.FindChild ("Item").GetComponent<UI2DSprite> ().sprite2D = Resources.Load<Sprite> (item.name);
		slot.transform.GetComponent<UIButton>().defaultColor = Color.green;
	}

	public void setObj(GameObject obj) {
		item = obj;
	}

	public void setIndex(int num) {
		index = num;
	}

	public GameObject getInvSlot() {
		return slot;
	}
}
