using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public AudioClip tutorialAudioEXPA;
    public AudioClip tutorialAudioEXPB;
    public GameObject nextButton;
    bool tutoStarted = false;
   public GameObject sceneDirector;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (sceneDirector.GetComponent<SceneDirector>().experimentType == SceneDirector.ExperimentType.ExpA)
            {
                StartCoroutine(StartTutorial("A"));
            }
            if (sceneDirector.GetComponent<SceneDirector>().experimentType == SceneDirector.ExperimentType.ExpB)
            {
                StartCoroutine(StartTutorial("B"));
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            nextButton.SetActive(true);
        }
    }

    IEnumerator StartTutorial(string type)
    {

        if (!tutoStarted)
        {
            if (type == "A")
            {
                AudioSource.PlayClipAtPoint(tutorialAudioEXPA, transform.position);
                tutoStarted = true;
                yield return new WaitForSeconds(tutorialAudioEXPA.length);
                nextButton.SetActive(true);
            }
            if (type == "B")
            {
                AudioSource.PlayClipAtPoint(tutorialAudioEXPB, transform.position);
                tutoStarted = true;
                yield return new WaitForSeconds(tutorialAudioEXPB.length);
                nextButton.SetActive(true);
            }


        }

    }
}
