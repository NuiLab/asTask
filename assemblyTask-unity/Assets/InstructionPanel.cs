using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPanel : MonoBehaviour
{
    private float LookedAt = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator CountTime()
    {
        while (true)
        {
            LookedAt += Time.deltaTime; // Time.deltaTime is in seconds, so we multiply by 1000 to get milliseconds
            yield return null; // Wait for the next frame
        }
    }
    void OnBecameVisible()
    {
        //Debug.Log("Visible");
        StartCoroutine(CountTime());
    }
    void OnBecameInvisible()
    {
        //Debug.Log("Invisible");
        StopCoroutine(CountTime());
        //Debug.Log(LookedAt);
    }
}
