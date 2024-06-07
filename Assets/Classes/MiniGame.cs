[System.Serializable]
public class MiniGame
{
    private int idGame; // Use id to check if game can be accessed 
    private string name;
    private int time;
    private int points;
    private bool completed;
    private bool isBoss;

    public int IdGame
    {
        get { return idGame; }
        set { idGame = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Time
    {
        get { return time; }
        set { time = value; }
    }

    public int Points
    {
        get { return points; }
        set { points = value; }
    }

    public bool Completed
    {
        get { return completed; }
        set { completed = value; }
    }

    public bool IsBoss
    {
        get { return isBoss; }
        set { isBoss = value; }
    }

    public MiniGame(int idGame, string name, int time, int points, bool completed, bool isBoss)
    {
        this.idGame = idGame;
        this.name = name;
        this.time = time;
        this.points = points;
        this.completed = completed;
        this.isBoss = isBoss;
    }
}