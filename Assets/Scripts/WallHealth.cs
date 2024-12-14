using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WallHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static int health;
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
        healthText.text = string.Format("%d/%d", health, maxHealth);
        healthbar.fillAmount = health/maxHealth;
    }

    public static void DealDamage(int damage)
    {
        health -= damage;
    }
}
