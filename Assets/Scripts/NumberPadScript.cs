using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NumberPadScript : MonoBehaviour
{

    private string ParticipantID = "";

    [SerializeField]
    private Text displayedID;



    private void Update()
    {

        displayedID.text = "ID: " + ParticipantID;

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
        DataStorage.ParticipantID = int.Parse(ParticipantID);
    }







}
