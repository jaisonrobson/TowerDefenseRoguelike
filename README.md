# Tower-Defense-JRG-prototype
This is a tower defense framework for Unity that utilizes many technologies geared towards Unity present in the market.

The idea is that this tool, with all the technologies (assets) combined, provides space for the creation of numerous tower defense games with greater ease, considering that only a few modifications will be necessary to have a new tower defense game working perfectly.

Technologies used:
* A* (star) Pathfinding
* Odin Inspector
* Odin Validator
* Odin Serialized Structures
* System LINQ
* DestroyIt

--- Design Patterns
* Singleton Pattern
* FSM (Finite State Machine)
* Object Pooling
* Observer Pattern

The differentiating factor that makes this project a framework is the fact that it is based on the way the RPG Maker tool works, centralizing all playable data in an internal database. In the case of RPG Maker, MYSQL is used, while in this project, I'm using Unity's serializable objects along with Odin Inspector to allow for more types of data and structures to be serialized, stored and validated, such as lists, etc.

<img src="https://raw.githubusercontent.com/jaisonrobson/Tower-Defense-JRG-prototype/main/Assets/Github%20Assets/Internal%20Database%20View.png" width="1024" height="512" />
