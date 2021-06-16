using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeAnimatorScript : MonoBehaviour
{
    [SerializeField]
    private bool startAnimation = false;


    [SerializeField]
    private Transform StartPoint;

    [SerializeField]
    private GameObject[] bars = new GameObject[6];

    [SerializeField]
    private int[] steps;





    [SerializeField]
    [Range(0f, 1f)]
    private float speed = 0.5f;

    [SerializeField]
    private int pauseTime = 10;


    private float lerpPercent;


    private bool barSpawned = false;
    private int currentIndex = 0;
    private GameObject currentBar;
    private GameObject targetBar;
    private bool pauseComplete = false;





    void Update()
    {

        if (startAnimation)
        {
            if (!barSpawned)
            {
                barSpawned = true;
                currentBar = Instantiate(bars[barval(steps[currentIndex])], StartPoint.position, StartPoint.rotation);
                targetBar = GameObject.Find(currentIndex.ToString());
            }

            if (!pauseComplete)
                if (lerpPercent >= pauseTime)
                {
                    lerpPercent = 0;
                    pauseComplete = true;
                }



            if (barSpawned && currentBar.transform.position != targetBar.transform.position && pauseComplete)
            {
                currentBar.transform.position = Vector3.Lerp(StartPoint.transform.position, targetBar.transform.position, (lerpPercent * speed));
                currentBar.transform.rotation = Quaternion.Lerp(StartPoint.transform.rotation, targetBar.transform.rotation, (lerpPercent * speed));

            }

            if (barSpawned && currentBar.transform.position == targetBar.transform.position && currentIndex <= steps.Length - 1 && pauseComplete)
            {
                pauseComplete = false;
                currentIndex++;
                barSpawned = false;
                lerpPercent = 0;

            }
        }






    }

    private void FixedUpdate()
    {
        if (startAnimation)
        {
            lerpPercent += Time.deltaTime;
        }

    }


    public void StartAnimation()
    {
        startAnimation = true;

    }




    int barval(int val)
    {
        return val - 3;
    }
}
