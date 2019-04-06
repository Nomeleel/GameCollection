using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager Instance;
    private Vector3 originPos;

    private void Awake()
    {
        Instance = this;
        if (birds.Count > 0)
        {
            originPos = birds[0].transform.position;
        }
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            birds[i].enabled = (i == 0 ? true : false);
            birds[i].sj.enabled = (i == 0 ? true : false);
        }
    }

    public void NextBird()
    {
        if (birds.Count > 0)
        {
            if (pigs.Count > 0)
            {
                birds[0].transform.position = originPos;
                birds[0].enabled = true;
                birds[0].sj.enabled = true;
            }
            else
            {
                // Nothing to do.
            }
        }
        else
        {
            // Nothing to do.
        }
    }

    void Update()
    {
        // Nothing to do.
    }
}
