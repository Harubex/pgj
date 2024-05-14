using System;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerEvents : BaseBehavior {
    #region Eventing
    /// <summary>
    /// Called when the player attempts to move in the specified direction.
    /// </summary>
    public static event Action<Direction> StartMoving;
    /// <summary>
    /// Called when the player stops trying to move in the specified direction.
    /// </summary>
    public static event Action<Direction> StopMoving;
    /// <summary>
    /// Called when the player presses the input that opens/closes the inventory.
    /// </summary>
    public static event Action ToggleInventory;
    #endregion


    [field: SerializeField]
    private InputActionReference MoveLeft { get; set; }

    [field: SerializeField]
    private InputActionReference MoveRight { get; set; }

    [field: SerializeField]
    private InputActionReference MoveUp { get; set; }

    [field: SerializeField]
    private InputActionReference MoveDown { get; set; }

    [field: SerializeField]
    private InputActionReference InventoryToggle { get; set; }

    protected override void OnStart() {
        // All events should be instrumented in here, otherwise they will never fire.
        MoveUp.action.started += (_) => StartMoving(Direction.Up);
        //MoveDown.action.started += (_) => StartMoving(Direction.Down);
        MoveLeft.action.started += (_) => StartMoving(Direction.Left);
        MoveRight.action.started += (_) => StartMoving(Direction.Right);

        MoveRight.action.canceled += (_) => StopMoving(Direction.Right);
        MoveLeft.action.canceled += (_) => StopMoving(Direction.Left);
        //MoveDown.action.canceled += (_) => StopMoving(Direction.Down);
        MoveUp.action.canceled += (_) => StopMoving(Direction.Up);

        //InventoryToggle.action.performed += (_) => ToggleInventory();

        // Bind default call to each event to avoid null reference errors.
        BindLogEvents();
    }

    private static void BindLogEvents() {
        StartMoving += (dir) => Log.Verbose($"StartMoving Event triggered (direction: {dir})");
        StopMoving += (dir) => Log.Verbose($"StopMoving Event triggered (direction: {dir})");
        ToggleInventory += () => Log.Verbose($"ToggleInventory Event triggered");
    }
}
