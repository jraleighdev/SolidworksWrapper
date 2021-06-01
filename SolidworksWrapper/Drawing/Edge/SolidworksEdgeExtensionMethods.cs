using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidworksWrapper.Drawing.Points.Interfaces;

namespace SolidworksWrapper.Drawing.Edge
{
    public static class SolidworksEdgeExtensionMethods
    {
        public static List<SolidworksDrawingEdge> CircularEdges(this IEnumerable<SolidworksDrawingEdge> edges) =>
            edges.Where(x => x.IsCircle).ToList();

        public static List<SolidworksDrawingEdge> LinearEdges(this IEnumerable<SolidworksDrawingEdge> edges) =>
            edges.Where(x => x.IsLine).ToList();

        public static List<SolidworksDrawingEdge> VerticalEdges(this IEnumerable<SolidworksDrawingEdge> edges) =>
            edges.LinearEdges()?.Where(x => x.IsVertical).ToList();

        public static List<SolidworksDrawingEdge> HorizontalEdges(this IEnumerable<SolidworksDrawingEdge> edges) =>
            edges.LinearEdges()?.Where(x => x.IsHorizontal).ToList();

        public static SolidworksDrawingEdge RightEdge(this IEnumerable<SolidworksDrawingEdge> edges)
        {
            var verticalEdges = edges.VerticalEdges();

            if (verticalEdges == null || verticalEdges.Count == 0) return null;

            SolidworksDrawingEdge rightEdge = null;

            var maxX = 0.00;

            for (var i = 0; i < verticalEdges.Count; i++)
            {
                var edge = verticalEdges[i];

                if (i == 0)
                {
                    rightEdge = edge;
                    maxX = edge.StartPoint.X;
                }
                else if (edge.StartPoint.X > maxX)
                {
                    rightEdge = edge;
                    maxX = edge.StartPoint.X;
                }
            }

            return rightEdge;
        }

        public static SolidworksDrawingEdge LeftEdge(this IEnumerable<SolidworksDrawingEdge> edges)
        {
            var verticalEdges = edges.VerticalEdges();

            if (verticalEdges == null || verticalEdges.Count == 0) return null;

            SolidworksDrawingEdge leftEdge = null;

            var minX = 0.00;

            for (var i = 0; i < verticalEdges.Count; i++)
            {
                var edge = verticalEdges[i];

                if (i == 0)
                {
                    leftEdge = edge;
                    minX = edge.StartPoint.X;
                }
                else if (edge.StartPoint.X < minX)
                {
                    leftEdge = edge;
                    minX = edge.StartPoint.X;
                }
            }

            return leftEdge;
        }

        public static SolidworksDrawingEdge TopEdge(this IEnumerable<SolidworksDrawingEdge> edges)
        {
            var horizontalEdge = edges.VerticalEdges();

            if (horizontalEdge == null || horizontalEdge.Count == 0) return null;

            SolidworksDrawingEdge topEdge = null;

            var maxY = 0.00;

            for (var i = 0; i < horizontalEdge.Count; i++)
            {
                var edge = horizontalEdge[i];

                if (i == 0)
                {
                    topEdge = edge;
                    maxY = edge.StartPoint.Y;
                }
                else if (edge.StartPoint.Y > maxY)
                {
                    topEdge = edge;
                    maxY = edge.StartPoint.Y;
                }
            }

            return topEdge;
        }

        public static SolidworksDrawingEdge BottomEdge(this IEnumerable<SolidworksDrawingEdge> edges)
        {
            var horizontalEdge = edges.VerticalEdges();

            if (horizontalEdge == null || horizontalEdge.Count == 0) return null;

            SolidworksDrawingEdge bottomEdge = null;

            var minY = 0.00;

            for (var i = 0; i < horizontalEdge.Count; i++)
            {
                var edge = horizontalEdge[i];

                if (i == 0)
                {
                    bottomEdge = edge;
                    minY = edge.StartPoint.Y;
                }
                else if (edge.StartPoint.Y < minY)
                {
                    bottomEdge = edge;
                    minY = edge.StartPoint.Y;
                }
            }

            return bottomEdge;
        }

        public static List<ISolidworksPoint> CircularPoints(this IEnumerable<SolidworksDrawingEdge> edges)
        {
            var circularEdges = edges.CircularEdges();

            return circularEdges.Select(x => x.CenterPoint).ToList();
        }

        public static SolidworksDrawingEdge LongestEdge(this IEnumerable<SolidworksDrawingEdge> edges)
        {
            var longestEdge = edges.Max(x => x.Length);

            return edges.FirstOrDefault(x => Math.Abs(x.Length - longestEdge) < 0.005);
        }

        public static bool AlignedWithSheet(this IEnumerable<SolidworksDrawingEdge> edges)
        {
            var alignedWithSheet = 0;

            var notAligned = 0;

            foreach (var e in edges.Where(x => x.IsLine))
            {
                // if both are true then the edge is to small
                if (e.IsVertical && e.IsHorizontal) continue;

                if (e.IsVertical || e.IsHorizontal)
                {
                    alignedWithSheet++;
                }
                else
                {
                    notAligned++;
                }
            }

            return alignedWithSheet > notAligned;
        }
    }
}
