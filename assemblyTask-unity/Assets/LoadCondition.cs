using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCondition : MonoBehaviour
{
    public string[] conditions;

    public int participantId;
    SceneDirector sceneDirector;
    // Start is called before the first frame update
    void Start()
    {
        sceneDirector = this.GetComponent<FindSM>().sceneD;
        participantId = sceneDirector.participantID = participantId;
        conditions = GetConditionFromCSV(false, participantId);
    }
    public string[] GetConditionFromCSV(bool testing, int participantId)
    {
        // Load the appropriate CSV file based on the testing flag
        TextAsset csvFile = testing ? Resources.Load<TextAsset>("Schedule/YokeTest") : Resources.Load<TextAsset>("Schedule/Yoke");

        // Split the CSV file into lines
        string[] lines = csvFile.text.Split('\n');

        // Iterate through each line
        foreach (string line in lines)
        {
            // Split the line into conditions
            string[] conditions = line.Split(',');

            // Check if the first value matches the participantId
            if (conditions.Length > 0 && int.TryParse(conditions[0], out int id) && id == participantId)
            {
                // Return the entire row as a string array
                return conditions;
            }
        }

        // Return null if no matching row is found
        return null;
    }

    void LoadNextCondition()
    {
        sceneDirector.LoadSceneByName(conditions[sceneDirector.shapeNumber]);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            sceneDirector.LoadSceneByName(conditions[sceneDirector.shapeNumber]);
        }
    }
}
