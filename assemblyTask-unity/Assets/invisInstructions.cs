using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class invisInstructions : MonoBehaviour
{
    public GameObject[] instructionBars;
    public string[] instructionTexts;
    public Material builtMat;
    public int currentStep = 0;
    public int mistakes;
    public bool stepByStep;
    public TMP_Text instructionPanel;
    public GameObject builtShape;
    // Start is called before the first frame update
    void Start()
    {
        instructionPanel.text = instructionTexts[currentStep];
        if (!stepByStep)
        {
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
                instructionPanel.text = instructionTexts[currentStep];
                // need to add a check for the last step
            }
        }
    }
    public void showBuiltShape()
    {
        builtShape.SetActive(true);
        // muss hier noch alles andere ausschalten um sicherzustellen dass quasi pausiert ist
    }
}