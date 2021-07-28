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

        string output = ""
                        + builderName + ", "
                        + baseName + ", "
                        + timeGrabbed + ", "
                        + timePlaced + ", "
                        + placedCorrectly.ToString() + ", ";



        return output;
    }

}



public class ShapeData
{
    public string shapeName { get; set; }
    public string sceneStartTime { get; set; }
    public string sceneEndTime { get; set; }
    public string timeElapsed { get; set; }

    public BuildData[] buildData { get; set; }


    public ShapeData()
    {
        shapeName = "NULL";
        sceneStartTime = "NULL";
        sceneEndTime = "NULL";
        timeElapsed = "NULL";

    }


    public override string ToString()
    {

        double percentCorrect = CalculatePercentCorrect(buildData);

        string output = ""
                        + sceneStartTime + ", "
                        + sceneEndTime + ", "
                        + timeElapsed + ", "
                        + percentCorrect + ", "
                        ;

        for (int i = 0; i < buildData.Length; i++)
        {
            if (i != 0)
                output += buildData[i].ToString();
        }


        return output;




    }


    private double CalculatePercentCorrect(BuildData[] ShapeArray)
    {
        double numberCorrect = 0;

        for (int i = 0; i < ShapeArray.Length; i++)
        {
            if (ShapeArray[i].placedCorrectly && i != 0)
            {
                numberCorrect += 1;
            }

        }

        double arrayLength = ShapeArray.Length - 1;

        double percentCorrect = numberCorrect / arrayLength;

        // TextDebugger.LogString(numberCorrect.ToString() + "/" + arrayLength.ToString() + " = " + percentCorrect.ToString());

        return percentCorrect * 100;



    }

}
