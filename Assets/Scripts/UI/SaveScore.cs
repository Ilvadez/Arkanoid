using UnityEngine;

public class SaveScore
{
    public void Save(string nameKey, int saveItem)
    {
        PlayerPrefs.SetInt(nameKey, saveItem);
        PlayerPrefs.Save();
    }
    public int GetScore(string nameKey)
    {
        return PlayerPrefs.GetInt(nameKey);
    }
}
