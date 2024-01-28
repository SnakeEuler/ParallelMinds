// CurveDrawer.cs

using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer (typeof (CurveAttribute))]
public class CurveDrawer: PropertyDrawer {
  public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
    var curve = attribute as CurveAttribute;
    if (property.propertyType != SerializedPropertyType.AnimationCurve) return;
    if (curve is { b: true })
      EditorGUI.CurveField (position, property, Color.cyan,
      new Rect (curve.posX, curve.posY, curve.rangeX, curve.rangeY));
  }
}
