using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    int highScore;

    const string SaveFileName = "Save.json";

    void Awake()
    {
        scoreText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        Load();
        GameManager.Instance.DeadLineBase.gameOver += () => UpdateBestScore(GameManager.Instance.Score);
    }

    void SetBestScore(int score)
    {
        scoreText.text = score.ToString();
    }

    void UpdateBestScore(int score)
    {
        if (highScore < score)
        {
            highScore = score;
            Save();
        }
        RefreshRankLines();
    }

    void Save()
    {
        SaveScore saveScore = new SaveScore();
        saveScore.bestScore = highScore;
        string jsonText = JsonUtility.ToJson(saveScore);

        string path = $"{Application.dataPath}/Save/";
        if (!Directory.Exists(path))         
        {
            Directory.CreateDirectory(path); 
        }

        File.WriteAllText($"{path}{SaveFileName}", jsonText);
    }

    void Load()
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
                // 파일도 있다.
                string jsonText = File.ReadAllText(fullPath);
                SaveScore loadedScore = JsonUtility.FromJson<SaveScore>(jsonText); // 데이터 변환
                highScore = loadedScore.bestScore;

                isSuccess = true;
            }
        }

        if (!isSuccess)  // 로딩이 실패했으면
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);    // 폴더가 없으면 만든다.
            }
        }
        RefreshRankLines();
    }
    void RefreshRankLines()
    {
        SetBestScore(highScore);
    }
}
