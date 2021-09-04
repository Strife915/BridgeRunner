using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore instance;
    float _scoreTimer = 0;

    public int score;
    public Text scoreText;
    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if(PlayerFinishEvents.instance._isFinished && LevelController.instance.gameActive)
        {
            _scoreTimer -= Time.deltaTime;
            if(_scoreTimer <0)
            {
                _scoreTimer = 0.3f;
                ChangeScore(1);
            }
        }
    }

    public void ChangeScore(int increment)
    {
        score += increment;
        scoreText.text = score.ToString();
    }
}
