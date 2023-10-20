using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindSM : MonoBehaviour
{
    public SceneDirector sceneD;
    // Start is called before the first frame update
    void Start()
    {
        sceneD = GameObject.FindWithTag("Manager").GetComponent<SceneDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nextScene()
    {
        sceneD.LoadNextTrialScene();
    }
}
