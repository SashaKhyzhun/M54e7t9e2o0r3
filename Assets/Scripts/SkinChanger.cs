using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    public PlayerController playerController;
    public SpriteRenderer background;
    public RuntimeAnimatorController[] characterAnimators;
    public float[] characterSpeeds;
    public Sprite[] backgrounds;

    public void ChangePlayer(int index)
    {
        playerController.gameObject.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[index];
        playerController.speed = characterSpeeds[index];
    }

    public void ChangeBackground(int index)
    {
        background.sprite = backgrounds[index];
    }
}
