using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager Instance;
    public GameObject win;
    public GameObject loser;
    public List<GameObject> starts;

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
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                birds[0].transform.position = originPos;
                birds[0].enabled = true;
                birds[0].sj.enabled = true;
            }
            else
            {
                loser.SetActive(true);
                //win.SetActive(true);
                //ShowStarts();
            }
        }
        else
        {
            win.SetActive(true);
        }
    }

    public void ShowStarts()
    {
        StartCoroutine("Show");
    }

    IEnumerable Show()
    {
        for (int i = 0; i < starts.Count; i++)
        {
            yield return new WaitForSeconds(0.2f);
            starts[i].SetActive(true);
        }
    }

    public void RePlay()
    {
        SceneManager.LoadScene(2);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        // Nothing to do.
    }
}
