using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using System.IO;
using UnityEngine.SceneManagement;

using UnityEngine.XR.Interaction.Toolkit;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Policy;
using System.Linq;
using UnityEngine.UIElements;

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
    bool isAligned = false;
    public GameObject check;
    GameObject tempCross;
    GameObject tempCheck;
    public bool isGrabbed = false;
    public bool checkSpawned = false;
    public int mistakes = 0;
    public GameObject manager;
    bool tookPos = false;
    Vector3 startPos;
    bool canBeBuilt = true;
    Quaternion originalRotation;
    invisInstructions inst;
    SceneDirector sceneDirector;
    string errortype = "placement";
    bool IsCloseToWorkbench = false;
    string expectedValue;
    string actualValue;
    AudioClip transferSound;
    bool shouldRepeat = false;


    // Start is called before the first frame update
    void Start()
    {
        instructions = GameObject.FindWithTag("SceneInstructions");
        manager = GameObject.FindWithTag("Manager");
        inst = instructions.GetComponent<invisInstructions>();
        sceneDirector = manager.GetComponent<SceneDirector>();
        transferSound = Resources.Load<AudioClip>("transferSound");
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("alignment"))
        {
            isAligned = true;
        }
        //Debug.Log("Triggered");
        if (other.CompareTag("instruction"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            lastTouchedBar = other.gameObject;
            if (distance <= 0.05f && CheckProperties(other))
            {
                //Debug.Log("Correct Placement");
                correctPlacement = true;
            }

            else correctPlacement = false;

            //Debug.Log("Distance between objects: " + distance);
        }
    }
    void OnTriggerExit()
    {
        correctPlacement = false;
        isAligned = false;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WorkbenchTest"))
        {
            IsCloseToWorkbench = true;
        }

    }
    bool CheckProperties(Collider other)
    {
        bool correct = true;
        if (other.GetComponent<propCheck>().barlength != 0)
        {
            if (this.gameObject.GetComponent<propCheck>().barlength != other.GetComponent<propCheck>().barlength)
            {
                //Debug.Log(this.gameObject.GetComponent<propCheck>().barlength + " " + other.GetComponent<propCheck>().barlength);
                correct = false;
                expectedValue = other.GetComponent<propCheck>().barlength.ToString();
                actualValue = this.gameObject.GetComponent<propCheck>().barlength.ToString();
                errortype = "length";
            }
        }
        if (other.GetComponent<propCheck>().color != null)
        {
            if (this.gameObject.GetComponent<propCheck>().color != other.GetComponent<propCheck>().color)
            {
                //Debug.Log(this.gameObject.GetComponent<propCheck>().color + ": " + other.GetComponent<propCheck>().color);
                correct = false;
                expectedValue = other.GetComponent<propCheck>().color.ToString();
                actualValue = this.gameObject.GetComponent<propCheck>().color.ToString();
                errortype = "color";
            }
        }
        //Debug.Log(correct);
        return correct;
    }

    void Update()
    {
        if (canBeBuilt)
        {
            if (isGrabbed)
            {
                if (!tookPos)
                {
                    startPos = this.transform.position;
                    originalRotation = transform.rotation;
                    tookPos = true;
                }
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
                        if (sceneDirector.trialNumber != 8)
                        {
                            if (rightTrigger && correctPlacement && isAligned && IsCloseToWorkbench)
                            {
                                canBeBuilt = false;
                                StartCoroutine("rightBar");
                                StartCoroutine("build");

                            }
                            if (rightTrigger && !correctPlacement && IsCloseToWorkbench)
                            {
                                canBeBuilt = false;
                                StartCoroutine("WrongBar");
                                Debug.Log(errortype);
                            }
                        }
                        else
                        {
                            if (rightTrigger && IsCloseToWorkbench)
                            {
                                canBeBuilt = false;
                                StartCoroutine("buildTransfer");
                            }
                        }
                    }
                }
                if (leftHandDevices[0] != null)
                {
                    if (leftHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out leftTrigger) && leftTrigger)
                    {
                        // main build button (Left Hand)
                        if (sceneDirector.trialNumber != 8)
                        {
                            if (leftTrigger && correctPlacement && isAligned && IsCloseToWorkbench)
                            {
                                canBeBuilt = false;
                                StartCoroutine("rightBar");
                                StartCoroutine("build");
                            }
                            if (leftTrigger && !correctPlacement && IsCloseToWorkbench)
                            {
                                canBeBuilt = false;
                                StartCoroutine("WrongBar");
                            }
                        }
                        else
                        {
                            if (leftTrigger && IsCloseToWorkbench)
                            {
                                canBeBuilt = false;
                                StartCoroutine("buildTransfer");
                            }
                        }

                    }
                }

            }
        }
    }
    public bool RepeatCheck()
    {
        bool temp =
        sceneDirector.RepeatCheck();
        Debug.Log("Does repeat?" + temp);
        return temp;
    }

    IEnumerator build()
    {
        this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 0;
        // when a bar is placed, it goes back to the original position and a new bar is created instead that cannot be picked up again.
        GameObject newBar = Instantiate(this.gameObject, lastTouchedBar.transform.position, lastTouchedBar.transform.rotation);
        instructions.GetComponent<invisInstructions>().builtBars.Add(newBar); //this is the bar that is being built
        newBar.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        newBar.gameObject.GetComponent<XROffsetGrabInteractable>().enabled = false;
        newBar.gameObject.GetComponent<invisiBuild>().enabled = false;
        newBar.gameObject.GetComponent<MeshCollider>().enabled = false;

        if (RepeatCheck())//if step is repeated, bar fades out and they have to go again.
        {
            yield return new WaitForSeconds(0.15f);
            FadeOut(newBar.gameObject, 2f);
            this.transform.position = startPos;
            this.transform.rotation = originalRotation;
            yield return new WaitForSeconds(2.0f);
            this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 1;
            manager.GetComponent<ExperimentLog>().AddData(this.gameObject.name, "Correct placement, Repeat", inst.currentStep.ToString());
            //instructions.GetComponent<invisInstructions>().toggleHands(true);
        }
        else
        {
            yield return new WaitForSeconds(0.15f);
            this.transform.position = startPos;
            this.transform.rotation = originalRotation;
            this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 1;
            manager.GetComponent<ExperimentLog>().AddData(this.gameObject.name, "Correct placement", inst.currentStep.ToString());
            instructions.GetComponent<invisInstructions>().nextStep();
            instructions.GetComponent<invisInstructions>().builtBars.Append(newBar.gameObject);

        }

        StartCoroutine("resetCanBeBuilt");
    }
    IEnumerator buildTransfer()
    {
        if (correctPlacement && isAligned)
        {
            manager.GetComponent<ExperimentLog>().AddData(this.gameObject.name, "Correct", inst.currentStep.ToString());
        }
        else
        {
            manager.GetComponent<ExperimentLog>().AddData(this.gameObject.name, "Error", inst.currentStep.ToString(), errortype, expectedValue, actualValue);
            inst.mistakes++;
        }
        AudioSource.PlayClipAtPoint(transferSound, this.transform.position);//play a confirm sound when something is placed

        GameObject newBar = Instantiate(this.gameObject, this.transform.position, this.transform.rotation);

        if (correctPlacement && isAligned)
        {
            newBar.transform.position = lastTouchedBar.transform.position;
            newBar.transform.rotation = lastTouchedBar.transform.rotation;
        } //this is the bar that is being built

        this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 0;
        newBar.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 0;
        newBar.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        newBar.gameObject.GetComponent<XROffsetGrabInteractable>().enabled = false;
        newBar.gameObject.GetComponent<invisiBuild>().enabled = false;
        instructions.GetComponent<invisInstructions>().nextStep();
        this.transform.position = startPos;
        this.transform.rotation = originalRotation;
        yield return new WaitForSeconds(0.3f);
        canBeBuilt = true;
        this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 1;
    }

    IEnumerator WrongBar()
    {
        if (sceneDirector.experimentType == SceneDirector.ExperimentType.ExpB)
        {
            //instructions.GetComponent<invisInstructions>().dataLog("Bar", "Correct");
            this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 0;
            this.transform.position = startPos;
            this.transform.rotation = originalRotation;
            inst.toggleHands(false);
            inst.mistakes++;
            inst.SetTempText();
            inst.builtShape.SetActive(true);
            inst.builtShape.transform.GetChild(1).gameObject.SetActive(true);
            inst.stepPanel.SetActive(false);
            //instructions.GetComponent<invisInstructions>().dataLog(this.gameObject.name, "incorrect placement", instructions.GetComponent<invisInstructions>().currentStep.ToString());
            manager.GetComponent<ExperimentLog>().AddData(this.gameObject.name, "Error", inst.currentStep.ToString(), errortype);
            if (!crossSpawned)
            {
                tempCross = Instantiate(cross, this.transform.position, Quaternion.identity);
                crossSpawned = true;
            }
            inst.FadeInCorrectBar();
            yield return new WaitForSeconds(2f);
            //shouldNotify = true;
            this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 1;
            inst.cross = tempCross;
            crossSpawned = false;
            StartCoroutine("resetCanBeBuilt");


        }
        else // experiment A here
        {
            this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 0;
            inst.toggleHands(false);
            inst.mistakes++;
            inst.SetTempText();
            inst.builtShape.SetActive(true);
            inst.builtShape.transform.GetChild(1).gameObject.SetActive(true);
            inst.stepPanel.SetActive(false);

            //instructions.GetComponent<invisInstructions>().dataLog(this.gameObject.name, "incorrect placement", instructions.GetComponent<invisInstructions>().currentStep.ToString());
            manager.GetComponent<ExperimentLog>().AddData(this.gameObject.name, "Error", inst.currentStep.ToString(), errortype);
            if (!crossSpawned)
            {
                tempCross = Instantiate(cross, this.transform.position, Quaternion.identity);
                crossSpawned = true;
            }
            yield return new WaitForSeconds(2f);
            //shouldNotify = true;
            this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 1;
            inst.cross = tempCross;
            crossSpawned = false;
            StartCoroutine("resetCanBeBuilt");
        }
    }
    public void SetIsGrabbed(bool value)
    {
        isGrabbed = value;
    }
    public void grabLog()
    {
        manager.GetComponent<ExperimentLog>().AddData(this.gameObject.name, "grabbed");
    }
    public void dataLog(string category, string action)
    {
        manager.GetComponent<ExperimentLog>().AddData(category, action);
    }

    IEnumerator rightBar()
    {
        //this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 0;
        //instructions.GetComponent<invisInstructions>().dataLog("Bar", "Correct");
        if (!checkSpawned)
        {
            tempCheck = Instantiate(check, this.transform.position, Quaternion.identity);
            checkSpawned = true;
        }

        yield return new WaitForSeconds(2f);
        Destroy(tempCheck);
        checkSpawned = false;

    }

    IEnumerator resetCanBeBuilt()
    {
        yield return new WaitForSeconds(0.3f);
        canBeBuilt = true;
        errortype = "placement";

    }
    public void FadeOut(GameObject obj, float duration)
    {
        StartCoroutine(FadeOutRoutine(obj, duration));
    }

    private IEnumerator FadeOutRoutine(GameObject obj, float duration)
    {
        obj.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 0;
        //Renderer renderer = obj.GetComponent<Renderer>();
        //Material material = renderer.material;
        //Color initialColor = material.color;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
           // float alpha = Mathf.Lerp(initialColor.a, 0, t / duration);
            //material.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

       // material.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
        //yield return new WaitForSeconds(0.2f);
        Destroy(obj);
    }
}


