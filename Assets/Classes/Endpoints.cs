public static class Endpoints
{

    public const string IP = "cat-enough-narwhal.ngrok-free.app";
    public static readonly string HOST_URL = $"https://{IP}/api/";

    private const string USER_CONTROLLER = "Usuarios";
    private const string PARTIDA_CONTROLLER = "Partida";
    private const string ZONE_CONTROLLER = "Zone";
    private const string MINIGAME_CONTROLLER = "Minigame";

    public static string GetUserId(string mail, string password)
    {
       return HOST_URL + $"{USER_CONTROLLER}/getId/{mail}/{password}";
    }

    public static string GetZonas(int id)
    {
        return HOST_URL + $"{ZONE_CONTROLLER}/byUserId/{id}";
    }

    public static string GetMiniGames(int id)
    {
        return HOST_URL + $"{MINIGAME_CONTROLLER}/byZona/{id}";
    }

    public static string GetPartida(int minigameId, int userId)
    {
        return HOST_URL + $"{PARTIDA_CONTROLLER}/{minigameId}/{userId}";
    }

    public static string PostPartida()
    {
        return HOST_URL + $"{PARTIDA_CONTROLLER}";
    }


}