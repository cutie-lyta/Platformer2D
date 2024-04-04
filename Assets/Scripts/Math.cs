using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A static class with static method for Math used in the game.
/// </summary>
public static class Math
{
    /// <summary>
    /// Quantize an angle, in Degree, to a certain quantization grid.
    /// </summary>
    /// <param name="angle"> The angle to quantize. </param>
    /// <param name="quantization"> The quantization factor, for example, 4 will let the value be 0, 90, 180 and 270. </param>
    /// <returns> The quantized angle. </returns>
    public static float QuantizeAngle(float angle, int quantization)
    {
        var array = new List<float>();

        for (int i = 0; i < 360; i += 360 / quantization)
        {
            array.Add(i);
        }

        array.Add(360);

        float rotation = angle;
        return array.OrderBy(x => System.Math.Abs((long)x - rotation)).First();
    }
}
