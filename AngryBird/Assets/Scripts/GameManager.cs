using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager Instance;

    public GameManager()
    {
        Instance = this;
    }


    // Start is called before the first frame update
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
            birds[0].enabled = true;
            birds[0].sj.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
