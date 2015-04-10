using UnityEngine;
using System.Collections;

public class Ragdoll : MonoBehaviour {

	public Transform body;
	public Transform head;
	public Transform camera;

	private Vector3 force;
	private Vector3 newRotation;
	private Vector3 resetCam;
	

	// Use this for initialization
	void Start () {
		force = new Vector3 (0, Random.Range(15000,18000), Random.Range(10000,11000));
		body.GetComponent<Rigidbody>().AddForce (force);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
