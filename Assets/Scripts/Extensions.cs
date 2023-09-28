using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tanks
{
public class Extensions : MonoBehaviour
{
    private static Dictionary<DirectionType, Vector3> m_directions;
    private static Dictionary<DirectionType, Vector3> m_rotation;

    static Extensions()
    {
        m_directions = new Dictionary<DirectionType, Vector3>
        {
            { DirectionType.Up , new Vector3(0f, 1f, 0f)},
            { DirectionType.Right , new Vector3(1f, 0f, 0f)},
            { DirectionType.Down , new Vector3(0f, -1f, 0f)},
            { DirectionType.Left , new Vector3(-1f, 0f, 0f)},
        };
        
        m_rotation = new Dictionary<DirectionType, Vector3>
        {
            {DirectionType.Up , new Vector3(0f, 1f, 0f)},
            { DirectionType.Right , new Vector3(1f, 0f, 270f)},
            { DirectionType.Down , new Vector3(0f, -1f, 180f)},
            { DirectionType.Left , new Vector3(-1f, 0f, 90f)},
        };
    }

    public static Vector3 ConvertTypeFromDirection(DirectionType type) => m_directions[type];
    public static Vector3 ConvertTypeFromDRotation(DirectionType type) => m_rotation[type];

    public static DirectionType ConvertDirectionFromType(Vector3 direction) =>
        m_directions.First(t => t.Value == direction).Key;

    public static DirectionType ConvertDirectionFromType(Vector2 direction)
    {
        var dir = (Vector3)direction;
        return m_directions.First(t => t.Value == dir).Key;
    }
    
    public static DirectionType ConvertRotationFromType( Vector3 rotation) =>
        m_rotation.First(t => t.Value == rotation).Key;
    

}
public enum DirectionType : byte
{
    Error, Up, Right, Down, Left
}
public enum SideType : byte
{
    None, Player, Enemy
}
}
