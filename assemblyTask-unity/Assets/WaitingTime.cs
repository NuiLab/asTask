using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WaitingTime : MonoBehaviour
{
    GameObject managerObject;
    ExperimentLog manager;
    public float waitingTime = 90f;
    public TextMeshPro progress;
    // Start is called before the first frame update
    void Start()
    {
        
        //if (manager == null) manager = GameObject.FindWithTag("Manager").GetComponent<ExperimentLog>();
        if (managerObject == null) managerObject = GameObject.FindWithTag("Manager");
        StartCoroutine(LoadSceneAfterDelay());
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
            progress.text= waitingTime.ToString() + " s";
            yield return new WaitForSeconds(1f);
            waitingTime--;
        }
    }
    public void LoadNextScene()
    {
        managerObject.GetComponent<SceneDirector>().LoadTempScene();
    }
}
