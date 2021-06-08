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

            DisplayIndicatorsAtLocation("SnapPoint(8_Base):7", "SnapOffset(Builder) (-2)");

        }

        if (currentIdex == 1)
        {
            removeAllIndicators();

            DisplayIndicatorsAtLocation("", "");

        }






        // removeAllIndicators();

    }

    void DisplayIndicatorsAtLocation(string BaselocationName, string BuilderlocationName)
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
