using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildOffset : MonoBehaviour
{



    public GameObject bar;
    public GameObject bar_preview;
    private GameObject preview_clone;


    public float distance_from_center;
    //private float offset = 0.255f * distance_from_center;



    //Might need to be changed to
    // public later
    private string tagISnapTo = "Bar_SP";

    private bool placed = false;


    private Quaternion offset_90 = Quaternion.Euler(0f, 0f, 90f);
    private Vector3 offset = new Vector3(0f, 0f, 0.076188f);


    // Debugging
    public Text Debug0;
    public Text Debug1;
    public Text Debug2;








    // later for buttons
    //void Update()
    //{
        
    //}




    private void OnTriggerStay(Collider other)
    {

        if (other.tag == tagISnapTo)
        {

            if (!placed)
            {
                preview_clone = Instantiate(bar_preview, other.transform.position + (offset * distance_from_center), other.transform.rotation * offset_90);
                placed = true;
                Debug0.text = "Placed";
            }

        }
        
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.tag == tagISnapTo)
        {
            Destroy(preview_clone);

            placed = false;

            Debug0.text = "Not Placed";

        }

    }
}
