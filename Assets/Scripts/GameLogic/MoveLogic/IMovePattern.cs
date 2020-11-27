using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public interface IMovePattern
{
    void Move(Vector2Int new_position);
    List<Vector2Int> GetPossibleMoves();

}


