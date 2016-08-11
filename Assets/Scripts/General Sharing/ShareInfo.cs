using UnityEngine;

public class ShareInfo : MonoBehaviour
{
    public string text;
    public string format;
    public Texture2D image;

    public string defaultText { get; set; }

    void Awake()
    {
        text = text.Replace("\\n", "\n");
        format = format.Replace("\\n", "\n");
        defaultText = text;
    }
}
