using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public BossEnemy BossEnemy;
    public Animator transition;


    public float transitionTime = 1f;

    void Update()
    {
        if (BossEnemy.health < 0)
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex +1));
    }

    IEnumerator LoadLevel(int LoadIndex)
    {
        transition.SetTrigger("NextLevel");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(LoadIndex);
    }
}
