using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1366, 768, false);
        StartCoroutine(LoadGame());
    }


    public IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
