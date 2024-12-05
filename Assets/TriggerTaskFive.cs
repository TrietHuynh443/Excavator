using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTaskFive : MonoBehaviour
{

    public static bool taskfive;

    void Start()
    {
        taskfive = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fork")
        {
            taskfive = true;

        }
    }
}
