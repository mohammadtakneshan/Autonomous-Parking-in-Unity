# Autonomous Parking in Unity

## Table of Contents

1. [Overview](#overview)
2. [Description of Scene](#description-of-scene)
3. [Training Process](#training-process)
    - [Environment Setup](#1-environment-setup)
    - [Training Configuration](#2-training-configuration)
    - [Training Data](#3-training-data)
    - [Training and Evaluation](#4-training-and-evaluation)
    - [Logging and Analysis](#5-logging-and-analysis)
4. [Key Files and Dependencies](#key-files-and-dependencies)
5. [Installing the Dependencies](#installing-the-dependencies)
6. [Running ML Agents](#running-ml-agents)
7. [Concepts](#concepts)
    - [Agent Observations](#1-agent-observations)
    - [Agent Training](#3-agent-training)
    - [Actions and Forces](#4-actions-and-forces)
    - [Reward and Punishment System](#5-reward-and-punishment-system)
8. [In-Depth File Explanation](#in-depth-file-explanation)
9. [Resources](#resources)
10. [Demo](#demo)


## Overview

The Unity project focuses on training agents to park a car using machine learning. The **ParkingLotAdvanced.unity** scene demonstrates reinforcement learning strategies for navigation and control. This project highlights the application of foundational reinforcement learning concepts in real-world scenarios like autonomous vehicle navigation systems and dynamic environment modeling.

## Description of Scene

- **Purpose**: Demonstrates how a car (agent) learns to park in a complex environment.
- **Key Features**:
    - Challenging environment with tight spaces and dynamic obstacles.
    - Advanced reinforcement learning strategies are used to adapt to these challenges.

## Training Process

### 1. Environment Setup

- Install Unity ML-Agents Toolkit and configure Unity Editor.
- Load the scene: **ParkingLotAdvanced.unity**.

### 2. Training Configuration

- **Config Files**: `/Assets/Config/SelfParking.yaml`, `/Assets/Config/SelfParking1.yaml`, etc.
    - Define parameters such as:
        - Reward signals (positive for successful parking, negative for collisions).
        - Continuous actions for precise control.
        - Observations (position, velocity, proximity to obstacles).

### 3. Training Data

- Results stored in **Unity-Parking/results**, including trained models and training logs.

### 4. Training and Evaluation

- Train using PPO (Proximal Policy Optimization). [Learn more about PPO](https://medium.com/@danushidk507/ppo-algorithm-3b33195de14a).
- Monitor metrics via TensorBoard.

### 5. Logging and Analysis

- Logs generated in `.json` format include:
    - **AgentSendState**: Observation and action masking time.
    - **DecideAction**: ML model's decision-making time.
    - **Cumulative Reward**: Measures performance over time.

## Key Files and Dependencies

1. **Config File**: `/Assets/Config/SelfParking.yaml` for agent training.
2. **Log Files**: Capture training metrics like actions and rewards.
3. **Dependencies**:
    - Unity ML-Agents Toolkit (v1.0.2).
    - Python for RL model training.
    - Unity Editor (tested with 2018.4.17f1 and 2019.3.5f1).
    - Anaconda for Python environment management.

## Installing the Dependencies

```
conda create -n unity python=3.9.7 -y
conda activate unity
conda config --add channels pytorch
conda install pytorch torchvision -c pytorch
conda install grpcio h5py -y
pip install mlagents==0.30.0 -q
pip install protobuf==3.20.0
```

## Running ML Agents

```
mlagents-learn CONFIG_FILE_NAME.yaml --run-id="RUN_NAME"
```

- Replace `CONFIG_FILE_NAME` with your configuration file and `RUN_NAME` with a unique identifier.

### Checking TensorBoard Logs

```
tensorboard --logdir results
```

Open TensorBoard in your browser: [http://localhost:####/](http://localhost/####/)

## Concepts

### 1. Agent Observations

- **Purpose**: Collect necessary data for decision-making.
- **Examples**:
    - Agent Position
    - Target Position
    - Velocity (x-axis, z-axis)

### 3. Agent Training

- **Algorithm**: PPO (Proximal Policy Optimization).
- **Reward System**:
    - Positive rewards for actions moving toward the goal.
    - Penalties for collisions or moving away from the target.

### 4. Actions and Forces

- **Action Vector**: Continuous actions for precise movement.
- **Physics**: Uses Unity’s rigid bodies for realistic simulations.

### 5. Reward and Punishment System

- **Success**: Reward if the agent reaches the target.
- **Failure**: Penalties for collisions or invalid moves.

## In-Depth File Explanation

### Config Files

- **`SelfParking.yaml`**:
    - Trainer type: PPO
        - ensures efficient and stable learning.
    - Learning rate: 0.0003
        - facilitates gradual policy improvements.
    - Max steps: 10,000,000
        - ensures adequate exploration of scenarios.
    - Reward signals: Extrinsic, with gamma=0.99
        - encourage long-term, strategic behaviors critical for mastering complex environments like parking in tight spaces.

### Scripts

1. **CarObstacle.cs**: Handles obstacles and penalties for collisions.
2. **CarSpots.cs**: Manages parked cars and goal positions.
3. **PlayerAgent.cs**: Core ML-Agent script for observations and actions.

## Resources

- [Unity ML-Agents Toolkit Documentation](https://github.com/Unity-Technologies/ml-agents)
- [PPO Algorithm Explanation](https://medium.com/@danushidk507/ppo-algorithm-3b33195de14a)
- [Python Installation Guide](https://www.python.org/downloads/)
- [Anaconda Documentation](https://docs.anaconda.com/)

## Demo

https://github.com/user-attachments/assets/91c44de0-2652-4ea2-957d-fa04be857eb7



