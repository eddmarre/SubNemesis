using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using RengeGames.HealthBars;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class SubmarineController : Health
{
    public SubmarineBaseState currentState;
    public SubmarineBaseState regularMovmentState = new SubmarineNormalState();
    public SubmarineBaseState DashState = new SubmarineDashState();
    public SubmarineBaseState DeathState = new SubmarineDeathState();


    [SerializeField] private GameObject missleShot;
    [SerializeField] private GameObject regularShot;
    [SerializeField] private GameObject playerHitVFX;
    [SerializeField] private GameObject boostVFX;
    [SerializeField] private GameObject boosterTransform;


    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float boostSpeed = 2000f;
    [SerializeField] private float dashCoolDown = 1f;
    [SerializeField] private UltimateCircularHealthBar healthBar;
    [SerializeField] private MMFeedback mmFeedbackDamage;
    [SerializeField] private MMF_Player mmFeedbackDash;


    private WaitForSeconds _dashCoolDown;
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private bool _isNotSpamming = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        var amount = _health / 10f;
        healthBar.SetSegmentCount(amount);

        _dashCoolDown = new WaitForSeconds(dashCoolDown);
    }

    private void Start()
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = false;
        _rigidbody.drag = 1f;
        _rigidbody.angularDrag = 1.5f;

        currentState = regularMovmentState;

        onTakeDamageAction += TakeDamage;

        missleShot.SetActive(false);
        regularShot.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnterState(this, collision);
    }


    public bool DashHandler()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isNotSpamming)
            {
                SpamCheck();
                var xDirection = Input.GetAxis("Horizontal");
                var yDirection = Input.GetAxis("Vertical");
                if (xDirection != 0)
                {
                    _rigidbody.AddRelativeForce(Vector3.right * boostSpeed * xDirection);
                }
                else if (yDirection < 0)
                {
                    _rigidbody.AddRelativeForce(Vector3.back * boostSpeed);
                }
                else if (xDirection != 0 && yDirection != 0)
                {
                    _rigidbody.AddRelativeForce(new Vector3(xDirection, 0, yDirection) * boostSpeed);
                }
                else

                {
                    _rigidbody.AddRelativeForce(Vector3.forward * boostSpeed);
                }

                mmFeedbackDash.PlayFeedbacks();

                var boost = Instantiate(boostVFX, boosterTransform.transform.position, Quaternion.identity,
                    boosterTransform.transform);
                Destroy(boost, dashCoolDown);

                return true;
            }
        }

        return false;
    }

    private void SpamCheck()
    {
        _isNotSpamming = false;
        StartCoroutine(ResetIsNotSpammingCheck());
    }

    IEnumerator ResetIsNotSpammingCheck()
    {
        yield return new WaitForSeconds(1f);
        _isNotSpamming = true;
    }

    public void ResetMovement()
    {
        StartCoroutine(ResetMovementControls());
    }

    private IEnumerator ResetMovementControls()
    {
        yield return _dashCoolDown;
        SwitchState(regularMovmentState);
    }

    public void MovementHandler()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddRelativeForce(Vector3.forward * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody.AddRelativeForce(Vector3.left * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddRelativeForce(Vector3.back * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddRelativeForce(Vector3.right * speed);
        }

        if (Input.GetAxis("Mouse X") > 0)
        {
            _rigidbody.AddRelativeTorque(Vector3.up * turnSpeed * Input.GetAxis("Mouse X"));
        }
        else if (Input.GetAxis("Mouse X") < 0)
        {
            _rigidbody.AddRelativeTorque(Vector3.down * turnSpeed * -Input.GetAxis("Mouse X"));
        }
    }

    public void ShootHandler()
    {
        if (Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(0))
        {
            missleShot.SetActive(true);
            GetComponent<UbhShotCtrl>().StartShotRoutine(0f);
        }

        if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(1))
        {
            regularShot.SetActive(true);
            GetComponent<UbhShotCtrl>().StartShotRoutine(0f);
        }

        if (Input.GetMouseButton(1) && Input.GetMouseButton(0))
        {
            regularShot.SetActive(false);
        }

        if (Input.GetMouseButtonUp(1))
        {
            missleShot.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            regularShot.SetActive(false);
        }
    }

    public void SwitchState(SubmarineBaseState state)
    {
        currentState = state;
    }

    public override void TakeDamage(float damageAmount)
    {
        if (_health <= 0)
        {
            Die();
        }
        else
        {
            _health -= damageAmount;
            var amount = damageAmount / 10f;
            healthBar.AddRemoveSegments(amount);
            mmFeedbackDamage.Play(transform.position);
            var hitVFX = Instantiate(playerHitVFX, transform.position, Quaternion.identity);
            Destroy(hitVFX, 3f);
            if (_health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        onTakeDamageAction -= TakeDamage;
        SwitchState(DeathState);
    }
}