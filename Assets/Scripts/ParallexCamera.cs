using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexCamera : MonoBehaviour
{  
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += deltaMovement;
        lastCameraPosition = cameraTransform.position;
    }
}
