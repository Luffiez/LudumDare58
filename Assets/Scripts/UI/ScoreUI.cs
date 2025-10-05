using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    TMP_Text label;

    private void Start()
    {
        label = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        label.text = "score: " + GameManager.Instance.Score;
    }
}
