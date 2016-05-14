using UnityEngine;
using System.Collections;

public class InputHeandler : MonoBehaviour
{
    public float firstTouchCooldown = 1f;

    public bool started { get; set; }

    public bool touched { get; private set; }

    private WaitForSeconds firstTouchCooldownWFS;

    //private Touch currentTouch;
    private WaitForEndOfFrame wfeof;

    void Start()
    {
        started = false;
        wfeof = new WaitForEndOfFrame();
        firstTouchCooldownWFS = new WaitForSeconds(firstTouchCooldown);
    }

    //    void Update() {
    //
    //#if UNITY_EDITOR
    ////        if (Input.GetMouseButtonDown(0)) {
    ////            if (started == false) {
    ////                started = true;
    ////            }
    ////        }
    //
    //        if (Input.GetMouseButtonUp(0)) {
    //            touched = false;
    //        }
    //
    //        touched = Input.GetMouseButtonDown(0);
    //#elif UNITY_ANDROID
    //        if(Input.touchCount > 0) {
    //            currentTouch = Input.GetTouch(0);
    //
    //            if (currentTouch.phase == TouchPhase.Began) {
    //                touched = true;
    ////            if (!started) {
    ////                started = true;
    ////            }
    //            }
    //
    //            if (currentTouch.phase == TouchPhase.Moved || currentTouch.phase == TouchPhase.Stationary || currentTouch.phase == TouchPhase.Canceled 
    //                || currentTouch.phase == TouchPhase.Ended) {
    //                touched = false;
    //            } 
    //
    //        }
    //#endif
    //    }

    public void HandleTouch()
    {
        if (/*canStart && */!started)
        {
            started = true;
            //StartCoroutine(FirstTouchCooldown());
        }
        else
        {
            touched = true;
            StartCoroutine(TouchRestore());
        }
    }

    IEnumerator TouchRestore()
    {
        yield return wfeof;
        touched = false;
    }

    //    IEnumerator FirstTouchCooldown()
    //    {
    //        canStart = false;
    //        yield return firstTouchCooldownWFS;
    //        canStart = true;
    //    }
}
