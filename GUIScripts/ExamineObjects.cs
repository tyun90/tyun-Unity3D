using UnityEngine;
using System.Collections;

public class ExamineObjects : MonoBehaviour {

	private static ExamineObjects examine;
	private GameObject examineObj;
	private bool mousePressed;

	private float xDeg;
	private float yDeg;
	private float xDegStart;
	private float yDegStart;
	private Quaternion newRot = Quaternion.identity;
	private Quaternion fromRot;
	private Quaternion toRot;

	public static ExamineObjects instance {
		get {
			if (examine == null) {
				examine = GameObject.FindObjectOfType<ExamineObjects> ();
			}
			return examine;
		}
	}

	void Update() {
		xDeg -= Input.GetAxis ("Mouse X");
		yDeg -= Input.GetAxis ("Mouse Y");
		if (mousePressed) {
			fromRot = examineObj.transform.localRotation;

			toRot = Quaternion.Euler (0, xDeg, yDeg);
			examineObj.transform.localRotation = Quaternion.Lerp (fromRot, toRot, 3f * Time.deltaTime);
		} 
	}

	public void SetObj(GameObject obj, Transform cam) {
		examineObj = obj;
		examineObj.transform.SetParent(cam.transform.FindChild("ObjectHolder"));
		examineObj.transform.localPosition = Vector3.zero;
		examineObj.transform.localScale = Vector3.one;
	}

	public void isPressed() {
		mousePressed = !mousePressed;
	}

}
