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


    //TIME Variables

    private static string experimentStartTime = "NULL";
    private static string experimentEndTime = "NULL";






    // variables to print out at the end
    private static string participantID = "NULL";



    private static ShapeData shapeA;

    private static ShapeData shapeB;

    private static ShapeData shapeC;

    private static ShapeData shapeD;

    private static ShapeData shapeE;

    private static ShapeData shapeF;

    private static ShapeData shapeG;

    private static ShapeData shapeH;


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

    public static ShapeData ShapeA
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

    public static ShapeData ShapeB
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

    public static ShapeData ShapeC
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

    public static ShapeData ShapeD
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

    public static ShapeData ShapeE
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

    public static ShapeData ShapeF
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

    public static ShapeData ShapeG
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

    public static ShapeData ShapeH
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
