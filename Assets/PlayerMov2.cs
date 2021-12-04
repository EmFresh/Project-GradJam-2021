using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov2 : MonoBehaviour{
    private Vector2 _moveInput = Vector2.zero;
    private bool _jumpPressed = false;
    private bool _slideInput = false;

    [SerializeField]
    private CharacterController _controller;
    [SerializeField]
    private float _moveSpeed = 9f;
    [SerializeField]
    private float _gravity = 9f;
    [SerializeField]
    private float _fallMultiplier = 1.3f;
    [SerializeField]
    private float _jumpHeight = 3.5f;

    [SerializeField]
    public int interpolationFramesCount = 60;

    int elapsedFrames = 0;

    private Vector3 _currentVelocity = Vector3.zero;
    private Vector3 _inputVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start(){ }

    // Update is called once per frame
    void FixedUpdate(){


        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

        _inputVelocity = Vector3.zero;
        _inputVelocity.x = _moveInput.x * _moveSpeed;
        _inputVelocity.y = _currentVelocity.y;

       
            if (_controller.isGrounded){
            _inputVelocity.y = 0;

            if (_slideInput)
            {
                


                Vector3 _SlideVector = _inputVelocity;
                _inputVelocity = Vector3.Lerp(_SlideVector, _SlideVector * _moveSpeed, interpolationRatio);

                if (_jumpPressed)
                {
                    _inputVelocity.x = Mathf.Sqrt(2 * _jumpHeight * _gravity);
                   
                }

            }
            // jump code here
            if (_jumpPressed){
                _inputVelocity.y = Mathf.Sqrt(2 * _jumpHeight * _gravity);
                _slideInput = false;
            }
        }

        if (_inputVelocity.y < 0){
            _inputVelocity.y = -1* _gravity * _fallMultiplier;    
        } else{
            _inputVelocity.y -= _gravity * Time.fixedDeltaTime;    
        }
        _currentVelocity = _inputVelocity;
        _controller.Move(_currentVelocity * Time.fixedDeltaTime);
        _jumpPressed = false;
        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);

       


    }

    public void OnMove(InputValue input){
        _moveInput = input.Get<Vector2>();
    }

    public void OnJump(InputValue input){
        _jumpPressed = Mathf.Approximately(1, input.Get<float>());
    }

    public void OnSlide(InputValue input)
    {
        _slideInput = Mathf.Approximately(1, input.Get<float>());
    }
}