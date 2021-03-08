using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


public class debuggingSnaps : MonoBehaviour
{

    public Text debug3;

    public Material goodMat;
    public Material badMat;


    // input devices
    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();



    // Update is called once per frame
    void Update()
    {

        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);

        var bars = GameObject.FindGameObjectsWithTag("Bar_SP");
        


        bool a_pressed = false;

        bool x_pressed = false;




        if (rightHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out a_pressed) && a_pressed)
        {

            foreach (var obj in bars)
            {
                if (obj.GetComponent<buildChecker>().getBuilt())
                {
                    debug3.text = "Trying to set the mat";
                    obj.GetComponent<MeshRenderer>().material = goodMat;
                }
                else
                {
                    obj.GetComponent<MeshRenderer>().material = badMat;
                }
                
                
            }



        }
        if (leftHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out x_pressed) && x_pressed)
        {


        }
        else
        {
            Debug.Log("No Devices found");
        }

        
        

    }
}
