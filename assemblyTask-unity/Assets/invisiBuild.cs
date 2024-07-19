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
            //Debug.Log("Aligned");
        }
        //Debug.Log("Triggered");
        if (other.CompareTag("instruction"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            lastTouchedBar = other.gameObject;
            if (distance <= 0.06f && CheckProperties(other))
            {
                correctPlacement = true;
            }
            else
            {
                correctPlacement = false;
            }
            //
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("instruction"))
        {
            correctPlacement = false;
            //Debug.Log("Exited" + other.name);
        }
        if (other.CompareTag("WorkbenchTest"))
        {
            IsCloseToWorkbench = false;
        }
        if (other.CompareTag("alignment"))
        {
            isAligned = false;
            //Debug.Log("Aligned");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WorkbenchTest"))
        {
            IsCloseToWorkbench = true;
        }
        if (other.CompareTag("instruction"))
        {

            //Debug.Log("Entered" + other.name);

        }

    }
    bool CheckProperties(Collider other)
    {
        bool correct = true;
        if (other.GetComponent<propCheck>().barlength != 0)
        {
            if (this.gameObject.GetComponent<propCheck>().barlength != other.GetComponent<propCheck>().barlength)
            {
                Debug.Log(this.gameObject.GetComponent<propCheck>().barlength + " " + other.GetComponent<propCheck>().barlength);
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
                Debug.Log(this.gameObject.GetComponent<propCheck>().color + ": " + other.GetComponent<propCheck>().color);
                correct = false;
                expectedValue = other.GetComponent<propCheck>().color.ToString();
                actualValue = this.gameObject.GetComponent<propCheck>().color.ToString();
                errortype = "color";
            }
        }

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
                Debug.Log("Correct:" + correctPlacement + "Type:" + errortype);

                bool rightTrigger = false;
                bool leftTrigger = false;
                if (rightHandDevices[0] != null)
                {
                    if (rightHandDevices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out rightTrigger) && rightTrigger)
                    {
                        // main build button (Right Hand)
                        if (sceneDirector.trialNumber != 8)
                        {
                            if (rightTrigger && correctPlacement && IsCloseToWorkbench)
                            {
                               
                                canBeBuilt = false;
                                StartCoroutine("rightBar");
                                StartCoroutine("build");
                            }
                            if (rightTrigger && !correctPlacement && IsCloseToWorkbench)
                            {
                                canBeBuilt = false;
                                StartCoroutine("WrongBar");
                               
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
                            if (leftTrigger && correctPlacement && IsCloseToWorkbench)
                            {
                                Debug.Log("Is correct placement:" + correctPlacement);
                                canBeBuilt = false;
                                StartCoroutine("rightBar");
                                StartCoroutine("build");
                            }
                            if (leftTrigger && !correctPlacement && IsCloseToWorkbench)
                            {
                                Debug.Log("Incorrect:" + correctPlacement + "Type:" + errortype);
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
        //Debug.Log("Does repeat?" + temp);
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
            Debug.Log("Repeat in build");
            instructions.GetComponent<invisInstructions>().ArrowRepeat();
            yield return new WaitForSeconds(0.25f);
            FadeOutPlaced(newBar);
            this.transform.position = startPos;
            this.transform.rotation = originalRotation;
            yield return new WaitForSeconds(.5f);
            this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 1;
            manager.GetComponent<ExperimentLog>().AddData(this.gameObject.name, "Correct placement, Repeat", inst.currentStep.ToString());
            //instructions.GetComponent<invisInstructions>().toggleHands(true);
        }
        else
        {
            Debug.Log("No repeat in build");
            inst.ArrowNext();
            yield return new WaitForSeconds(0.25f);
            this.transform.position = startPos;
            this.transform.rotation = originalRotation;
            this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 1;
            manager.GetComponent<ExperimentLog>().AddData(this.gameObject.name, "Correct placement", inst.currentStep.ToString());
            inst.nextStep();
            inst.builtBars.Append(newBar.gameObject);

        }

        StartCoroutine("resetCanBeBuilt");
    }
    public void FadeOutPlaced(GameObject bar)
    {
        StartCoroutine(FadeOutPlacedR(bar, 1f));

    }
    private IEnumerator FadeOutPlacedR(GameObject obj, float duration)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        Material originalMaterial = renderer.material;

        // Create a new material from fadeMaterial and set its color to the original material's color
        Material newMaterial = Instantiate(inst.fadingMaterial);
        newMaterial.color = originalMaterial.color;
        renderer.material = newMaterial;

        // Fade out the new material
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / duration);
            newMaterial.color = new Color(newMaterial.color.r, newMaterial.color.g, newMaterial.color.b, alpha);

            yield return null;
        }

        obj.SetActive(false);
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
        inst.nextStep();
        this.transform.position = startPos;
        this.transform.rotation = originalRotation;
        yield return new WaitForSeconds(0.3f);
        canBeBuilt = true;
        this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 1;
    }

    IEnumerator WrongBar()
    {
        Debug.Log("Wrong bar");
        if (sceneDirector.experimentType != SceneDirector.ExperimentType.ExpA)
        {
            this.gameObject.GetComponent<XROffsetGrabInteractable>().interactionLayerMask = 0;
            inst.toggleHands(false);
            if (!crossSpawned)
            {
                tempCross = Instantiate(cross, this.transform.position, Quaternion.identity);
                crossSpawned = true;
            }
            this.transform.position = startPos;
            this.transform.rotation = originalRotation;
            inst.mistakes++;
            inst.SetTempText();
            inst.builtShape.SetActive(true);
            inst.builtShape.transform.GetChild(1).gameObject.SetActive(true);
            inst.stepPanel.SetActive(false);
            manager.GetComponent<ExperimentLog>().AddData(this.gameObject.name, "Error", inst.currentStep.ToString(), errortype);
            inst.FadeInCorrectBar();
            yield return new WaitForSeconds(2f);
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
            manager.GetComponent<ExperimentLog>().AddData(this.gameObject.name, "Error", inst.currentStep.ToString(), errortype);
            if (!crossSpawned)
            {
                tempCross = Instantiate(cross, this.transform.position, Quaternion.identity);
                crossSpawned = true;
            }
            yield return new WaitForSeconds(2f);
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

}


