using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

using UnityEngine.XR.Interaction.Toolkit;


//https://github.com/Unity-Technologies/XR-Interaction-Toolkit-Examples/issues/29




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


    // state
    private bool ready_to_build = false;




    // Release hand stuff
    private XRGrabInteractable current_interactable;
    private XRGrabInteractable bar_being_held;
    private XRBaseInteractor hand_holding_bar;








    void Update()
    {




        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);




        bool a_pressed = false;

        bool x_pressed = false;



        //Debug2.text = bar_being_held.ToString() + " being held by " + hand_holding_bar.ToString();







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

            //if (x_pressed)
            //{
            //    RemoveCloneAndBar();
            //}

        }
        else
        {
            Debug.Log("No Devices found");
        }







        //Debug0.text = "ready_to_build: " + ready_to_build;
        //Debug1.text = "A pressed: " + a_pressed;




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
                }


                placed = true;

            }

        }

    }

    private void BuildBar()
    {
        Instantiate(bar, preview_clone.transform.position, preview_clone.transform.rotation);
        if (snapPoint)
        {
            snapPoint.setBuilt(true);
        }

        Debug2.text = "hi im in buildbar";

        RemoveCloneAndBar();




    }

    private void RemoveCloneAndBar()
    {

        //XRGrabInteractable bar_held = this.transform.root.gameObject.GetComponent<XRGrabInteractable>();
        //XRBaseInteractor held_by = bar_held.selectingInteractor;

        //bar_held.CustomForceDrop(held_by);
        this.transform.parent.gameObject.SetActive(false);
        Destroy(preview_clone);

        this.transform.parent.gameObject.tag = "cleanable";
        //StartCoroutine(Wait(3));

        //Destroy(this.transform.parent.gameObject);

        //bar_being_held = GetComponent<XRGrabInteractable>();
        //hand_holding_bar = bar_being_held.selectingInteractor;

        //bar_being_held.CustomForceDrop(hand_holding_bar);
        //bar_being_held.setActive(false);
        Debug0.text = this.transform.root.gameObject.name;
        //Destroy(this.transform.root.gameObject);


        //Destroy(this.transform.parent.gameObject);
        //Destroy(transform.root.gameObject);
        


    }

    //IEnumerator Wait(float duration)
    //{
    //    //This is a coroutine
    //    Debug.Log("Start Wait() function. The time is: " + Time.time);
    //    Debug.Log("Float duration = " + duration);
    //    yield return new WaitForSeconds(duration);   //Wait
    //    Debug.Log("End Wait() function and the time is: " + Time.time);
    //}



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
