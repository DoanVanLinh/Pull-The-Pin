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
    protected int currentIndexLevel;
    [Button()]
    public void StartState(int level = 0)
    {
        currentIndexLevel = level;
        currentLevel = Instantiate(levels[currentIndexLevel], Vector3.zero, Quaternion.identity);
    }
    [Button()]
    public bool NextLevel()
    {
        if (++currentIndexLevel >= levels.Count)
            return false;

        currentLevel.transform.DOMove(new Vector3(-50, 0, 0f), 1.5f)
                               .SetEase(Ease.OutQuart);

        Level clone = Instantiate(levels[currentIndexLevel], new Vector3(50,0, 0f), Quaternion.identity);

        clone.transform.DOMove(Vector3.zero, 1.5f)
                              .SetEase(Ease.OutQuart)
                              .OnComplete(() =>
                              {
                                  Destroy(currentLevel.gameObject);
                                  currentLevel = clone;
                              });

        DataManager.Instance.SetCurrentLevel(currentIndexLevel);
        return true;
    }
    [Button()]
    public void ResumeLevel()
    {
        Destroy(currentLevel.gameObject);

        currentLevel = Instantiate(levels[currentIndexLevel], Vector3.zero, Quaternion.identity);
    }
}
