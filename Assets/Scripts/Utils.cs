using UnityEngine;
using static Types;

public class Utils
{
    private static readonly float[,] DamageMatrix =
    {
        {1.00f, 1.00f, 1.50f, 1.00f},
        {1.50f, 2.00f, 0.75f, 1.00f},
        {0.75f, 1.25f, 0.75f, 2.00f},
        {1.00f, 1.00f, 1.00f, 1.00f}
    };
    public static float GetDamageMulti(DamageType damageType, ArmourType armourType)
    {
        return DamageMatrix[(int) damageType, (int) armourType];
    }

};
