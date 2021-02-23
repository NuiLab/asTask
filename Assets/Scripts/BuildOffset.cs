using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

using UnityEngine.XR.Interaction.Toolkit;




public class BuildOffset : MonoBehaviour
{



    public GameObject bar;
    public GameObject bar_preview;
    private GameObject preview_clone;


    public float distance_from_center;
    //private float offset = 0.255f * distance_from_center;



    // Might need to be changed to
    // public later
    private string tagISnapTo = "Bar_SP";

    private bool placed = false;


    public bool is_even = false;


    private Quaternion offset_90 = Quaternion.Euler(0f, 0f, 90f);
    private Vector3 offset = new Vector3(0f, 0f, 0.076188f);
    private Vector3 pos_even_offset = new Vector3(0f, 0f, 0.03807f);
    private Vector3 neg_even_offset = new Vector3(0f, 0f, -0.03807f);
    private Vector3 current_even_offset;

    buildChecker snapPoint;


    // Debugging
    public Text Debug0;
    public Text Debug1;
    public Text Debug2;



    // input devices
    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();
    XRGrabInteractable bar_being_held;
    XRBaseInteractor hand_holding_bar;

    // state
    private bool ready_to_build = false;
    



    // Release hand stuff
    XRGrabInteractable current_interactable;








    void Update()
    {

        


        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);

        if (GetComponent<XRGrabInteractable>())
        {
            bar_being_held = GetComponent<XRGrabInteractable>();
            hand_holding_bar = bar_being_held.selectingInteractor;
        }
        

        bool a_pressed = false;
        
        bool x_pressed = false;


        
        //Debug2.text = bar_being_held.name + " being held by " + hand_holding_bar.name;







        if (rightHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out a_pressed) && a_pressed)
        {

            //Debug0.text = "right: " + a_pressed.ToString();
            if (a_pressed && ready_to_build)
            {
                BuildBar();
                
                //bar_being_held.CustomForceDrop(hand_holding_bar);

            }

        }
        if (leftHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out x_pressed) && x_pressed)
        {

            //Debug1.text = "left: " + x_pressed.ToString();

        }
        else
        {
            Debug.Log("No Devices found");
        }






        Debug0.text = "ready_to_build: " + ready_to_build;
        Debug1.text = "A pressed: " + a_pressed;




        //Debug0.text = current_interactable.ToString();







    }


    private void OnTriggerStay(Collider other)
    {
        snapPoint = other.gameObject.GetComponent<buildChecker>();
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

                // Build bar
                if (!ready_to_build && !already_built)
                {
                    ready_to_build = true;
                    //BuildBar();
                    //RemoveClone();

                }


                placed = true;
                
            }

        }

    }

    private void BuildBar()
    {
        Instantiate(bar, preview_clone.transform.position, preview_clone.transform.rotation)
        if (snapPoint)
        {
            snapPoint.setBuilt(true);
        }
            

        RemoveCloneAndBar();
        Debug2.text = "hi im in buildbar";

        
        
    }

    private void RemoveCloneAndBar()
    {
        
        Destroy(preview_clone);
        bar_being_held.CustomForceDrop(hand_holding_bar);
        Destroy(this.transform.root.gameObject);
        //Destroy(this.transform.parent.gameObject);
        //Destroy(transform.root.gameObject);
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.tag == tagISnapTo)
        {
            Destroy(preview_clone);

            placed = false;
            ready_to_build = false;

            //Debug0.text = "Not Placed";

        }

    }
}
