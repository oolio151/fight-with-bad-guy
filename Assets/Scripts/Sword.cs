using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sword : MonoBehaviour
{
    [Header("Setup")]
    public GameObject swordPrefab;
    public Transform player;
    float currentLerpTime;
    bool readyToSwing;

    int enemiesKilled;
    int reloadTime;
    bool reloading;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Transform>();
        reloadTime = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !reloading)
            reloading = true;
        if (reloading) Reload();

        if (Input.GetAxis("Fire") > 0.1 && readyToSwing)
        {
            Swing();
        }
            
    }

    void Reload()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > reloadTime)
        {
            currentLerpTime = reloadTime;
        }

        //lerp!
        float perc = currentLerpTime / reloadTime;
        transform.localEulerAngles = Vector3.Lerp(new Vector3(90, 0, -90), new Vector3(450, 0, -90), perc);
        if(perc >= 1)
        {
            
        }
    }

    void ResetShot()
    {
        readyToSwing = true;
    }

    void Swing()
    {
        Camera camera = GetComponentInParent<Camera>();
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;
        Vector3 lookAtPoint;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Get the world coordinates of the point the camera is looking at
            lookAtPoint = hit.point;
            Debug.Log("Camera is looking at: " + lookAtPoint);
            if(hit.transform.gameObject.layer == 3)
            {
                hit.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
                hit.transform.GetComponent<MeshRenderer>().enabled = false;
                hit.transform.GetComponent<CapsuleCollider>().enabled = false;
                hit.transform.GetComponent<Player>().enabled = false;
                // hit.transform.GetComponentInChildren<CameraController>().enabled = false;
                hit.transform.GetComponent<Rigidbody>().useGravity = false;
                enemiesKilled++;
                if(enemiesKilled >= 3)
                {
                    transform.parent.GetChild(0).GetChild(4).gameObject.SetActive(true);
                    Debug.Log("You win!!");
                    Invoke("Leave", 5f);
                }
            }
        } else
        {
            lookAtPoint = camera.transform.position + camera.transform.forward * 300f;
        }

        readyToSwing = false;
    }

    void Leave()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}