using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public GameObject pauseButton;

    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }



    public void Retry()
    {
        SceneManager.LoadScene(2);
    }

    public void Pause()
    {
        //Time.timeScale = 0;
        pauseButton.SetActive(false);
        gameObject.SetActive(true);
        animator.SetBool("isPause", true);
    }

    public void RePause()
    {
        //Time.timeScale = 1;
        animator.SetBool("isPause", false);
        gameObject.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(1);
    }

}
