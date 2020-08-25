using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent onReset;

    public static GameManager instance;
    public GameObject readyPanel;

    public Text scoreText;
    public Text bestScoreText;
    public Text messageText;

    public bool isRoundActive = false;

    private int score = 0;

    public ShooterRotator shooterRotator;
    public CamFollow cam;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        UpdateUI();
    }

    private void Start()
    {
        StartCoroutine(nameof(RoundRoutine));
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateBestScore();
        UpdateUI();
    }

    void UpdateBestScore()
    {
        if (GetBestScore() < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }

    int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore");
    }

    // Update is called once per frame
    void UpdateUI()
    {
        scoreText.text = $"Score : {score}";
        bestScoreText.text = $"Best Score : {PlayerPrefs.GetInt("BestScore")}";
    }

    public void OnBallDestroy()
    {
        UpdateUI();
        isRoundActive = false;
    }

    public void Reset()
    {
        score = 0;
        UpdateUI();

        //라운드를 다시 처음부터 시작하는 코드
        StartCoroutine(nameof(RoundRoutine));
    }

    private IEnumerator RoundRoutine()
    {
        //READY
        onReset.Invoke();

        readyPanel.SetActive(true);
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Idle);
        shooterRotator.enabled = false;

        isRoundActive = false;

        messageText.text = "READY~!";
        
        yield return new WaitForSeconds(3f);
        
        //PLAY
        isRoundActive = true;
        readyPanel.SetActive(false);
        shooterRotator.enabled = true;

        cam.SetTarget(shooterRotator.transform, CamFollow.State.Ready);

        while (isRoundActive)
        {


            yield return null;
        }

        //END
        readyPanel.SetActive(true);
        shooterRotator.enabled = false;

        messageText.text = "Wait for next Round";

        yield return new WaitForSeconds(3f);
        Reset();
    }
}
