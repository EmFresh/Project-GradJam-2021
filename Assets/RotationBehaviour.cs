using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour{
    [SerializeField]
    private float _speed = 1f;

    private float _angle = 0;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        _angle += _speed * Time.deltaTime;
        Quaternion rot = Quaternion.AngleAxis(_angle, Vector3.up);
        transform.rotation = rot;
    }
}