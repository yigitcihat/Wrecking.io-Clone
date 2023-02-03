using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isGrounded;
    private bool _isGameStart, _isGameFail, _isGameWin;
    const float FORWARD_SPEED = 15;
    const float TURN_SPEED = 320;
    public VariableJoystick Joystick;
    public Transform StoneBallParent;
    private void OnEnable()
    {
        EventManager.OnLevelStart.AddListener(()=>_isGameStart =true);
        EventManager.OpenWinPanel.AddListener(()=> _isGameWin =true);
        EventManager.OpenFailPanel.AddListener(() => _isGameFail = true);
    }
    private void OnDisable()
    {
        EventManager.OnLevelStart.RemoveListener(() => _isGameStart = true);
        EventManager.OpenWinPanel.RemoveListener(() => _isGameWin = true);
        EventManager.OpenFailPanel.RemoveListener(() => _isGameFail = true);

    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isGrounded = true;
    }

    void Update()
    {
        if (_isGrounded && _isGameStart & !_isGameFail & !_isGameWin)
        {
            if (Joystick.Horizontal != 0)
            {
                transform.Rotate(transform.up, Joystick.Horizontal * TURN_SPEED * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {

                StoneBallParent.transform.RotateAround(transform.position, Vector3.up, 720 * Time.deltaTime);

            }
        }

    }
    private void FixedUpdate()
    {
        //if (!GameManager.Instance.isGameStarted || GameManager.Instance.isGameOver)
        //{
        //    return;
        //}
        if (_isGrounded & _isGameStart & !_isGameFail & !_isGameWin)
        {
            _rigidbody.AddForce(transform.forward * FORWARD_SPEED * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            _isGrounded = false;
            _rigidbody.isKinematic = true;
            EventManager.OpenFailPanel.Invoke();
        }
    }
}
