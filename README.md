Autonomous-Parking in Unity

# Overview

The Unity project, as described, focuses on training agents to park a car using machine learning. The **ParkingLotAdvanced.unity** scene represent a level of complexity, leveraging Unity ML-Agents to implement reinforcement learning strategies for navigation and control.

# **Description of Scene**

- **Purpose**: Demonstrates how a car (agent) learns to park in a  complex environment.
- **Key Features**:
    - A challenging environment, that includes a tight space and dynamic obstacles.
    - Advanced reinforcement learning strategies are utilized to adapt to dynamic challenges.

# **Training Process**

## **1. Environment Setup**

- Install Unity ML-Agents Toolkit and configure Unity Editor.
- Load the scene (**ParkingLotAdvanced.unity**).

## **2. Training Configuration**

- **Config File**: `/Assets/Config/SelfParking.yaml`
    - Defines parameters like learning rate, reward signals, neural network architecture, etc.
    - Example settings include:
        - **Reward Signals**: Positive for successful parking, negative for collisions.
        - **Behavior Type**: Continuous actions for precise control of the agent.
        - **Observation Settings**: Includes position, velocity, and proximity to obstacles.
- `/Assets/Config/SelfParking1.yaml`, `/Assets/Config/SelfParking2.yaml`, `/Assets/Config/SelfParking3.yaml` Represent the different configuration of the training.

## **3. Training Data**

- Unity-Parking/results
    - Represent **trained models**, **training logs**, and **configuration settings** for a Unity ML-Agents project focused on car behavior simulation.

## **4. Training and Evaluation**

- Train the agent using reinforcement learning algorithms such as PPO (Proximal Policy Optimization). More on PPO: https://medium.com/@danushidk507/ppo-algorithm-3b33195de14a
- Monitor metrics such as cumulative reward, time per action, and episode length, shown in tensorboard.
- Fine-tune hyperparameters and retrain for optimization.

## **5. Logging and Analysis**

- Logs generated in `.json` format capture performance metrics:
    - **AgentSendState**: Time collecting observations and action masks.
    - **DecideAction**: Time spent by the ML model deciding actions.
    - **AgentAct**: Time for the agent to execute actions.
    - **Cumulative Reward**: Measures agent performance over time.
- Example log files include `Basic_timers.json` and `ParkingLot_timers.json`.

# **Key Files and Dependencies**

1. **Config File**: `/Assets/Config/SelfParking.yaml`
    - Core configuration for training agents in parking scenarios.
2. **Log Files**: Captures detailed training metrics for analysis.
    - Includes performance data on agent actions, rewards, and efficiency.
3. **Dependencies**:
    - Unity ML-Agents Toolkit (v1.0.2)
    - Python environment for RL model training and evaluation.
    - Unity Editor (tested with versions 2018.4.17f1 and 2019.3.5f1).
    - Anaconda

# Installing the Dependencies

conda create -n unity python=3.9.7 -y

conda activate unity

conda config --add channels pytorch

conda install pytorch torchvision -c pytorch

conda install grpcio h5py -y

pip install mlagents==0.30.0 -q

pip install protobuf==3.20.0

# Running ML Agents

mlagents-learn CONFIG_FILE_NAME.yaml --run-id="RUN_NAME"

Change CONFIG_FILE_NAME to the name of your configuration file and RUN_NAME to the name that you want to see in TensorBoard.

Checking tensorboard logs:

tensorboard --logdir results

Open the website: http://localhost:####/

# Concepts

### **1. Agent Observations**

- **Purpose**: Collect data required by the agent for decision-making.
- **Observations Collected**:
    - **Agent Position**: Tracks where the agent is in the environment.
    - **Target Position**: Determines the location of the goal.
    - **Velocity**: Monitors the agent's movement dynamics (x-axis and z-axis).

### **3. Agent Training**

- **Training Algorithm**: Uses Proximal Policy Optimization (PPO) for reinforcement learning.
- **Reward System**:
    - Positive rewards for actions that move closer to the goal.
    - Penalties for incorrect actions, such as collisions or moving away from the target.
- **Episode Logic**:
    - Resets the environment after reaching the target or failing an episode.
    - Introduces randomization to prevent overfitting and improve adaptability.

### **4. Actions and Forces**

