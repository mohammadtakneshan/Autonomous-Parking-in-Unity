# Reinforcement-Learning-Proj-2110

The Unity project, as described, focuses on training agents to park a car using machine learning. The **ParkingLot.unity** and **ParkingLotAdvanced.unity** scenes represent two levels of complexity, leveraging Unity ML-Agents to implement reinforcement learning strategies for navigation and control.

---

### **Description of Scene**


- **Purpose**: Demonstrates how a car (agent) learns to park in a more complex environment.
- **Key Features**:
  - More challenging environment, including tighter spaces and dynamic obstacles.
  - Advanced reinforcement learning strategies are required to adapt to dynamic challenges.

---

### **Training Process**

#### **1. Environment Setup**
- Install Unity ML-Agents Toolkit and configure Unity Editor.
- Load the scene (FILENAME).

#### **2. Training Configuration**
- **Config File**: `/Assets/Config/SelfParking.yaml`
  - Defines parameters like learning rate, reward signals, neural network architecture, etc.
  - Example settings include:
    - **Reward Signals**: Positive for successful parking, negative for collisions.
    - **Behavior Type**: Continuous actions for precise control of the agent.
    - **Observation Settings**: Includes position, velocity, and proximity to obstacles.

#### **3. Training and Evaluation**
- Train the agent using reinforcement learning algorithms such as PPO (Proximal Policy Optimization).
- Monitor metrics such as cumulative reward, time per action, and episode length.
- Fine-tune hyperparameters and retrain for optimization.

#### **4. Logging and Analysis**
- Logs generated in `.json` format capture performance metrics:
  - **AgentSendState**: Time collecting observations and action masks.
  - **DecideAction**: Time spent by the ML model deciding actions.
  - **AgentAct**: Time for the agent to execute actions.
  - **Cumulative Reward**: Measures agent performance over time.
- Example log files include `Basic_timers.json` and `ParkingLot_timers.json`.

---

### **Key Files and Dependencies**
1. **Config File**: `/Assets/Config/SelfParking.yaml`  
   - Core configuration for training agents in parking scenarios.
   
2. **Log Files**: Captures detailed training metrics for analysis.
   - Includes performance data on agent actions, rewards, and efficiency.

3. **Dependencies**:
   - Unity ML-Agents Toolkit (v1.0.2)
   - Python environment for RL model training and evaluation.
   - Unity Editor (tested with versions 2018.4.17f1 and 2019.3.5f1).

---

### **Additional Features**
- **Dynamic Rewards**: Encourages agents to optimize their actions by providing context-sensitive feedback.
- **Customization**: The project allows for environment scaling, enabling users to introduce more obstacles or adjust complexity.
- **Visualization**: Materials and visual indicators help track agent success or failure during training.

---

### **Applications**
- Understanding foundational concepts in reinforcement learning and agent-based control.
- Developing intelligent navigation systems for autonomous vehicles.
- Benchmarking RL algorithms in dynamic and constrained environments.

---

### **Future Enhancements**
- Introduce real-time dynamic obstacles for added complexity.
- Test and integrate advanced RL algorithms for comparative studies.
- Develop intuitive visualization tools for performance analytics.

## License

This project is licensed under the MIT License.
