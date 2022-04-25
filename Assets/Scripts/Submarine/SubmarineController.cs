using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using RengeGames.HealthBars;
using SubNemesis.GamePlay;
using SubNemesis.States;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace SubNemesis.Submarine
{
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

        [SerializeField] private float dashCoolDown = 1f;

        [SerializeField] private UltimateCircularHealthBar healthBar;

        private FindGUIs _guIs;
        private FindFeels _feels;
        private SubmarineMovementHandler _movementHandler;
        private WaitForSeconds _dashCoolDown;

        private void Awake()
        {
            _guIs = gameObject.AddComponent<FindGUIs>();
            _feels = gameObject.AddComponent<FindFeels>();
            _movementHandler = gameObject.AddComponent<SubmarineMovementHandler>();

            healthBar = FindObjectOfType<UltimateCircularHealthBar>();

            var amount = _health / 10f;
            healthBar.SetSegmentCount(amount);

            _dashCoolDown = new WaitForSeconds(dashCoolDown);

            missleShot.SetActive(true);
            regularShot.SetActive(true);
        }

        private void Start()
        {
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
            return _movementHandler.HandleDash(missleShot, regularShot, _feels, boostVFX, boosterTransform);
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
            _movementHandler.HandleMovement(_guIs);
        }

        public void ShootHandler()
        {
            _movementHandler.HandleShoot(missleShot, regularShot);
        }

        void OnDisable()
        {
            GetComponent<UbhShotCtrl>().StopShotRoutineAndPlayingShot();
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
                Damage(damageAmount);
                if (_health <= 0)
                {
                    Die();
                }
            }
        }

        private void Damage(float damageAmount)
        {
            _health -= damageAmount;

            var removeAmount = damageAmount / 10f;
            healthBar.AddRemoveSegments(removeAmount);

            _feels.PlayDamageFeel();

            var hitVFX = Instantiate(playerHitVFX, transform.position, Quaternion.identity);
            Destroy(hitVFX, 3f);
        }

        private void Die()
        {
            onTakeDamageAction -= TakeDamage;
            _guIs.GameOver();
            Cursor.lockState = CursorLockMode.None;
            SwitchState(DeathState);
        }
    }
}