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


        ShapeArray = new BuildData[SHAPEARRAYSIZE];

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

                    ShapeArray[currentStep].placedCorrectly = true;
                    ShapeArray[currentStep].baseName = DataStorage.CurrentBasePoint;
                    ShapeArray[currentStep].builderName = DataStorage.CurrentBuilderPoint;
                    ShapeArray[currentStep].timeGrabbed = DataStorage.LastgrabTime;
                    ShapeArray[currentStep].timePlaced = DataStorage.LastbuildTime;

                    SaveShapeArray();

                }

            }

            // if it isnt then set the proper variables
            if (!barIsCorrectlyPlaced && !baseManager.getChecked())
            {
                baseManager.setChecked(true);

                ShapeArray[currentStep].placedCorrectly = false;
                ShapeArray[currentStep].baseName = DataStorage.CurrentBasePoint;
                ShapeArray[currentStep].builderName = DataStorage.CurrentBuilderPoint;
                ShapeArray[currentStep].timeGrabbed = DataStorage.LastgrabTime;
                ShapeArray[currentStep].timePlaced = DataStorage.LastbuildTime;

                SaveShapeArray();

            }

        }



    }


    private void SaveShapeArray()
    {


        string upperShapeLetter = shapeLetter.ToUpper();

        switch (upperShapeLetter)
        {
            case "A":
                DataStorage.ShapeA.buildData = ShapeArray;
                break;
            case "B":
                DataStorage.ShapeB.buildData = ShapeArray;
                break;
            case "C":
                DataStorage.ShapeC.buildData = ShapeArray;
                break;
            case "D":
                DataStorage.ShapeD.buildData = ShapeArray;
                break;
            case "E":
                DataStorage.ShapeE.buildData = ShapeArray;
                break;
            case "F":
                DataStorage.ShapeF.buildData = ShapeArray;
                break;
            case "G":
                DataStorage.ShapeG.buildData = ShapeArray;
                break;
            case "H":
                DataStorage.ShapeH.buildData = ShapeArray;
                break;

            default:
                Debug.Log("An unavailable letter was chosen in ShapeChecker.cs");
                break;
        }
    }
}
