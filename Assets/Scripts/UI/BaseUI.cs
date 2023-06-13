using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{

    public virtual void Open()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    public abstract void LoadData();
    public abstract void SaveData();
    public virtual void OnEnable() { LoadData(); }
    public virtual void OnDisable() { SaveData(); }

}
