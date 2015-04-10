using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIItemText : MonoBehaviour {

	public string msg = "Press G to pick up";
	public Text thisText;
	public float initialDelay = 1f;
	public float typeDelay = 0.00001f;

	// Use this for initialization
	void Start () {
	}

	void Awake() {
		//thisText = this.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
	}

	IEnumerator Typing() {
		yield return new WaitForSeconds(initialDelay);
		for (int i = 0; i <= msg.Length; i++) {
			thisText.text = msg.Substring(0,i);
			yield return new WaitForSeconds(typeDelay);
		}
	}

	IEnumerator ClearText() {
		thisText.text = "";
		yield return null;
	}
}
