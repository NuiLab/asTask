using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class ExperimentLog : MonoBehaviour
{
    public static ExperimentLog instance;
    public string filePath;
    public string filePathW;
    public int participantNumber = 0;
    StreamWriter writer;
    StreamWriter writerW;
    SceneDirector manager;
    public float time_s = 0;
    float tempTime = 0f;
    int counter = 1;
    public bool testing = true;
    public GameObject DebugLog;
    public bool toggleLog = false;

    // Start is called before the first frame update
    void Start()
    {

        // Ensure that only one instance of ExperimentLog exists.
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            //Debug.Log("Destroyed"+SceneManager.GetActiveScene().name);
        }
        else
        {
            instance = this;
        }

        manager = this.gameObject.GetComponent<SceneDirector>();
        DebugLog = GameObject.FindWithTag("DebugWindow");
        var rnd = new System.Random();

#if UNITY_EDITOR
        filePath = Application.dataPath + "/Records";
#else
        filePath = Application.persistentDataPath + "/Records";

#endif
        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        // Make this GameObject persistent across scene loads.
        if (instance == this) DontDestroyOnLoad(transform.gameObject);
        // activate this for testing
        if (SceneManager.GetActiveScene().name != "Tutorial Video" && instance == this && testing)
        {
            SetParticipantNumber(rnd.Next(1, 60));
            if (DebugLog && toggleLog)
                DebugLog.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        time_s += Time.deltaTime;

    }
    // This method sets the participant number and initializes the log files.
    public void SetParticipantNumber(int pNum)
    {
        participantNumber = pNum;
        string temp = filePath;

        if (manager.experimentType == SceneDirector.ExperimentType.ExpB)
        {
            if (testing)
            {
                manager.schedule = manager.GetNumbersFromCSV(true, participantNumber);
            }
            else
            {
                manager.schedule = manager.GetNumbersFromCSV(false, participantNumber);
            }
        }
        if (manager.experimentType == SceneDirector.ExperimentType.Usability)
        {
            manager.schedule = manager.GetNumbersFromCSV(false, 111);

        }


        manager.participantID = participantNumber;
        Debug.Log(manager.schedule[0]);
        filePath = filePath + "/Participant" + participantNumber.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmssf") + ".csv";
        filePathW = temp + "/WideParticipant" + participantNumber.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".csv";
        using (writer = File.CreateText(filePath))
        {
            writer.WriteLine("Participant_Number;Shape;Condition;Adaptivity;Trial;Timestamp;Time_in_trial;Category;Action;Errortype;Expected;Actual;Step;TimeSinceLastEvent");
        }
        using (writerW = File.CreateText(filePathW))
        {
            //Debug.Log("Creating Wide File");
            writerW.WriteLine("");
            writerW.WriteLine("");
        }

    }
    // This method adds a new line to the log file.
    public void AddData(string category = "n/a", string action = "n/a", string step = "n/a", string errorType = "n/a", string expected = "n/a", string actual = "n/a")
    {
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        string[] splitSceneName = sceneName.Split('_');

        float miliS = time_s * 1000;
        int seconds = ((int)time_s % 60);
        int minutes = ((int)time_s / 60);
        string timeString = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliS);
        float timeSinceLastEvent = miliS - tempTime;

        string newLine = participantNumber.ToString();
        if (splitSceneName.Length == 3)
        {
            newLine += ";" + splitSceneName[0];
            newLine += ";" + splitSceneName[1];
            newLine += ";" + splitSceneName[2];
        }
        else
        {
            newLine += ";" + sceneName;
            newLine += ";" + "n/a";
            newLine += ";" + "n/a";
        }
        newLine += ";" + manager.trialNumber.ToString();
        newLine += ";" + DateTime.Now.ToString("HH:mm.ss");
        newLine += ";" + Mathf.Round(miliS).ToString();
        newLine += ";" + category;
        newLine += ";" + action;
        newLine += ";" + errorType;
        newLine += ";" + expected;
        newLine += ";" + actual;
        newLine += ";" + step;
        newLine += ";" + Mathf.Round(timeSinceLastEvent).ToString();
        tempTime = miliS;
        using (writer = File.AppendText(filePath))
        {
            writer.WriteLine(newLine);
        }

    }
    // This method adds a new line to the wide log file. Francisco wanted this as a sort of summary of the experiment data.
    public void AddWideData(int trialNumber, int mistakesMade)
    {
        //        Debug.Log("Adding Wide Data");
        //Participant_Number;Shape;Condition;Adaptivity;PositionInExp;Trial;TotalTime;MistakesMade";
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        string[] splitSceneName = sceneName.Split('_');
        string newLine = "";
        float miliSW = time_s * 1000;
        if (counter == 1)
        {
            newLine = participantNumber.ToString() + ";";
        }

        if (splitSceneName.Length == 3)
        {
            newLine += splitSceneName[0];
            newLine += ";" + splitSceneName[1];
            newLine += ";" + splitSceneName[2];
        }
        else
        {
            newLine += sceneName;
            newLine += ";" + "n/a";
            newLine += ";" + "n/a";
        }
        newLine += ";" + manager.shapeNumber.ToString();
        newLine += ";" + trialNumber.ToString();
        newLine += ";" + Mathf.Round(miliSW).ToString();
        newLine += ";" + mistakesMade.ToString() + ";";

        string[] lines = File.ReadAllLines(filePathW);

        if (counter == 1)
        {
            lines[0] += "Participant_Number;Shape" + counter + ";Condition" + counter + ";Adaptivity" + counter + ";PositionInExp" + counter + ";Trial" + counter + ";TotalTime" + counter + ";MistakesMade" + counter + ";";
            Debug.Log("Added Lines");
        }
        else
        {
            lines[0] += "Shape" + counter + ";Condition" + counter + ";Adaptivity" + counter + ";PositionInExp" + counter + ";Trial" + counter + ";TotalTime" + counter + ";MistakesMade" + counter + ";";
            Debug.Log("Added Lines 2");
        }
        lines[^1] += newLine;
        counter++;
        File.WriteAllLines(filePathW, lines);

    }

}
