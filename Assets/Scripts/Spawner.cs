using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] tier1;
    public GameObject[] tier2;
    public GameObject[] tier3;
    public GameObject[] tier4;
    public int wavesBtwnTiers =5;
    public int currentTier = 1;
    public int wave = 1;
    public Vector3 destination;
    public int activeTier = 1;


    public int numSpawners = 10;
    public static List<GameObject> spawners = new List<GameObject>();

    public static List<GameObject> aliveEnemies = new List<GameObject>();
    private void Awake()
    {
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(aliveEnemies.Count == 0)
        {
            for(int i = 0; i < numSpawners; i++)
            {
                //radius of 100 meters
                float radpos = Random.Range(0f, 2f * Mathf.PI);
                float xpos = Mathf.Cos(radpos);
                float zpos = Mathf.Sin(radpos);
                Vector3 pos = new Vector3(xpos*100, 1, zpos*100);
                Debug.Log(pos);
                Spawn(pos);
            }
            wave++;
        }
    }

    public void Spawn(Vector3 location)
    {
        Debug.Log(gameObject.name);
            
            if (wave <= 5)
            {
                currentTier = 1;
            }
            else if (wave <= 10)
            {
                currentTier = 2;
            }
            else if (wave <= 15)
            {
                currentTier = 3;
            }
            else
            {
                currentTier = 4;
            }

            if (currentTier >= activeTier)
            {

                if (currentTier == 1)
                {


                    GameObject n = Instantiate(tier1[Random.Range(0, tier1.Length-1)]);
                    n.transform.position = location;
                    Vector3 direction = new Vector3(-location.x, 0, -location.z).normalized;

                    n.transform.GetComponent<Rigidbody>().linearVelocity = direction * n.transform.GetComponent<EnemyAI>().moveSpeed;
                }
                else if (currentTier == 2)
                {
                    GameObject n = Instantiate(tier2[Random.Range(0, tier2.Length - 1)]);
                n.transform.position = location;
                Vector3 direction = new Vector3(-location.x, 0, -location.z).normalized;

                    n.transform.GetComponent<Rigidbody>().linearVelocity = direction * n.transform.GetComponent<EnemyAI>().moveSpeed;
                }
                else if (currentTier == 3)
                {
                    GameObject n = Instantiate(tier3[Random.Range(0, tier3.Length-1)]);
                    n.transform.position = location;
                    Vector3 direction = new Vector3(-location.x, 0, -location.z).normalized;

                    n.transform.GetComponent<Rigidbody>().linearVelocity = direction * n.transform.GetComponent<EnemyAI>().moveSpeed;
                }
                else if (currentTier == 4)
                {
                    GameObject n = Instantiate(tier4[Random.Range(0, tier4.Length-1)]);
                    n.transform.position = location;
                    Vector3 direction = new Vector3(-location.x, 0, -location.z).normalized;

                    n.transform.GetComponent<Rigidbody>().linearVelocity = direction * n.transform.GetComponent<EnemyAI>().moveSpeed;
                }
            

        }
    }
}
