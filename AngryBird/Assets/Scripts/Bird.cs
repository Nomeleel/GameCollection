using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [HideInInspector]
    private TestMyTrail trail;
    private bool isCanMove;
    private bool canTriggerKill;
    private BirdStatus status;

    protected Rigidbody2D rb;

    public SpringJoint2D sj;
    public Transform leftTransform;
    public Transform rightTransform;
    public float MaxDistance = 2.2f;
    public LineRenderer left;
    public LineRenderer right;
    public GameObject boom;
    public float smooth = 3.14f;
    public AudioClip birdSelectAudio;
    public AudioClip birdFlyAudio;
    public AudioClip birdCollisionAudio;

    private void Awake()
    {
        sj = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TestMyTrail>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isCanMove = true;
        //status = BirdStatus.Heartbeat;
    }

    public void SetStatus(BirdStatus birdStatus)
    {
        status = birdStatus;
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case BirdStatus.Drag:
                {
                    transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

                    if (Vector3.Distance(transform.position, leftTransform.position) > MaxDistance)
                    {
                        Vector3 direction = (transform.position - leftTransform.position).normalized;
                        direction *= MaxDistance;
                        transform.position = direction + leftTransform.position;
                    }
                    DrawLine();
                    break;
                }
            case BirdStatus.Fly:
                ShowKillHandle();
                break;
        }

        CameraFollowUp();

    }

    private void ShowKillHandle()
    {
        if (canTriggerKill)
        {
            if (Input.GetMouseButton(0))
            {
                canTriggerKill = false;
                ShowSkill();
            }
        }

    }

    private void CameraFollowUp()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, 
            new Vector3(Mathf.Clamp(transform.position.x, 0, 15),
                Camera.main.transform.position.y,
                Camera.main.transform.position.z),
            smooth * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        if (isCanMove)
        {
            status = BirdStatus.Drag;
            rb.isKinematic = true;
            PlayAudio(birdSelectAudio);
        }
    }

    private void OnMouseUp()
    {
        status = BirdStatus.Fly;
        isCanMove = false;
        rb.isKinematic = false;
        Invoke("Fly", 0.2f);
        PlayAudio(birdFlyAudio);
        left.enabled = false;
        right.enabled = false;
    }

    private void Fly()
    {
        canTriggerKill = true;
        sj.enabled = false;
        trail.StartTrail();
        Invoke("Dead", 4.5f);
    }

    public void Heartbeat()
    {
        //StartCoroutine(HeartbeatHandle());
        //gameObject.AddComponent<Animator>();
    }

    IEnumerator HeartbeatHandle()
    {
        //System.Random random = new System.Random();
        //float time = random.Next(1, 2);
        yield return new WaitForSeconds(1.7f);
        if (transform.position.y <= -2.85f)
        {
            transform.position += new Vector3(0, 0.5f, 0);
        }
        yield return new WaitForSeconds(5.7f);
    }

    private void Dead()
    {
        GameManager.Instance.birds.Remove(this);
        GameManager.Instance.NextBird();
        trail.ClearTrail();
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
    }

    private void DrawLine()
    {
        left.enabled = true;
        right.enabled = true;

        left.SetPosition(0, leftTransform.position);
        left.SetPosition(1, transform.position);

        right.SetPosition(0, rightTransform.position);
        right.SetPosition(1, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        trail.ClearTrail();
        PlayAudio(birdCollisionAudio);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }

    protected virtual void ShowSkill()
    {
        
    }

}

public enum BirdStatus
{
    Heartbeat,
    Select,
    Drag,
    Fly,
    Skil,
    Dead
}
