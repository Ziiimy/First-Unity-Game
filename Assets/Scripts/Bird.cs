using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool IsClick;
    public float MaxDis;

    public Transform LeftPos;
    public Transform RightPos;
    public LineRenderer LLine;
    public LineRenderer RLine;

    [HideInInspector]
    public Component[] sps,rgs;
    [HideInInspector]
    public SpringJoint2D sp;
    private Rigidbody2D rg;

    public GameObject boom;
    private BirdTrail birdtrail;
    // Start is called before the first frame update
    private void Awake()
    {
        sps = GetComponents<SpringJoint2D>();
        rgs = GetComponents<Rigidbody2D>();
        birdtrail = GetComponent<BirdTrail>();
    }
    private void OnMouseDown()
    {
        IsClick = true;
        foreach(Rigidbody2D rg in rgs)
        {
            rg.isKinematic = true;
        }
    }
    public void OnMouseUp()
    {
        IsClick = false;
        foreach (Rigidbody2D rg in rgs)
        {
            rg.isKinematic = false;
            
        }

        Invoke("Fly",0.2f);
        LLine.enabled = false;
        RLine.enabled = false;
    }
    // Update is called once per frame
    private void Update()
    {
        if (IsClick == true)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position -= new Vector3(0, 0,Camera.main.transform.position.z);

            if (Vector3.Distance(RightPos.position,transform.position) > MaxDis)
            {
                Vector3 Pos = (transform.position - RightPos.position).normalized;
                Pos = Pos * MaxDis;
                transform.position = Pos + RightPos.position;
            }

            WLine();
        }
    }
    void Fly()
    {
        foreach (SpringJoint2D sp in sps)
        {
            sp.enabled = false;
            birdtrail.Starttrail();
            Invoke("Next", 5);
        }
    }
    void WLine()
    {
        LLine.enabled = true;
        RLine.enabled = true;
        LLine.SetPosition(0, LeftPos.position);
        LLine.SetPosition(1, transform.position);
        RLine.SetPosition(0, RightPos.position);
        RLine.SetPosition(1, transform.position);
    }

    public void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        birdtrail.Endtrail();
    }
}
