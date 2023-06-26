using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider _healthBar;

    public void SetHealth(int health)
    {
        _healthBar.value = health;
    }

    public void SetMaxHealth(int maxHealth)
    {
        _healthBar.maxValue = maxHealth;
    }
}
