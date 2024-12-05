using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_05 : MonoBehaviour {


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (TriggerTaskFive.taskfive == true) transform.Translate(0, 0, 0.014f);

        if (TriggerTaskThree.taskthree == true) Destroy(gameObject);

    }
}
