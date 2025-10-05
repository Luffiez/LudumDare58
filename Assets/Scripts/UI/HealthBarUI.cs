using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] Image fill;
    [SerializeField] TMP_Text label;
    [SerializeField] PlayerHealth playerHealth;

    void Update()
    {
        fill.fillAmount = (float)playerHealth.CurrentHealth / playerHealth.MaxHealth;
        label.text = $"{playerHealth.CurrentHealth} / {playerHealth.MaxHealth}";
    }
}
