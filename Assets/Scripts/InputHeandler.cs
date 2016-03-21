using UnityEngine;
using System.Collections;

public class InputHeandler : MonoBehaviour {

    public bool started { get; set; }
    public bool touched { get; set; }

    void Start() {
        started = false;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (started == false) {
                started = true;
            }
        }

        //if (Input.GetMouseButton(0)) {
        //    if (touched) {
        //        touched = false;
        //    }
        //}

        if (Input.GetMouseButtonUp(0)) {
            touched = false;
        }

        touched = Input.GetMouseButtonDown(0);
    }


}
