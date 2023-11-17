using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tocsin {
public abstract class TocsinBase<TDelegate>: ILinkBase<TDelegate> where TDelegate : class {
  public uint PersistentListenerCount { get { return pCount; } }
  public uint NonceListenerCount { get { return nCount; } }

  protected bool hasLink = false;

  protected TDelegate[] listeners = new TDelegate[1];
  protected uint pCount = 0;
  protected uint pCap = 0;

  protected TDelegate[] nonceListeners;
  protected uint nCount = 0;
  protected uint nCap = 0;

  protected static IndexOutOfRangeException eIOOR = new IndexOutOfRangeException (
  "Fewer listeners than expected. See guidelines in on using RemoveListener and RemoveAll within Tocsin listeners.");


  public bool Contains (TDelegate listener) { return Contains (listeners, pCount, listener); }
  public bool AddListener (TDelegate listener, bool allowDuplicates = false) {
    if (!allowDuplicates && Contains (listener)) return false;
    if (pCount >= pCap) {// Changed from == to >= to handle the case where pCap might be 0
      if (pCap == 0) {
        pCap = 1;// Set a starting capacity if it was 0
      } else {
        pCap *= 2;// Otherwise, double the capacity
      }
      listeners = Expand (listeners, pCap, pCount);
    }
    listeners[pCount] = listener;
    pCount++;
    return true;
  }

  public ITocsinBinding BindListener (TDelegate listener, bool allowDuplicates = false) {
    if (AddListener (listener, allowDuplicates)) {

      return new TocsinBinding<TDelegate> (this, listener, allowDuplicates, true);

    }
    return null;
  }
  public bool AddNonce (TDelegate listener, bool allowDuplicates = false) {
    if (!allowDuplicates && Contains (nonceListeners, nCount, listener)) return false;
    if (nCount == nCap) {
      if (nCap == 0) {
        nCap = 1;
      } else {
        nCap *= 2;
      }
      nonceListeners = Expand (nonceListeners, nCap, nCount);
    }
    nonceListeners[nCount] = listener;
    ++ nCount;

    return true;
  }

  public bool RemoveListener (TDelegate listener) {
    bool result = false;
    for (uint i = 0;i < pCount;++ i) {
      if (listeners[i].Equals (listener)) {
        RemoveAt (i);
        result = true;
        break;
      }
    }
    return result;
  }

  public bool RemoveNonceListener (TDelegate listener) {
    bool result = false;
    for (uint i = 0;i < pCount;++ i) {
      if (nonceListeners[i].Equals (listener)) {
        RemoveAt (i);
        result = true;
        break;
      }
    }
    return result;
  }

  public void RemoveAllListeners (bool removePersistant = true, bool removeNonce = true) {
    if (removePersistant) {
      Array.Clear (listeners, 0, (int)pCap);

      pCount = 0;
    }
    if (removeNonce && nCount > 0) {
      Array.Clear (nonceListeners, 0, (int)nCap);
      nCount = 0;
    }
  }

  protected void RemoveAt (uint i) { pCount = RemoveAt (listeners, pCount, i); }

  protected void RemoveOnceAt (uint i) { nCount = RemoveAt (nonceListeners, nCount, i); }

  protected uint RemoveAt (TDelegate[] arr, uint pCount, uint i) {
    -- pCount;
    for (uint j = i;j < pCount;++ j) {
      arr[j] = arr[j + 1];
    }
    arr[pCount] = null;
    return pCount;
  }

  bool Contains (TDelegate[] arr, uint c, TDelegate d) {
    for (uint i = 0;i < c;++ i) {
      if (arr[i].Equals (d)) {
        return true;
      }
    }
    return false;
  }
  TDelegate[] Expand (TDelegate[] arr, uint cap, uint count) {
    TDelegate[] newArr = new TDelegate[cap];
    for (int i = 0;i < count;++ i) {
      newArr[i] = arr[i];
    }
    return newArr;
  }
}
}
