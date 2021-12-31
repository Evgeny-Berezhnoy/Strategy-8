using UnityEngine;

[CreateAssetMenu(fileName = nameof(PointableValue), menuName = "Strategy Game/" + nameof(PointableValue), order = 4)]
public class PointableValue : UIValue<IPointable> { }