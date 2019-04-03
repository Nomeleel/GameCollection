using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxSpeed = 10;
    public float minSpeed = 5;

    private SpriteRenderer spriteRenderer;
    public Sprite hurt;
    public GameObject boom;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude >= maxSpeed)
        {
            Dead();
        } 
        else if (collision.relativeVelocity.magnitude < maxSpeed && collision.relativeVelocity.magnitude > minSpeed)
        {
            spriteRenderer.sprite = hurt;
        }
        else
        {

        }
    }

    private void Dead()
    {
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
    }

}
