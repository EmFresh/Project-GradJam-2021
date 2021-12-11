using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerMov2 : MonoBehaviour
{
    private Vector2 _moveInput = Vector2.zero;
    private bool _jumpPressed = false;
    private bool _slideInput = false;
    private bool _fastfall = false;
    Color colorEnd = Color.black;


    private bool _parry1 = false;

    private bool _parry2 = false;

    private bool _parry3 = false;

    public Shake _shake;

    [SerializeField]
    private Animator _Anim;

    [SerializeField]
    private VisualEffect visualEffect;


    [SerializeField]
    private VisualEffect _NoteEffect, _NoteEffect2, _NoteEffect3;



    [SerializeField]
    private Material _ParryMat1;

    [SerializeField]
    private Material _ParryMat2;

    [SerializeField]
    private Material _ParryMat3;


    [SerializeField]
    public GameObject _parry1Obj;

    [SerializeField]
    public GameObject _parry2Obj;

    [SerializeField]
    public GameObject _parry3Obj;

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

    float duration = 1.0f;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<Renderer>();

        //Anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_parry1)
        {

            StartCoroutine(_shake.MyShake(.1f, 0.05f));

            Debug.Log("UpdateTest");
            _ParryMat1.SetColor("_Color", Color.red);

            _NoteEffect.Play();


        }
        else
        {
            _ParryMat1.SetColor("_Color", Color.white);
        }


        if (_parry2)
        {

            StartCoroutine(_shake.MyShake(.1f, 0.05f));

            Debug.Log("UpdateTest");
            _ParryMat2.SetColor("_Color", Color.blue);

            _NoteEffect2.Play();


        }
        else
        {
            _ParryMat2.SetColor("_Color", Color.white);
        }

        if (_parry3)
        {

            StartCoroutine(_shake.MyShake(.1f, 0.05f));

            Debug.Log("UpdateTest");


            _ParryMat3.SetColor("_Color", Color.yellow);
            _NoteEffect3.Play();
        }
        else
        {
            _ParryMat3.SetColor("_Color", Color.white);
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {



        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

        _inputVelocity = Vector3.zero;
        _inputVelocity.x = _moveInput.x * _moveSpeed;
        _inputVelocity.y = _currentVelocity.y;


        if (_controller.isGrounded)
        {
            _inputVelocity.y = 0;


            _Anim.SetBool("IsJumping", false);

            //visualEffect.SetFloat("YAxis", 0);

            if (_slideInput)
            {



                Vector3 _SlideVector = _inputVelocity;
                _inputVelocity = Vector3.Lerp(_SlideVector, _SlideVector * _moveSpeed, interpolationRatio);

                if (_jumpPressed)
                {

                    _Anim.SetBool("IsJumping", true);

                    _inputVelocity.x = Mathf.Sqrt(2 * _jumpHeight * _gravity);

                    visualEffect.SetFloat("YAxis", -20);


                }

            }
            // jump code here
            if (_jumpPressed)
            {
                _inputVelocity.y = Mathf.Sqrt(2 * _jumpHeight * _gravity);
                _slideInput = false;
            }
        }

        if (_inputVelocity.y < 0)
        {
            _inputVelocity.y = -1 * _gravity * _fallMultiplier;
        }
        else
        {
            _inputVelocity.y -= _gravity * Time.fixedDeltaTime;
        }
        _currentVelocity = _inputVelocity;
        _controller.Move(_currentVelocity * Time.fixedDeltaTime);
        _jumpPressed = false;
        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);




    }

    public void OnMove(InputValue input)
    {
        _moveInput = input.Get<Vector2>();

        Debug.Log("Moving");
    }

    public void OnJump(InputValue input)
    {
        _jumpPressed = Mathf.Approximately(1, input.Get<float>());

        Debug.Log("Jump");
    }


    public void OnSlide(InputValue input)
    {
        _slideInput = Mathf.Approximately(1, input.Get<float>());
    }

    public void OnParry1(InputValue input)
    {

        Debug.Log("OnParry1");
        _parry1 = Mathf.Approximately(1, input.Get<float>());




    }

    public void OnParry2(InputValue input)
    {
        Debug.Log("OnParry2");
        _parry2 = Mathf.Approximately(1, input.Get<float>());




    }

    public void OnParry3(InputValue input)
    {
        Debug.Log("OnParry3");
        _parry3 = Mathf.Approximately(1, input.Get<float>());




    }
}