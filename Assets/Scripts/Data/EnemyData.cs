using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "We/EnemyData", order = 0)]
    public class EnemyData : SerializedScriptableObject
    {
        public Dictionary<string, EnemyStats> enemies = new Dictionary<string, EnemyStats>();
#if UNITY_EDITOR

        [Button("Get Data")]
        void GetData()
        {
            string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vTzKkERW7dfKeErYX1dwB9SBM9sY0Jv-pKUMULwcFLNEWyz4JX0W0ChFmr4ONXcyqBQngxYC_mdyBgF/pub?gid=112364316&single=true&output=csv";

            enemies = new Dictionary<string, EnemyStats>();

            System.Action<string> actionComplete = new System.Action<string>((string str) =>
            {
                var data = CSVReader.ReadCSV(str);
                for (int i = 1; i < data.Count; i++)
                {
                    var _data = data[i];

                    EnemyStats enemyStats = new EnemyStats();

                    enemyStats.nameEnemy = _data[0];
                    enemyStats.level = int.Parse(_data[1]);
                    enemyStats.dame = Helper.ParseFloat(_data[2]);
                    enemyStats.hp = Helper.ParseFloat(_data[3]);
                    enemyStats.attackSpeed = Helper.ParseFloat(_data[4]);
                    enemyStats.moveSpeed = Helper.ParseFloat(_data[5]);
                    enemyStats.exp = Helper.ParseFloat(_data[6]);

                    enemies.Add(_data[0], enemyStats);
                }
                UnityEditor.EditorUtility.SetDirty(this);
            });
            EditorCoroutine.start(Helper.IELoadData(url, actionComplete));
        }
#endif
    }
    [Serializable]
    public class EnemyStats
    {
        public string nameEnemy;
        public int level;
        public float dame;
        public float hp;
        public float attackSpeed;
        public float moveSpeed;
        public float exp;
    }
}