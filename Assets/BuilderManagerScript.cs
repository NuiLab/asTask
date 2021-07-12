using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderManagerScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "base" || other.gameObject.tag != "builder")
        {
            DataStorage.LastgrabTime = System.DateTime.Now.ToString();
        }

    }
}
