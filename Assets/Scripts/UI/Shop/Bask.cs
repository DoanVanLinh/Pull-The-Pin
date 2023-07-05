using Assets.Scripts.UI.RewardRecive;
using Assets.Scripts.UI.Streak;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Shop
{
    public class Bask : MonoBehaviour
    {
        public Rigidbody unicornVisual;
        public List<Rigidbody> unicornPieceVisual;
        public Animator ani;

        public void Init()
        {
            ani.enabled = true;
            int length = unicornPieceVisual.Count;
            for (int i = 0; i < length; i++)
            {
                unicornPieceVisual[i].isKinematic = true;
            }
            unicornVisual.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        public void OnBreak()
        {
            Break();
        }
        private void Break()
        {
            ani.enabled = false;
            int length = unicornPieceVisual.Count;
            for (int i = 0; i < length; i++)
            {
                unicornPieceVisual[i].isKinematic = false;

                unicornPieceVisual[i].AddExplosionForce(1500, unicornVisual.transform.position, 10);
            }
            unicornVisual.gameObject.SetActive(false);

            float randomReward = Random.Range(-1f, 1f);
            int coins = Random.Range(100, 1000);
            if (randomReward > 0)//coin
            {
                ((RewardRecivePanel)UIManager.Instance.rewardRecivePanel).Init(ERewardType.Coins, coins, () => DataManager.Instance.AddCoins(coins));
            }
            else
            {
                string id = GameManager.Instance.GetRandomItemByType(EItemUnlockType.LuckyWheel);

                if (!string.IsNullOrEmpty(id))//item
                {
                    ((RewardRecivePanel)UIManager.Instance.rewardRecivePanel).Init(ERewardType.Item, coins, null, id);
                    DataManager.Instance.GetData().AddItem(id);
                }
                else//coin
                    ((RewardRecivePanel)UIManager.Instance.rewardRecivePanel).Init(ERewardType.Coins, coins, () => DataManager.Instance.AddCoins(coins));
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(unicornVisual.transform.position, 1);
            Gizmos.DrawSphere(unicornPieceVisual[0].transform.position, 2);
        }

#endif
    }


}