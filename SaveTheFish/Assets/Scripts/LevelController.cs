using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    public static LevelController Current;
    public bool gameActive = true;
    public GameObject startMenu, gameMenu, gameOverMenu, finishMenu;
    public Text scoreText, finishScoreText, currentLevelText, nextLevelText;
    public TextMeshProUGUI aquareumCapacityText, currentFishText;
    [SerializeField] GameObject aquareumAll;
    [SerializeField] GameObject capacityImage;
    public int aquareumCapacityScore, currentFishScore;
    public Slider levelProgressBar;
    public float maxDistance;
    public GameObject finishLine;
    public int currentLevel = 0;
    private int score;
    [SerializeField] private GameObject shattaredAquareum;
    [SerializeField] private GameObject aquareum1;
    [SerializeField] private ParticleSystem particleEffect;
    [SerializeField] GameObject moneyImage;
    [SerializeField] List<GameObject> confettiList = new List<GameObject>();


    void Start()
    {
        Current = this;
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        aquareumCapacityScore = PlayerController.Current.aquareumcapacity1;
    }

    void Update()
    {
        if (gameActive)
        {
            PlayerController player = PlayerController.Current;
            float distance = finishLine.transform.position.z - PlayerController.Current.transform.position.z;
            levelProgressBar.value = 1 - (distance / maxDistance);

            ChangeFishScore();

        }
        if (PlayerController.Current.finished)
        {
            capacityImage.SetActive(false);
            aquareumAll.SetActive(false);
            moneyImage.SetActive(true);
            scoreText.gameObject.SetActive(true);
        }
    }

    public void StartLevel()
    {
        maxDistance = finishLine.transform.position.z - PlayerController.Current.transform.position.z;
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
        gameActive = true;

    }

    public void RestartLevel()
    {
        LevelLoader.Current.ChanceLevel(SceneManager.GetActiveScene().name);

    }

    public void LoadNextLevel()
    {
        
        if (currentLevel == 4)
        {

            PlayerPrefs.SetInt("currentLevel", currentLevel - 4);
            currentLevel -= 4;
            LevelLoader.Current.ChanceLevel("Level "  + currentLevel);
        }else
        {
            LevelLoader.Current.ChanceLevel("Level " + (currentLevel + 1));
        }
  
    }

    public void GameOver()
    {
        gameActive = false;
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        particleEffect.Play();
        aquareum1.SetActive(false);
        shattaredAquareum.SetActive(true);

        for (int i = 0; i < PlayerController.Current.fishList.Count; i++)
        {
            Destroy(PlayerController.Current.fishList[i]);
        }

    }

    public void FinishGame()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevel + 1);
        Debug.Log("current level : " + currentLevel);
        finishScoreText.text = score.ToString();
        gameActive = false;
        gameMenu.SetActive(false);
        finishMenu.SetActive(true);
        for (int i = 0; i < confettiList.Count; i++)
        {
            confettiList[i].SetActive(true);
        }

    }

    public void ChangeScore(int increment)
    {
        score += increment;
        scoreText.text = score.ToString();

    }
    public void ChangeFishScore()
    {
        currentFishScore = PlayerController.Current.fishList.Count;
        currentFishText.text = currentFishScore.ToString();
    }

    public void ChanceAquareumCapacity(int increment)
    {

        aquareumCapacityScore = increment;
        aquareumCapacityText.text = aquareumCapacityScore.ToString();
    }
}

