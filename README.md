# Networked Multiplayer Rock-Paper-Scissor Game

A real-time, networked Rock-Paper-Scissor game developed for the CS408 Computer Networks course.

## Features
- **Multiplayer Support**: Up to 4 players can join a game room. Extra players wait in a queue.
- **Leaderboard**: Tracks player wins and updates in real-time.
- **Error Handling**: Informative messages ensure a smooth gaming experience.
- **Game Statistics**: Win/loss records are maintained by the server.

## Game Rules
- Players select gestures (Rock, Paper, Scissors) within a 10-second window.
- Results are evaluated, and ties lead to additional rounds.
- The last player standing is the winner.

## How to Run
1. **Server Setup**: Start the server on a specified port.
2. **Client Connection**: Clients connect using the server's IP and port. Unique player names are required.
3. **Gameplay**: Once 4 players connect, the game begins with a countdown.

## Tech Stack
- **Languages**: C#
