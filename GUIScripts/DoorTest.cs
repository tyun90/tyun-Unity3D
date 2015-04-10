using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;

public class DoorTest : MonoBehaviour {

	public Transform player;
	public FullBodyBipedIK ik;
	public InteractionSystem interactionSystem;
	[SerializeField] InteractionObject interactionObject; // The object to interact to
	[SerializeField] FullBodyBipedEffector[] effectors; // The effectors to interact with


	private Transform door;

	private Transform handleA;
	private Transform handleB;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M)) {
			ik.enabled = true;
			//CharacterManager.instance.toggleInput();
			SingleDoor A = new SingleDoor();
			A.openDoor(interactionSystem, effectors, interactionObject);
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Door") {
			door = col.transform;
			handleA = col.transform.Find("ldthm/DoorHinge/DoorHandle/obj_10");
			handleB = col.transform.Find("ldthm/DoorHinge/Group002/obj_13");
			if (Vector3.Distance(player.position, handleA.position) < Vector3.Distance(player.position, handleB.position)) {
				interactionObject = handleA.GetComponent<InteractionObject>();
			} else {
				interactionObject = handleB.GetComponent<InteractionObject>();
			}
		}
	}

}
