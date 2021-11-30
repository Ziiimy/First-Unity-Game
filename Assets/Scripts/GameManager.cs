using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<pig> pigs;

    public static GameManager _instance;
    private Vector3 OriginPos;

    private void Awake()
    {
        _instance = this;
        if (birds.Count > 0)
        {
            OriginPos = birds[0].transform.position;
        }
    }

    private void Start()
    {
        Initialized();
    }
    private void Initialized()
    {
        for (int i = 0;i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[i].enabled = true;
                birds[i].transform.position = OriginPos;
                foreach (SpringJoint2D sp in birds[i].sps)
                {
                    sp.enabled= true;
                }
            }
            else
            {
                birds[i].enabled = false;
                foreach (SpringJoint2D sp in birds[i].sps)
                {
                    sp.enabled = false;
                }
            }
        }
    }
    public void NextBird()
    {
        if(pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                Initialized();
            }
            else
            {

            }
        }
        else
        {

        }
    }
}
