using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] float minCapacity = 10f;
    [SerializeField] float capacityModifier = 0.25f;

    public float Timer { get; private set; }
    public float Score { get; private set; }

    public int PendingScore => Mathf.FloorToInt(pendingScore);

    private float pendingScore;

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

    public void AddScore(float score)
    {
        Score += score;
        pendingScore -= score;
        if (pendingScore <= 0)
            pendingScore = 0;
    }

    public void AddPendingScore(float score)
    {
        if(!IsAtMaxCapacity())
            pendingScore += score;
    }

    public int GetMaxCapacity() =>
        Mathf.FloorToInt(minCapacity + Score * capacityModifier);

    public float GetCurrentCapacityPercentage() =>
        PendingScore / (float)GetMaxCapacity();

    public bool IsAtMaxCapacity()
    {
        float per = GetCurrentCapacityPercentage();
        return per >= 1f;
    }
}
