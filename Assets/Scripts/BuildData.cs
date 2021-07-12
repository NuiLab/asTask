using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildData
{

    public string builderName { get; set; }
    public string baseName { get; set; }

    public bool placedCorrectly { get; set; }

    public string timeGrabbed { get; set; }
    public string timePlaced { get; set; }


    public BuildData()
    {
        placedCorrectly = false;
        builderName = "NULL";
        baseName = "NULL";
        timeGrabbed = "NULL";
        timePlaced = "NULL";

    }

    public override string ToString()
    {
        return "Placed Correctly: " + placedCorrectly.ToString() +
               "\nBuilderName: " + builderName +
               "\nBaseName: " + baseName +
               "\timeGrabbed: " + timeGrabbed +
               "\nBaseName: " + timePlaced;
    }

}
