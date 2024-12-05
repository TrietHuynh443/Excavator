using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_01 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (TriggerTaskOne.taskone == true) transform.Translate(0, 0, 0.001f); 

    }
}
