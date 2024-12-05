using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_02 : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (TriggerTaskTwo.tasktwo == true) transform.Translate(0, 0, 0.015f);

        if (TriggerTaskFive.taskfive == true) Destroy(gameObject);

    }
}
