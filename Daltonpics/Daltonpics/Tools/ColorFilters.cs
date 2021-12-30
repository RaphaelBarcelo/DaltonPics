using System;
using System.Collections.Generic;
using System.Text;

namespace Daltonpics.Tools
{
    public enum ColorBlindnessType { VISION_NORMALE, DEUTERANOMALIE, DEUTERANOPIE, PROTOANOMALIE, PROTOANOPIE, TRITANOMALIE, TRITANOPIE, ACHROMATOPSIE };

    public class ColorFilters
    {
        public ColorFilters()
        { }

        public readonly Dictionary<ColorBlindnessType, float[]> ColorBlindnessFilters = new Dictionary<ColorBlindnessType, float[]>()
        {
            { ColorBlindnessType.VISION_NORMALE , new float[] {
                    1f, 0, 0, 0, 0,
                    0, 1f, 0, 0, 0,
                    0, 0, 1f, 0, 0,
                    0,     0,     0,     1f, 0
            } },
           { ColorBlindnessType.DEUTERANOMALIE , new float[]{
                    0.60f, 0, 0, 0, 0,
                    0, 1f, 0, 0, 0,
                    0, 0, 1f, 0, 0,
                    0,     0,     0,     1f, 0
           } },
           { ColorBlindnessType.DEUTERANOPIE , new float[] {
                    0, 0, 0, 0, 0,
                    0, 1f, 0, 0, 0,
                    0, 0, 1f, 0, 0,
                    0,     0,     0,     1f, 0
            } }  ,

            { ColorBlindnessType.PROTOANOMALIE , new float[] {
                    1f, 0, 0, 0, 0,
                    0, 0.60f, 0, 0, 0,
                    0, 0, 1f, 0, 0,
                    0,     0,     0,     1f, 0
            } },

            { ColorBlindnessType.PROTOANOPIE , new float[] {
                    1f, 0, 0, 0, 0,
                    0, 0, 0, 0, 0,
                    0, 0, 1f, 0, 0,
                    0,     0,     0,     1f, 0
            } },

            { ColorBlindnessType.TRITANOMALIE , new float[] {
                    1f, 0, 0, 0, 0,
                    0, 1f, 0, 0, 0,
                    0, 0, 0.60f, 0, 0,
                    0,     0,     0,     1f, 0
            } },

            { ColorBlindnessType.TRITANOPIE , new float[] {
                    1f, 0, 0, 0, 0,
                    0, 1f, 0, 0, 0,
                    0, 0, 0, 0, 0,
                    0,     0,     0,     1f, 0
            } },

            { ColorBlindnessType.ACHROMATOPSIE , new float[] {
                    0.21f, 0.72f, 0.07f, 0, 0,
                    0.21f, 0.72f, 0.07f, 0, 0,
                    0.21f, 0.72f, 0.07f, 0, 0,
                    0,     0,     0,     1f, 0
            } }


        };





    }
}
