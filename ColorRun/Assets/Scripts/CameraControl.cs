using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField]
    private float speed = 10f;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