- **Action Vector**: Defines continuous actions for movement along the x-axis and z-axis.
- **Physics Application**: Uses Unity’s rigid bodies to simulate realistic movement by applying forces based on agent actions.

### **5. Reward and Punishment System**

- **Success Criteria**:
    - Agent receives a reward if the distance to the target is less than a threshold.
    - Materials or visual indicators change to signal success.
- **Failure Criteria**:
    - Ends the episode if the agent falls or makes invalid moves.
    - Material signals (e.g., red for failure) visually represent this outcome.

### **6. Logging and Performance Analysis**

- **Log Files**: Captures detailed data during training:
    - **Cumulative Reward**: Measures how well the agent is performing.
    - **Action Times**: Tracks how long the agent spends deciding and executing actions.
- **Example Files**: `Basic_timers.json`, `PlayerMaze_timers.json`.

### **7. Fine-Tuning**

- **Speed Adjustments**: Demonstrates tweaking speed parameters for improved agent control.
- **Mass Adjustments**: Highlights the impact of object mass on agent performance.
- **Prefabs**: Encourages organizing reusable components like training areas.

### **8. Model Training and Deployment**

- **Training Execution**: Uses Unity’s ML-Agents Toolkit to train the model over multiple episodes and iterations.
- **Model Integration**: After training, the learned model is deployed in Unity for real-time demonstrations.
- **Performance**: Evaluates the model’s success rate (mean reward) and visual performance.

# **In Depth File Explanation**

## Project Settings:

1. **Physics2DSettings.asset**: Configures 2D physics settings for the Unity project, such as gravity and collision layers.
2. **PresetManager.asset**: Manages custom presets for Unity components and assets.
3. **ProjectSettings.asset**: Contains global project settings, including layers, tags, and player preferences.
4. **ProjectVersion.txt**: Indicates the Unity editor version used for the project.
5. **QualitySettings.asset**: Manages graphical quality settings, such as anti-aliasing and shadow quality.
6. **TagManager.asset**: Configures layers and tags for the Unity project.
7. **TimeManager.asset**: Defines time settings like fixed time steps and time scale.
8. **UnityConnectSettings.asset**: Stores settings related to Unity cloud services.
9. **VFXManager.asset**: Configures settings for the Visual Effect Graph.
10. **XRSettings.asset**: Manages XR (Extended Reality) settings, such as VR or AR configurations.
11. **AudioManager.asset**: Manages global audio settings, including sample rates and spatial sound settings.
12. **BurstAotSettings_iOS.json**: Configures Ahead-Of-Time (AOT) compilation settings for the Burst Compiler on iOS【45†source】.
13. **ClusterInputManager.asset**: Configures input settings for cluster-based systems or networked input devices.
14. **DynamicsManager.asset**: Manages global physics and dynamics settings for Unity.
15. **EditorBuildSettings.asset**: Defines the build settings, including the scenes to be included in the build process.
16. **EditorSettings.asset**: Configures Unity editor-specific settings, such as asset serialization modes and external script editor preferences.
17. **GraphicsSettings.asset**: Specifies global graphics settings, including rendering pipelines, shaders, and texture compression.
18. **InputManager.asset**: Defines input axes, keybindings, and input settings for the project.
19. **NavMeshAreas.asset**: Manages navigation mesh area settings used for AI pathfinding and navigation.

## Packages

**`manifest.json`**: Manages Unity project dependencies and package versions.

## Physics

1. **`CarPhysics.physicMaterial`**: Defines car physics properties (friction, drag).
2. **`CarPhysics.physicMaterial.meta`**: Metadata for `CarPhysics.physicMaterial`.
3. **`RoadPhysics 1.physicMaterial`**: Defines road surface properties (friction).
4. **`RoadPhysics 1.physicMaterial.meta`**: Metadata for `RoadPhysics 1.physicMaterial`.

## SelfParking.yaml

The **`SelfParking.yaml`** file is a configuration file for training the **CarBehavior** agent using **PPO (Proximal Policy Optimization)**. It includes:

1. **Trainer Type**:
    - `ppo`: Specifies the reinforcement learning algorithm.
