// CurveAttribute.cs

using UnityEngine;

public class CurveAttribute: PropertyAttribute {
  public readonly float posX;
  public readonly float posY;
  public readonly float rangeX;
  public readonly float rangeY;
  public readonly bool b;
  public int x;

  public CurveAttribute (float posX, float posY, float RangeX, float RangeY, bool b, int x) {
    this.posX = posX;
    this.posY = posY;
    this.rangeX = RangeX;
    this.rangeY = RangeY;
    this.b = b;
    this.x = x;
  }
}
