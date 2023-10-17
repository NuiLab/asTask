using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingTime : MonoBehaviour
{
    GameObject managerObject;
    ExperimentLog manager;
    // Start is called before the first frame update
    void Start()
    {
        if (manager == null) manager = GameObject.FindWithTag("Manager").GetComponent<ExperimentLog>();
        if (managerObject == null) managerObject = GameObject.FindWithTag("Manager");
        StartCoroutine(LoadSceneAfterDelay(90f));
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        managerObject.GetComponent<SceneDirector>().LoadNextTrialScene();
    }
}
