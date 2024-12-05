using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTaskFour : MonoBehaviour {

    public static bool taskfour;

    void Start()
    {
        taskfour = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if ( (TriggerTaskThree.taskthree == true) && (other.tag == "Fork") )
        {
            taskfour = true;

        }
    }
}
