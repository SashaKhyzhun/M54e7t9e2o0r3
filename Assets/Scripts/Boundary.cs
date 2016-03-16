using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

    public Transform targetPlayer;
    public Transform targetEnemy;

    public float lastOffsetX;
    public float lastOffsetY;

    private Transform playerTransform;
    private Transform enemyTransform;

    private Renderer playerRenderer;
    private Renderer enemyRenderer;

    private float cameraExtentY;
    private float cameraExtentX;

    private float playerExtentY;
    private float playerExtentX;

    private float enemyExtentY;
    private float enemyExtentX;

    void Start () {
        // for player
        playerTransform = transform;
        playerRenderer = targetPlayer.GetComponent<Renderer>();
        // for enemy
        enemyTransform = transform;
        enemyRenderer = targetEnemy.GetComponent<Renderer>();
        // for all
        cameraExtentY = Camera.main.orthographicSize;
        cameraExtentX = cameraExtentY * Camera.main.aspect;
        //for player
        playerExtentY = playerRenderer.bounds.extents.y;
        playerExtentX = playerRenderer.bounds.extents.x;
        //for enemy
        enemyExtentY = enemyRenderer.bounds.extents.y;
        enemyExtentX = enemyRenderer.bounds.extents.x;

    }

    void Update () {
        //for player
	    if (playerRenderer.bounds.center.x >= playerTransform.position.x + cameraExtentX + playerExtentX + lastOffsetX) {
            targetPlayer.position += new Vector3(-(cameraExtentX + playerExtentX + lastOffsetX) * 2, 0, 0);
        }
        if (playerRenderer.bounds.center.x < playerTransform.position.x - cameraExtentX - playerExtentX - lastOffsetX) {
            targetPlayer.position += new Vector3((cameraExtentX + playerExtentX + lastOffsetX) * 2, 0, 0);
        }
        if (playerRenderer.bounds.center.y >= playerTransform.position.y + cameraExtentY + playerExtentY + lastOffsetY) {
            targetPlayer.position += new Vector3(0, -(cameraExtentY + playerExtentY + lastOffsetY) * 2, 0);
        }
        if (playerRenderer.bounds.center.y < playerTransform.position.y - cameraExtentY - playerExtentY - lastOffsetY) {
            targetPlayer.position += new Vector3(0, (cameraExtentY + playerExtentY + lastOffsetY) * 2, 0);
        }

        //for enemy
        if (enemyRenderer.bounds.center.x >= enemyTransform.position.x + cameraExtentX + enemyExtentX + lastOffsetX) {
            targetEnemy.position += new Vector3(-(cameraExtentX + enemyExtentX + lastOffsetX) * 2, 0, 0);
        }
        if (enemyRenderer.bounds.center.x < enemyTransform.position.x - cameraExtentX - enemyExtentX - lastOffsetX) {
            targetEnemy.position += new Vector3((cameraExtentX + enemyExtentX + lastOffsetX) * 2, 0, 0);
        }
        if (enemyRenderer.bounds.center.y >= enemyTransform.position.y + cameraExtentY + enemyExtentY + lastOffsetY) {
            targetEnemy.position += new Vector3(0, -(cameraExtentY + enemyExtentY + lastOffsetY) * 2, 0);
        }
        if (enemyRenderer.bounds.center.y < enemyTransform.position.y - cameraExtentY - enemyExtentY - lastOffsetY) {
            targetEnemy.position += new Vector3(0, (cameraExtentY + enemyExtentY + lastOffsetY) * 2, 0);
        }

    }


}

