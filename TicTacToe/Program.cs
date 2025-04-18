using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace TicTacToe;

class Program
{
    public static Jugador Jugador = new();
    static async Task Main()
    {
        var url = "http://localhost:8080/";
        Uri uri = new (url);

        using HttpClient client = new()
        {
            BaseAddress = uri
        };
        
        var resposta = await client.GetFromJsonAsync<List<string>>("jugadors");
        await ObtenirDesqualificats(resposta);
        await ObtenirJugadors(resposta);
        
        foreach (var jugador in Jugador.nom)
        {
            Console.WriteLine($"El nom: {jugador.Key}, el pais: {jugador.Value}");
        }

        await ProcesarPartides(client);
        
        foreach (var punt in Jugador.punts)
        {
            Console.WriteLine($"Nom: {punt.Key}, punts: {punt.Value}");
        }

        MostrarResultats();
    }

    private static async Task ObtenirDesqualificats(List<string> resposta)
    {
        Regex desquali = new Regex(@"participant ([A-Z][\w'-]+ [A-Z][\w'-]+).*(desqualifica(da|t))");
        //Llista desqualificats
        foreach (var respostes in resposta)
        {
            var match = desquali.Match(respostes);
            if (match.Success)
            {
                string nom = match.Groups[1].Value;
                Jugador.desqualificats.Add(nom);
            }
            else
            {
                Console.WriteLine($"No s'ha trobat coincidència");
            }
        }
    }

    private static async Task ObtenirJugadors(List<string> resposta)
    {
        Regex rg = new Regex(@"participant ([A-Z]+\w+ [A-Z-'a-z]+\w+).*representa(nt)? (a |de )([A-Z-a-z]+\w+)");
        foreach (var respostes in resposta)
        {
            var match2 = rg.Match(respostes);
            if (match2.Success)
            {
                string nom = match2.Groups[1].Value;
                string pais = match2.Groups[4].Value;
                if (!Jugador.desqualificats.Contains(nom))
                {
                    Jugador.nom.Add(nom, pais);
                    Jugador.punts.Add(nom, 0);
                }
            }
            else
            {
                Console.WriteLine($"No s'ha trobat coincidència");
            }
        }
    }

    private static async Task ProcesarPartides(HttpClient client) 
    {
        for (var i = 1; i <= 10000; i++) {
            var partida = await client.GetFromJsonAsync<Resposta>($"partida/{i}");
            
            string un = partida.tauler[0];
            string dos = partida.tauler[1];
            string tres = partida.tauler[2];
            
            var O = "";
            var X = "";
            
            if (Jugador.punts.ContainsKey(partida.jugador1) && Jugador.punts.ContainsKey(partida.jugador2)) {
                O = partida.jugador1;
                X = partida.jugador2;
                
                if (un[0] == 'X' && un[1] == 'X' && un[2] == 'X' || dos[0] == 'X' && dos[1] == 'X' && dos[2] == 'X' ||
                    tres[0] == 'X' && tres[1] == 'X' && tres[2] == 'X' ||
                    un[0] == 'X' && dos[0] == 'X' && tres[0] == 'X' ||
                    un[1] == 'X' && dos[1] == 'X' && tres[1] == 'X' || un[2] == 'X' && dos[2] == 'X' && tres[2] == 'X')
                {
                    Jugador.punts[X]++;
                }
                else if (un[0] == 'O' && un[1] == 'O' && un[2] == 'O' ||
                         dos[0] == 'O' && dos[1] == 'O' && dos[2] == 'O' ||
                         tres[0] == 'O' && tres[1] == 'O' && tres[2] == 'O' ||
                         un[0] == 'O' && dos[0] == 'O' && tres[0] == 'O' ||
                         un[1] == 'O' && dos[1] == 'O' && tres[1] == 'O' ||
                         un[2] == 'O' && dos[2] == 'O' && tres[2] == 'O')
                {
                    Jugador.punts[O]++;
                }
                //Diagonals
                else if (un[0] == 'X' && dos[1] == 'X' && tres[2] == 'X' ||
                         un[2] == 'X' && dos[1] == 'X' && tres[0] == 'X')
                {
                    Jugador.punts[X]++;
                }
                else if (un[0] == 'O' && dos[1] == 'O' && tres[2] == 'O' ||
                         un[2] == 'O' && dos[1] == 'O' && tres[0] == 'O')
                {
                    Jugador.punts[O]++;
                }
                //En cas que sigui empat més endavant es mostrarà
            }
        }
    }

    private static void MostrarResultats()
    {
        List<string> guanyador = new();
        var mesgran = 0;
        bool empat = false;
        foreach (var punt in Jugador.punts) {
            if (punt.Value > mesgran)
            {
                guanyador.Clear();
                guanyador.Add(punt.Key);
                mesgran = punt.Value;
                empat = false;
            }
            else if (punt.Value == mesgran)
            {
                guanyador.Add(punt.Key);
                empat = true;
            }
        }
        if (empat) {
            Console.WriteLine("Empat. Els guanyadors són:");
            foreach (var winner in guanyador)
            {
                Console.WriteLine($"{winner} - {Jugador.nom[winner]}");
            }
        }
        else {
            string winner = guanyador[0];
            Console.WriteLine($"El guanyador és: {winner}   País: {Jugador.nom[winner]}");
        }
    }
}
