using UnityEngine;

namespace ParallelMinds.Utility {
public class SpringVector2: BaseSpring<Vector2> {
  private readonly FloatSpring xSpring = new ();
  private readonly FloatSpring ySpring = new ();

  public override float Damping {
    get => base.Damping;
    set {
      xSpring.Damping = value;
      ySpring.Damping = value;
      base.Damping = value;
    }
  }

  public override float Stiffness {
    get { return base.Stiffness; }
    set {
      xSpring.Stiffness = value;
      ySpring.Stiffness = value;
      base.Stiffness = value;
    }
  }

  public override Vector2 InitialVelocity {
    get { return new Vector2 (xSpring.InitialVelocity, ySpring.InitialVelocity); }
    set {
      xSpring.InitialVelocity = value.x;
      ySpring.InitialVelocity = value.y;
    }
  }

  public override Vector2 StartValue {
    get { return new Vector2 (xSpring.StartValue, ySpring.StartValue); }
    set {
      xSpring.StartValue = value.x;
      ySpring.StartValue = value.y;
    }
  }
  public override Vector2 EndValue {
    get { return new Vector2 (xSpring.EndValue, ySpring.EndValue); }
    set {
      xSpring.EndValue = value.x;
      ySpring.EndValue = value.y;
    }
  }

  public override Vector2 CurrentVelocity {
    get { return new Vector2 (xSpring.CurrentVelocity, ySpring.CurrentVelocity); }
    set {
      xSpring.CurrentVelocity = value.x;
      ySpring.CurrentVelocity = value.y;
    }
  }

  public override Vector2 CurrentValue {
    get { return new Vector2 (xSpring.CurrentValue, ySpring.CurrentValue); }
    set {
      xSpring.CurrentValue = value.x;
      ySpring.CurrentValue = value.y;
    }
  }

  public override Vector2 Evaluate (float deltaTime) {
    CurrentValue = new Vector2 (xSpring.Evaluate (deltaTime), ySpring.Evaluate (deltaTime));
    CurrentVelocity = new Vector2 (xSpring.CurrentVelocity, ySpring.CurrentVelocity);
    return CurrentValue;
  }

  public override void Reset () {
    xSpring.Reset ();
    ySpring.Reset ();
  }

  public override void UpdateEndValue (Vector2 value, Vector2 velocity) {
    xSpring.UpdateEndValue (value.x, velocity.x);
    ySpring.UpdateEndValue (value.y, velocity.y);
  }
}
}
