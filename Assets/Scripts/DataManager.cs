using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string playerName;

    public HighScore highScore;

    public InputField nameInput;

    void Awake(){
        if(Instance != null){
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadHighScore();
    }

    public void StartGame(){
        string playerInput = nameInput.text;
        if(playerInput != null){
            playerInput = playerInput.Trim();
            if(playerInput.Length > 0){
                playerName = playerInput;
                SceneManager.LoadScene(1);
            }
        }
    }

    [System.Serializable]
    public class HighScore{
        public string name;
        public int score;
    }

    public void SaveHighScore(){
        string data = JsonUtility.ToJson(highScore);
        File.WriteAllText(Application.persistentDataPath + "/savedata.json", data);
    }

    public void LoadHighScore(){
        try{
            string data = File.ReadAllText(Application.persistentDataPath + "/savedata.json");
            if(data != null) {
                highScore = JsonUtility.FromJson<HighScore>(data);
            }
        } catch(System.Exception e){
            highScore = new HighScore();
            highScore.name = "nobody";
            highScore.score = 0;
        }
    }
}
