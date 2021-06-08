using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneDirector : MonoBehaviour
{


    //public Text RemainingBars;
    //public Text totalBarsDebug;

    private int sceneBars;

    private void Start()
    {
        GameObject[] bars = GameObject.FindGameObjectsWithTag("Builder");

        sceneBars = bars.Length;

        //totalBarsDebug.text = sceneBars.ToString();

    }


    private void Update()
    {

        GameObject[] bars = GameObject.FindGameObjectsWithTag("Builder");

        //RemainingBars.text = "Bars Remaining: " + bars.Length;


        if (bars.Length < sceneBars)
        {
            ClearPreviews();
            sceneBars -= 1;
            //totalBarsDebug.text = sceneBars.ToString();
        }


    }

    public void ClearPreviews()
    {
        GameObject[] previews = GameObject.FindGameObjectsWithTag("Preview");
        foreach (GameObject preview in previews)
        {
            Destroy(preview);
        }

    }

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

    public void OpenTestArea()
    {
        SceneManager.LoadScene(sceneBuildIndex: 3);
    }

    public void OpenShapeAFinalProduct()
    {
        SceneManager.LoadScene(sceneBuildIndex: 4);

    }


    public void OpenShapeAStepbyStep()
    {
        SceneManager.LoadScene(sceneBuildIndex: 5);

    }

    public void OpenShapeAFullyGuided()
    {
        SceneManager.LoadScene(sceneBuildIndex: 6);

    }



}
