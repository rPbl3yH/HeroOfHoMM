﻿using UnityEngine;

public class StandaloneInputService : InputService
{
    public override Vector2 Axis {
        get {
            Vector2 axis = new MobileInputService().Axis;
            if (axis == Vector2.zero) {
                axis = UnityAxis();
            }
            return axis;
        }
    }

    private static Vector2 UnityAxis() => new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
}