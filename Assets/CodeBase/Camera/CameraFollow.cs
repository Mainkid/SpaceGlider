using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float Distance;

        [SerializeField] private float OffsetY;

        [SerializeField] private Transform _following;

        private void LateUpdate()
        {
            if (_following == null)
                return;
            
            Vector3 position = _following.rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();
            
            transform.position = position;
        }

        public void Follow(GameObject following) =>
            _following = following.transform;


        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += OffsetY;
            return followingPosition;
        }
    }
}