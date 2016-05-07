using UnityEngine;
using System.Collections;

public class InputHeandler : MonoBehaviour {

	public bool started { get; set; }
    public bool touched { get; private set; }

    //private Touch currentTouch;
	private WaitForEndOfFrame wfeof;

    void Start() {
        started = false;
		wfeof = new WaitForEndOfFrame ();
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

	public void HandleTouch() {
		if (!started)
			started = true;
		else {
			touched = true;
			StartCoroutine (TouchRestore ());
		}
	}

	IEnumerator TouchRestore() {
		yield return wfeof;
		touched = false;
	}
}
