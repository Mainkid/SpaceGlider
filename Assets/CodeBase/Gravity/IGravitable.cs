using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGravitable
{
    public Rigidbody2D Rb { get; }
    public Transform TransformProperty { get; }
    public void AddRotationForce(float angle);
    public void AddRelativeForce(Vector3 force);
    public Vector2 CalculateForwardVector();
}