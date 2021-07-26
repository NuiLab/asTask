using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataOutput : MonoBehaviour
{


    private string FileName = "FutureofWorkData.csv";
    private string FilePath;




    public void outputDataToFile()
    {
        FilePath = Path.Combine(Application.persistentDataPath, FileName);


        // check if file exists
        // if not generate and write out title line
        if (!File.Exists(FilePath))
        {
            writeTitleLine();
        }

        writeIDandVersion();

        TextDebugger.LogString(DataStorage.ShapeA.ToString());

        writeShape(DataStorage.ShapeA);
        writeShape(DataStorage.ShapeB);
        writeShape(DataStorage.ShapeC);
        writeShape(DataStorage.ShapeD);
        writeShape(DataStorage.ShapeE);
        writeShape(DataStorage.ShapeF);
        writeShape(DataStorage.ShapeG);
        writeShape(DataStorage.ShapeH);

    }


    void writeTitleLine()
    {
        string titleLineText = "ParticipantID, ExperimentType, ";

        string[] shapes = { "A", "B", "C", "D", "E", "F", "G", "H" };
        int[] piecesPerShape = { 4, 4, 7, 9, 8, 9, 11, 8 };

        for (int i = 0; i < shapes.Length; i++)
        {
            string currentShape = shapes[i];
            int numberOfSteps = piecesPerShape[i];

            titleLineText += ("Shape" + currentShape + "-sceneStartTime, "
                            + "Shape" + currentShape + "-sceneEndTime, "
                            + "Shape" + currentShape + "-percentCorrect, "
                            );

            for (int j = 0; j < numberOfSteps; j++)
            {

                if (j != 0)
                {
                    titleLineText += ""
                                    + "Shape" + currentShape + j + "-BuilderPoint, "
                                    + "Shape" + currentShape + j + "-basePoint, "
                                    + "Shape" + currentShape + j + "-timeGrabbed, "
                                    + "Shape" + currentShape + j + "-timePlaced, "
                                    + "Shape" + currentShape + j + "-placedCorrectly, ";
                }


            }

        }

        File.WriteAllText(FilePath, titleLineText);



    }

    void writeIDandVersion()
    {
        File.AppendAllText(FilePath, "\n" + DataStorage.ParticipantID + ", " + versionName(DataStorage.ExperimentVersion) + ", ");
    }

    string versionName(int version)
    {
        switch (version)
        {
            case 1:
                return "1: Final Product";
            case 2:
                return "2: Fully Guided";
            case 3:
                return "3: Step By Step";
            default:
                return "UNKNOWN";
        }



    }


    void writeShape(ShapeData shapeData)
    {
        //TextDebugger.LogString(shapeData.ToString());

        Debug.Log("TOSTRING: " + shapeData.ToString());
        Debug.Log("!TOSTRING: " + shapeData);

        File.AppendAllText(FilePath, shapeData.ToString());

    }








}
