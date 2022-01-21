using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviourSingleton<GameManager>
{
    //Score manager
    [SerializeField] private TMP_Text scoreText;
    public int score;
    public int Score
    {
        get { return score; }
        set 
        { 
            score = value;
            scoreText.text = "Score : " + score.ToString();
        }
    }
    
    //Timer Manager
    [SerializeField] private TMP_Text timerText;
    private float timerLeft = 120f;
    [SerializeField] private bool timerIsRunning = false;

    //Object Manager
    private ObjectSpawner objectSpawner;

    void Awake()
    {
        objectSpawner = GetComponent<ObjectSpawner>();

        SpawnNewObject();

        score = 0;
    }

    void Update()
    {
        if(timerLeft > 0 && timerIsRunning)
        {
            timerLeft -= Time.deltaTime;
            FormatTime(timerLeft);
        }
    }

    public void SpawnNewObject()
    {
        objectSpawner.SpawnObject();
    }

    private void FormatTime( float timeToDisplay )
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);}
}
