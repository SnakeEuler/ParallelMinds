using UnityEngine;

namespace ParallelMinds.Utility {
public class SpringVector3: BaseSpring<Vector3> {
  private readonly FloatSpring xSpring = new ();
  private readonly FloatSpring ySpring = new ();
  private readonly FloatSpring zSpring = new ();

  public override float Damping {
    get { return base.Damping; }
    set {
      xSpring.Damping = value;
      ySpring.Damping = value;
      zSpring.Damping = value;
      base.Damping = value;
    }
  }

  public override float Stiffness {
    get { return base.Stiffness; }
    set {
      xSpring.Stiffness = value;
      ySpring.Stiffness = value;
      zSpring.Stiffness = value;
      base.Stiffness = value;
    }
  }

  public override Vector3 StartValue {
    get { return new Vector3 (xSpring.StartValue, ySpring.StartValue, zSpring.StartValue); }
    set {
      xSpring.StartValue = value.x;
      ySpring.StartValue = value.y;
      zSpring.StartValue = value.z;
    }
  }
  public override Vector3 EndValue {
    get { return new Vector3 (xSpring.EndValue, ySpring.EndValue, zSpring.EndValue); }
    set {
      xSpring.EndValue = value.x;
      ySpring.EndValue = value.y;
      zSpring.EndValue = value.z;
    }
  }

  public override Vector3 InitialVelocity {
    get {
      return new Vector3 (xSpring.InitialVelocity, ySpring.InitialVelocity,
      zSpring.InitialVelocity);
    }
    set {
      xSpring.InitialVelocity = value.x;
      ySpring.InitialVelocity = value.y;
      zSpring.InitialVelocity = value.z;
    }
  }

  public override Vector3 CurrentVelocity {
    get {
      return new Vector3 (xSpring.CurrentVelocity, ySpring.CurrentVelocity,
      zSpring.CurrentVelocity);
    }
    set {
      xSpring.CurrentVelocity = value.x;
      ySpring.CurrentVelocity = value.y;
      zSpring.CurrentVelocity = value.z;
    }
  }

  public override Vector3 CurrentValue {
    get { return new Vector3 (xSpring.CurrentValue, ySpring.CurrentValue, zSpring.CurrentValue); }
    set {
      xSpring.CurrentValue = value.x;
      ySpring.CurrentValue = value.y;
      zSpring.CurrentValue = value.z;
    }
  }

  public override Vector3 Evaluate (float deltaTime) {
    CurrentValue = new Vector3 (xSpring.Evaluate (deltaTime), ySpring.Evaluate (deltaTime),
    zSpring.Evaluate (deltaTime));
    CurrentVelocity = new Vector3 (xSpring.CurrentVelocity, ySpring.CurrentVelocity,
    zSpring.CurrentVelocity);
    return CurrentValue;
  }

  public override void Reset () {
    xSpring.Reset ();
    ySpring.Reset ();
    zSpring.Reset ();
  }

  public override void UpdateEndValue (Vector3 value, Vector3 velocity) {
    xSpring.UpdateEndValue (value.x, velocity.x);
    ySpring.UpdateEndValue (value.y, velocity.y);
    zSpring.UpdateEndValue (value.z, velocity.z);
  }
}
}
