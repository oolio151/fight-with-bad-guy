using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int damage;
    public int health;
    public float moveSpeed;

    private void Awake()
    {
        Spawner.aliveEnemies.Add(this.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            WallHealth.DealDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("dying");
        Spawner.aliveEnemies.Remove(this.gameObject);
    }
}
