using System;
using System.Collections.Generic;
using UnityEngine;

using RNG = UnityEngine.Random;

public class MazeGen {
    private static List<(Direction, Vector2Int)> DirectionVectors { get; } = new() {
        (Direction.Left, Vector2Int.left),
        (Direction.Right, Vector2Int.right),
        (Direction.Up, Vector2Int.up),
        (Direction.Down, Vector2Int.down)
    };


    public static Node[,] WalkMaze(int width, int height) {
        var mazeNodes = new Node[width, height];
        // Seed maze with a basic noise map initially
        var noiseSeed = RNG.Range(short.MinValue, short.MaxValue);
        var cutoffValue = -10;// RNG.Range(0.2f, 0.5f); // cutoff value for determining a full vs empty node
        Debug.Log($"Noise seed: {noiseSeed}, noise cutoff: {cutoffValue}");
        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                var noiseValue = Mathf.Abs(Mathf.PI * Noise.GradientNoiseHQ(x, y, noiseSeed));
                mazeNodes[x, y] = new Node(new(x, y), noiseValue < cutoffValue ? NodeState.Empty : NodeState.Filled);
            }
        }
        // Now build out the maze with a closed loop random walk
        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                if (mazeNodes[x, y].State == NodeState.Filled) {
                    RandomWalk(new(x, y), ref mazeNodes);
                }
            }
        }
        return mazeNodes;
    }

    private static void RandomWalk(Vector2Int position, ref Node[,] map) {
        var (dir, posVec) = RandomDirection();
        while (!ValidNode(position + posVec, map)) {
            (dir, posVec) = RandomDirection();
        }
        var nextPos = position + posVec;
        while (map[nextPos.x, nextPos.y].State == NodeState.Filled) {
            map[nextPos.x, nextPos.y].State = NodeState.Empty;
            while (!ValidNode(position, map)) {
                (dir, posVec) = RandomDirection();
                nextPos += posVec;
            }
        }
    }

    private static bool ValidNode(Vector2Int position, Node[,] map) {
        if (position.x < 0 || position.y < 0) {
            return false;
        }
        if (position.x >= map.GetLength(0) || position.y >= map.GetLength(1)) {
            return false;
        }
        return true;// map[position.x, position.y].State == NodeState.Filled;
    }

    private static (Direction, Vector2Int) RandomDirection() {
        return DirectionVectors[RNG.Range(0, DirectionVectors.Count)];
    }
}

[Flags]
public enum Direction {
    Idle = 0,
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8
}
