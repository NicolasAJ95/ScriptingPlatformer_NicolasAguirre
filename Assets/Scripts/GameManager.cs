using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static GameManager instance = null;

    [SerializeField ]
    public int localPoints;
    [SerializeField ]
    public int worldPoints;

    public GameObject winCanvas;

    public GameObject guiCanvas;

    public Text scoreText;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            print("Objeto duplicado eliminado.");
        }
        else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }

        winCanvas.SetActive(false);

        guiCanvas.SetActive(true);
    }

    public void LoadNextLevel()
    {
        worldPoints += localPoints;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
        localPoints = 0;
        winCanvas.SetActive(false);

        guiCanvas.SetActive(true);
    }

    public void WinLevel()
    {
        Time.timeScale = 0;
        winCanvas.SetActive(true);
        guiCanvas.SetActive(false);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        localPoints = 0;
        winCanvas.SetActive(false);

        guiCanvas.SetActive(true);
    }

    public void MutantKilled()
    {
        localPoints += 1;
    }

    public void AlienKilled()
    {
        localPoints += 2;
        
    }
    // Use this for initialization
    void Start () {
        localPoints = 0;
        
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "SCORE: " + localPoints;
    }
}
