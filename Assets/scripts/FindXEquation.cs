using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VectorDraw.Functional
{
    /// <summary>
    /// <para>Represents a linear equation of the form x = my + C</para>
    /// <para>Given a line segment represented by two 2D positions, this class can derive the equation representing the straight line between them.</para>
    /// <para>This class is useful in finding the intersetion points between two lines.</para>
    /// </summary>
    public class FindXEquation
    {
        private float slope;
        private float C;
        private bool initialized = false;

        public bool Initialized
        {
            get
            {
                return initialized;
            }

            set
            {
                initialized = value;
            }
        }

        public float Constant
        {
            get
            {
                return C;
            }
        }


        public FindXEquation(float slope)
        {
            this.slope = slope;
        }

        public float GetX(float y)
        {
            return y * slope + Constant;
        }

        public void CalculateConstant(float x, float y)
        {
            C = x - slope * y;
            initialized = true;
        }
    }
}
