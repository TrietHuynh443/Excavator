using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_04_2 : MonoBehaviour {


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (TriggerTaskFour.taskfour == true) transform.Translate(0, 0, 0.001f);
    }
}
