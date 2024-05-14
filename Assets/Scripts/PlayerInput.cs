using System.Collections.Generic;

public partial class PlayerBehavior : BaseBehavior {

    /// <summary>
    /// A bitwise flag representing zero or more directions that the player is currently trying to move.
    /// </summary>
    /// <remarks>To future hannah - MoveSpeed is arbitrary, and just used to account for when it drops to 0.</remarks>
    private Direction CurrentDirection {
        get {
            var x = 0f;
            var y = 0f;

            if (activeDirections.Contains(Direction.Up)) {
                y -= MoveSpeed;
            }
            if (activeDirections.Contains(Direction.Down)) {
                y += MoveSpeed;
            }
            if (activeDirections.Contains(Direction.Left)) {
                x -= MoveSpeed;
            }
            if (activeDirections.Contains(Direction.Right)) {
                x += MoveSpeed;
            }
            var direction = Direction.Idle;
            if (x > 0) {
                direction |= Direction.Left;
            } else if (x < 0) {
                direction |= Direction.Right;
            }
            if (y < 0) {
                direction |= Direction.Up;
            } else if (y > 0) {
                direction |= Direction.Down;
            }

            return direction;
        }
    }

    private void InitializeInput() {
        PlayerEvents.StartMoving += OnStartMoving;
        PlayerEvents.StopMoving += OnStopMoving;
    }

    private readonly IList<Direction> activeDirections = new List<Direction>();

    private void OnStartMoving(Direction direction) {
        if (!activeDirections.Contains(direction)) {
            activeDirections.Add(direction);
        }
    }

    private void OnStopMoving(Direction direction) {
        if (activeDirections.Contains(direction)) {
            activeDirections.Remove(direction);
        }
    }
}
