using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VectorDraw.Functional
{
    public class LineIntersectionManager
    {
        public static IntersectionCheck DoLinesIntersect(Vector3 line1Start, Vector3 line1End, Vector3 line2Start, Vector3 line2End)
        {
            //Create equations.
            Equation line1Equation = new Equation(line1Start, line1End);

            Equation line2Equation = new Equation(line2Start, line2End);

            //find out the intersection point
            Vector3 intersection = line1Equation.FindIntersection(line2Equation);

            //Does intersection point fall within the line segments?
            if(IsIntersectionWithinLineX(line1Start, line1End, intersection)) {
                if(IsIntersectionWithLineY(line1Start, line1End, intersection))
                {
                    if(IsIntersectionWithinLineX(line2Start, line2End, intersection))
                    {
                        if (IsIntersectionWithLineY(line2Start, line2End, intersection))
                        {
                            IntersectionCheck ic = new IntersectionCheck {
                                didIntersect = true,
                                intersectionPoint = intersection
                            };
                            return ic;
                        }
                    }
                }
            };
            return new IntersectionCheck
            {
                didIntersect = false,
                intersectionPoint = intersection
            };
        }

        private static bool IsIntersectionWithinLineX(Vector3 line1Start, Vector3 line1End, Vector3 intersection)
        {
            bool isInterPoint = false;
            if (line1Start.x > intersection.x)
            {
                if (line1End.x < intersection.x)
                {
                    isInterPoint = true;
                }
            }
            else if (line1Start.x < intersection.x)
            {
                if (line1End.x > intersection.x)
                {
                    isInterPoint = true;
                }

            }
            else if (line1Start.x == intersection.x)
            {
                isInterPoint = true;
            }
            else if (intersection.x == line1End.x)
            {
                isInterPoint = true;
            }
            return isInterPoint;
        }

        private static bool IsIntersectionWithLineY(Vector3 line1Start, Vector3 line1End, Vector3 intersection)
        {
            bool isInterPoint = false;
            if (line1Start.y > intersection.y)
            {
                if (line1End.y < intersection.y)
                {
                    isInterPoint = true;
                }
            }
            else if (line1Start.y < intersection.y)
            {
                if (line1End.y > intersection.y)
                {
                    isInterPoint = true;
                }

            }
            else if (line1Start.y == intersection.y)
            {
                isInterPoint = true;
            }
            else if (intersection.y == line1End.y)
            {
                isInterPoint = true;
            }
            return isInterPoint;
        }

        public struct IntersectionCheck
        {
            public bool didIntersect;
            public Vector3 intersectionPoint;
        }
    }
}
