using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NumberPadScript : MonoBehaviour
{
    GameObject managerObject;
    ExperimentLog manager;
    // Start is called before the first frame update

    private string ParticipantID = "";

    [SerializeField]
    private Text displayedID;



    private void Update()
    {

        displayedID.text = "ID: " + ParticipantID;

    }

    void Start()
    {
        if (manager == null) manager = GameObject.FindWithTag("Manager").GetComponent<ExperimentLog>();
        if (managerObject == null) managerObject = GameObject.FindWithTag("Manager");

    }
    public void AddNumber(string inputNumber)
    {
        string tempID = ParticipantID + inputNumber;
        ParticipantID = tempID;

    }


    public void ClearNumber()
    {
        ParticipantID = "";
    }


    public void SaveID()
    {
        manager.SetParticipantNumber(int.Parse(ParticipantID));
        DataStorage.ParticipantID = int.Parse(ParticipantID);
    }







}
