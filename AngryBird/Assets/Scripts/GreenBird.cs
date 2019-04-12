using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird
{
    protected override void ShowSkill()
    {
        Vector3 speed = rb.velocity;
        speed.x *= -1;
        rb.velocity = speed;
    }
}
