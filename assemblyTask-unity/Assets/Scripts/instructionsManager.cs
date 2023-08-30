using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This class is used to manage the instructions for the game and store some global variables
public class instructionsManager : MonoBehaviour
{
    public GameObject[] holes;
    public string[] instructions;
    public int step = 0;
    // Start is called before the first frame update
    void Start()
    {
        getBuilders();
        parseInstructions();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Ideally this function will spit out a list of all holes for the current step
    void getBuilders()
    {
        holes = GameObject.FindGameObjectsWithTag("Hole");
        List<GameObject> filteredHoles = new List<GameObject>();
        for (int i = 0; i < holes.Length; i++)
        {
            if (holes[i].GetComponent<hProps>().barLength == instructions[step][0].ToString())
            {
                if (holes[i].GetComponent<hProps>().holeNumber == instructions[step][1].ToString())
                {
                    filteredHoles.Add(holes[i]);
                    holes[i].GetComponent<hProps>().instructionsManager = this; // hole number
                }

            }
        }
        holes = filteredHoles.ToArray();
    }

    public void parseInstructions()
    {
        for (int i = 0; i < holes.Length; i++)
        {
            // format:Whichbarlength,WhichHole, TargetBarlength, TargetHole, TargetColor 
            // Colors: 0 = red, 1 = blue, 2 = green, 3 = yellow
            holes[i].GetComponent<hProps>().targetBarLength = instructions[step][2].ToString(); // target bar length
            holes[i].GetComponent<hProps>().targetHoleNumber = instructions[step][3].ToString(); // target hole number
            if (instructions[step][4].ToString() != "n") holes[i].GetComponent<hProps>().targetBarColor = instructions[step][4].ToString(); // target bar color if no color is specified put n
        }


    }
    public void stepComplete()
    {
        step++;
        getBuilders();
        parseInstructions();
    }

}
