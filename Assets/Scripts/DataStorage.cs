using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataStorage
{

    // temporary variables to store current info

    private static string currentBasePoint = "NULL";
    private static string currentBuilderPoint = "NULL";
    private static string lastbuildTime = "NULL";
    private static string lastgrabTime = "NULL";
    private static int experimentVersion = 0;
    private static bool barGrabReset = true;


    public static string CurrentBasePoint
    {
        get
        {
            return currentBasePoint;

        }
        set
        {
            currentBasePoint = value;
        }
    }

    public static string CurrentBuilderPoint
    {
        get
        {
            return currentBuilderPoint;
        }
        set
        {
            currentBuilderPoint = value;
        }
    }

    public static string LastbuildTime
    {
        get
        {
            return lastbuildTime;
        }
        set
        {
            lastbuildTime = value;
        }
    }



    public static string LastgrabTime
    {
        get
        {
            return lastgrabTime;
        }
        set
        {
            lastgrabTime = value;
        }
    }

    public static int ExperimentVersion
    {
        get
        {
            return experimentVersion;
        }
        set
        {
            experimentVersion = value;
        }
    }

    public static bool BarGrabReset
    {
        get
        {
            return barGrabReset;
        }
        set
        {
            barGrabReset = value;
        }
    }


    // variables to print out at the end
    private static string participantID = "NULL";


    private static BuildData[] shapeA = new BuildData[3];
    private static BuildData[] shapeB;
    private static BuildData[] shapeC;
    private static BuildData[] shapeD;
    private static BuildData[] shapeE;
    private static BuildData[] shapeF;
    private static BuildData[] shapeG;
    private static BuildData[] shapeH;


    public static string ParticipantID
    {
        get
        {
            return participantID;
        }
        set
        {
            participantID = value;
        }
    }

    public static BuildData[] ShapeA
    {
        get
        {
            return shapeA;
        }
        set
        {
            shapeA = value;
        }
    }

    public static BuildData[] ShapeB
    {
        get
        {
            return shapeB;
        }
        set
        {
            shapeB = value;
        }
    }

    public static BuildData[] ShapeC
    {
        get
        {
            return shapeC;
        }
        set
        {
            shapeC = value;
        }
    }

    public static BuildData[] ShapeD
    {
        get
        {
            return shapeD;
        }
        set
        {
            shapeD = value;
        }
    }

    public static BuildData[] ShapeE
    {
        get
        {
            return shapeE;
        }
        set
        {
            shapeE = value;
        }
    }

    public static BuildData[] ShapeF
    {
        get
        {
            return shapeF;
        }
        set
        {
            shapeF = value;
        }
    }

    public static BuildData[] ShapeG
    {
        get
        {
            return shapeG;
        }
        set
        {
            shapeG = value;
        }
    }

    public static BuildData[] ShapeH
    {
        get
        {
            return shapeH;
        }
        set
        {
            shapeH = value;
        }
    }



}
