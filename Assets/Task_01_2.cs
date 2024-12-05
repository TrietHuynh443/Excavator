using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_01_2 : MonoBehaviour {

    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(0, 0, 0.001f);
        if (TriggerTaskOne.taskone == true) Destroy(gameObject);

    }
}

