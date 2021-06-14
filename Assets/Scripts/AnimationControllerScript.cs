using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class AnimationControllerScript : MonoBehaviour
{


    [SerializeField]
    private Transform barA;

    

    [SerializeField]
    private Transform barB;


    [SerializeField]
    private Transform startPoint;


     

    



    [SerializeField]
    [Range(0f, 1f)]
    private float lerpPercent = 0.5f;

    [SerializeField]
    [Range(0f, 1f)]
    private float speed = 0.5f;



    void Update()
    {

        // Make barA move towards barB

        barA.transform.position = Vector3.Lerp(startPoint.transform.position, barB.transform.position, lerpPercent * speed );
        barA.transform.rotation = Quaternion.Lerp(startPoint.transform.rotation, barB.transform.rotation, lerpPercent * speed);


    }

    private void FixedUpdate()
    {
        lerpPercent += Time.deltaTime;
    }
}
