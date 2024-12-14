using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WallHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static int health = 100;
    int maxHealth;
    public TextMeshProUGUI healthText;
    public Image healthbar;

    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString() + " / " + maxHealth.ToString();
        healthbar.fillAmount = (float)health/maxHealth;
    }

    public static void DealDamage(int damage)
    {
        health -= damage;
    }
}
