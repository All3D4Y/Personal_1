using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleScore : MonoBehaviour
{
    const string SaveFileName = "Save.json";

    TextMeshProUGUI score;

    void OnEnable()
    {
        score = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        Load();
    }

    void Update()
    {
        
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
                score.text = loadedScore.bestScore.ToString();

                isSuccess = true;
            }
            else
            {
                score.text = "00000";
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
