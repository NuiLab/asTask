using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class SceneDirector : MonoBehaviour
{
    private static SceneDirector instance;
    // Input Devices to check for grabbing
    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();

    private int sceneBars;
    static Scene tempScene;
    public string tempSceneName;
    public int trialNumber = 1;
    private void Awake()
    {

    }

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("WA_A2_AT");
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("WB_A2_AT");
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("WC_A2_AT");
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
        {
            SceneManager.LoadScene("WD_A2_AT");
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("WE_A2_AT");
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("WF_A2_AT");
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("WG_A2_AT");
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene("WH_A2_AT");
        }

    }




    public void ClearPreviews()
    {
        GameObject[] previews = GameObject.FindGameObjectsWithTag("Preview");
        foreach (GameObject preview in previews)
        {
            Destroy(preview);
        }

    }


    public int[] DetermineTrack()
    {


        int[][] possibleTracks = new int[][]
        {
            new int[] { 0, 1, 2, 3 },
            new int[] { 1, 3, 0, 2 },
            new int[] { 3, 2, 1, 0 },
            new int[] { 2, 0, 3, 1 }
        };

        int trackIndex = DataStorage.ParticipantID % 4;

        return possibleTracks[trackIndex];
    }


    public void LogInstructionTimes()
    {
        DataStorage.LastInstructionSceneStartTime = DataStorage.MostRecentSceneStartTime;
        DataStorage.LastInstructionSceneEndTime = System.DateTime.Now.ToString();
        DataStorage.LastInstructionSceneElapsedTime = Time.timeSinceLevelLoad.ToString();

    }


    public void LogExperimentStartTime()
    {
        DataStorage.ExperimentStartTime = System.DateTime.Now;
    }

    public void LogExperimentEndTime()
    {
        this.GetComponent<ExperimentLog>().AddData("Trial", "ended");
    }


    public void ResetTrackStep()
    {
        DataStorage.CurrentTrackStep = 0;
    }


    public void OpenParticipantIDScene()
    {
        SceneManager.LoadScene("ParticipantIDScene");
    }

    public void OpenExperimentVersion()
    {
        SceneManager.LoadScene("ExperimentVersion");


    }
    public void OpenTutorialBuildScene()
    {
        SceneManager.LoadScene("Tutorial (Build)");

    }
    public void OpenTutorialVideoScene()
    {
        SceneManager.LoadScene("Tutorial Video");

    }


    public void LoadNextTrialScene()
    {
        trialNumber++;
        tempScene = SceneManager.GetActiveScene();
        tempSceneName = tempScene.name;
        if (trialNumber == 9)
        {
            SceneManager.LoadScene("WaitingRoom");
            trialNumber = 1;

        }
        else if (trialNumber == 7)
        {
            Debug.Log("Loading Waiting Room");
            Debug.Log(tempScene.name);
            SceneManager.LoadSceneAsync("WaitingRoomTrial");
        }
        else
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
    public void LoadSceneByName(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void LoadTempScene()
    {
        Debug.Log("Loading Temp Scene");
        Debug.Log(tempSceneName);
        trialNumber++;
        SceneManager.LoadScene(tempSceneName);
    }
    public void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Application.Quit();
    }



}
