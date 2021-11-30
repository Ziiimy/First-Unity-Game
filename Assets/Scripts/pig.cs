using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{
    public float MaxV = 10;
    public float MinV = 5;
    private SpriteRenderer sp;
    public Sprite hurted;
    public GameObject boom;
    public GameObject score_3000;

    public bool IsPig = false;
    // Start is called before the first frame update
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude >= MaxV)
        {
            Dead();
        }
        else if (collision.relativeVelocity.magnitude > MinV && collision.relativeVelocity.magnitude < MaxV)
        {
            sp.sprite = hurted; 
        }
    }
    void Dead()
    {
        if (IsPig == true)
        {
            GameManager._instance.pigs.Remove(this);
        }
        Destroy(gameObject);

        Instantiate(boom, transform.position, Quaternion.identity);
        GameObject go = Instantiate(score_3000, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(go, 1.5f);
    }
}
