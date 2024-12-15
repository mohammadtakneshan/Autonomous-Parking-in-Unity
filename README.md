# Reinforcement-Learning-Proj-2110
## Overview

This project demonstrates various reinforcement learning (RL) environments and training scenarios developed using Unity ML-Agents. The repository consists of training data, configurations, and environment implementations for RL tasks.

## Repository Structure

- **Basic Environment**
    - Folder: `Basic`
    - Description: A simple environment designed for basic RL tasks like movement and decision-making.
- **BasicAvoidance Environment**
    - Folder: `BasicAvoidance`
    - Description: The agent learns to avoid obstacles or threats while navigating the environment.
- **BasicPathFinding Environment**
    - Folder: `BasicPathFinding`
    - Description: The agent is trained to navigate a maze or grid to reach a target point.
- **GridWorld Environment**
    - Folder: `GridWorld`
    - Description: A grid-based environment for navigation tasks, often used for experiments with discrete actions.
- **Hallway Environment**
    - Folder: `Hallway`
    - Description: Sequential decision-making tasks where the agent navigates through hallways, learning directional choices.
- **ParkingLot Environment**
    - Folder: `ParkingLot`
    - Description: A navigation and avoidance task where the agent learns to park or move vehicles.
- **ParkingLotAdvanced Environment**
    - Folder: `ParkingLotAdvanced`
    - Description: A more complex version of the Parking Lot environment with advanced observation and decision tasks.
- **PushBlock Environment**
    - Folder: `PushBlock`
    - Description: An object manipulation task where the agent pushes blocks to target locations.
- **VisualHallway Environment**
    - Folder: `VisualHallway`
    - Description: Similar to the Hallway environment but with visual observation inputs.
- **VisualPushBlock Environment**
    - Folder: `VisualPushBlock`
    - Description: The Push Block environment with a focus on visual observations.

## Key Files and Logs

Each environment contains a `.json` log file capturing metrics during training, including:

- `AgentSendState`: Time spent collecting observations and action masks.
- `DecideAction`: Time used by the ML model to decide actions.
- `AgentAct`: Time for the agent to execute an action.
- Cumulative reward: Measures agent performance over time.

Examples of log files:

- `Basic_timers.json`
- `BasicAvoidance_timers.json`
- `BasicPathFinding_timers.json`
- ...

## Training Process

1. **Environment Setup**:
    - Install Unity ML-Agents and configure the Unity Editor.
    - Load the desired environment (e.g., `Basic`, `PushBlock`).
2. **Agent Training**:
    - Run the environment and collect observations.
    - Train the agent using RL algorithms such as PPO (Proximal Policy Optimization).
3. **Evaluation**:
    - Monitor training metrics (reward, episode length, etc.) using Unity's built-in tools or exported logs.
    - Fine-tune hyperparameters and retrain for optimization.
4. **Logging and Analysis**:
    - Logs are automatically generated in `.json` format, containing detailed performance data.

## Dependencies

- **Unity ML-Agents**: Version 1.0.2
- **Unity Editor**: Tested with versions 2018.4.17f1 and 2019.3.5f1
- **Python**: Used for RL model training and log analysis.

## How to Use

1. Clone the repository:
    
    ```
    git clone https://github.com/mohammadt-git/Reinforcement-Learning-Proj-2110.git
    ```
    
2. Install Unity ML-Agents:
    
    ```
    pip install mlagents
    ```
    
3. Open the Unity project and load the desired scene (e.g., `PushBlock`, `GridWorld`).
4. Train the agent by running the Unity simulation and observing performance metrics.
5. Analyze the results using the `.json` log files.

## Future Work

- Enhance complexity in environments (e.g., dynamic obstacles).
- Implement additional RL algorithms for comparative studies.
- Develop visualization tools for analyzing agent performance and decision-making.

## Contributors

- Mohammad T.
- Team Members

## License

This project is licensed under the MIT License.
