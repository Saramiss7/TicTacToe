# TicTacToe Championship Analysis 💻

## 🚀 DESCRIPTION (ENGLISH)

This project analyses the results of a global Tic Tac Toe championship using real data obtained from a REST API. The goal is to determine the true winner among thousands of recorded matches.

### 🔍 What does this programme do?

- Connects to a local server containing information about 10,000 matches.
- Retrieves the names of participants and their countries.
- Detects which players were disqualified and excludes them.
- Processes every match to check who won.
- Displays the player who won the most matches (or if it was a tie).

### 🛠️ What technologies does it use?

- **Language:** C# (.NET)
- **Regex:** To extract names, countries, and disqualified players.
- **HttpClient:** To communicate with the API.
- **Data structures:** Dictionaries and lists to store players and scores.

### 🧪 Requirements

- **Docker** installed to run the match server:

```bash
docker run -d --rm -ti -p 8080:8080 utrescu/tictactoeapi:latest
```

- .NET 6 or above

### 📆 Final result

When the programme is run, it shows:
- The valid participants (those not disqualified)
- How many matches each player has won
- Who the winner of the tournament is

---

## 🚀 DESCRIPCIÓN (SPANISH)

# TicTacToe – Análisis del campeonato mundial de Tic Tac Toe

Este proyecto analiza los resultados de un campeonato mundial de Tic Tac Toe (Tres en raya) usando datos reales obtenidos desde una API REST. El objetivo es encontrar al verdadero ganador entre miles de partidas registradas.

### 🔍 ¿Qué hace este programa?

- Se conecta a un servidor local con información de 10.000 partidas.
- Recupera los nombres de los participantes y su país de origen.
- Detecta qué participantes han sido descalificados y los excluye.
- Procesa todas las partidas, revisando quién ganó cada una.
- Muestra el jugador que ha ganado más partidas (o si hubo un empate).

### 🛠️ ¿Qué tecnologías usa?

- **Lenguaje:** C# (.NET)
- **Regex:** Para extraer nombres, países y jugadores descalificados.
- **HttpClient:** Para conectarse a la API.
- **Estructuras de datos:** Diccionarios y listas para guardar participantes y puntajes.

### 🧪 Requisitos

- Tener **Docker** instalado para correr el servidor de partidas:

```bash
docker run -d --rm -ti -p 8080:8080 utrescu/tictactoeapi:latest
```

- .NET 6 o superior

### 📆 Resultado final

Al ejecutar el programa, verás:
- Los participantes válidos (no descalificados)
- Cuántas partidas ha ganado cada uno
- Quién ha sido el campeón del torneo
