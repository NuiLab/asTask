using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildChecker : MonoBehaviour
{
    public  bool built = false;
    public bool getBuilt()
    {
        return built;
    }
    public void setBuilt(bool b)
    {
        built = b;
    }
}
