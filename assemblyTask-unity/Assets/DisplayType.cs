using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DisplayType : MonoBehaviour
{
    public SceneDirector sceneDirector;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            sceneDirector.experimentType = SceneDirector.ExperimentType.ExpA;
        }
         if (Input.GetKeyDown(KeyCode.B))
        {
            sceneDirector.experimentType = SceneDirector.ExperimentType.ExpB;
        }
        String type = sceneDirector.experimentType.ToString();
        this.gameObject.GetComponent<TextMeshPro>().text = type;

    }
}
