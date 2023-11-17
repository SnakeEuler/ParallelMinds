using UnityEngine;
namespace Tocsin {
public class EventContext {
  private readonly object data;
  public EventContext (object data) { this.data = data; }
  public T As<T> () where T : new () { return (T)data; }
}
}

