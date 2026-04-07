using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "NewLevelData", menuName = "MatchThree/LevelData")]
    public class LevelData : ScriptableObject
    {
        [Header("Grid Settings")]
        public Vector2Int dimensions = new Vector2Int(8, 8);
        
        [Header("Game Rules")]
        public int maxMoves = 30;
        public int targetScore = 5000;
        
        [Header("Visuals (Optional)")]
        public string levelName = "Level 1";
    }
}
