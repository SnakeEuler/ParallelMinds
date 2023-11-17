Tocsin System Guide
Introduction

The Tocsin system is a robust and flexible event handling system for Unity games that allows components to communicate with each other through events. It's designed to be easy to use, strongly typed, and to avoid common pitfalls like modifying listeners while events are dispatching.
Features

    Strongly-typed events.
    Support for persistent and nonce listeners.
    Efficient listener management with dynamic array resizing.
    Safe modification of listeners during event dispatch.
    Customizable and expandable for various event types.

Getting Started
Prerequisites

    Unity 2020.3 LTS or later.
    Basic understanding of C# and Unity's MonoBehaviour lifecycle.

Installation

    Clone the repository or download the Tocsin system package.
    Import the package into your Unity project.
    Ensure the scripts are placed in a folder within your project's Assets directory.

Usage
Defining Events

Define your events using standard C# delegate syntax. For example:

```csharp

public delegate void HealthChangedEvent(int currentHealth);
```
Creating Tocsins

Instantiate a Tocsin for your event. For example:

```csharp

Tocsin<HealthChangedEvent> healthChangedTocsin = new Tocsin<HealthChangedEvent>();
```
Adding Listeners

Add listeners to the Tocsin to respond to the event. For example:

```csharp

healthChangedTocsin.AddListener(OnHealthChanged);

void OnHealthChanged(int currentHealth) {
// Handle the event
}
```
Dispatching Events

Dispatch an event using the Tocsin. For example:

```csharp

healthChangedTocsin.Dispatch(currentHealth);
```
Removing Listeners

Remove listeners when they are no longer needed. For example:

```csharp

healthChangedTocsin.RemoveListener(OnHealthChanged);
```
Advanced Usage

    Nonce Listeners: Add listeners that automatically remove themselves after one event dispatch.
    Persistent Listeners: Add listeners that persist through multiple event dispatches.

Best Practices

    Use nonce listeners for temporary subscriptions.
    Use persistent listeners for long-lived subscriptions.
    Remove listeners explicitly when they are no longer needed to prevent memory leaks.