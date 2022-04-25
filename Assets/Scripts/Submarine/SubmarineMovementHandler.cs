using System;
using System.Collections;
using SubNemesis.GamePlay;
using UnityEngine;

namespace SubNemesis.Submarine
{

    public class SubmarineMovementHandler : MonoBehaviour
    {
        [SerializeField] private float speed = 13f;
        [SerializeField] private float turnSpeed = 7f;
        [SerializeField] private float boostSpeed = 2000f;
        [SerializeField] private float dashCoolDown = 1f;


        private Rigidbody _rigidbody;
        private bool _isNotSpamming = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = false;
            _rigidbody.drag = 3f;
            _rigidbody.angularDrag = 4.5f;
        }


        public bool HandleDash(GameObject missleShot, GameObject regularShot, FindFeels feels, GameObject boostVFX,
            GameObject boosterTransform)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_isNotSpamming)
                {
                    SpamCheck();

                    missleShot.SetActive(false);
                    regularShot.SetActive(false);

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
                        _rigidbody.AddRelativeForce(Vector3.right * boostSpeed * xDirection);
                        _rigidbody.AddRelativeForce(Vector3.forward * boostSpeed * yDirection);
                    }
                    else

                    {
                        _rigidbody.AddRelativeForce(Vector3.forward * boostSpeed);
                    }

                    feels.PlayDashFeel();

                    var boost = Instantiate(boostVFX, boosterTransform.transform.position, transform.rotation,
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

        public void HandleMovement(FindGUIs GUIs)
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GUIs.TogglePause();
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

        public void HandleShoot(GameObject missleShot, GameObject regularShot)
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
                GetComponent<UbhShotCtrl>().StartShotRoutine();
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
    }
}