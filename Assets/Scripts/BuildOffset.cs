using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

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



    // input devices
    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();

    // Buttons
    private bool a_pressed = false;
    private bool x_pressed = false;



    
    void Update()
    {


        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);


        a_pressed = false;
        x_pressed = false;

        if (rightHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out a_pressed) && a_pressed)
        {

            //Debug0.text = "right: " + a_pressed.ToString();

        }
        if (leftHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out x_pressed) && x_pressed)
        {

            //Debug1.text = "left: " + x_pressed.ToString();

        }
        else
        {
            Debug.Log("No Devices found");
        }

        Debug0.text = "right: " + a_pressed.ToString();
        Debug1.text = "left: " + x_pressed.ToString();


    }


    private void OnTriggerStay(Collider other)
    { 

        buildChecker snapPoint = other.gameObject.GetComponent<buildChecker>();

        bool already_built = snapPoint.getBuilt();
        
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

                // preview bar
                if (is_even)
                {
                    preview_clone = Instantiate(bar_preview, other.transform.position + (offset * (distance_from_center + offset_distance_center)) + current_even_offset, other.transform.rotation * offset_90);
                }
                else
                {
                    preview_clone = Instantiate(bar_preview, other.transform.position + (offset * distance_from_center), other.transform.rotation * offset_90);

                }

                //Debug1.text =  " != initial a: " + initial_a.ToString();

                Debug2.text = "already built: " + (!already_built).ToString();



                // Build bar
                if (a_pressed && !already_built)
                {
                    Instantiate(bar, preview_clone.transform.position, preview_clone.transform.rotation);
                    snapPoint.setBuilt(true);
                    already_built = true;
                    Destroy(preview_clone);
                    //Destroy(this.transform.parent.gameObject);
                    Destroy(transform.root.gameObject);

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
