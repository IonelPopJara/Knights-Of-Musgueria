using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public static TextMeshProUGUI timeText;
    public static TextMeshProUGUI winnerText;

    public static float currentTime;

    public static string winnerUserName;

    public static bool gameStarted;

    private void Awake()
    {
        gameStarted = false;

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        timeText = GameObject.Find("Time Text").GetComponent<TextMeshProUGUI>();
        winnerText = GameObject.Find("Winner Text").GetComponent<TextMeshProUGUI>();

        winnerText.text = "";
    }

    private void Update()
    {
        if (!gameStarted)
            return;

        timeText.text = $"Time left: {currentTime.ToString("0")}s";

        if(currentTime <= 0)
        {
            StartCoroutine(EndRound());
        }
    }

    private IEnumerator EndRound()
    {
        timeText.text = $"Time left: 0s";

        winnerText.text = $"{winnerUserName} ES EL MAS WEA DE TODOS!";

        yield return new WaitForSeconds(5f);

        winnerText.text = "";
    }
}
