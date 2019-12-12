using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MovableBox : MonoBehaviour {

    private Vector3 moveTo;

    private bool beRay = false;

    public Text textboard;

    private Transform _transform;
    private Rigidbody _rigidbody;

    private Vector3 oldPos;
    private Vector3 prePos;

    // Use this for initialization
    void Start () {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Click");
            RayCheck();
            oldPos = _transform.position;
        }

        if (beRay) {
            Debug.Log("ray hit!");
            MovePoisition();
        }

        if (Input.GetMouseButtonUp(0)) {
            beRay = false;
            //_rigidbody.velocity = ((_transform.position - prePos) / Time.deltaTime);
            _rigidbody.AddForce(10*((_transform.position - prePos) / Time.deltaTime));
            StartCoroutine(DelayMethod(0.3f, () => {
                MovePrediction(_transform);
            }));
        }
        Debug.Log("角度 : "+_transform.rotation.eulerAngles);
        Debug.Log("速度 : "+_rigidbody.velocity.magnitude+", 向き : "+_rigidbody.velocity);
        prePos = _transform.position;
    }

    private void MovePrediction(Transform nowPos) {
        Debug.Log("now predict. nowPos: " + nowPos.position + ", oldPos: " + oldPos);

        if (Math.Abs(_rigidbody.velocity.x) < 0.2f && Math.Abs(_rigidbody.velocity.z) < 0.2f) {
            if (_rigidbody.velocity.y < -1f) {
                textboard.text = "落下";
                Debug.Log("update Text");
            } else if (_rigidbody.velocity.y > 1f) {
                textboard.text = "打ち上げ";
                Debug.Log("update Text");
            }
        } else {
            textboard.text = "投射";
            Debug.Log("update Text");
        }

        if (nowPos.position != oldPos && _rigidbody.velocity.magnitude < 1f) {
            textboard.text = "移動";
            Debug.Log("update Text");
        }

        oldPos = _transform.position;
    }

    private void RayCheck() {
        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) && hit.collider == gameObject.GetComponent<Collider>()) {
            beRay = true;
            _rigidbody.velocity = new Vector3();
        } else {
            beRay = false;
        }

    }

    private void MovePoisition() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;

        moveTo = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = moveTo;
    }

    private IEnumerator DelayMethod(float waitTime, Action action) {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    private IEnumerator calcurateVector() {
        prePos = _transform.position;
        yield return null;

    }
}
