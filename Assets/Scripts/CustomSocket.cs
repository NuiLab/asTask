using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSocket : MonoBehaviour {

    private bool object_snapped = false;
    private Collider update_obj;
    private Vector3 socket_position;
    //bool rotation_frozen = false;

    private void Start()
    {
        socket_position = this.transform.position;
    }

    void FixedUpdate () {
        // Every frame check if there is an object
        // attached and  call the snap_object_here_function
        if (object_snapped)
        {
            snap_object_here (update_obj);
        }

        //Debug.Log("NAME: " + update_obj.name + "ROTATION: " + update_obj.attachedRigidbody.freezeRotation);

    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "block")
        {

            // START HERE
            // When a new object enters the snap zone
            // and its tag is "block"

            // if the socket is empty
            if (!object_snapped) {
                //set the object to the current object being snapped
                // TODO: make this log into the record somehow.
                Debug.Log(this.name + " is snapping to " + other.name);
                update_obj = other;
                object_snapped = true;
            }

        }

    }

    //Called when the bar leaves the snap zone
    private void OnTriggerExit (Collider other)
    {

        if (other.tag == "bar")
        {
            // release the current object from the socket
            object_snapped = false;

            // stop freezing its rotation
            //if (rotation_frozen)
            //    un_freeze_rotation(update_obj);
        }
    }

    // Sets the position of the object and is called by fixed update
    // every 20ms
    void snap_object_here (Collider obj) {

        // TODO: Check if the object rotation dosent have to be set and fix the rotation problem?

        obj.transform.position = socket_position;

        //obj.transform.rotation = Quaternion.Euler(0, obj.transform.rotation.y, obj.transform.rotation.z);

        obj.attachedRigidbody.freezeRotation = true;
        //rotation_frozen = true;
    }




    //void un_freeze_rotation(Collider obj)
    //{

    //    obj.attachedRigidbody.freezeRotation = false;
    //    rotation_frozen = false;


    //}
}

// https://www.youtube.com/watch?v=Q1xZGt41N80
