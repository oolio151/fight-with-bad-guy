using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    [Header("Setup")]
    public GameObject bulletPrefab;
    public TextMeshPro ammoText;
    public Transform bulletOrigin;
    public Transform player;

    [Header("Bullet Stats")]
    public float bulletSpeed;

    [Header("Magazine")]
    public int maxAmmoPerMag;
    public int ammoInMag;
    int ammoInStock;

    [Header("Shooting")]
    public float timeBetweenShots;
    bool readyToShoot;

    [Header("Reloading")]
    public float reloadTime;
    public bool reloading;

    float currentLerpTime;

    int enemiesKilled;

    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;

        //testing purposes only
        ammoInMag = 30;
        ammoInStock = 60;
        player = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = ammoInMag.ToString() + "/" + ammoInStock.ToString();
        if (Input.GetKeyDown(KeyCode.R) && !reloading && ammoInMag < maxAmmoPerMag && ammoInStock > 0)
            reloading = true;
        if (reloading ) Reload();

        if (Input.GetAxis("Fire") > 0.1 && ammoInMag > 0 && readyToShoot)
        {
            Shoot();
        }
            
    }

    public void ExReload()
    {
        if (!reloading && ammoInMag < maxAmmoPerMag && ammoInStock > 0)
            reloading = true;
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
            reloading = false;
            currentLerpTime = 0;
            if(ammoInStock > maxAmmoPerMag)
            {
                ammoInStock -= maxAmmoPerMag - ammoInMag;
                ammoInMag += maxAmmoPerMag - ammoInMag;
            } else
            {
                ammoInMag += ammoInStock;
                ammoInStock = 0;
            }
            
        }
    }

    void ResetShot()
    {
        readyToShoot = true;
    }

    void Shoot()
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
                hit.transform.GetComponent<PlayerController>().enabled = false;
                hit.transform.GetComponentInChildren<CameraController>().enabled = false;
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


        ammoInMag--;
        GameObject neue = Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.identity);
        neue.GetComponent<Rigidbody>().velocity = (lookAtPoint-bulletOrigin.position).normalized * bulletSpeed;
        Destroy(neue , 10f);
        readyToShoot = false;
        Invoke("ResetShot", timeBetweenShots);
    }

    void Leave()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}