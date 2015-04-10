using UnityEngine;
using System.Collections;
using RootMotion.FinalIK;

public class SingleDoor : Door {

	private Transform thisDoor;
	private Transform reqKey;
	private string ID;
	
	public SingleDoor(Transform door, Transform key, string doorID) {
		thisDoor = door; 
		reqKey = key;
		ID = doorID;
	}

	public SingleDoor() {

	}

	public void openDoor(InteractionSystem system, FullBodyBipedEffector[] effector, InteractionObject obj) {
		foreach (FullBodyBipedEffector e in effector) {
			system.StartInteraction(e, obj, true);
		}
	}

}
