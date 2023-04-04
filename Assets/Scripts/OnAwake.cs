using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAwake : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        PlayerPrefs.SetInt("Health", 5);
    }
}
