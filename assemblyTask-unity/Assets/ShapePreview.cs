using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShapePreview : MonoBehaviour
{
    public Material newMaterial; // Define your material here

    public bool considerColor = false;
    public bool isPreview = false;
    public Material white;
    public bool isPracticeTask = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!considerColor && isPreview)
        {
            foreach (Transform child in transform)
            {
                MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.material = newMaterial;
                }
            }
        }
        if (!considerColor && !isPreview && !isPracticeTask)
        {
            Transform firstChild = transform.GetChild(0);
            MeshRenderer meshRenderer = firstChild.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = newMaterial;
            }
        }
        if (!considerColor)
            SetPropCheckColorToNull(this.transform);

        if (isPreview)
            DisableAllColliders(this.transform);

        // if (!isPreview)
        //    DisableAllTextMeshPro(this.transform);
    }
    void OnEnable()
    {
        if (isPreview)
            StartCoroutine(disappear());
    }

    // Update is called once per frame
    void Update()
    {

    }
    void DisableAllTextMeshPro(Transform parent)
    {
        foreach (Transform child in parent)
        {
            TextMeshPro tmp = child.GetComponent<TextMeshPro>();
            if (tmp != null)
            {
                tmp.enabled = false;
            }

            // Recursively disable TextMeshPro for children's children
            DisableAllTextMeshPro(child);
        }
    }
    void DisableAllColliders(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Collider collider = child.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Recursively disable colliders for children's children
            DisableAllColliders(child);
        }
    }
    void SetPropCheckColorToNull(Transform parent)
    {
        foreach (Transform child in parent)
        {
            propCheck propCheck1 = child.GetComponent<propCheck>();
            if (propCheck1 != null)
            {
                propCheck1.color = null;
                if(!isPracticeTask)child.GetComponent<MeshRenderer>().material = white;
            }

            // Recursively set color to null for children's children
            SetPropCheckColorToNull(child);
        }
    }
    IEnumerator disappear()
    {
        yield return new WaitForSeconds(10f);
        this.gameObject.transform.parent.gameObject.SetActive(false);//turns off the grandparent object

    }
}