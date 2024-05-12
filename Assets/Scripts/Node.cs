using System;
using System.Diagnostics;
using UnityEngine;

[DebuggerDisplay("{State, nq}", Name = "{Position, nq}")]
public class Node : IEquatable<Node> {

    public Vector2Int Position { get; set; }
    public NodeState State { get; set; }

    public Node(Vector2Int position, NodeState state = NodeState.Empty) {
        Position = position;
        State = state;
    }

    // dirty
    public override bool Equals(object obj) {
        return Equals((Node)obj);
    }

    public bool Equals(Node other) {
        return Position == other.Position;
    }

    public override int GetHashCode() {
        return Position.GetHashCode();
    }
}

public enum NodeState {
    Empty,
    Filled
}