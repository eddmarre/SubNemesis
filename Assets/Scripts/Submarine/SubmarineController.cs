using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using RengeGames.HealthBars;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class SubmarineController : Health
{
    public SubmarineBaseState currentState;
    public SubmarineBaseState regularMovmentState = new SubmarineNormalState();
    public SubmarineBaseState DashState = new SubmarineDashState();
    public SubmarineBaseState DeathState = new SubmarineDeathState();


    [SerializeField] private GameObject missleShot;
    [SerializeField] private GameObject regualrShot;
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
        regualrShot.SetActive(false);
    }

    private void Update()
    {
        currentState.UpdateState(this);
        // Debug.Log($"{_health}", this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnterState(this, collision);
    }


    public bool DashHandler()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mmFeedbackDash.PlayFeedbacks();
            _rigidbody.AddRelativeForce(Vector3.forward * boostSpeed);
            var boost = Instantiate(boostVFX, boosterTransform.transform.position, Quaternion.identity,
                boosterTransform.transform);
            Destroy(boost, dashCoolDown);
            _boxCollider.enabled = false;
            return true;
        }

        return false;
    }

    public void ResetMovement()
    {
        StartCoroutine(ResetMovementControls());
    }

    private IEnumerator ResetMovementControls()
    {
        yield return _dashCoolDown;
        _boxCollider.enabled = true;
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
            _rigidbody.AddRelativeTorque(Vector3.down * turnSpeed * -Input.GetAxis("Horizontal"));
        }

        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddRelativeForce(Vector3.back * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddRelativeTorque(Vector3.up * turnSpeed * Input.GetAxis("Horizontal"));
        }
    }

    public void ShootHandler()
    {
        if (Input.GetKey(KeyCode.F) && !Input.GetMouseButton(0))
        {
            missleShot.SetActive(true);
            GetComponent<UbhShotCtrl>().StartShotRoutine(0f);
        }
        else if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.F))
        {
            regualrShot.SetActive(true);
            GetComponent<UbhShotCtrl>().StartShotRoutine(0f);
        }
        else
        {
            regualrShot.SetActive(false);
            missleShot.SetActive(false);
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
        Debug.Log($"{gameObject.name} is dead  ", this);
        SwitchState(DeathState);
        // onTakeDamageAction -= TakeDamage;
    }
}