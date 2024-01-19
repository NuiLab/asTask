using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using UnityEngine;

public class NoColor : MonoBehaviour
{
    public Material material;
    public bool isColored;
    // Start is called before the first frame update
    void Start()
    {
        changeAllMaterials(this.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void changeAllMaterials(Transform parent)
    {
        if (!isColored)
        {
            foreach (Transform child in transform)
            {
                MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.material = material;
                }
            }
        }
    }
}
