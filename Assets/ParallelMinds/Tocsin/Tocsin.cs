using System;
using System.Collections.Generic;
namespace Tocsin {
public class Tocsin: TocsinBase<Action>, ILink {
  private ILink link = null;

  public ILink plink {
    get {
      if (!hasLink) {
        link = new Link (this);
        hasLink = true;
      }
      return link;
    }
  }

  public void Dispatch () {
    for (uint i = pCount;i > 0;-- i) {
      if (i > pCount) throw eIOOR;
      if (listeners[i - 1] != null) {
        listeners[i - 1] ();
      } else {
        RemoveAt (i - 1);
      }
    }
    for (uint i = nCount;i > 0;-- i) {
      var l = nonceListeners[i - 1];
      nCount = RemoveAt (nonceListeners, nCount, i - 1);
      if (l != null) { l (); }
    }
  }
}

public class Tocsin<T>: TocsinBase<Action<T>>, ILink<T> {
  private ILink<T> link = null;

  public ILink<T> plink {
    get {
      if (!hasLink) {
        link = new Link<T> (this);
        hasLink = true;
      }
      return link;
    }
  }

  public void Dispatch (T t) {
    for (uint i = pCount;i > 0;-- i) {
      if (i > pCount) throw eIOOR;
      if (listeners[i - 1] != null) {
        listeners[i - 1] (t);
      } else {
        RemoveAt (i - 1);
      }
    }
    for (uint i = nCount;i > 0;-- i) {
      var l = nonceListeners[i - 1];
      nCount = RemoveAt (nonceListeners, nCount, i - 1);
      if (l != null) { l (t); }
    }
  }
}

public class Tocsin<T, U>: TocsinBase<Action<T, U>>, ILink<T, U> {
  private ILink<T, U> link = null;

  public ILink<T, U> plink {
    get {
      if (!hasLink) {
        link = new Link<T, U> (this);
        hasLink = true;
      }
      return link;
    }
  }

  public void Dispatch (T t, U u) {
    for (uint i = pCount;i > 0;-- i) {
      if (i > pCount) throw eIOOR;
      if (listeners[i - 1] != null) {
        listeners[i - 1] (t, u);
      } else {
        RemoveAt (i - 1);
      }
    }
    for (uint i = nCount;i > 0;-- i) {
      var l = nonceListeners[i - 1];
      nCount = RemoveAt (nonceListeners, nCount, i - 1);
      if (l != null) { l (t, u); }
    }
  }
}

public class Tocsin<T, U, V>: TocsinBase<Action<T, U, V>>, ILink<T, U, V> {
  private ILink<T, U, V> link = null;

  public ILink<T, U, V> plink {
    get {
      if (!hasLink) {
        link = new Link<T, U, V> (this);
        hasLink = true;
      }
      return link;
    }
  }

  public void Dispatch (T t, U u, V v) {
    for (uint i = pCount;i > 0;-- i) {
      if (i > pCount) throw eIOOR;
      if (listeners[i - 1] != null) {
        listeners[i - 1] (t, u, v);
      } else {
        RemoveAt (i - 1);
      }
    }
    for (uint i = nCount;i > 0;-- i) {
      var l = nonceListeners[i - 1];
      nCount = RemoveAt (nonceListeners, nCount, i - 1);
      if (l != null) { l (t, u, v); }
    }
  }
}

public class Tocsin<T, U, V, W>: TocsinBase<Action<T, U, V, W>>, ILink<T, U, V, W> {
  private ILink<T, U, V, W> link = null;

  public ILink<T, U, V, W> plink {
    get {
      if (!hasLink) {
        link = new Link<T, U, V, W> (this);
        hasLink = true;
      }
      return link;
    }
  }

  public void Dispatch (T t, U u, V v, W w) {
    for (uint i = pCount;i > 0;-- i) {
      if (i > pCount) throw eIOOR;
      if (listeners[i - 1] != null) {
        listeners[i - 1] (t, u, v, w);
      } else {
        RemoveAt (i - 1);
      }
    }
    for (uint i = nCount;i > 0;-- i) {
      var l = nonceListeners[i - 1];
      nCount = RemoveAt (nonceListeners, nCount, i - 1);
      if (l != null) { l (t, u, v, w); }
    }
  }
}
}
