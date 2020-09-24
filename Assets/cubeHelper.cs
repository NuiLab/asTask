using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private int interval = 20;
    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % interval == 0)
        {
            this.gameObject.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "bar")
        {
            Debug.Log("WHY ME!");
            //Destroy(this.gameObject);
            //Destroy(other.gameObject);
            this.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        }
    }
}
