using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isClick = false;
    private SpringJoint2D sj;
    private Rigidbody2D rb;

    public Transform leftTransform;
    public Transform rightTransform;
    public float MaxDistance = 2.2f;
    public LineRenderer left;
    public LineRenderer right;

    private void Awake()
    {
        sj = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
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
    }

    private void OnMouseDown()
    {
        isClick = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isClick = false;
        rb.isKinematic = false;
        Invoke("Fly", 0.2f);
    }

    private void Fly()
    {
        sj.enabled = false;
    }

    private void DrawLine()
    {
        left.SetPosition(0, leftTransform.position);
        left.SetPosition(1, transform.position);

        right.SetPosition(0, rightTransform.position);
        right.SetPosition(1, transform.position);
    }

}
