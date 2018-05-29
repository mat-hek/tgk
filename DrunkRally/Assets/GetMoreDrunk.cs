using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMoreDrunk : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
        Hide();
        Invoke("Reset", 5);
        other.transform.parent.GetComponentInChildren<AlcoholNoiseGenerator>().DrinkAlcohol();
    }

    public void Hide() {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
    }

    public void Reset() {
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().enabled = true;
    }
}
