using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    public enum DummyState 
    {
        Idle,
        Sucking
    }

    public DummyState State = DummyState.Idle;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            State = State == DummyState.Idle ? DummyState.Sucking : DummyState.Idle;
    }
}
