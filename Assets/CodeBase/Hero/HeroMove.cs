using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Camera;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent (typeof(Rigidbody2D))]
    [RequireComponent (typeof(BoxCollider2D))]
    public class HeroMove : MonoBehaviour, IGravitable
    {
        public Rigidbody2D Rb
        {
            get { return _rb; }
        }

        public Transform TransformProperty
        {
            get { return transform; }
        }

        [SerializeField] private float maxVelocityMagnitude;
        [SerializeField] private float forceFrictionCoef;
        [SerializeField] private float speed;

        private Rigidbody2D _rb;
        private UnityEngine.Camera _camera;
        private IInputService _inputService;
        private float _totalAngle = 0;
        private float _prevAngle = 0;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _rb = GetComponent<Rigidbody2D>();
            _inputService = AllServices.Container.Single<IInputService>();
        }

       


        private void FixedUpdate()
        {
            ApplyFrictionToForce();
            if (_inputService.isMoveForwardButtonUp())
                Move(CalculateForwardVector() * speed);
        }

        private void Move(Vector2 dirVector)
        {
            if (_rb.velocity.magnitude < maxVelocityMagnitude)
                _rb.AddForce(dirVector, ForceMode2D.Impulse);
        }

        public Vector2 CalculateForwardVector()
        {
            float rotationCos = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
            float rotationSin = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
            return new Vector2(-rotationSin, rotationCos);
        }

        private void ApplyFrictionToForce() =>
            _rb.velocity *= 1 - forceFrictionCoef;


        public void AddRelativeForce(Vector3 force)
        {
            _rb.AddForce(force);
        }

        public void AddRotationForce(float angle)
        {
            if (_prevAngle <-265&& angle > 85)
                _totalAngle += 360;
            else if (_prevAngle > 85 && angle < -265)
                _totalAngle -= 360;
            
            
            _prevAngle = angle;
            angle -= _totalAngle;

            float deltaRotation = (_rb.rotation-angle)* Time.fixedDeltaTime;
            _rb.MoveRotation(Convert.ToInt32(_rb.rotation-deltaRotation));
            
        }
    }
}