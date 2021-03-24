using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{

    public void OpenMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);

    }
     
    public void OpenShapeA()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);

    }

    public void OpenShapeC()
    {
        SceneManager.LoadScene(sceneBuildIndex: 2);

    }

}
