using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisInstructions : MonoBehaviour
{
    public GameObject[] instructionBars;
    public Material builtMat;
    public int currentStep = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void nextStep()
    {
        instructionBars[currentStep].SetActive(false);
        if (currentStep+1 < instructionBars.Length)
        {
            currentStep++;
            instructionBars[currentStep].SetActive(true);
            // need to add a check for the last step
        }
    }
}
