# Co-Op-Snake-

A classic snake game built in Unity, with additional features like power-ups, food mechanics, and a co-op multiplayer mode. This game brings the nostalgia of the old keypad snake game while adding a twist with new mechanics.

## Features

### Core Snake Game Functionality:
1. **Movement**: The snake moves in all four directions (⬆️ ⬇️ ⬅️➡️).
2. **Screen Wrapping**: The snake wraps around the screen edges.
3. **Snake Growth**: The snake grows after consuming food.
4. **Self-Collision**: The snake dies upon biting itself.

### Power-Ups:
1. **Shield**: Protects the snake from death for a short time.
2. **Score Boost**: Doubles the score gained when active.
3. **Speed Up**: Temporarily increases the snake's speed.

### Cooldown for Power-Ups:
- Each power-up has a customizable cooldown (default is 3 seconds).

### Power-Up Spawning:
- Power-ups spawn randomly on the screen after random intervals of time.

### Food Mechanics:
1. **Mass Gainer**: Increases the snake’s length when consumed.
2. **Mass Burner**: Decreases the snake’s length (doesn't spawn if the snake is already small).
3. **Food Expiry**: Food disappears after a certain time if not eaten.
4. **Flexible Length Changes**: The amount by which the snake grows or shrinks can be customized.

### Co-Op Mode:
- **Two-Player Mode**: 
  - Player 1 controls a snake using `WASD`.
  - Player 2 controls another snake using the arrow keys (⬆️ ⬇️ ⬅️➡️).
  - If one snake bites another, the bitten snake dies.

### Scoring:
- Eating **Mass Gainer** food increases the score.
- Eating **Mass Burner** food decreases the score.

### UI:
- **Basic UI**: Includes death screens, win conditions, score displays, and lobby screens.
- **Game Controls**: Pause/Resume, Restart, and Quit buttons are implemented.

## How to Play

1. **Single Player Mode**: 
   - Use the arrow keys (⬆️ ⬇️ ⬅️➡️) to control the snake.
   - Eat **Mass Gainer** foods to grow longer and increase your score.
   - Avoid eating yourself or the **Mass Burner** foods unless strategically shrinking.

2. **Co-Op Mode**: 
   - Player 1 uses `WASD` to control their snake.
   - Player 2 uses arrow keys (⬆️ ⬇️ ⬅️➡️) to control theirs.
   - If one snake bites another, the game ends.

3. **Power-Ups**: Collect power-ups to gain temporary advantages.
   - Shield: Prevents death upon collision.
   - Speed Up: Increases movement speed.
   - Score Boost: Doubles your score for a limited time.

4. **Screen Wrapping**: The snake will appear on the opposite side of the screen when moving off one edge.

## Installation & Setup

1. Clone or download this repository.
2. Open the project in Unity (version 2020.3 LTS or later recommended).
3. Press the Play button in Unity to start the game.

## Scripts

- **Snake.cs**: Manages the movement, growth, and shrinking of the snake.
- **FoodSpawner.cs**: Spawns food at random intervals and positions.
- **PowerUpSpawner.cs**: Spawns power-ups at random intervals and positions.
- **UIManager.cs**: Manages the game's UI elements such as the score, death, and pause menus.
- **GameController.cs**: Handles the game states (pause, resume, restart, and quit).
- **TwoPlayerMode.cs**: Handles the second player controls and interactions in co-op mode.

## Customization

- **Power-Up Cooldown**: Change the cooldown time of power-ups in `PowerUpSpawner.cs`.
- **Snake Growth/Shrink Rate**: Adjust how many segments the snake grows or shrinks when eating food in `Snake.cs`.
- **Food Lifespan**: Modify the lifespan of food before it disappears in `FoodSpawner.cs`.

## Future Enhancements

- **Additional Power-Ups**: Introduce more unique power-ups to add variety to the game.
- **New Game Modes**: Add different game modes such as timed challenges or survival.
- **Visual Enhancements**: Add more animations and particle effects to make the game visually appealing.

## Credits

- Developed by Prateek Tomar
- Special thanks to the Unity community for various tips and resources.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
