using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    // Start is called before the first frame update
    public SceneDirector sceneD;
    // Start is called before the first frame update
    void Start()
    {
        sceneD = GameObject.FindWithTag("Manager").GetComponent<SceneDirector>();
        sceneD.experimentType = SceneDirector.ExperimentType.ExpB;
       // sceneD.GetNumbersFromCSV(false, 123); // sets the testing schedule for the demo
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
