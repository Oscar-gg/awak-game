[System.Serializable]
public class Partida
{
    public int idUsuario;
    public int idMinijuego;
    public int puntaje;
    public int tiempo;

    public Partida(int idUsuario, int idMinijuego, int puntaje, int tiempo)
    {
        this.idUsuario = idUsuario;
        this.idMinijuego = idMinijuego;
        this.puntaje = puntaje;
        this.tiempo = tiempo;
    }
}