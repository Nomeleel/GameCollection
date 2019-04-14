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
    public GameObject score;
    public bool isPig = true;
    public AudioClip collisionAudio;
    public AudioClip deadAudio;

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
            PlayAudio(collisionAudio);  
        }
        else
        {
        }
    }

    public void Dead()
    {
        if (isPig)
        {
            GameManager.Instance.pigs.Remove(this);
        }
        PlayAudio(deadAudio);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameObject tempScore = Instantiate(score, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(tempScore, 2);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }

}
