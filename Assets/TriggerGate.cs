using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGate : MonoBehaviour {

    public static bool taskgate;

    void Start()
    {
        taskgate = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fork")
        {
            taskgate = true;

        }
    }
}
