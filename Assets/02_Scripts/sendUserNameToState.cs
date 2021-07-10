using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sendUserNameToState : MonoBehaviour
{
    public InputField usernameInputField;
    public GameObject AuthPanel;
    public GameObject topRightText;

    // Start is called before the first frame update
    public void senduserNameToState()
    {
        if (usernameInputField.text.Length >= 2)
        {
            GameControl.control.userName = usernameInputField.text;

            topRightText.SetActive(true);
            AuthPanel.SetActive(false);
            topRightText.transform.GetChild(0).GetComponent<Text>().text = usernameInputField.text;

        }
        else
        {
            Debug.Log("NEED TO ENTER USERNAME IN EMAIL FIELD");
        }

    }
}
