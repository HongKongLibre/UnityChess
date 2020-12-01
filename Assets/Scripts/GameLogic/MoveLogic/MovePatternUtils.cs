using System.Collections.Generic;
using UnityEngine;


public static class MovePatternUtils
{
    public static bool IsPositionOnBoard(Vector2Int position)
    {
        Vector2Int lowest_board_point = new Vector2Int(1, 1);
        Vector2Int highest_board_point = new Vector2Int(8, 8);
        return Vector2Int.Min(lowest_board_point, position) == lowest_board_point &&
               Vector2Int.Max(highest_board_point, position) == highest_board_point;
    }


    public static bool IsLineEmpty(Dictionary<Vector2Int, Piece> board, Vector2Int start, Vector2Int end)
    {
        int x_difference = end[0] - start[0];
        int y_difference = end[1] - start[1];
        if (x_difference == 0 || y_difference == 0 || Mathf.Abs(x_difference) == Mathf.Abs(y_difference))
        {
            Vector2Int direction = DirectionVector(end - start);
            while (start != end)
            {
                if (board.ContainsKey(start)) return false;
                start += direction;

            }
            return true;
        }
        else
        {
            Debug.LogError("Not a line");
            return false;
        }

    }

    public static bool HasPieceMoved(Piece piece)
    {
        return MoveHistory.Instance.HasPieceMoved(piece);
    }

    public static int GetDirection(int value)
    {
        var new_value = value == 0 ? 0 : Mathf.Abs(value) / value;
        return new_value;
    }

    public static Vector2Int DirectionVector(Vector2Int vector)
    {
        return new Vector2Int(GetDirection(vector.x), GetDirection(vector.y));
    }

    public static void MirrorVectors(ref List<Vector2Int> vectors)
    {
        var mirrored_vectors = new List<Vector2Int>();
        foreach (Vector2Int vector in vectors)
            mirrored_vectors.Add(vector * (-1));
        vectors.Clear();
        vectors.AddRange(mirrored_vectors);
    }

}

