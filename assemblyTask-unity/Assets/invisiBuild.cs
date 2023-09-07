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
    public GameObject instructions;
    bool crossSpawned = false;
    public GameObject cross;
    public GameObject check;
    GameObject tempCross;
    GameObject tempCheck;
    public bool isGrabbed = false;
    public bool checkSpawned = false;
    public int mistakes = 0;
    // Start is called before the first frame update
    void Start()
    {
        instructions = GameObject.FindWithTag("SceneInstructions");
    }

    // Update is called once per frame

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("instruction"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            lastTouchedBar = other.gameObject;
            if (distance <= 0.03f && CheckProperties(other))
            {
                correctPlacement = true;
            }

            else correctPlacement = false;

            Debug.Log("Distance between objects: " + distance);
        }
    }
    void OnTriggerExit()
    {
        correctPlacement = false;
    }
    bool CheckProperties(Collider other)
    {
        bool correct = true;
        if (other.GetComponent<propCheck>().barlength != 0)
        {
            if (this.gameObject.GetComponent<propCheck>().barlength != other.GetComponent<propCheck>().barlength)
            {
                correct = false;
            }
        }
        if (other.GetComponent<propCheck>().color != null)
        {
            if (this.gameObject.GetComponent<propCheck>().color != other.GetComponent<propCheck>().color)
            {
                correct = false;
            }
        }
        
        return correct;
    }

    void Update()
    {
        if (isGrabbed)
        {
            // button presses
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);

            bool rightTrigger = false;
            bool leftTrigger = false;
            if (rightHandDevices[0] != null)
            {
                if (rightHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out rightTrigger) && rightTrigger)
                {
                    // main build button (Right Hand)

                    if (rightTrigger && correctPlacement)
                    {
                        StartCoroutine("rightBar");
                        build();

                    }
                    if (rightTrigger && !correctPlacement)
                    {
                        StartCoroutine("WrongBar");
                    }
                }
            }
            if (leftHandDevices[0] != null)
            {
                if (leftHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out leftTrigger) && leftTrigger)
                {
                    // main build button (Left Hand)

                    if (leftTrigger && correctPlacement)
                    {
                        StartCoroutine("rightBar");
                        build();
                    }
                    if (leftTrigger && !correctPlacement)
                    {
                        StartCoroutine("WrongBar");
                    }
                }
            }

        }
    }
    void build()
    {
        GameObject newBar = Instantiate(this.gameObject, lastTouchedBar.transform.position, lastTouchedBar.transform.rotation); //this is the bar that is being built
        newBar.gameObject.GetComponent<Renderer>().material = instructions.GetComponent<invisInstructions>().builtMat;
        //this.gameObject.transform.position = lastTouchedBar.transform.position;
        //this.gameObject.transform.rotation = lastTouchedBar.transform.rotation;
        newBar.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        newBar.gameObject.GetComponent<MeshCollider>().enabled = false;
        newBar.gameObject.GetComponent<XROffsetGrabInteractable>().enabled = false;
        newBar.gameObject.GetComponent<invisiBuild>().enabled = false;   
        instructions.GetComponent<invisInstructions>().nextStep();
        this.gameObject.SetActive(false);


        //need to change this to a new instructions script
    }
    IEnumerator WrongBar()
    {
        instructions.GetComponent<invisInstructions>().mistakes++;
        instructions.GetComponent<invisInstructions>().builtShape.SetActive(true);  
        if (!crossSpawned)
        {
            tempCross = Instantiate(cross, this.transform.position, Quaternion.identity);
            crossSpawned = true;
        }
        yield return new WaitForSeconds(2f);
        Destroy(tempCross);
        crossSpawned = false;
    }
    public void SetIsGrabbed(bool value)
    {
        isGrabbed = value;
    }
    IEnumerator rightBar()
    {

        if (!checkSpawned)
        {
            tempCheck = Instantiate(check, this.transform.position, Quaternion.identity);
            checkSpawned = true;
        }
        yield return new WaitForSeconds(2f);
        Destroy(tempCheck);
        checkSpawned = false;
    }
}
