using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        totalHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }

    // Update is called once per frame
    private void Update()
    {
        currentHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
}
