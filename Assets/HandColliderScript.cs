using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColliderScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Builder")
        {

            DataStorage.LastgrabTime = System.DateTime.Now.ToString();

        }
    }
}
