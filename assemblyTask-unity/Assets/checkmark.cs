using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkmark : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(check());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator check()
    {

        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

    }
}
