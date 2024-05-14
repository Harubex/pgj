using System;
using UnityEngine;

public partial class PlayerBehavior : BaseBehavior {
    [field: SerializeField]
    private float MoveSpeed { get; set; } = 1;


    protected override void OnStart() {
        InitializeInput();
    }

    protected override void OnUpdate(TimeSpan delta) {
        var dt = (float)delta.TotalSeconds;
        if ((CurrentDirection & Direction.Left) > Direction.Idle) {
            Position -= dt * MoveSpeed * Vector3.left;
        } else if ((CurrentDirection & Direction.Right) > Direction.Idle) {
            Position -= dt * MoveSpeed * Vector3.right;
        } else if ((CurrentDirection & Direction.Up) > Direction.Idle) {
            Position -= dt * MoveSpeed * Vector3.up;
        }
    }
}
