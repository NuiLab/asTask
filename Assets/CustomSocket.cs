using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSocket : MonoBehaviour
{
    //private bool is_connected;


    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame

    
    //void Update()
    //{
    
    //}


    //private void OnTriggerEnter(Collider other)
    //{
        
    //}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "block")
        {
            Debug.Log("Hi im " + other.name);

            snap_object_here(other);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "block")
        {
            Debug.Log("I'm " + other.name + " and im leaving");
        }
    }




    void snap_object_here(Collider obj)
    {
        obj.transform.position = this.transform.position;
        obj.transform.rotation = this.transform.rotation;
    }
}
