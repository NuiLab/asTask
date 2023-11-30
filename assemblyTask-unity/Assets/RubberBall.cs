using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -5F, 0);
        float[] values = { 0.96f, 0.97f, 0.98f };
        int index = Random.Range(0, values.Length);
        this.GetComponent<Collider>().material.bounciness = values[index];
        StartCoroutine(Stop());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(80);
        this.GetComponent<Rigidbody>().isKinematic = true;
    }
}
