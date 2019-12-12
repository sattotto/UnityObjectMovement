using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePrediction : MonoBehaviour {

    public Text textboard;
    private Transform MyObjectPos;
    private Vector3 oldPos;
    

    // Start is called before the first frame update
    void Start() {
        MyObjectPos = GetComponent<Transform>();
        oldPos = MyObjectPos.position;
    }

    // Update is called once per frame
    void Update() {
        if (oldPos != MyObjectPos.position) {
            textboard.text = "移動した";
            Debug.Log("update Text");
        }
        oldPos = MyObjectPos.position;
    }
}
