using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    TMP_Text label;

    private void Start()
    {
        label = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        label.text = "timer: " + GameManager.Instance.Timer.ToString("0.");
    }
}
