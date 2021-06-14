using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShapeAGuideScript : MonoBehaviour
{



    public Text Debug0;
    public Text Debug1;
    public Text Debug2;
    public Text Debug3;


    public GameObject[] orderedBuilders;

    public GameObject[] orderedPreviews;

    public GameObject startingBase;

    public GameObject PlacementIndicator;

    public GameObject BaseSnapPoint;
    public GameObject BuilderSnapPoint;


    public Color highlightColor;

    private int numBuilders;
    private int currentIndex = 0;

    private bool BaseIndicatorPlaced = false;
    private bool BuilderIndicatorPlaced = false;
    


    void Start()
    {

        numBuilders = orderedBuilders.Length;
        orderedBuilders[0].GetComponent<Renderer>().material.color = highlightColor;

    }

    void Update()
    {


        GameObject[] bars = GameObject.FindGameObjectsWithTag("Builder");


        if (bars.Length < numBuilders)
        {
            numBuilders -= 1;
            currentIndex += 1;
            orderedBuilders[currentIndex].GetComponent<Renderer>().material.color = highlightColor;
        }



        // state machine

        if (currentIndex == 0)
        {
            removeAllIndicators();

            DisplayIndicatorsAtLocations("SnapPoint(8_Base):7", "SnapOffset(7_Builder) (-2)");

        }

        if (currentIndex == 1)
        {
            removeAllIndicators();

            DisplayIndicatorsAtLocations("SnapPoint(7_Base):7", "SnapOffset(6_Builder) (-3)");

        }

        if (currentIndex == 2)
        {
            removeAllIndicators();

            DisplayIndicatorsAtLocations("SnapPoint(6_Base):6", "SnapOffset(8_Builder) (3)");

        }

        if (currentIndex == 3)
        {
            removeAllIndicators();
        }






        // removeAllIndicators();

    }

    void DisplayIndicatorsAtLocations(string BaselocationName, string BuilderlocationName)
    {
        if (!BaseIndicatorPlaced)
        {
            BaseSnapPoint = GameObject.Find(BaselocationName);

            Instantiate(PlacementIndicator, BaseSnapPoint.transform.position, BaseSnapPoint.transform.rotation);
        }

        if (!BuilderIndicatorPlaced)
        {
            BuilderSnapPoint = GameObject.Find(BuilderlocationName);

            Instantiate(PlacementIndicator, BuilderSnapPoint.transform.position, BuilderSnapPoint.transform.rotation);
        }

    }


    void removeAllIndicators()
    {
        GameObject[] indicators = GameObject.FindGameObjectsWithTag("Indicator");

        foreach (GameObject indicator in indicators)
        {
            Destroy(indicator);
        }

        BaseIndicatorPlaced = false;
        BuilderIndicatorPlaced = false;
    }
}
