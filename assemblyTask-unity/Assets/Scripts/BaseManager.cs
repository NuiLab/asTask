using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{

    private bool hasBeenChecked = false;

    public void setChecked(bool checkedVal)
    {
        hasBeenChecked = checkedVal;
    }

    public bool getChecked()
    {
        return hasBeenChecked;
    }

}