2. **Hyperparameters**:
    - `batch_size`: 128 – Number of experiences per batch.
    - `buffer_size`: 2048 – Size of the experience buffer.
    - `learning_rate`: 0.0003 – Rate at which the model learns.
    - `beta`: 0.01 – Entropy regularization to encourage exploration.
    - `epsilon`: 0.2 – PPO clipping range.
    - `lambd`: 0.95 – Generalized Advantage Estimation parameter.
    - `num_epoch`: 3 – Number of passes through training data.
    - `learning_rate_schedule`: Linear decay over training.
3. **Network Settings**:
    - `normalize`: False – Input data is not normalized.
    - `hidden_units`: 128 – Number of units in each hidden layer.
    - `num_layers`: 2 – Number of hidden layers.
4. **Reward Signals**:
    - `extrinsic`:
        - `gamma`: 0.99 – Discount factor for future rewards.
        - `strength`: 1.0 – Weight of the extrinsic reward.
5. **Training Steps**:
    - `max_steps`: 10,000,000 – Maximum number of steps for training.
    - `time_horizon`: 64 – Number of steps used to calculate advantages.
    - `summary_freq`: 5000 – Frequency of logging training progress.
6. **Performance**:
    - `threaded`: True – Enables threaded execution for faster training.

## Scripts

1. **CarObstacle.cs**:
    - Manages different types of obstacles (e.g., barriers, trees) in the environment.
    - Penalizes the agent when it collides with an obstacle.
2. **CarSpots.cs**:
    - Handles the setup and randomization of parked cars and car goals within a scene.
    - Caches initial positions and rotations of parked cars for reset functionality.
3. **Singleton.cs**:
    - Implements a generic singleton pattern to ensure a single instance of a given class.
4. **PlayerAgent.cs**:
    - A core ML-Agent script that interacts with Unity ML-Agents.
    - Collects observations, executes actions, and handles rewards and penalties based on the agent's performance.
5. **TargetMoving.cs**:
    - Manages the movement of the target object in the environment.
    - Resets the target's position and handles penalties for boundary violations.
6. **AgentAvoidance.cs**:
    - Manages agent behaviors related to avoiding collisions with obstacles or other agents.
    - Handles rewards or penalties for successful or failed avoidance.
7. **BaseAgent.cs**:
    - Serves as a base class for all agents, providing common functionality such as initialization, resetting, and basic observation collection.
    - Facilitates extensibility for specific agent types like cars or players.
8. **CarAgent.cs**:
    - A specialized agent controlling the car's movement and decisions using ML-Agents.
    - Implements observations (e.g., distance to the goal or obstacles) and actions (e.g., acceleration, steering).
9. **CarController.cs**:
    - Handles the car's physical movement, including acceleration, braking, and steering logic.
    - Integrates with Unity’s physics system to simulate realistic car dynamics.
10. **CarGoal.cs**:
    - Defines the goal location or object that the agent must reach.
    - Triggers rewards for the agent upon successful completion of the goal.

## Asset Files

- **AssetStore**: Downloaded assets.
- **Config**: Configuration files.
- **Materials**: Visual materials (textures, shaders).
- **ML-Agents**: Machine learning agent resources.
- **Physics**: Physics-related assets.
- **Prefabs**: Reusable GameObject templates.
- **Scenes**: Unity scenes (`.unity` files).
- **Scripts**: C# scripts for logic.
- **Sound**: Audio files.
- **TextMesh Pro**: Advanced text rendering assets.
- **TrainingData**: Data for training ML agents.

# **Future Enhancements**

- **Dynamic Environments**: Adds moving obstacles or variable goals for increased complexity.
- **Advanced RL Algorithms**: Experiment with alternative RL approaches for comparative analysis.
- **Data Visualization**: Enhance analytics tools for deeper insights into agent decision-making.

# **Additional Features**

- **Dynamic Rewards**: Encourages agents to optimize their actions by providing context-sensitive feedback.
- **Customization**: The project allows for environment scaling, enabling users to introduce more obstacles or adjust complexity.
- **Visualization**: Materials and visual indicators help track agent success or failure during training.

# **Applications**

- Understanding foundational concepts in reinforcement learning and agent-based control.
- Developing intelligent navigation systems for autonomous vehicles.
- Benchmarking RL algorithms in dynamic and constrained environments.

# License

This project is licensed under the MIT License.
