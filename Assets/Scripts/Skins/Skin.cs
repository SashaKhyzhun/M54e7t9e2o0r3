using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Skin : MonoBehaviour
{
    public GameObject selectedGfx;
    public GameObject unavailableGfx;
    public GameObject purchasedGfx;
    public Image thumbnailGfx;
    public int cost;

    public bool available { get; set; }

    public bool selected { get; set; }

    public bool owned { get; set; }

    public void UpdateGfx()
    {
        selectedGfx.SetActive(false);
        unavailableGfx.SetActive(false);
        purchasedGfx.SetActive(false);
        thumbnailGfx.color = Color.white;  

        if (!selected)
        {
            if (owned)
            {
                purchasedGfx.SetActive(true);
            }
            else if (!available)
            {
                unavailableGfx.SetActive(true);
                thumbnailGfx.color = new Color(.3f, .3f, .3f);
            }
        }
        else
        {
            selectedGfx.SetActive(true);
        }
    }
}
