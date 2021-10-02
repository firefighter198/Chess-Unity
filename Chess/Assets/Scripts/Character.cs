using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Team team;
    public CharacterType type;
}

public enum CharacterType
{
    Rook, Knight, Bishop, Queen, King, Pawn
}
