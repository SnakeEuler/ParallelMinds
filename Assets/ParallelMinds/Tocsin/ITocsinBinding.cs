using UnityEngine;
namespace Tocsin {
public interface ITocsinBinding  {
  bool Enabled { get; }
  bool AllowDuplicates { get; set; }
  uint PersistentListenerCount { get; }
  bool Enable (bool enable);
}
}
