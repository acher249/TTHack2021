using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPromptTextUpdate : MonoBehaviour
{

    [Header("PromptText")]
    public Text ErrorText;
    public GameObject ErrorPanel;

    // Update is called once per frame
    void Update()
    {
        // simple way to run ui updates for errors

        if (GameControl.control.CustomErrorException != "")
        {
            ErrorPanel.SetActive(true);
            ErrorText.text = GameControl.control.CustomErrorException;

            GameControl.control.CustomErrorException = "";
        }
    }
}
