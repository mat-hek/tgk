using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (-transform.up * 1000);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey ("w")) {
			rb.AddForce (transform.forward*20000);
//			transform.Translate(new Vector3 (0, 0, 1));
		}
		if (Input.GetKey ("s")) {
			rb.AddForce (-transform.forward*20000);
		}
		if (Input.GetKey ("a")) {
			transform.Rotate(new Vector3 (0, (float)-0.5, 0));
		}
		if (Input.GetKey ("d")) {
			transform.Rotate(new Vector3 (0, (float)0.5, 0));
		}
	}
}
