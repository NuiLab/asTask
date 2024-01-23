using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshPro progress;
    public float waitingTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(updateBar());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator updateBar()
    {
        while (waitingTime >= 0)
        {
            progress.text = waitingTime.ToString() + " s";
            yield return new WaitForSeconds(1f);
            waitingTime--;
        }
        progress.enabled = false;
    }
}
