using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using System.IO;

using UnityEngine.XR.Interaction.Toolkit;


//https://github.com/Unity-Technologies/XR-Interaction-Toolkit-Examples/issues/29


public class BuildOffset : MonoBehaviour
{

    // Debuggers
    // These can be atached to text boxes in the scene for debugging the oculus quest without an output console
    //public Text Debug0;
    //public Text Debug1;
    //public Text Debug2;
    //public Text Debug3;
    //public Text Debug4;

    // Public Variables
    public GameObject bar;
    public GameObject barPreview;
    public bool isEven;
    public int distanceFromCenter;

    // Globals
    private GameObject previewClone;
    private bool readyToBuild = false;
    private buildChecker snapPoint;
    private string TagISnapTo = "Bar_SP";
    bool placed = false;
    private string fname;
    private string fpath;



    // XR
    // input devices
    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();



    private void Start()
    {
        fname = "participantData.csv";
        fpath = Path.Combine(Application.persistentDataPath, fname);


        if (!File.Exists(fpath))
        {
            File.WriteAllText(fpath, "Participant data:\n\n");
        }
    }



    // Update is called once per frame
    void Update()
    {

        // button presses
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);

        bool a_pressed = false;
        bool x_pressed = false;

        if (rightHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out a_pressed) && a_pressed)
        {
            // main build button (Right Hand)

            if (a_pressed && readyToBuild)
            {
                BuildBar();
            }
        }
        if (leftHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out x_pressed) && x_pressed)
        {
            // main build button (Right Hand)

            if (x_pressed && readyToBuild)
            {
                BuildBar();
            }
        }
        else
        {
            Debug.Log("No Devices found");
        }
    }



    private void BuildBar()
    {

        Instantiate(bar, previewClone.transform.position, previewClone.transform.rotation);
        if (snapPoint)
            snapPoint.setBuilt(true);

        Cleanup();

    }

    private void Cleanup()
    {
        this.transform.parent.gameObject.tag = "cleanable";
        this.transform.parent.gameObject.SetActive(false);
        Destroy(previewClone);

    }


    private void OnTriggerStay(Collider other)
    {


        // Generate Bar Previews
        if (TagISnapTo == other.tag)
        {
            // TODO: Output these to a file to record
            // which point connects to which
            //Debug0.text = other.gameObject.name;
            //Debug1.text = this.gameObject.name;

            File.AppendAllText(fpath, other.gameObject.name + " CONNECTS TO " + this.gameObject.name + "\n");

            if (!placed)
            {
                // Set variables
                Vector3 positionOffset;
                Quaternion rotationOffset = Quaternion.Euler(0f, 0f, 90f);
                bool alreadyBuilt = AlreadyBuilt(other);
                bool rotated = BarIsRotataed(other);

                // Determine Offset
                positionOffset = DeterminePosition(rotated);

                previewClone = Instantiate(barPreview, other.transform.position + positionOffset, other.transform.rotation * rotationOffset);
                placed = true;

                // tell build bar we are ready to build
                if (!alreadyBuilt && !readyToBuild)
                    readyToBuild = true;
            }
        }
    }

    private bool AlreadyBuilt(Collider other)
    {
        snapPoint = other.gameObject.GetComponent<buildChecker>();
        return snapPoint.getBuilt();

    }

    private bool BarIsRotataed(Collider other)
    {

        int z = (int)(other.transform.parent.gameObject.transform.localRotation.z * 100);



        //Debug1.text = "z: " + z;

        int zRotation = Mathf.Abs(z);

        //Debug0.text = "zRotation: " + zRotation.ToString();

        if (zRotation == 50)
            return true;


        return false;

    }

    private Vector3 DeterminePosition(bool rotated)
    {
        float offsetFloat = 0.076188f;
        float evenAdjustmentFloat;

        Vector3 offset;


        if (distanceFromCenter < 0)
            evenAdjustmentFloat = 0.03807f;
        else
            evenAdjustmentFloat = -0.03807f;

        if (rotated)
        {
            if (isEven)
            {
                offset = new Vector3(evenAdjustmentFloat + (offsetFloat * distanceFromCenter), 0f, 0f);

            }
            else
            {
                offset = new Vector3((offsetFloat * distanceFromCenter), 0f, 0f);
            }

        }
        else
        {
            if (isEven)
            {
                offset = new Vector3(0f, 0f, evenAdjustmentFloat + (offsetFloat * distanceFromCenter));
            }
            else
            {
                offset = new Vector3(0f, 0f, (offsetFloat * distanceFromCenter));
            }

        }

        return offset;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == TagISnapTo)
        {
            Destroy(previewClone);

            placed = false;
            readyToBuild = false;
        }
    }
}
