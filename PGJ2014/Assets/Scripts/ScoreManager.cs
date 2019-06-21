using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    private int score = 0;
    private Text scoreText;
    public GameObject ScorePrefab;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddScore(int addValue)
    {
        score += addValue;
        scoreText.text = score.ToString("D7");
        ScorePrefab.AddComponent<Score>().score = score;
    }
}
