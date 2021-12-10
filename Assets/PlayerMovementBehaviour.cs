//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;


////, PlayerInput.IPlayerActions
//public class PlayerMovementBehaviour : MonoBehaviour
//{
//    private Vector2 _moveInput = Vector2.zero;
//    private bool _jumpPressed = false;
//    private bool _IsGrounded = false;

//    PlayerInput.PlayerActions _Pactions;

//    private bool _slideInput = false;

//    [SerializeField]
//    private PlayerInput _Input;

//    [SerializeField]
//    private Rigidbody m_Rigidbody;
//    [SerializeField]
//    private GameObject _IsGround;
//    [SerializeField]
//    private CharacterController _controller;
//    [SerializeField]
//    private float _moveSpeed = 9f;
//    [SerializeField]
//    private float _gravity = 9f;
//    [SerializeField]
//    private float _fallMultiplier = 1.5f;
//    [SerializeField]
//    private float _jumpHeight = 3f;

//    private Vector3 _currentVelocity = Vector3.zero;
//    private Vector3 _inputVelocity = Vector3.zero;

//    // Start is called before the first frame update
//    void Start(){
//        if (m_Rigidbody == null)
//        {
//            m_Rigidbody = GetComponent<Rigidbody>();
//        }

//        if(_Input == null)
//        {
//            _Input = GetComponent<PlayerInput>();
//        }
//    }

//    // Update is called once per frame
//    void FixedUpdate(){
//        _inputVelocity = Vector3.zero;
//        _inputVelocity.x = _moveInput.x * _moveSpeed;
//        _inputVelocity.y = _currentVelocity.y;
//        if (_IsGrounded == true)
//        {
//            _inputVelocity.y = 0;
//            if (_slideInput)
//            {
//                _inputVelocity.x = 50;
//            }

//            // jump code here
//            if (_jumpPressed){
//                _inputVelocity.y = Mathf.Sqrt(2 * _jumpHeight * _gravity);
//                _slideInput = false;
//            }
//        }

//        if (_inputVelocity.y < 0){
//            _inputVelocity.y = -1 * _gravity * _fallMultiplier;    
//        } else{
//            _inputVelocity.y -= _gravity * Time.fixedDeltaTime;    
//        }
//        _currentVelocity = _inputVelocity;
//        _currentVelocity = _currentVelocity * Time.fixedDeltaTime; //Left right movement
//       _jumpPressed = false;
//    }

// public void OnMove(Vector2 input){
//        _moveInput = input;
// }

// public void OnJump(float input){
//     _jumpPressed = Mathf.Approximately(1, input);
// }

// public void OnSlide(float input)
// {
//     _slideInput = Mathf.Approximately(1, input);
// }


//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.Equals(_IsGround)){
//            _IsGrounded = true;
//            Debug.Log("Ground");
//        }
//        else
//        {
//            _IsGrounded = false;

//            Debug.Log("Ground False");
//        }
//    }

//    //public void OnMove(InputAction.CallbackContext input)
//    //{
//    //    _moveInput = input.ReadValue<Vector2>();
//    //}

//    //public void OnJump(InputAction.CallbackContext input)
//    //{
//    //    float test = input.ReadValue<float>();
//    //    _jumpPressed = Mathf.Approximately(1, test);
//    //}

//    //public void OnSlide(InputAction.CallbackContext input)
//    //{
//    //    float test = input.ReadValue<float>();
//    //    _slideInput = Mathf.Approximately(1, test);
//    //    _slideInput = Mathf.Approximately(1, test);
//    //}

//    public void OnEnable()
//    {
//        if(_Input == null)
//        {
//            _Input = new PlayerInput();
//            _Input.Player.Enable();

//            _Pactions = _Input.Player;

//            _Pactions.Jump.performed += ctx=>OnJump(ctx.action.ReadValue<float>());

//            _Pactions.Slide.performed += ctx => OnSlide(ctx.action.ReadValue<float>());

//            _Pactions.Move.performed += ctx => OnMove(ctx.action.ReadValue<Vector2>());

//        }
//    }

//    public void OnDisable()
//    {
//        _Input.Player.Disable();
//    }
//}
