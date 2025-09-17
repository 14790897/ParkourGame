# Endless Runner Prototype

A lightweight 2D endless runner built with Unity. Keep the player alive by timing your jumps and dodging randomly spawned obstacles while the score ticks up over time.

## Requirements
- Unity 6000.2.4f1 (project created and last opened with this version)
- TextMeshPro package (installed automatically when the project opens)

## Getting Started
1. Clone or download this repository to your local machine.
2. Open Unity Hub and add the `fight` folder as an existing project.
3. Launch the project with Unity 6000.2.4f1 and open the main scene.
4. Enter Play mode to start testing.

## Controls
- Press any key to leave the Ready state and begin the run.
- Press `Space` or click the left mouse button to jump (double-jump supported).
- Press any key after a crash to restart from the Game Over screen.

## Gameplay Overview
- `GameManager` drives the Ready → Playing → Game Over state machine, updates the score text, and restarts the scene.
- `PlayerController` reads input, performs grounded checks, applies jump forces, and notifies the `GameManager` when an obstacle is hit.
- `Spawner` periodically instantiates obstacle prefabs, randomizes their spawn height, and slowly increases their travel speed.

## Tuning Tips
- Adjust jump behaviour via `PlayerController` inspector values (`jumpForce`, `jumpSpeed`, `maxJumpCount`).
- Change difficulty by editing `Spawner` inspector properties (spawn rate, vertical range, starting speed, acceleration).
- Update UI labels, font sizes, and score pacing in the `GameManager` inspector.
- Replace the obstacle prefab or player animation clips to change visuals without modifying scripts.

## Repository Layout
- `Assets/Scripts/` – gameplay logic for the player, spawning, obstacle movement, and game state.
- `ProjectSettings/` – Unity project configuration, including the recorded editor version.

Feel free to expand the project with additional mechanics such as collectible coins, parallax backgrounds, or power-ups. Happy prototyping!
