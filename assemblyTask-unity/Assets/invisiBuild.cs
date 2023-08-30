using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using System.IO;
using UnityEngine.SceneManagement;

using UnityEngine.XR.Interaction.Toolkit;
public class invisiBuild : MonoBehaviour
{

    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();
    public bool correctPlacement = false;
    GameObject lastTouchedBar;
    //starts at 2 to when all transforms are listed, it starts at te correct one
    public Transform[] children;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("instruction"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            lastTouchedBar = other.gameObject;
            if (distance <= 0.03f) correctPlacement = true;
            else correctPlacement = false;

            Debug.Log("Distance between objects: " + distance);
        }
    }
    void OnTriggerExit()
    {
        correctPlacement = false;
    }
    void Update()
    {

        // button presses
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);

        bool rightTrigger = false;
        bool leftTrigger = false;

        if (rightHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out rightTrigger) && rightTrigger)
        {
            // main build button (Right Hand)

            if (rightTrigger && correctPlacement)
            {
                build();

            }
            if (rightTrigger && !correctPlacement)
            {
                StartCoroutine("WrongBar");
            }
        }
        if (leftHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out leftTrigger) && leftTrigger)
        {
            // main build button (Left Hand)

            if (leftTrigger && correctPlacement)
            {

                build();
            }
            if (leftTrigger && !correctPlacement)
            {
                StartCoroutine("WrongBar");
            }
        }

    }
    void build()
    {
        this.gameObject.transform.position = lastTouchedBar.transform.position;
        this.gameObject.transform.rotation = lastTouchedBar.transform.rotation;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.gameObject.GetComponent<MeshCollider>().enabled = false;
        this.gameObject.GetComponent<XROffsetGrabInteractable>().enabled = false;
        this.enabled = false;

        //need to change this to a new instructions script
    }
}
