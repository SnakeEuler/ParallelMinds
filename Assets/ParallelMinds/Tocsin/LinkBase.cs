using System;
using UnityEngine;
using UnityEngine.Events;
namespace Tocsin {
public abstract class LinkBase<TDelegate>: ILinkBase<TDelegate> where TDelegate : class {
  protected TocsinBase<TDelegate> tocsin;

  //Constructors
  private LinkBase () { }// To force use of parameters
  public LinkBase (TocsinBase<TDelegate> tocsin) { this.tocsin = tocsin; }


  //Implementation
  public uint PersistentListenerCount { get { return tocsin.PersistentListenerCount; } }
  public uint NonceListenerCount { get { return tocsin.NonceListenerCount; } }

  public bool Contains (TDelegate listener) { return tocsin.Contains (listener); }

  public bool AddListener (TDelegate listener, bool allowDuplicates = false) {
    return tocsin.AddListener (listener, allowDuplicates);
  }

  public ITocsinBinding BindListener (TDelegate listener, bool allowDuplicates = false) {
    return tocsin.BindListener (listener, allowDuplicates);
  }

  public bool AddNonce (TDelegate listener, bool allowDuplicates = false) {
    return tocsin.AddNonce (listener, allowDuplicates);
  }

  public bool RemoveListener (TDelegate listener) { return tocsin.RemoveListener (listener); }

  public bool RemoveNonceListener (TDelegate listener) {
    return tocsin.RemoveNonceListener (listener);
  }

  public void RemoveAllListeners (bool removePersistant = true, bool removeNonce = true) {
    tocsin.RemoveAllListeners (removePersistant, removeNonce);
  }
}

public class Link: LinkBase<Action>, ILink {
  public Link (TocsinBase<Action> tocsin): base (tocsin) { }
}
public class Link<T>: LinkBase<Action<T>>, ILink<T> {
  public Link (TocsinBase<Action<T>> tocsin): base (tocsin) { }
}
public class Link<T, U>: LinkBase<Action<T, U>>, ILink<T, U> {
  public Link (TocsinBase<Action<T, U>> tocsin): base (tocsin) { }
}
public class Link<T, U, V>: LinkBase<Action<T, U, V>>, ILink<T, U, V> {
  public Link (TocsinBase<Action<T, U, V>> tocsin): base (tocsin) { }
}
public class Link<T, U, V, W>: LinkBase<Action<T, U, V, W>>, ILink<T, U, V, W> {
  public Link (TocsinBase<Action<T, U, V, W>> tocsin): base (tocsin) { }
}
}
