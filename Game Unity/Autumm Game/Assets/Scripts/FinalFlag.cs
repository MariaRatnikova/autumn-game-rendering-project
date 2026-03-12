using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalFlag : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CompleteLevel();
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
