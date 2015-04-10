using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

	private static CharacterManager character;

	public CharacterController charController;
	public MonoBehaviour[] controllerScripts;
	public MonoBehaviour[] camLookScripts;


	public static CharacterManager instance {
		get {
			if (character == null) {
				character = GameObject.FindObjectOfType<CharacterManager> ();
			}
			return character;
		}
	}
	
	// Use this for initialization
	void Start () {
		charController = GameObject.Find ("First Person Controller").GetComponent<CharacterController> ();
		controllerScripts = GameObject.Find ("First Person Controller").GetComponents<MonoBehaviour> ();
		camLookScripts = GameObject.FindGameObjectWithTag ("Main Camera").GetComponents<MonoBehaviour> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleInput() {
		charController.enabled = !charController.enabled;
		foreach (MonoBehaviour m in controllerScripts) {
			m.enabled = !m.enabled;
		}
		foreach (MonoBehaviour m in camLookScripts) {
			m.enabled = !m.enabled;
		}
	}


}
