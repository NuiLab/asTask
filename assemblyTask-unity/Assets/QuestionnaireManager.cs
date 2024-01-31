using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRQuestionnaireToolkit;

public class QuestionnaireManager : MonoBehaviour
{
    public GameObject questionnaire;
    public bool firstLaunch = true;
    GameObject instructionPanel;
    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) // do Not spawn questionnaire  in the first waitingroom, since no shape has been completed
    {
        if (SceneManager.GetActiveScene().name == "WaitingRoom" && !questionnaire && !firstLaunch)
        {
            instructionPanel = GameObject.FindWithTag("InstructionPanel");
            instructionPanel.SetActive(false);
            questionnaire = GameObject.FindWithTag("VRQuestionnaireToolkit");
            questionnaire.GetComponent<StudySetup>().ParticipantId = this.gameObject.GetComponent<ExperimentLog>().participantNumber.ToString();
            questionnaire.GetComponent<StudySetup>().Condition = this.gameObject.GetComponent<SceneDirector>().tempSceneName;
            questionnaire.GetComponent<GenerateQuestionnaire>().enabled = true;
        }
        else if (SceneManager.GetActiveScene().name == "WaitingRoom" && firstLaunch)
        {
            firstLaunch = false;
            
        }

        Debug.Log("QuestionnaireManager: OnSceneLoaded");
    }

}
