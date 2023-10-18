using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapes : MonoBehaviour
{
    public GameObject[] previewBars;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void hide()
    {
        previewBars[4].SetActive(false);
        previewBars[1].SetActive(false);
        previewBars[2].SetActive(false);
        previewBars[3].SetActive(false);
    }
}
