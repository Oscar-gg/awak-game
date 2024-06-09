using UnityEngine;

public static class Tips
{
    private static bool initialized;
    private static string[] tips;
    private static Sprite[] backgrounds;

    public static void InitializeClass()
    {
        if (!initialized)
        {
            initialized = true;
            LoadTipData();
            LoadBackgroundData();
        }
    }

    private static void LoadTipData(){
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/Tips");

        Tip tipsJson = JsonUtility.FromJson<Tip>(jsonFile.text);
        
        tips = tipsJson.tips;
    }

    private static void LoadBackgroundData()
    {
        backgrounds = Resources.LoadAll<Sprite>("Backgrounds");
    }

    public static string GetRandomTip(){
        InitializeClass();
        int randomIndex = Random.Range(0, tips.Length);
        return tips[randomIndex];
    }

    public static Sprite GetRandomBackground()
    {
        InitializeClass();
        int randomIndex = Random.Range(0, backgrounds.Length);
        Debug.Log(backgrounds.Length);
        return backgrounds[randomIndex];
    }
}