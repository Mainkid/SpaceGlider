using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AffectedBody
{

    private void FixedUpdate()
    {
        CheckUserInput();
        AddFrictionToForce();
    }

    private void CheckUserInput()
    {
       

        if (Input.GetKey(KeyCode.Space))
        {
            if (rb.velocity.magnitude<3)
                rb.AddForce(CalculateForwardVector());
        }
    }

   

}
