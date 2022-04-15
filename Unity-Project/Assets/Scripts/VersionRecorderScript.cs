using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionRecorderScript : MonoBehaviour
{


    public void SetVersionNumber(int versionNumber)
    {
        DataStorage.ExperimentVersion = versionNumber;
    }
}
