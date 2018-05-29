using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LapTrigger : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
        //GameObject.Find("Tocus").SendMessage("StartLap");
        System.Type t = other.GetType();
        other.GetComponentInParent<Timer>().SendMessage("StartLap");
    }
}
