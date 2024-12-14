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
    int currentTier = 1;
    public Vector3 destination;
    public int activeTier = 1;

    public static List<GameObject> aliveEnemies = new List<GameObject>();
    void Start()
    {
        GameObject n = Instantiate(tier1[Random.Range(0, aliveEnemies.Count)]);

        n.transform.GetComponent<Rigidbody>().linearVelocity = new Vector3(n.transform.position.x * -1, n.transform.position.y* 1, n.transform.position.z * -1) * n.transform.GetComponent<EnemyAI>().moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(aliveEnemies.Count == 0 && currentTier == activeTier)
        {
            switch(currentTier)
            {
                case 1:
                    currentTier = 2;
                    GameObject n = Instantiate(tier1[Random.Range(0, aliveEnemies.Count)]);
                    n.transform.GetComponent<Rigidbody>().linearVelocity = new Vector3(n.transform.position.x * -1, n.transform.position.y * 1, n.transform.position.z * -1) * n.transform.GetComponent<EnemyAI>().moveSpeed;
                    break;
                case 2:
                    currentTier = 3;
                    GameObject m = Instantiate(tier1[Random.Range(0, aliveEnemies.Count)]);
                    m.transform.GetComponent<Rigidbody>().linearVelocity = new Vector3(m.transform.position.x * -1, m.transform.position.y * 1, m.transform.position.z * -1) * m.transform.GetComponent<EnemyAI>().moveSpeed;
                    break;
                case 3:
                    currentTier = 4;
                    GameObject l = Instantiate(tier1[Random.Range(0, aliveEnemies.Count)]);
                    l.transform.GetComponent<Rigidbody>().linearVelocity = new Vector3(l.transform.position.x * -1, l.transform.position.y * 1, l.transform.position.z * -1) * l.transform.GetComponent<EnemyAI>().moveSpeed;
                    break;
                case 4:
                    GameObject p = Instantiate(tier1[Random.Range(0, aliveEnemies.Count)]);
                    p.transform.GetComponent<Rigidbody>().linearVelocity = new Vector3(p.transform.position.x * -1, p.transform.position.y * 1, p.transform.position.z * -1) * p.transform.GetComponent<EnemyAI>().moveSpeed;
                    break;
            }
        }
    }
}
