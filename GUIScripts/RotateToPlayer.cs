using UnityEngine;
using System.Collections;

public class RotateToPlayer : MonoBehaviour {

	private Transform canvas;
	public Transform player;

	private Vector3 rotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		rotation = transform.position - player.position;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (rotation), 10 * Time.deltaTime);

	}
}
