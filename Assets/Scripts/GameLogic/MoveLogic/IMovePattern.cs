using System.Collections.Generic;
using UnityEngine;


public interface IMovePattern
{
    void Move(Vector2Int new_position);
    List<Vector2Int> GetPossibleMoves();

}


