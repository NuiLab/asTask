using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGuidanceScript : MonoBehaviour
{
    [SerializeField]
    private Color highlightColor;

    [SerializeField]
    private GameObject[] orderedBuilders;

    [SerializeField]
    private GameObject[] previews;

    [SerializeField]
    private int[] steps;



    private int numBuilders;
    private int currentIndex = 0;
    private bool previewDisplayed;


    

    // Start is called before the first frame update
    void Start()
    {

        numBuilders = orderedBuilders.Length;

        orderedBuilders[0].GetComponent<Renderer>().material.color = highlightColor;

        
        
    }

    // Update is called once per frame
    void Update()
    {

        GameObject[] bars = GameObject.FindGameObjectsWithTag("Builder");


        if (bars.Length < numBuilders)
        {
            numBuilders--;
            currentIndex++;

            orderedBuilders[currentIndex].GetComponent<Renderer>().material.color = highlightColor;

            previewDisplayed = false;

        }

        if (bars.Length == numBuilders && !previewDisplayed)
        {
            previewDisplayed = true;

            GameObject currentBar = GameObject.Find((currentIndex + 1).ToString());




            GameObject currentPreview = Instantiate(previews[barval(steps[currentIndex])], currentBar.transform.position, currentBar.transform.rotation);




            
        }

    }



    int barval(int val)
    {
        return val - 3;
    }
}
