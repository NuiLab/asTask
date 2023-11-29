using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindSM : MonoBehaviour
{
    public SceneDirector sceneD;
    public invisInstructions instrManager;
    // Start is called before the first frame update
    void Start()
    {
        sceneD = GameObject.FindWithTag("Manager").GetComponent<SceneDirector>();
        if (GameObject.FindWithTag("SceneInstructions").GetComponent<invisInstructions>() != null)
            instrManager = GameObject.FindWithTag("SceneInstructions").GetComponent<invisInstructions>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void nextScene()
    {
        sceneD.LoadNextTrialScene();
    }
    public void LoadSceneByName(string scenename)
    {
        sceneD.LoadSceneByName(scenename);
    }
    public void activateHands()
    {
        instrManager.toggleHands(true);
    }
    public void StartTrial()
    {
        instrManager.dataLog("Trial", "started");
    }
    public void ContinueTrial()
    {
        instrManager.dataLog("Trial", "continued");
    }

}