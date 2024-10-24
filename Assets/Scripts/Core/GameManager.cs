using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 점수 표시용 UI
    /// </summary>
    ScoreText scoreTextUI;

    const string SaveFileName = "Save.json";

    DeadLine deadLine;

    Canvas mainCanvas;

    Droper droper;

    public ScoreText ScoreText
    {
        get
        {
            if (scoreTextUI == null)
            {
                scoreTextUI = FindAnyObjectByType<ScoreText>();
            }
            return scoreTextUI;
        }
    }

    public DeadLine DeadLineBase
    {
        get
        {
            if (!deadLine)
            {
                deadLine = FindAnyObjectByType<DeadLine>();
            }
            return deadLine;
        }
    }

    public Droper DroperBase
    {
        get
        {
            if (!droper)
            {
                droper = FindAnyObjectByType<Droper>();
            }
            return droper;
        }
    }

    /// <summary>
    /// ScoreText의 score를 확인하는 프로퍼티
    /// </summary>
    public int Score => ScoreText.Score;    // get만 있는 프로퍼티

    protected override void OnInitialize()
    {
        scoreTextUI = FindAnyObjectByType<ScoreText>();
        scoreTextUI?.OnInitialize();
        deadLine = FindAnyObjectByType<DeadLine>();
        droper = FindAnyObjectByType<Droper>();
        mainCanvas = FindAnyObjectByType<Canvas>();
    }

    /// <summary>
    /// 점수 추가하는 함수
    /// </summary>
    /// <param name="score">추가되는 점수</param>
    public void AddScore(int score)
    {
        ScoreText?.AddScore(score);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        mainCanvas.transform.GetChild(1).gameObject.SetActive(false);
        mainCanvas.transform.GetChild(2).gameObject.SetActive(false);
        mainCanvas.transform.GetChild(0).gameObject.SetActive(true);
        TextMeshProUGUI text = mainCanvas.transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>();
        text.text = scoreTextUI.Score.ToString();
    }
    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        mainCanvas = FindAnyObjectByType<Canvas>();

        Debug.Log("Restart Game");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Main");
        mainCanvas = FindAnyObjectByType<Canvas>();
    }

    public void ResetScore()
    {
        bool isSuccess = false;

        // Assets/Save 폴더에 있는 Save.json이라는 파일을 읽어서 랭킹 정보 덮어쓰기
        string path = $"{Application.dataPath}/Save/";
        if (Directory.Exists(path))
        {
            // 폴더가 있다
            string fullPath = $"{path}{SaveFileName}";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        if (!isSuccess)  // 로딩이 실패했으면
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);    // 폴더가 없으면 만든다.
            }
        }
    }
}
