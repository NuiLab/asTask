using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class invisInstructions : MonoBehaviour
{
    public GameObject[] instructionBars;
    public string[] instructionTexts;
    public Material builtMat;
    public int currentStep = 0;
    public int mistakes;
    public bool stepByStep;
    public TMP_Text instructionPanel;
    public GameObject stepPanel;
    public GameObject builtShape;
    public GameObject[] hands;
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
            else
            {
                instructionPanel.text = "You have completed the instructions!";
            }
        }
    }
    public void showBuiltShape()
    {
        builtShape.SetActive(true);
        // muss hier noch alles andere ausschalten um sicherzustellen dass quasi pausiert ist
    }
    public void toggleHands(bool temp)
    {
        foreach (GameObject hand in hands)
        {
            hand.GetComponent<XRDirectInteractor>().enabled = temp;
            hand.GetComponent<XRDirectInteractor>().attachTransform.gameObject.transform.parent = null;
        }
    }
}
