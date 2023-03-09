using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // jinak by nám SceneManager nejel

public class Menu : MonoBehaviour
{
    public void Play() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Repeat()
    {
        SceneManager.LoadScene(1);
    }
}
