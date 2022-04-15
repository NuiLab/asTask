using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderManagerScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if ((other.name == "Right Hand" || other.name == "Left Hand") && DataStorage.BarGrabReset)
        {
            DataStorage.LastgrabTime = System.DateTime.Now.ToString();

            DataStorage.BarGrabReset = false;
        }

    }
}
