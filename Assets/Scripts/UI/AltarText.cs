using UnityEngine;

public class AltarText : MonoBehaviour
{
    [SerializeField] float interval = 30f;
    [SerializeField] float visibility = 30f;
    [SerializeField] GameObject target;
    float timer = 0;

    void Update()
    {
        if (target.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer > interval)
            {

            }
        }
    }
}
