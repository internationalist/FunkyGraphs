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
    public class Equation
    {
        private readonly float slope;
        private readonly float C;

        public float Constant
        {
            get
            {
                return C;
            }
        }

        public float Slope
        {
            get
            {
                return slope;
            }
        }

        /// <summary>
        /// <para>Given two points (x1,y1) and (x2,y2) the slope of the straight line between then can be calculated thus:</para>
        /// <para>(x1 - x2)/(y1 - y2)</para>
        /// </summary>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        public Equation(Vector3 lineStart, Vector3 lineEnd)
        {
            this.slope = (lineEnd.x - lineStart.x) / (lineEnd.y - lineStart.y);
            this.C = lineStart.x - Slope * lineStart.y;
        }

        public float GetX(float y)
        {
            return y * Slope + Constant;
        }

        public float GetY(float x)
        {
            return (x - Constant)/Slope;
        }

        public Vector3 FindIntersection(Equation other)
        {
            //Equate the two equations.
            float y = (other.Constant - Constant)/(Slope - other.Slope);
            //now derive the x value.
            float X = GetX(y);
            Vector3 intersection = new Vector3 {
                x = X,
                y = y
            };
            return intersection;
        }
    }
}
