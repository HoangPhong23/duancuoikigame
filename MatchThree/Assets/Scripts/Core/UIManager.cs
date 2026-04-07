using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        [Header("Win Panel")]
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private TextMeshProUGUI _winScoreText;
        [SerializeField] private Button _nextLevelButton;

        [Header("Lose Panel")]
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private TextMeshProUGUI _loseScoreText;

        public void ShowWinPanel(int score)
        {
            if (_winPanel == null) return;
            
            _winPanel.SetActive(true);
            if (_winScoreText != null) _winScoreText.text = "SCORE: " + score.ToString();

            // Chỉ hiện nút "Next" nếu còn màn tiếp theo
            if (_nextLevelButton != null)
            {
                bool hasNext = (LevelManager.SelectedLevelIndex + 1) < GameManager.Instance.TotalLevels;
                _nextLevelButton.gameObject.SetActive(hasNext);
            }
        }

        public void ShowLosePanel(int score)
        {
            if (_losePanel == null) return;
            
            _losePanel.SetActive(true);
            if (_loseScoreText != null) _loseScoreText.text = "SCORE: " + score.ToString();
        }

        // Gắn vào nút Restart
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Gắn vào nút Menu/Home
        public void GoToMenu()
        {
            SceneManager.LoadScene("MenuScene");
        }

        // Gắn vào nút Next Level
        public void LoadNextLevel()
        {
            LevelManager.SelectedLevelIndex++;
            // Reload the same scene but with updated index
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
