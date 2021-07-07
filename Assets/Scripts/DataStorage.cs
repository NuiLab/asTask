using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataStorage
{
    private static string participantID;

    private static bool[] shapeA;

    private static bool[] shapeB;

    private static bool[] shapeC;


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



    public static bool[] ShapeA
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



}
