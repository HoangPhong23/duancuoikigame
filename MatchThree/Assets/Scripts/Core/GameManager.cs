using System;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Tools;

namespace Core
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        [Header("LEVEL DATA")]
        [SerializeField] private List<LevelData> _allLevels;
        public int TotalLevels => _allLevels != null ? _allLevels.Count : 0;

        private TextMeshProUGUI _scoreText;
        private TextMeshProUGUI _moveText;
        private TextMeshProUGUI _targetScoreText;
        private UIManager _uiManager;

        [Header("SHOP DATA")]
        public int money = 1000;
        public int lives = 0;

        private int _maxAllowedMove;
        private int _score;
        private int _targetScore;
        private Vector2Int _dimensions;
        private MatchableGrid _grid;
        private bool _isGameOver = false;

        protected override void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            base.Awake();
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    Debug.Log("Scene Loaded: " + scene.name);

    // 🔥 AUTO FIND UI
    AutoFindUI();

    if (scene.name == "SampleScene")
    {
        // 🔥 TRỪ 1 LIFE KHI VÀO GAME
        if (lives > 0)
        {
            lives--;
            Debug.Log("Lives còn lại: " + lives);
        }

        SetupGame();
    }
}

        // ================== AUTO FIND UI ==================
        private void AutoFindUI()
        {
            // tìm theo tên (giống ảnh bạn gửi)
            _scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
            _moveText = GameObject.Find("MoveCounter")?.GetComponent<TextMeshProUGUI>();
            _targetScoreText = GameObject.Find("TargetScoreText")?.GetComponent<TextMeshProUGUI>();

            _uiManager = FindObjectOfType<UIManager>();

            Debug.Log("Auto UI Connected: "
                + (_scoreText != null ? "Score ✔ " : "Score ❌ ")
                + (_moveText != null ? "Move ✔ " : "Move ❌ "));
        }

        private void UpdateUI()
        {
            if (_scoreText != null)
                _scoreText.text = _score.ToString("D5");

            if (_moveText != null)
                _moveText.text = _maxAllowedMove.ToString();

            if (_targetScoreText != null)
                _targetScoreText.text = "Target: " + _targetScore;
        }

        private void SetupGame()
        {
            Debug.Log("Setup Game Scene");

            _isGameOver = false;

            LevelData currentLevel = null;

            if (_allLevels != null && _allLevels.Count > 0)
            {
                int levelIdx = Mathf.Clamp(LevelManager.SelectedLevelIndex, 0, _allLevels.Count - 1);
                currentLevel = _allLevels[levelIdx];
            }

            if (currentLevel != null)
            {
                _dimensions = currentLevel.dimensions;
                _maxAllowedMove = currentLevel.maxMoves;
                _targetScore = currentLevel.targetScore;
            }
            else
            {
                _dimensions = new Vector2Int(8, 8);
                _maxAllowedMove = 30;
                _targetScore = 5000;
            }

            _grid = MatchableGrid.Instance as MatchableGrid;

            if (_grid == null)
            {
                Debug.LogError("MatchableGrid not found!");
                return;
            }

            _grid.transform.position = Vector3.zero;
            _grid.InitializeGrid(_dimensions);
            _grid.PopulateGrid();

            _score = 0;

            // 🔥 update UI sau khi setup
            UpdateUI();
        }

        // ================== SHOP ==================
        public bool BuyLife(int price, int amount)
        {
            if (money >= price)
            {
                money -= price;
                lives += amount;
                return true;
            }
            return false;
        }

        // ================== GAME ==================
        public void IncreaseScore(int value)
        {
            if (_isGameOver) return;

            _score += value;
            UpdateUI();

            if (_score >= _targetScore)
                WinGame();
        }

        public bool CanMoveMatchables()
        {
            return _maxAllowedMove > 0 && !_isGameOver;
        }

        public void DecreaseMove()
        {
            if (_isGameOver) return;

            _maxAllowedMove--;
            UpdateUI();

            if (_maxAllowedMove <= 0)
            {
                if (_score >= _targetScore)
                    WinGame();
                else
                    LoseGame();
            }
        }

        private void WinGame()
        {
            if (_isGameOver) return;
            _isGameOver = true;

            Debug.Log("WIN");

            if (_uiManager != null)
                _uiManager.ShowWinPanel(_score);
        }

        private void LoseGame()
        {
            if (_isGameOver) return;
            _isGameOver = true;

            Debug.Log("LOSE");

            if (_uiManager != null)
                _uiManager.ShowLosePanel(_score);
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}