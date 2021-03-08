using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;



public class cleaner : MonoBehaviour
{

    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();

    public Text debug4;
    
    
    

    
    void Update()
    {
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);


        bool r_grip_pressed = false;
        bool l_grip_pressed = false;

        rightHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out r_grip_pressed);
        leftHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out l_grip_pressed);

        var objects = GameObject.FindGameObjectsWithTag("cleanable");
        var objectCount = objects.Length;

        debug4.text = "r:" + r_grip_pressed + "l:" + l_grip_pressed + "len:" + objectCount;


        foreach (var obj in objects)
        {
            if(!r_grip_pressed && !l_grip_pressed)
            {
                Destroy(obj.gameObject);
                debug4.text = "Destroying things :)" + objectCount;
            }
            
        }

    }
}
