using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    [SerializeField] private bool sucking = false;  
    public enum DummyState 
    {
        Idle,
        Sucking
    }

    public DummyState State = DummyState.Idle;

    private void Update()
    {
       State = sucking ? DummyState.Sucking : DummyState.Idle;
    }
}
