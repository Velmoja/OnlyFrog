using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class DeathCounter : MonoBehaviour
{
    public static int DeathCount = 0;
    public TextMesh deathCountText;

    void Update()
    {
        deathCountText.text = "Помер: " + DeathCounter.DeathCount;
        YandexGame.NewLeaderboardScores("Dead", DeathCount);
    }



}
