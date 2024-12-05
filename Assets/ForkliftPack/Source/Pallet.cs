using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pallet : MonoBehaviour
{
    public Transform fork;
    public GameObject item;
    public GameObject tempParent;
    public Transform guide;
    float forkMaxDown;

    void Start()
    {
        forkMaxDown = fork.transform.localPosition.z;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Fork"))
        {

            if (fork.transform.localPosition.z <= forkMaxDown)
            {
                print("ground");
            }

            else
            {
                print("attached");
                item.GetComponent<Rigidbody>().useGravity = false;
                item.GetComponent<Rigidbody>().isKinematic = true;
                item.transform.position = guide.transform.position;
                item.transform.rotation = guide.transform.rotation;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fork"))
        {
            print("detached");
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}


