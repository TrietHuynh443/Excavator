using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_03 : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (TriggerTaskThree.taskthree == true) transform.Translate(0, 0, 0.001f);

       }
}
