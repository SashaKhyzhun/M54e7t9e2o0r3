using UnityEngine;
using System.Collections;

public class PlayerChanger : MonoBehaviour
{
    public PlayerController playerController;
    public RuntimeAnimatorController[] characterAnimators;
    public float[] characterSpeeds;

    public void ChangePlayer(int index)
    {
        playerController.gameObject.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[index];
        playerController.speed = characterSpeeds[index];
    }
}
