using System.Collections.Generic;

[System.Serializable]
public class Zone
{
    public string name;
    public string description;
    public int id;

    List<MiniGame> miniGames;

    public Zone(int id, string name, string description)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.miniGames = new List<MiniGame>();
    }

    public void SetMiniGames(List<MiniGame> miniGames)
    {
        this.miniGames = miniGames;
    }

    public void AddMinigame(MiniGame m)
    {
        miniGames.Add(m);
    }

    public int GetPoints()
    {
        int total = 0;
        for (int i = 0; i < miniGames.Count; i++)
        {
            total += miniGames[i].Points;
        }

        return total;
    }

    public int GetCompletedMinigames()
    {
        int total = 0;
        for (int i = 0; i < miniGames.Count; i++)
            if (miniGames[i].Points > 0)
                total++;

        return total;
    }

    public MiniGame GetMiniGame(string name)
    {

        for (int i = 0; i < miniGames.Count; i++)
        {
            if (miniGames[i].Name == name)
                return miniGames[i];
        }
        return null;
    }

    public bool UpdateMiniGame(int id, int puntaje, int tiempo)
    {

        for (int i = 0; i < miniGames.Count; i++)
        {
            if (miniGames[i].IdGame == id)
            {
                miniGames[i].Points = puntaje;
                miniGames[i].Time = tiempo;
                return true;
            }
        }
        return false;
    }
}