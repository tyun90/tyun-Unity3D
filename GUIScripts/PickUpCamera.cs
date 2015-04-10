using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;

[RequireComponent(typeof(InteractionSystem))]
public class PickUpCamera : MonoBehaviour {

	public Camera MainCamera;
	public CharacterController temp;
	[SerializeField] InteractionObject interactionObject; // The object to interact to
	[SerializeField] FullBodyBipedEffector[] effectors; // The effectors to interact with
	private InteractionSystem interactionSystem;

	public FullBodyBipedIK ik;
	public LookAtIK lookTarget;
	public Transform head;

	public bool pickedUp;
	public bool nearItem;
	public Transform targetItem;
	public Transform targetMove;

	public Vector3 x = new Vector3(0,0,0);

	private Transform[] destroyItems;

	private Vector3 lookAtPickup;

	void Awake() {
		lookTarget.Disable ();
		interactionSystem = GetComponent<InteractionSystem>();
		pickedUp = false;
		nearItem = false;
		destroyItems = new Transform[2];
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//x.x = Screen.width / 2;
		//x.y = Screen.height / 2;

		//RaycastHit inRange;

		//if (Physics.Raycast(MainCamera.ViewportPointToRay(x), out inRange, 5)) {
		//	if (inRange.distance <= 5 && inRange.collider.gameObject.tag == "PickUp") {
		//		Debug.Log ("Near");
		//		nearItem = true;
		//		lookTarget.solver.IKPosition = inRange.collider.transform.position;

		//	}
		//} else {
		//	nearItem = false;
		//}


		if (Input.GetKeyDown(KeyCode.G) && nearItem) {
			ik.enabled = true;
			nearItem = false;
			//lookTarget.solver.IKPosition = lookAtPickup;
			foreach (FullBodyBipedEffector e in effectors) {
				interactionSystem.StartInteraction(e, interactionObject, true);
			}
			pickedUp = true;
		}	

		//If Camera is picked up, move camera to target position in front of face
		if (!interactionSystem.inInteraction && pickedUp){

			//Smoothing, don't know why there's stutter if this is ommitted.
			if (targetItem.parent.name != "EYE_DEF") {
				targetItem.localPosition = new Vector3(.06f,0,0);
			}

			//Reset LookAt Weight to 1f because pick up sets it to 0
			lookTarget.solver.SetLookAtWeight(1f, 0.5f, 1f, 1f, 0.5f, 0.5f, 0.5f);

			targetItem.SetParent(head.FindChild("EYE_DEF"));
			targetItem.localPosition = Vector3.Slerp(targetItem.localPosition,targetMove.localPosition, 10f * Time.deltaTime);
			targetItem.localRotation = Quaternion.Lerp(targetItem.localRotation, targetMove.localRotation, 10f * Time.deltaTime);

			//If hand and camera are almost in position, set the position to the default target position
			if (Mathf.Abs(targetItem.localPosition.x - targetMove.localPosition.x) < 0.001) {
				targetItem.localPosition = targetMove.localPosition;
				targetItem.localRotation = targetMove.localRotation;
				targetMove.gameObject.SetActive(true);
				GameObject.Find("First Person Controller").transform.FindChild("unitychan").GetComponent<HeadRotation>().targetHand = targetMove.transform.FindChild("DefaultPos").transform;
				pickedUp = false;
				Destroy (targetItem.gameObject);
				//Destroy (destroyItems[0].gameObject);
				Destroy (destroyItems[1].gameObject);
			}
		}
	}
	

	void OnTriggerEnter(Collider col) {
		if (col.tag == "PickUp") {
			nearItem = true;
			interactionObject = col.transform.GetComponent<InteractionObject>();
			targetItem = col.transform.parent.GetChild(1);
			//destroyItems[0] = col.transform;
			destroyItems[1] = col.transform.parent;
			lookAtPickup = col.transform.parent.position;
			col.transform.parent = null;
		}
	}

	void OnTriggerExit(Collider col) {
			nearItem = false;
	}

}
