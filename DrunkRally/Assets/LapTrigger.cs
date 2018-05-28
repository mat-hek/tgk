using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTrigger : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
        GameObject.Find("Tocus").SendMessage("StartLap");
    }
}
