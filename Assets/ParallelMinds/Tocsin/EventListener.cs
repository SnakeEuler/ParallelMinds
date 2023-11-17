using UnityEngine;
using UnityEngine.Events;
namespace Tocsin {
public class EventListener<T>: MonoBehaviour where T : new(){
  [SerializeField]
  private ScriptableEvent<T> scriptableEvent;
  [SerializeField]
  protected UnityEvent<T> callbacks;

  protected virtual void OnEnable () { scriptableEvent?.Register (OnSignalReceived); }

  protected virtual void OnDisable () { scriptableEvent?.Unregister (OnSignalReceived); }
  public virtual void OnSignalReceived (T signal) { callbacks.Invoke (signal); }
}
}
