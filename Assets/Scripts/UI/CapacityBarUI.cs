using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CapacityBarUI : MonoBehaviour
{
    [SerializeField] Image fill;
    [SerializeField] TMP_Text label;
   
    void Update()
    {
        fill.fillAmount = GameManager.Instance.GetCurrentCapacityPercentage();
        label.text = $"{(float)GameManager.Instance.PendingScore} / {GameManager.Instance.GetMaxCapacity()}";
    }
}
