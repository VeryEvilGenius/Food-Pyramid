using UnityEngine;

public sealed class DirectionEnum
{
    public static readonly DirectionEnum None = new(null);
    public static readonly DirectionEnum Up = new("Attack");
    public static readonly DirectionEnum Right = new("Dodge_Right");
    public static readonly DirectionEnum Left = new("Dodge_Left");

    private DirectionEnum(string value)
    {
        if (value == null)
        {
            Hash = 0;
            return;
        }
            
        Hash = Animator.StringToHash(value);
    }

    public override string ToString()
    {
        return GetType().Name;
    }

    public int Hash { get; }
}
