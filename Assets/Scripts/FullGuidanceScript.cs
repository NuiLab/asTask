using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullGuidanceScript : MonoBehaviour
{

    [SerializeField]
    private Color highlightColor;

    [SerializeField]
    private string[] highlightedBasePoints;

    [SerializeField]
    private string[] highlightedBuilderPoints;

    [SerializeField]
    private GameObject[] orderedBuilders;


    [SerializeField]
    private GameObject PlacementIndicator;


    public bool pointsCurrentlyHighlighted = false;

    public int numBuilders;
    public int currentIndex = 0;




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
            numBuilders -= 1;
            currentIndex += 1;

            orderedBuilders[currentIndex].GetComponent<Renderer>().material.color = highlightColor;

            pointsCurrentlyHighlighted = false;

        }


        if (bars.Length == numBuilders && !pointsCurrentlyHighlighted)
        {

            pointsCurrentlyHighlighted = true;

            removeAllIndicators();

            DisplayIndicatorsAtLocations(highlightedBasePoints[currentIndex], highlightedBuilderPoints[currentIndex]);


        }

        if (numBuilders == 0)
        {
            removeAllIndicators();
        }

    }



    void DisplayIndicatorsAtLocations(string BaselocationName, string BuilderlocationName)
    {

        GameObject BaseSnapPoint = GameObject.Find(BaselocationName);
        GameObject BuilderSnapPoint = GameObject.Find(BuilderlocationName);

        GameObject baseIndicator = Instantiate(PlacementIndicator, BaseSnapPoint.transform.position, BaseSnapPoint.transform.rotation);
        baseIndicator.transform.parent = BaseSnapPoint.transform;

        GameObject builderIndicator = Instantiate(PlacementIndicator, BuilderSnapPoint.transform.position, BuilderSnapPoint.transform.rotation);
        builderIndicator.transform.parent = BuilderSnapPoint.transform;

    }


    void removeAllIndicators()
    {
        GameObject[] indicators = GameObject.FindGameObjectsWithTag("Indicator");

        foreach (GameObject indicator in indicators)
        {
            Destroy(indicator);
        }

    }
}
