namespace ParallelMinds.Utility {
public abstract class BaseSpring<T> {
  // Default to critically damped
  public virtual float Damping { get; set; } = 26f;
  protected float Mass { get; set; } = 1f;
  public virtual float Stiffness { get; set; } = 169f;
  public virtual T StartValue { get; set; }
  public virtual T EndValue { get; set; }
  public virtual T InitialVelocity { get; set; }
  public virtual T CurrentValue { get; set; }
  public virtual T CurrentVelocity { get; set; }


  // Reset all values to initial states.
  public abstract void Reset ();


  // Update the end value in the middle of motion.
  // This reuse the current velocity and interpolate the value smoothly afterwards.
  // <param name="Value">End value</param>
  public virtual void UpdateEndValue (T value) => UpdateEndValue (value, CurrentVelocity);


  // Update the end value in the middle of motion but using a new velocity.

  public abstract void UpdateEndValue (T value, T velocity);


  // Advance a step by deltaTime(seconds).

  public abstract T Evaluate (float deltaTime);
}
}
