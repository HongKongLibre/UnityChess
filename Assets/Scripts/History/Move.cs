using UnityEngine;


public struct Move
{
    public readonly Vector2Int old_position;
    public readonly Vector2Int new_position;
    public readonly Piece piece;

    public Move(Vector2Int old_position, Vector2Int new_position, Piece piece)
    {
        this.old_position = old_position;
        this.new_position = new_position;
        this.piece = piece;

    }

    public (Vector2Int, Vector2Int, Piece) Unzip()
    {
        return (this.old_position, this.new_position, this.piece);
    }
}

