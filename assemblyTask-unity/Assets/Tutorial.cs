using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public AudioClip tutorialAudio;
    public GameObject nextButton;
    bool tutoStarted = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartTutorial());
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            nextButton.SetActive(true);
        }
    }

    IEnumerator StartTutorial()
    {

        if (!tutoStarted)
        {
            tutoStarted = true;
            AudioSource.PlayClipAtPoint(tutorialAudio, transform.position);
            yield return new WaitForSeconds(tutorialAudio.length);
            nextButton.SetActive(true);
            
        }

    }
}
