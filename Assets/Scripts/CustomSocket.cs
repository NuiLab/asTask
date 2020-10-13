using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSocket : MonoBehaviour {

    private bool object_snapped = false;
    private Collider attatched_cube;
    private Vector3 socket_position;
    private Quaternion socket_rotation;
    //private GameObject attatched_cube_reset_position;
    //bool rotation_frozen = false;

    private void Start()
    {
        socket_position = this.transform.position;
        socket_rotation = this.transform.rotation;
    }

    void OnTriggerStay () {
        // Every frame check if there is an object
        // attached and  call the snap_object_here_function

        if (object_snapped)
        {
            snap_object_to_socket (attatched_cube);
        }
        
        

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
                attatched_cube = other;

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


        }
    }

    // Sets the position of the object and is called by fixed update
    // every 20ms
    void snap_object_to_socket (Collider obj) {


        obj.transform.position = socket_position;

        obj.transform.rotation = socket_rotation;

       
    }




  
}


