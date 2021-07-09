using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
// this is for writing out binary files
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;


public class GameControl : MonoBehaviour
{
    public static GameControl control;

    [Header("Errors")]
    public string CustomErrorException = "";
    // add other data
    // add other data
    // add other data
    // add other data

    // SINGLETON PATTERN FOR DDOL
    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

    }
}