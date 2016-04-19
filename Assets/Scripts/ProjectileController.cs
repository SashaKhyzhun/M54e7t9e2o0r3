using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    public ScoreController scoreController;

    public float lifeSpan = 5;

    private WaitForSeconds lifeSpanWFS;

    void Awake() { 
        lifeSpanWFS = new WaitForSeconds(lifeSpan);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Kill();
    }

    void OnEnable() {
        StartCoroutine(Live()); // if enabled - begin to live;
    }

    void OnDisable() {
        StopAllCoroutines(); // if disabled - stop counting seconds to death
    }

    IEnumerator Live() {
        yield return lifeSpanWFS; // you have only this much
        Kill(); // the only purpose of life is death
    }

    void Kill() {
        gameObject.SetActive(false); // not very interesting death
    }

}