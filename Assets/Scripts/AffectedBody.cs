using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectedBody:MonoBehaviour
{
    protected Rigidbody2D rb;
    protected float forceFrictionCoef = 0.005f;

    public Rigidbody2D Rb
    {
        get
        {
            return rb;
        }
    }

    public Transform TransformProperty { get; }

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void AddRotationForce(float angleCos, float maxAngularVelocity)
    {
        if (rb.angularVelocity <= maxAngularVelocity)
        {
            rb.AddTorque(angleCos);
            rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, 0, maxAngularVelocity);
        }
    }


    protected void AddFrictionToForce()
    {
        rb.velocity *= 1 - forceFrictionCoef;
        //rb.angularVelocity *= 1 - forceFrictionCoef;
    }

    public void AddRelativeForce(Vector3 force)
    {
        rb.AddForce(force);
    }

    public Vector2 CalculateForwardVector()
    {
       

        float rotationCos = Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles).z);
        float rotationSin = Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles).z);

        return new Vector2(-rotationSin, rotationCos);

    }
    
}
