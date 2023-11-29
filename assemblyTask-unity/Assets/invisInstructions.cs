using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using System.Security;
using TreeEditor;

public class invisInstructions : MonoBehaviour
{
    ExperimentLog manager;
    GameObject managerObj;
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
    string tempText;
    public bool instructionsAreSeperated = false;
    public GameObject cross;
    // Start is called before the first frame update
    void Start()
    {
        if (manager == null) managerObj = GameObject.FindWithTag("Manager");
        if (stepPanel == null) stepPanel = GameObject.FindWithTag("InstructionPanel");
        instructionPanel = stepPanel.GetComponent<TMP_Text>();
        manager = managerObj.GetComponent<ExperimentLog>();

        if (instructionsAreSeperated)
        {

            stepPanel.transform.localPosition = new Vector3(-1.557f, 0.9f, 0.355f);
            stepPanel.transform.rotation = Quaternion.Euler(13, -90, 0); // Add this line
        }

        toggleHands(false);

        if (managerObj.GetComponent<SceneDirector>().trialNumber == 6 || managerObj.GetComponent<SceneDirector>().trialNumber == 8)
        {
            instructionPanel.text = "Please perform Step 1";
        }
        else
        {
            instructionPanel.text = instructionTexts[currentStep];

        }
        stepPanel.SetActive(false);

        tempText = instructionTexts[currentStep];
        if (!stepByStep || managerObj.GetComponent<SceneDirector>().trialNumber == 8)
        {
            foreach (GameObject bar in instructionBars)
            {
                bar.SetActive(true);
            }
        }

        if (managerObj.GetComponent<SceneDirector>().trialNumber == 8)
        {
            DisableMeshRenderersRecursive(builtShape.transform); // hides shape to be built in transfer trial
        }


        StartCoroutine(wait(1));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DisableMeshRenderersRecursive(Transform parent)
    {
        foreach (Transform child in parent)
        {
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            // Recursively disable mesh renderers of children's children
            DisableMeshRenderersRecursive(child);
        }
    }

    public void dataLog(string category, string action)
    {
        manager.AddData(category, action, currentStep.ToString());
    }

    public void nextStep()
    {
        Debug.Log("Next Step");
        if (stepByStep)
        {
            instructionBars[currentStep].SetActive(false);
            if (currentStep + 1 < instructionBars.Length)
            {
                if (previewBars[currentStep] != null) previewBars[currentStep].SetActive(true);
                //dataLog("Step", "completed");
                currentStep++;
                instructionBars[currentStep].SetActive(true);
                setText();

                // need to add a check for the last step
            }
            else
            {
                instructionPanel.text = "You have completed the instructions!";
                dataLog("Trial", "complete");
                toggleHands(false);
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

        GameObject cross = GameObject.FindWithTag("cross");
        if (cross != null)
        {
            Destroy(cross);
        }
    }
    void setText()
    {
        tempText = instructionTexts[currentStep];
        Debug.Log(tempText + currentStep);
        if (manager.GetComponent<SceneDirector>().trialNumber + currentStep >= 6)
        {
            int tempStep = currentStep + 1;
            instructionPanel.text = "Please perform Step " + tempStep;
            // this is the part where the scaffold is removed, it still needs to be made adaptive
        }
        else
            SetCurrentStepText();
    }
    public void SetCurrentStepText()
    {
        instructionPanel.text = instructionTexts[currentStep];
    }
    public void SetTempText()
    {
        if (isAdaptive)
            instructionPanel.text = tempText;
    }
    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        dataLog("Trial", "loaded");
    }
}
