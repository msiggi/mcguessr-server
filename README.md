# Minecraft GeoGuessr (MCGuessr)

A browser-based guessing game: you're shown a screenshot from a Minecraft world and have to pinpoint the location on an interactive map. The closer your guess, the more points you earn — up to 5,000 per round.

## Features

- **3 rounds** per game with 50 possible locations
- **30-second timer** per round with a color-shifting boss bar
- **Interactive map** with zoom and drag support
- **Minecraft skin preview** via [Minotar API](https://minotar.net)
- **Leaderboard** (Top 10, persisted across sessions)

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Getting Started

```bash
cd mcguessr-app
dotnet run
```

Then open your browser at [http://localhost:5000](http://localhost:5000).

## Project Structure

```
mcguessr-app/
├── Controllers/
│   └── LeaderboardController.cs   # GET + POST /leaderboard
├── Models/
│   └── LeaderboardEntry.cs        # { name, score }
├── Services/
│   └── LeaderboardService.cs      # Thread-safe JSON file read/write
├── wwwroot/
│   ├── index.html                 # App shell
│   ├── mcguessr.css               # Styles
│   ├── mcguessr.js                # Game logic (Vanilla JS)
│   └── images/                    # Screenshots and map assets
├── leaderboard.json               # Persistent high score storage
└── Program.cs                     # ASP.NET Core startup
```

## API

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/leaderboard` | Retrieve top 10 scores |
| POST | `/leaderboard` | Submit a score `{ "name": "...", "score": 1234 }` |

## Tech Stack

- **Frontend:** Vanilla HTML/CSS/JavaScript
- **Backend:** ASP.NET Core 10 Web API
- **Storage:** `leaderboard.json` (no database required)
