using UnityEngine;

namespace ChestProject.Chest
{
    [CreateAssetMenu(fileName = "NewChestSO", menuName = "ScriptableObjects/ChestSO")]
    public class ChestSO : ScriptableObject
    {
        public ChestTypes ChestTypes;
        public int MinCoinCount;
        public int MaxCoinCount;
        public int MinGemCount;
        public int MaxGemCount;
        public Sprite ClosedChestSprite;
        public Sprite OpenChestSprite;
        public int TimerSecs;
    }
}

