using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeUnitManager : MonoBehaviour
{

    public GameObject attatchedItem;


    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders () { return colliders; }

 

     private void OnTriggerEnter (Collider other) {
         if (!colliders.Contains(other)) { colliders.Add(other); }
     }

     private void OnTriggerExit (Collider other) {
         colliders.Remove(other);
         if(other.gameObject.tag == "block")
        {
            Destroy(other.gameObject);
        }
     }
}
