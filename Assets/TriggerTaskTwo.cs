using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTaskTwo : MonoBehaviour {

        public static bool tasktwo;

        void Start()
        {
            tasktwo = false;
        }


        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Fork")
            {
                tasktwo = true;

            }
        }

    }