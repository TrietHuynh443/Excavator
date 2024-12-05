using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWorker : MonoBehaviour {

    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(0, 0, 0.02f);

    }
}

