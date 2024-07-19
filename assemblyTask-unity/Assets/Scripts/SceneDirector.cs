using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using VRQuestionnaireToolkit;
using System.IO;


public class SceneDirector : MonoBehaviour
{
    public TextAsset csvFile;
    static SceneDirector instance;
    // Input Devices to check for grabbing
    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();
    ExperimentLog expLog;
    private int sceneBars;
    static Scene tempScene;
    [HideInInspector] public string tempSceneName;
    public int trialNumber = 1;
    public int shapeNumber = 1;
    public bool firstWait = true;
    [SerializeField]
    public int[] schedule;
    public int stepCounter = 0;
    public int participantID;
    public ExperimentType experimentType;
    [HideInInspector] public ExperimentType initialType;
    public enum ExperimentType
    {
        ExpA,
        ExpB,
        Usability
    }
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
        expLog = instance.GetComponent<ExperimentLog>();
        participantID = expLog.participantNumber;
        initialType = experimentType;
    }

    public void resetType()
    {
        experimentType = initialType;
    }
    private void Update()
    {
        //Left Shift plus Letter loads the Adaptive Scene for that letter with NO Color and the instructions at the bench
        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                LoadSceneByName("A_A2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                LoadSceneByName("B_A2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                LoadSceneByName("C_A2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                LoadSceneByName("D_A2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                LoadSceneByName("E_A2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                LoadSceneByName("F_A2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                LoadSceneByName("G_A2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                LoadSceneByName("H_A2_AT");
            }
        }
        // Right Shift plus Letter loads the Adaptive Scene for that letter with COLOR and the instructions at the bench
        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                LoadSceneByName("A_A3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                LoadSceneByName("B_A3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                LoadSceneByName("C_A3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                LoadSceneByName("D_A3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                LoadSceneByName("E_A3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                LoadSceneByName("F_A3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                LoadSceneByName("G_A3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                LoadSceneByName("H_A3_AT");
            }
        }
        // S plus Letter loads the  Scene for that letter with  COLOR and the instructions offset
        if (Input.GetKey(KeyCode.Alpha6))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                LoadSceneByName("A_S3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                LoadSceneByName("B_S3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                LoadSceneByName("C_S3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                LoadSceneByName("D_S3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                LoadSceneByName("E_S3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                LoadSceneByName("F_S3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                LoadSceneByName("G_S3_AT");
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                LoadSceneByName("H_S3_AT");
            }
        }
        // K plus Letter loads the  Scene for that letter with NO color and the instructions offset
        if (Input.GetKey(KeyCode.Alpha4))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                LoadSceneByName("A_S2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                LoadSceneByName("B_S2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                LoadSceneByName("C_S2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                LoadSceneByName("D_S2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                LoadSceneByName("E_S2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                LoadSceneByName("F_S2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                LoadSceneByName("G_S2_AT");
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                LoadSceneByName("H_S2_AT");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            resetView();
        }
    }

    public int[] GetNumbersFromCSV(bool testing, int participantId)
    {
        if (!testing) { csvFile = Resources.Load<TextAsset>("Schedule/Yoke"); }
        else { csvFile = Resources.Load<TextAsset>("Schedule/YokeTest"); }

        string[] lines = csvFile.text.Split('\n');
        foreach (string line in lines)
        {
            string[] values = line.Split(',');
            if (values.Length > 0 && int.TryParse(values[0], out int id) && id == participantId)
            {
                int[] numbers = new int[values.Length - 1];
                for (int i = 1; i < values.Length; i++)
                {
                    if (int.TryParse(values[i], out int number))
                    {
                        numbers[i - 1] = number;
                    }
                }
                return numbers;
            }
        }
        return null; // Return null if no matching row is found
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
    void resetView()
    {
        List<InputDevice> devices = new();
        InputDevices.GetDevices(devices);
        if (devices.Count != 0)
        {
            devices[0].subsystem.TryRecenter();
        }

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

    public void resetTime()
    {
        expLog.time_s = 0;
    }
    public bool RepeatCheck()
    {
        if (experimentType != ExperimentType.ExpA)
        {
            if (schedule[stepCounter] == 0)
            {
                Debug.Log("Does not repeat." + schedule[stepCounter]);
                return false;
            }
            else
            {
                Debug.Log("Repeats " + schedule[stepCounter] + " times.");
                schedule[stepCounter]--;
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    public void LoadNextTrialScene()
    {
        expLog.time_s = 0;
        trialNumber++;
        tempScene = SceneManager.GetActiveScene();
        tempSceneName = tempScene.name;
        if (trialNumber == 9)
        {
            shapeNumber++;
            //.Condition = tempSceneName;
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
        expLog.time_s = 0;
        SceneManager.LoadScene(scenename);
    }

    public void LoadTempScene()
    {
        expLog.time_s = 0;
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
    public string getCurrentShape()
    {
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        string[] splitSceneName = sceneName.Split('_');
        return splitSceneName[0];
    }


}
