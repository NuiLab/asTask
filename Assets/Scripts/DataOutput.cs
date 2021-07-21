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

            for (int j = 0; j < numberOfSteps; j++)
            {
                int currentIndex = j + 1;

                titleLineText += "Shape" + currentShape + currentIndex + "-basePoint, "
                                + "Shape" + currentShape + currentIndex + "-BuilderPoint, "
                                + "Shape" + currentShape + currentIndex + "-timeGrabbed, "
                                + "Shape" + currentShape + currentIndex + "-timePlaced, "
                                + "Shape" + currentShape + currentIndex + "-placedCorrectly, ";

            }

        }

        File.WriteAllText(FilePath, titleLineText);



    }

    void writeIDandVersion()
    {
        File.AppendAllText(FilePath, "\n" + DataStorage.ParticipantID + ", " + DataStorage.ExperimentVersion + ", ");
    }


    void writeShape(BuildData[] ShapeArray)
    {

        // foreach (var dataStore in ShapeArray)
        // {
        //     File.AppendAllText(FilePath, dataStore.baseName + ", "
        //                             + dataStore.builderName + ", "
        //                             + dataStore.timeGrabbed + ", "
        //                             + dataStore.timePlaced + ", "
        //                             + dataStore.placedCorrectly + ", ");
        // }

        for (int i = 0; i < ShapeArray.Length; i++)
        {
            if (i == 0)
            {

                File.AppendAllText(FilePath, "NULL" + ", "
                                    + "NULL" + ", "
                                    + "NULL" + ", "
                                    + "NULL" + ", "
                                    + ShapeArray[i].placedCorrectly + ", ");

            }
            else
            {
                File.AppendAllText(FilePath, ShapeArray[i].baseName + ", "
                                    + ShapeArray[i].builderName + ", "
                                    + ShapeArray[i].timeGrabbed + ", "
                                    + ShapeArray[i].timePlaced + ", "
                                    + ShapeArray[i].placedCorrectly + ", ");
            }
        }

    }





}
