using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShapeCheckerScript : MonoBehaviour
{



    [SerializeField]
    private GameObject InvisibleShape;



    [SerializeField]
    private Text[] textDebuggers;


    private bool alreadydone = false;


    private bool[] placedPieces;







    // Start is called before the first frame update
    void Start()
    {
        placedPieces = new bool[InvisibleShape.transform.childCount];


    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < textDebuggers.Length; i++)
        {
            textDebuggers[i].text = placedPieces[i].ToString();
        }




        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");

        foreach (Transform child in InvisibleShape.transform)
        {


            foreach (GameObject bas in bases)
            {


                if (Vector3.Distance(bas.transform.position, child.transform.position) <= 0.02f && !alreadydone)
                {

                    int nameIndex = 0;

                    if (int.TryParse(child.name, out nameIndex))
                    {
                        placedPieces[nameIndex] = true;

                    }

                    

                }

            }

        }

    }
}
