using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

    public Transform targetPlayer;
    public float lastOffsetX;

    private Transform playerTransform;
    private Renderer playerRenderer;
    private float cameraExtentX;
    private float playerExtentX;

    void Start () { 
        playerTransform = transform;
        playerRenderer = targetPlayer.GetComponent<Renderer>();
        cameraExtentX =  Camera.main.aspect;
        playerExtentX = playerRenderer.bounds.extents.x;
    }

    void Update () {
	    if (playerRenderer.bounds.center.x >= playerTransform.position.x + cameraExtentX + playerExtentX + lastOffsetX) {
            targetPlayer.position += new Vector3(-(cameraExtentX + playerExtentX + lastOffsetX) * 2, 0, 0);
            }
        if (playerRenderer.bounds.center.x < playerTransform.position.x - cameraExtentX - playerExtentX - lastOffsetX) {
            targetPlayer.position += new Vector3((cameraExtentX + playerExtentX + lastOffsetX) * 2, 0, 0);
           }
    }


}

