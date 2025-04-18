namespace TicTacToe;

public class Jugador
{
    public Dictionary<string, string> nom { get; set; } = new();
    public Dictionary<string, int> punts { get; set; } = new();
    public List<string> desqualificats { get; set; } = new();
}