using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class AdWarning : MonoBehaviour
{
    public GameObject warningPanel;
    public TextMeshPro timerText;
    public float waitTime = 3f;

    void Start()
    {
        warningPanel.SetActive(false);
    }

    public void ShowWarning()
    {
        warningPanel.SetActive(true);
        StartCoroutine(Countdown());     
    }

    IEnumerator Countdown()
    {
        float timeLeft = waitTime;
        while (timeLeft > 0)
        {
            timerText.text = "Реклама через " + Mathf.CeilToInt(timeLeft) + "...";
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
        warningPanel.SetActive(false);
    }
}
