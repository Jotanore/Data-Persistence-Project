using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;


public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public TextMeshProUGUI bestScore;

    public string name;
    public string record;
    public int maxPunt;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;

        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        bestScore.text = record;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void EntradaNombre(string s)
    {
        name = s;
        Debug.Log(name);
    }

    [System.Serializable]
    class SaveData
    {
        public string highScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.highScore = record;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/record.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/record.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            record = data.highScore;
        }
    }
}
