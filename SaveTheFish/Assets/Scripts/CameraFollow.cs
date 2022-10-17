using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public Transform targetObject;
    public Vector3 cameraOffset;
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cameraOffset = transform.position - targetObject.transform.position;
        
    }

    void LateUpdate()
    {
        if (playerController.finished)
        {
            transform.DOMoveZ(targetObject.position.z -25f, 1f);
        }
    }
}
