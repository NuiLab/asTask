using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using System.IO;
using UnityEngine.SceneManagement;

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
    private string currentConnectedPointName;



    // XR
    // input devices
    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();



    private void Start()
    {
        fname = "FOW-DataLog.csv";
        fpath = Path.Combine(Application.persistentDataPath, fname);


        if (!File.Exists(fpath))
        {
            File.WriteAllText(fpath, "Scene Name, Participant ID, Base Point, Builder Point, Time Placed\n");
        }
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

            if (rightTrigger && readyToBuild)
            {
                BuildBar();
            }
        }
        if (leftHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out leftTrigger) && leftTrigger)
        {
            // main build button (Left Hand)

            if (leftTrigger && readyToBuild)
            {
                BuildBar();
            }
        }

    }

    // Creates a new base bar at the location of the current preview.
    private void BuildBar()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        File.AppendAllText(fpath, sceneName + ", " + DataStorage.ParticipantID + ", " + currentConnectedPointName + ", " + this.gameObject.name + ", " + System.DateTime.Now + "\n");

        DataStorage.CurrentBasePoint = currentConnectedPointName;
        DataStorage.CurrentBuilderPoint = this.gameObject.name;
        DataStorage.LastbuildTime = System.DateTime.Now.ToString();


        Instantiate(bar, previewClone.transform.position, previewClone.transform.rotation);
        if (snapPoint)
            snapPoint.setBuilt(true);

        Cleanup();

    }

    // Removes the builder and the preview from the scene
    private void Cleanup()
    {
        this.transform.parent.gameObject.tag = "cleanable";
        this.transform.parent.gameObject.SetActive(false);
        Destroy(previewClone);

    }



    /*
     * This is the main logic of the program:
     * Triggered when the snapOffset on a builder
     * bar stays inside of the snapPoint of a base
     *
     * Determines the position of the snapPoint and
     * then offsets itself by its own snapOffset from
     * that point. Once this is determined the preview
     * is instantiated at that offset, and it sets the
     * readytobuild bool to true. This is then read in
     * the update function whenever a button is pressed.
     *
     */

    private void OnTriggerStay(Collider other)
    {


        // Generate Bar Previews
        if (TagISnapTo == other.tag)
        {

            currentConnectedPointName = other.gameObject.name;



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


    // Method for checking if the snap point already has a bar attached to it
    // and therfore cant be filled.
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


    // Determine the float value to offset the preview from
    // the current base bar.
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


    //Destroys preview and resets variables
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
