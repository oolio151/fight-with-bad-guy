using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PowerUpManager : MonoBehaviour
{
    public float spawnTimer = 5f;
    public GameObject powerup;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn), 10, spawnTimer);
    }

    // Update is called once per frame
    void Update()
    {

    }



    void Spawn() {
        float x = Random.Range(-80f, 80f);
        float z = Random.Range(-80f, 80f);
        Vector3 spawnPos = new Vector3();

        RaycastHit hit;
        var ray = new Ray(Vector3.down, new Vector3(x, 100f, z));
        if (Physics.Raycast(ray, out hit)) {
            spawnPos = hit.point;
        }
        spawnPos.y += 3.5f;

        Instantiate(powerup, spawnPos, UnityEngine.Quaternion.identity);

        Debug.Log(spawnPos);
    }
}
