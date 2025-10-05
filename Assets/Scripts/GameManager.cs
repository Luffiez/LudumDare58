using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public float Timer { get; private set; }
    public float Score { get; private set; }

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Update()
    {
        Timer += Time.deltaTime;
    }

    public void AddScore(float score) =>
        Score += score;
}
