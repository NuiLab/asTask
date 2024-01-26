using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCICollector : MonoBehaviour
{
    // Start is called before the first frame update
    int barCount = 0;
    public GameObject bciMenu;
    public GameObject instructions;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (barCount == 12)
        {
            Debug.Log("All bars are in");
            bciMenu.SetActive(true);
            instructions.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Builder")
        {
            barCount++;
            Debug.Log(barCount);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Builder")
        {
            barCount--;
        }
    }
}
