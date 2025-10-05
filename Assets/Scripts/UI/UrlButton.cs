using UnityEngine;

public class UrlButton : MonoBehaviour
{
    [SerializeField] string url;

    public void OnClickUrl()
    {
        Application.OpenURL(url);
    }
}
