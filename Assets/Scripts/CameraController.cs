using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CameraController : MonoBehaviour {

    private float x;
    private float y;

    public float sensitivity = 2.5f;
    private Vector3 rotate;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }  

    void Update() {
        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");

        rotate = new Vector3(-x, y  * sensitivity, 0);

        transform.eulerAngles = transform.eulerAngles - rotate;
    }
}