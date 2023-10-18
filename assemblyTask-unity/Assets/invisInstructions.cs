using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using System.Security;

public class invisInstructions : MonoBehaviour
{
    ExperimentLog manager;
    public GameObject[] instructionBars;
    public string[] instructionTexts;
    public GameObject[] previewBars;
    public int currentStep = 0;
    public int mistakes;
    public bool stepByStep;
    public TMP_Text instructionPanel;
    public GameObject stepPanel;
    public GameObject builtShape;
    public GameObject[] hands;
    public bool isAdaptive;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        if (manager == null) manager = GameObject.FindWithTag("Manager").GetComponent<ExperimentLog>();
        Debug.Log(manager);

        instructionPanel.text = instructionTexts[currentStep];
        if (!stepByStep)
        {
            foreach (GameObject bar in instructionBars)
            {
                bar.SetActive(true);
            }
        }
        //dataLog("Experiment", "started");
        StartCoroutine(wait(1));
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void dataLog(string category, string action)
    {
        manager.AddData(category, action, currentStep.ToString());
    }

    public void nextStep()
    {
        if (stepByStep)
        {
            instructionBars[currentStep].SetActive(false);
            if (currentStep + 1 < instructionBars.Length)
            {
                if (previewBars[currentStep] != null) previewBars[currentStep].SetActive(true);
                dataLog("Step", "completed");
                currentStep++;
                instructionBars[currentStep].SetActive(true);
                setText();

                // need to add a check for the last step
            }
            else
            {
                instructionPanel.text = "You have completed the instructions!";
                button.SetActive(true);
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

        }
    }
    void setText()
    {
        if (manager.GetComponent<SceneDirector>().trialNumber + currentStep >= 7 && isAdaptive)
        {
            instructionPanel.text = "Step " + currentStep;
            // this is the adaptive part, depending on the trial number, it removes the scaffold
        }
        else
            instructionPanel.text = instructionTexts[currentStep];
    }
    public void SetCurrentStepText()
    {
        instructionPanel.text = instructionTexts[currentStep];
    }
    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        dataLog("Experiment", "started");
    }
}
