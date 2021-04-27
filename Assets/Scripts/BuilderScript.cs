using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class BuilderScript : MonoBehaviour
{
    public GameObject foundation;
    public GameObject preview;
    private GameObject preview_clone;

    private MeshRenderer myRend;
    //public Material good_mat; //green
    //public Material bad_mat; //red
    public Text Debug0;
    public Text Debug1;
    public Text Debug2;


    //private BuildSystem buildSytem;

    public List<string> tagsISnapTo = new List<string>();



    public bool isFoundation = true;

    private bool placed = false;

    // buttons
    private bool a_pressed = false;
    private bool x_pressed = false;


    // input devices
    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();




    private void Update()
    {

        a_pressed = false;

        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);


        bool pressed;


        if (rightHandDevices.Count >= 1)
        {
            if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.primaryButton, out pressed))
            {
                a_pressed = pressed;

            }

        }
        if (leftHandDevices.Count >= 1)
        {
            if (leftHandDevices[0].TryGetFeatureValue(CommonUsages.primaryButton, out pressed))
            {
                x_pressed = pressed;

            }

        }
        else
        {
            Debug0.text = "no devices";
        }


        Debug0.text = "A pressed: " + a_pressed.ToString();
        Debug1.text = "X pressed: " + x_pressed.ToString();



    }




    private void OnTriggerStay(Collider other)
    {




        for (int i = 0; i < tagsISnapTo.Count; i++)
        {
            string CurrentTag = tagsISnapTo[i];

            if (other.tag == CurrentTag)
            {
                buildChecker snapPoint = other.gameObject.GetComponent<buildChecker>();

                if (!placed)
                {
                    preview_clone = (GameObject) Instantiate(preview, other.transform.position, other.transform.rotation * Quaternion.Euler(0f, 0f, 90f));
                    placed = true;





                }
                //if (a_pressed && placed && !snapPoint.getBuilt())
                //{
                //    Instantiate(foundation, other.transform.position, other.transform.rotation * Quaternion.Euler(0f, 0f, 90f));
                //    snapPoint.setBuilt(true);
                //    break;
                //}






            }
        }



    }


    private void OnTriggerExit(Collider other)
    {

        for (int i = 0; i < tagsISnapTo.Count; i++)
        {
            string CurrentTag = tagsISnapTo[i];

            if (other.tag == CurrentTag)
            {

                Destroy(preview_clone);

                placed = false;
                Debug2.text = "Exited";
            }
        }

    }


}
