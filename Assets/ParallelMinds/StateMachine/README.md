# ParallelMinds.StateMachine.HFSM

The `ParallelMinds.StateMachine.HFSM` provides a robust hierarchical finite state machine (HFSM) for Unity, designed to be lightweight and easy to use across various game development scenarios. With its fluent interface and domain-specific language (DSL), developers can define states, transitions, and conditions in a readable and maintainable manner.

## Features

- Hierarchical States: Organize states within states to represent complex behaviors.
- Fluent Interface: Define state machines in a human-readable manner.
- Domain-Specific Language: Utilize a custom DSL for state and transition definitions.
- State Actions: Attach entry, update, and exit actions to states.
- Event Handling: Use events to trigger state transitions.
- Parallel States: Support concurrent states within the same parent state.
- Debugging Tools: Visualize and log the state machine for easier debugging.
- Time Management: Handle time-based transitions and delays.

## Installation

To use the `HFSM` in your Unity project:

1. Copy the `ParallelMinds.StateMachine` folder into your Unity project's `Assets` directory.
2. Ensure your project uses the new Unity Input System for mouse interactions.
3. Attach the `HFSM` script to your desired GameObject.

## Quick Start

Here's a quick example of setting up a state machine with `HFSM`:

```csharp
public class InteractionFSM : MonoBehaviour
{
    private HFSM fsm;

    void Start()
    {
        fsm = new HFSM(this);

        fsm.AddState("Idle")
            .UponEnteringDo(() => Debug.Log("Idle State Entered"))
            .WhileUpdatingDo(() => Debug.Log("Idle State Update"))
            .UponExitingDo(() => Debug.Log("Idle State Exited"));

        // Additional states and transitions setup here...

        fsm.Initialize("Idle"); // Start the FSM in the Idle state
    }
}
