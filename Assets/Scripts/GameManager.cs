using UnityEngine;
using TMPro;

public enum GameState { Ready, Playing, GameOver }

public class GameManager : MonoBehaviour
{
    public GameState State { get; private set; } = GameState.Ready;

    [Header("Refs")]
    public PlayerController player;
    public Spawner spawner;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    [Header("Score")]
    public float score;
    public float scorePerSecond = 5f;

    [Header("UI Scaling")]
    public Vector2 scoreFontSizeRange = new Vector2(36f, 96f);
    public Vector2 gameOverFontSizeRange = new Vector2(48f, 160f);

    private bool scoringEnabled;

    void Awake()
    {
        ApplyTextAutoSizing(scoreText, scoreFontSizeRange);
        ApplyTextAutoSizing(gameOverText, gameOverFontSizeRange);
    }

    void Start()
    {
        SetUI();
    }

    void Update()
    {
        switch (State)
        {
            case GameState.Ready:
                if (Input.anyKeyDown) StartGame();
                break;
            case GameState.Playing:
                if (!scoringEnabled) break;
                score += scorePerSecond * Time.deltaTime;
                SetUI();
                break;
            case GameState.GameOver:
                if (Input.anyKeyDown) Restart();
                break;
        }
    }

    public void StartGame()
    {
        State = GameState.Playing;
        scoringEnabled = true;
        score = 0f;
        if (spawner) spawner.StartSpawn();
        if (gameOverText) gameOverText.gameObject.SetActive(false);
        SetUI();
    }

    public void GameOver()
    {
        if (State != GameState.Playing) return;

        State = GameState.GameOver;
        scoringEnabled = false;
        if (spawner) spawner.StopSpawn();
        if (gameOverText)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over\n按任意键重来";
        }
        SetUI();
    }

    private void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void SetUI()
    {
        if (scoreText) scoreText.text = $"Score: {Mathf.FloorToInt(score)}";
    }

    private void ApplyTextAutoSizing(TextMeshProUGUI target, Vector2 sizeRange)
    {
        if (!target) return;

        float min = Mathf.Max(1f, Mathf.Min(sizeRange.x, sizeRange.y));
        float max = Mathf.Max(min, Mathf.Max(sizeRange.x, sizeRange.y));

        target.enableAutoSizing = true;
        target.fontSizeMin = min;
        target.fontSizeMax = max;
    }
}