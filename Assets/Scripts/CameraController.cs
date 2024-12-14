using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class CameraController : MonoBehaviour
{
    public Transform player; 
    float cameraVerticalRotation = 0f; 
    public float cameraSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.X)) {
            // player.Rotate(Vector3.up.Scale(0.1));
        }
        if (Input.GetKey(KeyCode.Y)) {
            cameraVerticalRotation -= 1;
        }
        
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right*cameraVerticalRotation * cameraSpeed;

        transform.position = player.position;
    }

    
}