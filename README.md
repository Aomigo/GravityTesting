# Custom Gravity Sandbox
**Technical Demo: Physics Overrides**

A C# driven project in Unity that replaces default physics with a modular, multi-source gravity system.

## Technical Implementation
* **Custom Gravity Manager**: A global system that handles multiple concurrent gravity sources.
* **Vector Math**: Implements `Vector3.Dot` and `transform.up` logic to calculate gravitational pull relative to rotated surfaces.
* **Inheritance Architecture**: Uses a `GravitySource` base class to allow for rapid creation of different gravity types (Planes, Spheres, etc.).
* **Dynamic Orientation**: The `OrbitCamera` and Player Controller dynamically align their local "Up" axis to match the strongest local gravity vector.
