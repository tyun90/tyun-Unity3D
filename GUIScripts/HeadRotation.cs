using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;

public class HeadRotation : MonoBehaviour {

	public Transform head;
	public Transform camera;
	public Transform Spine1;
	public Transform Spine2;
	public Transform Spine3;
	public Transform Neck;
	public Transform targetHand;
	public Vector3 newRotation;
	public Vector3 resetCam;

	public FullBodyBipedIK ik;

	private IKEffector lefthand;

	public bool itemHeld;

	private float posweight;
	private float rotweight;


	void Awake() {
		newRotation = new Vector3 (0, 0, 0);
		resetCam = new Vector3 (0, 0, 0);
		posweight = 0f;
		rotweight = 0f;
		lefthand = ik.solver.leftHandEffector;
	}

	// Use this for initialization
	void Start () {
		itemHeld = false;
		ik.Disable ();
	}
	
	// Update is called once per frame
	void Update () {

		//If phone is a child of head and if it is picked up
		if (head.FindChild("EYE_DEF").FindChild("Carried Phone") != null) {
			itemHeld = true;
		}
	}

	void LateUpdate(){
		//head.localEulerAngles = new Vector3 (head.localEulerAngles.x, head.localEulerAngles.y, -camera.localEulerAngles.x);
		//camera.localEulerAngles = new Vector3 (0, camera.localEulerAngles.y, 0);
		RotateHead ();
		if (itemHeld) {
			RotateArm ();
		}
	}

	void RotateHead() {
		newRotation.x = head.localEulerAngles.x;
		newRotation.y = head.localEulerAngles.y;
		newRotation.z = -camera.localEulerAngles.x;
		resetCam.y = camera.localEulerAngles.y;
		
		head.localEulerAngles = newRotation;
		camera.localEulerAngles = resetCam;
	}

	void RotateArm() {
		lefthand.position = targetHand.position;
		lefthand.rotation = targetHand.rotation;
		//if (posweight < 1f) {
		//	StartCoroutine("moveArm");
		//	lefthand.positionWeight = posweight;
		//	lefthand.rotationWeight = rotweight;
		//}
		lefthand.positionWeight = 1f;
		lefthand.rotationWeight = 1f;
		ik.solver.Update ();
	}

	IEnumerator moveArm() {
		for (float f = posweight; f < 1.1f; f+=0.1f) {
			posweight = f;
			rotweight = f;
			yield return null;

		}
	}
}
