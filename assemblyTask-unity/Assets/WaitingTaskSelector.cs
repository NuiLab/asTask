using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitingTaskSelector : MonoBehaviour
{
    public enum TaskType
    {
        Normal,
        BCI
    }
    public TextMeshPro instructionText;
    // Create a variable of type TaskType.
    public TaskType task;
    public GameObject[] normalObjects;
    public GameObject[] bciObjects;
    GameObject managerObject;
    ExperimentLog manager;
    public float waitingTime = 90f;
    public TextMeshPro progress;
    // Start is called before the first frame update
    void Start()
    {
        if (managerObject == null) managerObject = GameObject.FindWithTag("Manager");

        if (task == TaskType.Normal) //Take normal instructions and start 90s timer for bouncing ball
        {
            instructionText.text = "Please wait for 90 seconds. You will then need to build the shape again with no instructions. Please count how many times the ball bounces and tell the experimenter once it stops.";
            StartCoroutine(LoadSceneAfterDelay());
        }
        else
        {
            instructionText.text = "Please take apart this shape before you and lay the pieces out on the table.";
        }
        foreach (GameObject obj in normalObjects) // This is used to toggle the objects that are used in the normal task.
        {
            obj.SetActive(task == TaskType.Normal);
        }
        foreach (GameObject obj in bciObjects) // This is used to toggle the objects that are used in the BCI task.
        {
            obj.SetActive(task == TaskType.BCI);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator LoadSceneAfterDelay()
    {
        StartCoroutine(updateBar());
        yield return new WaitForSeconds(waitingTime);
        Debug.Log(managerObject.GetComponent<SceneDirector>().tempSceneName);
        managerObject.GetComponent<SceneDirector>().LoadTempScene();
    }
    IEnumerator updateBar()
    {
        while (true)
        {
            progress.text = waitingTime.ToString() + " s";
            yield return new WaitForSeconds(1f);
            waitingTime--;
        }
    }
}
