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


    public bool is_even = false;


    private Quaternion offset_90 = Quaternion.Euler(0f, 0f, 90f);
    private Vector3 offset = new Vector3(0f, 0f, 0.076188f);
    private Vector3 pos_even_offset = new Vector3(0f, 0f, 0.03807f);
    private Vector3 neg_even_offset = new Vector3(0f, 0f, -0.03807f);
    private Vector3 current_even_offset;


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
        bool is_negative = distance_from_center < 0;
        int offset_distance_center = 0;

        if (is_even)
        {
            if (!is_negative)
            {
                current_even_offset = pos_even_offset;
                offset_distance_center = -1;
                
            }
            else
            {
                current_even_offset = neg_even_offset;
                offset_distance_center = 1;
                
            }
        }
        else
        {
            current_even_offset = new Vector3(0f, 0f, 0f);
        }


        if (other.tag == tagISnapTo)
        {

            if (!placed)
            {

                if (is_even)
                {
                    preview_clone = Instantiate(bar_preview, other.transform.position + (offset * (distance_from_center + offset_distance_center)) + current_even_offset, other.transform.rotation * offset_90);
                }
                else
                {
                    preview_clone = Instantiate(bar_preview, other.transform.position + (offset * distance_from_center), other.transform.rotation * offset_90);

                }
                
                
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
