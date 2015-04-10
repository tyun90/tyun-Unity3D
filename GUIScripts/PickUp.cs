using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;

public class PickUp : MonoBehaviour {

	public FullBodyBipedIK ik;
	public Transform Hand;
	public Transform newObj;

	private bool isHeld;

	private Transform itemInfoCanvas;
	private Animator itemInfo;
	public GUIItemText text;
	public GUIItemText text2;

	void Awake() {
		itemInfoCanvas = transform.parent.FindChild ("GUI").FindChild("Canvas");
		itemInfo = transform.parent.FindChild("GUI").FindChild("Canvas").GetComponent<Animator> ();
		text = transform.parent.FindChild ("GUI").FindChild("Canvas").FindChild ("InfoFadeIn").GetComponentInChildren<GUIItemText> ();
		text2 = transform.parent.FindChild ("GUI").FindChild("Canvas").FindChild ("ItemImage").GetComponentInChildren<GUIItemText> ();
	}

	// Use this for initialization
	void Start () {
		isHeld = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (ik.solver.leftHandEffector.positionWeight > 0.99 && !isHeld) {
			Vector3 scaling = transform.localScale;
			InventoryManager.instance.itemPickedUp(transform.gameObject);
			InventoryManager.instance.setOpen(true);
			transform.gameObject.SetActive(false); 
			itemInfoCanvas.gameObject.SetActive(false);

			newObj.gameObject.SetActive(true);
			newObj.SetParent(Hand);
			newObj.localScale = scaling;
			//newObj.localPosition = new Vector3(-0.09935392f,-0.3808476f,-0.6535484f);
			//newObj.localRotation = new Quaternion(0,0,0,0);
			//newObj.localRotation = new Quaternion(14.83635f, 52.90372f, 322.2132f,0);
			//transform.localPosition = new Vector3(0,0,0);
			isHeld = true;
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			itemInfo.SetTrigger("FadeIn");
			text.StartCoroutine("Typing");
			text2.StartCoroutine("Typing");
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.tag == "Player") {
			itemInfo.SetTrigger("FadeOut");
			text.StartCoroutine("ClearText");
			text2.StartCoroutine("ClearText");
		}
	}
}
