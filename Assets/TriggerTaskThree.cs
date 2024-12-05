using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTaskThree : MonoBehaviour {

    public static bool taskthree;

    void Start()
    {
        taskthree = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fork")
        {
            taskthree = true;

        }
    }
}
