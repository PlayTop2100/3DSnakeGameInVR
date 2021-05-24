using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    public Material active = null;
    public Material inactive = null;
    private MeshRenderer meshRenderer = null;
    private XRDirectInteractor xRDirectInteractor = null;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        xRDirectInteractor = GetComponent<XRDirectInteractor>();

        xRDirectInteractor.onHoverEnter.AddListener(SetActive);
        xRDirectInteractor.onHoverExit.AddListener(SetInctive);
    }


    // Update is called once per frame
    //void Update()
    //{
        
    //}


    private void SetActive(XRBaseInteractable interactable)
    {
        meshRenderer.material = active;
    }

    private void SetInctive(XRBaseInteractable interactable)
    {
        meshRenderer.material = inactive;
    }

}
