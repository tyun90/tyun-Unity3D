using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {

	public Transform top;
	public Transform bottom;
	public Transform mainCam;

	private bool animfinished;
	private float rBlink;
	private float elapsedTime;

	// Use this for initialization
	void Start () {
		animfinished = true;
		rBlink = Random.Range (5f, 10f);
	}
	
	// Update is called once per frame
	void Update () {
		rBlink -= Time.deltaTime;
		if (rBlink <= 0) {
			rBlink = Random.Range (5f, 10f);
			mainCam.GetComponent<Camera>().cullingMask ^= 1 << LayerMask.NameToLayer("Player Layer");
			top.GetComponent<Animation>().Play("TopLid");
			bottom.GetComponent<Animation>().Play("BottomLid");
			animfinished = false;
		}

		if ((!top.GetComponent<Animation>().isPlaying || !bottom.GetComponent<Animation>().isPlaying) && !animfinished) {
			mainCam.GetComponent<Camera>().cullingMask ^= 1 << LayerMask.NameToLayer("Player Layer");
			animfinished = true;
		}
	}
}
