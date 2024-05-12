using System.Collections.Generic;
using UnityEngine;

public class MazeBehavior : BaseBehavior {
    [field: SerializeField]
    private GameObject DirtPrefab { get; set; }

    [field: SerializeField]
    private int Width { get; set; } = 100;

    [field: SerializeField]
    private int Height { get; set; } = 100;

    private List<GameObject> mazeTiles = new();

    protected override void OnStart() {
        var offset = new Vector3(0.5f, 0.5f);
        var mazeNodes = MazeGen.WalkMaze(Width, Height);
        for (var x = -5; x < Width + 5; x++) {
            for (var y = -5; y < Height + 5; y++) {
                // ghetto way of adding a border
                if (x < 0 || y < 0 || x >= Width || y >= Height || mazeNodes[x, y].State == NodeState.Filled) {
                    mazeTiles.Add(Instantiate(DirtPrefab, new Vector3(x, y) + offset, Quaternion.identity, transform));
                }
            }
        }
    }
}
