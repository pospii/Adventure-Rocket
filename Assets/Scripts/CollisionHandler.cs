using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float loadDelay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    public Text healthText;
    public int HP = 5;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Cursor.visible = false;
    }

    void Update()
    {
        HP = PlayerPrefs.GetInt("Health", 0);
        healthText.text = "Životy " + HP.ToString();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }
        if (other.gameObject.tag == "Friendly")
        {
            return;
        }  
        else if (other.gameObject.tag == "Finish")
        {
            Success();
        }
        else
        {
            if (HP > 0)
                {
                    ReduceHealth();
                    Crash();
                }
            if (HP <= 0)
                {
                    Crash();
                    Invoke("Reset", loadDelay);
                    Debug.Log("Health " + HP.ToString());
                }
        }
    }

    void Crash()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("Reload", loadDelay);
    }

    void Success()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNext", loadDelay);

    }

    void LoadNext()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        SceneManager.LoadScene(nextScene);
        if (nextScene == 4)
        {
            HP = 5;
            PlayerPrefs.SetInt("Health", HP);
            Cursor.visible = true;
        }
    }

    void Reload()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void ReduceHealth()
    {
        HP = PlayerPrefs.GetInt("Health", 0);
        HP--;
        PlayerPrefs.SetInt("Health", HP);
        Debug.Log("Health " + HP.ToString());
    }

    void Reset()
    {
        HP = 5;
        PlayerPrefs.SetInt("Health", HP);
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }
}
