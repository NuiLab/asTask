using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepParentWithChild : MonoBehaviour
{

    
    void Update()
    {
        transform.parent.position = transform.position - transform.localPosition;
    }
}
