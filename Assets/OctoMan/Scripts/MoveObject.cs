using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Animator anim;
    public GameObject item;
    public GameObject tempParent;
    public Transform guide;

    void Start()
    {
        item.GetComponent<Rigidbody>().useGravity = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Excavator"))
        {
            print("enter");
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.transform.position = guide.transform.position;
            item.transform.rotation = guide.transform.rotation;
            item.transform.parent = tempParent.transform;
        }
    }


    void OnTriggerStay(Collider other)
    {
         if (anim.GetInteger("ShovelPosition") == 2)
         {
            print("go");
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.transform.parent = null;
         }
    }
}

   

        /*
        angle = shovel.transform.rotation.eulerAngles.z;

        if (other.gameObject.CompareTag("Excavator")) // & angle > 215)
        {            
        */