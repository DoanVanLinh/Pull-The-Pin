using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "DailyMissionData", menuName = "We/DailyMissionData", order = 0)]
    public class DailyMissionData : ScriptableObject
    {
        public int id;
        public string description;
        public int level;
        public int amountStar=>level* initaAountStar;
        public int value => level * initValue;

        public int initaAountStar;
        public int initValue;
    }
}