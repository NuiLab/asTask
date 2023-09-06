using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisInstructions : MonoBehaviour
{
    public GameObject[] instructionBars;
    public Material builtMat;
    public int currentStep = 0;
    public int mistakes;
    public bool stepByStep;
    // Start is called before the first frame update
    void Start()
    {
        if(!stepByStep){
            foreach (GameObject bar in instructionBars)
            {
                bar.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void nextStep()
    {
        if (stepByStep)
        {
            instructionBars[currentStep].SetActive(false);
            if (currentStep + 1 < instructionBars.Length)
            {
                currentStep++;
                instructionBars[currentStep].SetActive(true);
                // need to add a check for the last step
            }
        }
    }
}
