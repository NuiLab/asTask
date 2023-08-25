using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructionsManager : MonoBehaviour
{
    public GameObject[] builders;
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
    void getBuilders()
    {
        builders = GameObject.FindGameObjectsWithTag("Builder");
        List<GameObject> filteredBuilders = new List<GameObject>();
        string lengthFilter = instructions[step][0].ToString();

        for (int i = 0; i < builders.Length; i++)
        {
            if (builders[i].GetComponent<BuildOffset>().barLength == lengthFilter)
            {
                filteredBuilders.Add(builders[i]);
            }
        }
        builders = filteredBuilders.ToArray();
    }
    
    public void parseInstructions()
    {
        for (int i = 0; i < builders.Length; i++)
        {
            // format:Whichbarlength, TargetBarlength, TargetHole, TargetColor 
            // Colors: 0 = red, 1 = blue, 2 = green, 3 = yellow
            builders[i].GetComponent<BuildOffset>().targetBarLength = instructions[step][1].ToString();
            builders[i].GetComponent<BuildOffset>().targetHoleNumber = instructions[step][2].ToString();
            builders[i].GetComponent<BuildOffset>().targetBarColor = instructions[step][3].ToString();
        }
        step++;

    }

}
