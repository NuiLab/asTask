using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSocket : MonoBehaviour
{

    private bool object_snapped = false;
    private Collider update_obj;

    void Update()
    {
        // Every frame check if there is an object
        // attatched and  call the snap_object_here_function
        if (object_snapped)
        {
            snap_object_here(update_obj);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "block")
        {
            // When a new object enters the snap zone
            // and its tag is "block"

            if (!object_snapped)
            {
                update_obj = other;
                object_snapped = true;
            }

        }

    }


    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "bar")
        {
            Debug.Log("I'm " + other.name + " and im leaving");
            object_snapped = false;
        }
    }




    void snap_object_here(Collider obj)
    {
        obj.transform.position = this.transform.position;
        obj.transform.rotation = this.transform.rotation;
    }
}


// https://www.youtube.com/watch?v=Q1xZGt41N80