using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }


    private IEnumerator LoadWithDelay()
    {
        yield return new WaitForSeconds(loadDelay);
    }

    public void LoadMainMenu()
    {
        LoadWithDelay();
        SceneManager.LoadScene(0);
    }
}
