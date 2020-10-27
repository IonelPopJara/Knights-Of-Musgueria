using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public TMP_InputField usernameField;
    public TMP_InputField ipAdress;

    public GameObject knights;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        ipAdress.text = "127.0.0.1";
    }

    public void ConnectToServer()
    {
        knights.SetActive(false);
        Client.instance.ip = ipAdress.text;
        startMenu.SetActive(false);
        usernameField.interactable = false;
        Client.instance.ConnectToServer();

        TimeManager.gameStarted = true;
    }
}