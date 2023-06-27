using Assets.Scripts.UI.Play;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField]
    protected List<Level> levels = new List<Level>();

    protected Level currentLevel;
    protected int currentIndexLevel = 0;

    public int maxLevel => levels.Count;
    [Button()]
    public void StartState(int level = 0)
    {
        ClearStage();
        currentIndexLevel = level;
        currentLevel = Instantiate(levels[currentIndexLevel], Vector3.zero, Quaternion.identity);
        currentLevel.Init(currentIndexLevel == maxLevel - 1);
        GameManager.Instance.SetCameraSize(currentLevel.cameraSize);
        ((PlayPanel)UIManager.Instance.gamePlayPanel).stagePanel.Init(maxLevel);
    }
    [Button()]
    public bool NextLevel(bool animation = true)
    {
        if (++currentIndexLevel >= maxLevel)
            return false;

        ((PlayPanel)UIManager.Instance.gamePlayPanel).stagePanel.NextLevel();

        Level clone = Instantiate(levels[currentIndexLevel], new Vector3(50, 0, 0f), Quaternion.identity);

        clone.Init(currentIndexLevel == maxLevel - 1);

        if (animation)
        {
            currentLevel.transform.DOMove(new Vector3(-50, 0, 0f), 1.5f)
                                   .SetEase(Ease.OutQuart);


            clone.transform.DOMove(Vector3.zero, 1.5f)
                                  .SetEase(Ease.OutQuart)
                                  .OnComplete(() =>
                                  {
                                      Destroy(currentLevel.gameObject);
                                      currentLevel = clone;
                                  });
        }
        else
        {
            Destroy(currentLevel.gameObject);
            clone.transform.position = Vector3.zero;
            currentLevel = clone;
        }
        GameManager.Instance.SetCameraSize(clone.cameraSize);

        DataManager.Instance.SetCurrentLevel(currentIndexLevel);
        return true;
    }
    public void ClearStage()
    {
        if (currentLevel != null)
            Destroy(currentLevel.gameObject);
    }
}
