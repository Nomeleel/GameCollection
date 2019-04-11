using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isClick = false;
    [HideInInspector]
    public SpringJoint2D sj;
    private Rigidbody2D rb;
    private TestMyTrail trail;
    private bool isCanMove;

    public Transform leftTransform;
    public Transform rightTransform;
    public float MaxDistance = 2.2f;
    public LineRenderer left;
    public LineRenderer right;
    public GameObject boom;
    public float smooth = 0.618f;
    public AudioClip birdSelectAudio;
    public AudioClip birdFlyAudio;
    public AudioClip birdCollisionAudio;

    private void Awake()
    {
        sj = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<TestMyTrail>();
        isCanMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick)
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
        }

        CameraFollowUp();
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
            isClick = true;
            rb.isKinematic = true;
            PlayAudio(birdSelectAudio);
        }
    }

    private void OnMouseUp()
    {
        isClick = false;
        isCanMove = false;
        rb.isKinematic = false;
        Invoke("Fly", 0.2f);
        PlayAudio(birdFlyAudio);
        left.enabled = false;
        right.enabled = false;
    }

    private void Fly()
    {
        sj.enabled = false;
        trail.StartTrail();
        Invoke("Dead", 3.5f);
    }

    private void Dead()
    {
        GameManager.Instance.birds.Remove(this);
        GameManager.Instance.NextBird();
        trail.ClearTrail();
        Destroy(gameObject);
        //Instantiate(boom, transform.position, Quaternion.identity);
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

}
