using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTaskOne : MonoBehaviour
{

    public static bool taskone;

    void Start()
    {
        taskone = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fork")
        {
            taskone = true;

        }
    }

}