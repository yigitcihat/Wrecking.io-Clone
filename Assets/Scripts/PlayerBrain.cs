using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isGrounded, isTurnBallPower;
    private bool _isGameStart, _isGameFail, _isGameWin;
    private LineRenderer ropeLine;
    const float FORWARD_SPEED = 15;
    const float TURN_SPEED = 320;
    public VariableJoystick Joystick;
    public Transform StoneBallParent;

    private void OnEnable()
    {
        EventManager.OnLevelStart.AddListener(() => _isGameStart = true);
        EventManager.OpenWinPanel.AddListener(() => _isGameWin = true);
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
        ropeLine = transform.parent.GetComponentInChildren<LineRenderer>();
        StoneBallParent = transform.parent.GetComponentInChildren<StoneBallCollision>().transform.parent;
    }

    void Update()
    {
        if (_isGrounded && _isGameStart & !_isGameFail & !_isGameWin)
        {
            if (Joystick.Horizontal != 0)
            {
                transform.Rotate(transform.up, Joystick.Horizontal * TURN_SPEED * Time.deltaTime);
            }
            if (isTurnBallPower)
            {
                StoneBallParent.transform.RotateAround(transform.position, Vector3.up, 720 * Time.deltaTime);

            }
        }

    }
    private void FixedUpdate()
    {
       
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
        else if (other.gameObject.layer == LayerMask.NameToLayer("PowerBox"))
        {
            PowerActivate(other);
        }
    }
    public void PowerActivate(Collider other)
    {
        Debug.Log("PowerUp");
        isTurnBallPower = true;
        StoneBallParent.GetComponentInChildren<Rigidbody>().isKinematic = true;
        StoneBallParent.GetComponentInChildren<ConfigurableJoint>().connectedBody = null;
        ropeLine.enabled = false;
        Destroy(other.gameObject);
        StartCoroutine(WaitAndClosePowerUp());
    }
    IEnumerator WaitAndClosePowerUp()
    {
        yield return new WaitForSeconds(3f);
        isTurnBallPower = false;
        ropeLine.enabled = true;
        StoneBallParent.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
        StoneBallParent.GetComponentInChildren<Rigidbody>().isKinematic = false;
        StoneBallParent.GetComponentInChildren<ConfigurableJoint>().connectedBody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().WakeUp();


    }
}
