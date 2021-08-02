using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;


public class ShapeChecker : MonoBehaviour
{
    [SerializeField]
    private int SHAPEARRAYSIZE = 4;

    [SerializeField]
    private string shapeLetter = "A";

    [SerializeField]
    private GameObject InvisibleShape;


    ShapeData shapeData;
    BuildData[] ShapeArray;




    private GameObject[] bases;


    // Start is called before the first frame update
    void Start()
    {
        //placedPieces = new bool[InvisibleShape.transform.childCount];

        DataStorage.CurrentBasePoint = "NULL";
        DataStorage.CurrentBuilderPoint = "NULL";

        DataStorage.LastgrabTime = "NULL";
        DataStorage.LastbuildTime = "NULL";

        DataStorage.BarGrabReset = true;

        shapeData = new ShapeData();
        ShapeArray = new BuildData[SHAPEARRAYSIZE];

        shapeData.sceneStartTime = System.DateTime.Now.ToString();

        shapeData.buildData = ShapeArray;


        for (int i = 0; i < SHAPEARRAYSIZE; i++)
        {
            ShapeArray[i] = new BuildData();
        }



    }

    // Update is called once per frame
    void Update()
    {
        bases = GameObject.FindGameObjectsWithTag("Base");


        foreach (GameObject bas in bases)
        {

            BaseManager baseManager = bas.GetComponent<BaseManager>();
            bool barIsCorrectlyPlaced = false;
            int currentStep = bases.Length - 1;

            foreach (Transform child in InvisibleShape.transform)
            {
                // check if the base is in one of the right spots
                if (Vector3.Distance(bas.transform.position, child.transform.position) <= 0.02f && !baseManager.getChecked())
                {
                    baseManager.setChecked(true);
                    barIsCorrectlyPlaced = true;

                    shapeData.buildData[currentStep].placedCorrectly = true;
                    shapeData.buildData[currentStep].baseName = DataStorage.CurrentBasePoint;
                    shapeData.buildData[currentStep].builderName = DataStorage.CurrentBuilderPoint;
                    shapeData.buildData[currentStep].timeGrabbed = DataStorage.LastgrabTime;
                    shapeData.buildData[currentStep].timePlaced = DataStorage.LastbuildTime;

                    SaveShapeArray();

                }

            }

            // if it isnt then set the proper variables
            if (!barIsCorrectlyPlaced && !baseManager.getChecked())
            {
                baseManager.setChecked(true);

                shapeData.buildData[currentStep].placedCorrectly = false;
                shapeData.buildData[currentStep].baseName = DataStorage.CurrentBasePoint;
                shapeData.buildData[currentStep].builderName = DataStorage.CurrentBuilderPoint;
                shapeData.buildData[currentStep].timeGrabbed = DataStorage.LastgrabTime;
                shapeData.buildData[currentStep].timePlaced = DataStorage.LastbuildTime;

                SaveShapeArray();

            }

        }



    }

    public void LogTime()
    {
        shapeData.timeElapsed = Time.timeSinceLevelLoad.ToString();
        shapeData.sceneEndTime = System.DateTime.Now.ToString();

        shapeData.instructionSceneStartTime = DataStorage.LastInstructionSceneStartTime;
        shapeData.instructionSceneEndTime = DataStorage.LastInstructionSceneEndTime;
        shapeData.instructionSceneElapsedTime = DataStorage.LastInstructionSceneElapsedTime;

        SaveShapeArray();
    }



    private void SaveShapeArray()
    {


        string upperShapeLetter = shapeLetter.ToUpper();

        switch (upperShapeLetter)
        {
            case "A":
                DataStorage.ShapeA = shapeData;
                break;
            case "B":
                DataStorage.ShapeB = shapeData;
                break;
            case "C":
                DataStorage.ShapeC = shapeData;
                break;
            case "D":
                DataStorage.ShapeD = shapeData;
                break;
            case "E":
                DataStorage.ShapeE = shapeData;
                break;
            case "F":
                DataStorage.ShapeF = shapeData;
                break;
            case "G":
                DataStorage.ShapeG = shapeData;
                break;
            case "H":
                DataStorage.ShapeH = shapeData;
                break;

            default:
                Debug.Log("An unavailable letter was chosen in ShapeChecker.cs");
                break;
        }
    }
}
