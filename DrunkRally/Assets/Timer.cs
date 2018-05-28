using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Text timerText;
    private float startTime;
	// Use this for initialization
	void Start () {
		StartLap();
	}
	
	// Update is called once per frame
	void Update () {
		float dt = Time.time - startTime;
        string minutes = ((int) dt / 60).ToString();
        string seconds = (dt % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds; 
	}

    public void StartLap() {
        startTime = Time.time;
    }
}
