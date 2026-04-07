using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        [Header("Win/Lose Panels")]
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private TextMeshProUGUI _winScoreText;
        [SerializeField] private TextMeshProUGUI _loseScoreText;

        [Header("Menu Elements")]
        [SerializeField] private GameObject _levelSelectionPanel;
        [SerializeField] private Transform _buttonContainer;
        [SerializeField] private GameObject _levelButtonPrefab;

        public void ShowWinPanel(int score)
        {
            if (_winPanel == null)
            {
                Debug.LogError("Win Panel is NOT assigned in UIManager! Please drag your WinPanel object into the 'Win Panel' slot.");
                return;
            }
            
            _winPanel.SetActive(true);
            
            if (_winScoreText != null)
                _winScoreText.text = "SCORE: " + score.ToString();
            else
                Debug.LogWarning("Win Score Text is NOT assigned in UIManager.");
        }

        public void ShowLosePanel(int score)
        {
            if (_losePanel == null)
            {
                Debug.LogError("Lose Panel is NOT assigned in UIManager! Please drag your LosePanel object into the 'Lose Panel' slot.");
                return;
            }

            _losePanel.SetActive(true);
            
            if (_loseScoreText != null)
                _loseScoreText.text = "SCORE: " + score.ToString();
            else
                Debug.LogWarning("Lose Score Text is NOT assigned in UIManager.");
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GoToMenu()
        {
            // Implementation for returning to menu
        }

        // Methods to populate level buttons based on progress
    }
}
