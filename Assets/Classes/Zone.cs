using System.Collections.Generic;

[System.Serializable]
public class Zone
{
    public string name;
    public string description;
    List<MiniGame> miniGames;

    public Zone(string name, string description)
    {
        this.name = name;
        this.description = description;
        this.miniGames = new List<MiniGame>();
    }

    public void AddMinigame(MiniGame m)
    {
        miniGames.Add(m);
    }
    
    public int GetPoints()
    {
        int total = 0;
        for(int i = 0; i < miniGames.Count; i++)
        {
            total += miniGames[i].Points;
        }

        return total;
    }

    public int GetCompletedMinigames()
    {
        int total = 0;
        for(int i = 0; i < miniGames.Count;i++)
            if (miniGames[i].Completed)
                total++;

        return total;
    }

    public MiniGame GetMiniGame(string name) { 
        
        for(int i = 0; i < miniGames.Count;i++)
        {
            if (miniGames[i].Name == name)
                return miniGames[i];
        }
        return null;
    }
}