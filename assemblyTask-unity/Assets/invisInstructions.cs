using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using System.Security;
using TreeEditor;
using UnityEngine.TextCore.Text;

public class invisInstructions : MonoBehaviour
{
    // This script is pretty much the bread and butter of the entire project. It handles the entire instruction part. It is used in every trial, and is used to display the instructions to the user. It also handles the step by step instructions, and the adaptive instructions. It also handles the data logging for the instructions.
    ExperimentLog log;
    GameObject managerObj;
    SceneDirector sceneDirector;
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
    TMP_FontAsset badFont;

    public List<GameObject> builtBars;

    // Start is called before the first frame update
    void Start()
    {

        if (log == null) managerObj = GameObject.FindWithTag("Manager");
        if (stepPanel == null) stepPanel = GameObject.FindWithTag("InstructionPanel");
        instructionPanel = stepPanel.GetComponent<TMP_Text>();
        if (managerObj.GetComponent<ExperimentLog>() != null) log = managerObj.GetComponent<ExperimentLog>();
        sceneDirector = managerObj.GetComponent<SceneDirector>();
        if (instructionsAreSeperated) // This causes the instructions to be set to high extraneous load. In this case it decreases font size and changes the location to be offset. Also changes font to different asset with poor contrast. This is done to make the instructions harder to read.
        {
            // Gets the two Quads from the stepPanel and sets the first one to be inactive and the second one to be active. This is done to change the background of the wordy instructions.
            Transform firstChild = stepPanel.transform.GetChild(0);
            if (firstChild != null)
            {
                firstChild.gameObject.SetActive(false);
            }

            // Get the second child of stepPanel and activate it
            Transform secondChild = stepPanel.transform.GetChild(1);
            if (secondChild != null)
            {
                secondChild.gameObject.SetActive(true);
            }
            stepPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(24f, 5f);
            stepPanel.transform.localPosition = new Vector3(-1.557f, 1.4f, 0.355f);
            stepPanel.transform.rotation = Quaternion.Euler(0, -90, 0);
            //instructionPanel.font = badFont;

        }

        toggleHands(false);

        if (sceneDirector.trialNumber == 6 || sceneDirector.trialNumber == 8)
        {
            instructionPanel.text = "Please perform Step 1";
        }
        else
        {
            instructionPanel.text = instructionTexts[currentStep];

        }
        stepPanel.SetActive(false);

        tempText = instructionTexts[currentStep];
        if (!stepByStep || sceneDirector.trialNumber == 8)
        {
            foreach (GameObject bar in instructionBars)
            {
                bar.SetActive(true);
            }
        }

        if (sceneDirector.trialNumber == 8)
        {
            DisableMeshRenderersRecursive(builtShape.transform); // hides shape to be built in transfer trial
        }

        StartCoroutine(wait(1));
    }

    // Update is called once per frame
    void Update()
    {

    }
    // This is used to disable all bars in the preview. 
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

    // these functions create data log entries
    public void dataLog(string category, string action)
    {
        log.AddData(category, action, currentStep.ToString());
    }
    public void WideDataLog()
    {
        log.AddWideData(sceneDirector.trialNumber, mistakes);
    }

    // This calls the next step in the instructions. It also handles the data logging for the instructions.
    public void nextStep()
    {
        //        Debug.Log("Next Step");
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


            }
            else
            {
                instructionPanel.text = "You have completed the instructions!";
                dataLog("Trial", "complete");
                WideDataLog();
                toggleHands(false);
                button.SetActive(true);
                StartCoroutine(disableShape());
            }
        }
    }
    public void showBuiltShape()
    {
        builtShape.SetActive(true);

    }
    //used to turn the hands off when a mistake was made or the trial is started
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
        //Debug.Log(tempText + currentStep);
        if (sceneDirector.trialNumber + currentStep >= 6)
        {
            int tempStep = currentStep + 1;
            instructionPanel.text = "Please perform Step " + tempStep;

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
        sceneDirector.resetTime();
        dataLog("Trial", "loaded");
    }
    IEnumerator disableShape()
    {
        yield return new WaitForSeconds(5f);
        foreach (GameObject bar in builtBars)
        {
            bar.SetActive(false);
        }
    }
}
