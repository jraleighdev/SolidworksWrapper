using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.swconst;
using SolidworksWrapper.Drawing.Points.Interfaces;

namespace SolidworksWrapper.Drawing.Points.Extensions
{
    public static class SolidworksPointExtensionMethods
    {
        public static ISolidworksPoint GetMaxXMaxYPoint(this IEnumerable<ISolidworksPoint> points)
        {
            if (points.IsEmpty()) return null;

            return points.GetMaxXPoints().GetMaxYPoint();
        }

        public static ISolidworksPoint GetMaxXMinYPoint(this IEnumerable<ISolidworksPoint> points)
        {
            if (points.IsEmpty()) return null;

            return points.GetMaxXPoints().GetMinYPoint();
        }

        public static ISolidworksPoint GetMinXMaxYPoint(this IEnumerable<ISolidworksPoint> points)
        {
            if (points.IsEmpty()) return null;

            return points.GetMinXPoints().GetMaxYPoint();
        }

        public static ISolidworksPoint GetMinXMinYPoint(this IEnumerable<ISolidworksPoint> points)
        {
            if (points.IsEmpty()) return null;

            return points.GetMinXPoints().GetMinYPoint();
        }

        public static ISolidworksPoint GetMaxXPoint(this IEnumerable<ISolidworksPoint> points)
        {
            if (points.IsEmpty()) return null;

            var maxX = points.Max(x => x.X);

            return points.FirstOrDefault(x => Math.Abs(x.X - maxX) < 0.005);
        }

        public static ISolidworksPoint GetMaxYPoint(this IEnumerable<ISolidworksPoint> points)
        {
            if (points.IsEmpty()) return null;

            var maxY = points.Max(x => x.Y);

            return points.FirstOrDefault(x => Math.Abs(x.Y - maxY) < 0.005);
        }

        public static ISolidworksPoint GetMinXPoint(this IEnumerable<ISolidworksPoint> points)
        {
            if (points.IsEmpty()) return null;

            var minX = points.Min(x => x.X);

            return points.FirstOrDefault(x => Math.Abs(x.X - minX) < 0.005);
        }

        public static ISolidworksPoint GetMinYPoint(this IEnumerable<ISolidworksPoint> points)
        {
            if (points.IsEmpty()) return null;

            var minY = points.Min(x => x.Y);

            return points.FirstOrDefault(x => Math.Abs(x.Y - minY) < 0.005);
        }

        public static List<ISolidworksPoint> GetMaxXPoints(this IEnumerable<ISolidworksPoint> points)
        {
            if (points.IsEmpty()) return null;

            var maxX = points.Max(x => x.X);

            return points.Where(x => Math.Abs(x.X - maxX) < 0.005).ToList();
        }

        public static List<ISolidworksPoint> GetMaxYPoints(this IEnumerable<ISolidworksPoint> points)
        {
            if (points.IsEmpty()) return null;

            var maxY = points.Max(x => x.Y);

            return points.Where(x => Math.Abs(x.Y - maxY) < 0.005).ToList();
        }

        public static List<ISolidworksPoint> GetMinXPoints(this IEnumerable<ISolidworksPoint> points)
        {
            if (points.IsEmpty()) return null;

            var minX = points.Min(x => x.X);

            return points.Where(x => Math.Abs(x.X - minX) < 0.005).ToList();
        }

        public static List<ISolidworksPoint> GetMinYPoints(this IEnumerable<ISolidworksPoint> points)
        {
            if (points.IsEmpty()) return null;

            var minY = points.Min(x => x.Y);

            return points.Where(x => Math.Abs(x.Y - minY) < 0.005).ToList();
        }

        public static bool IsEmpty(this IEnumerable<ISolidworksPoint> points)
        {
            return (points == null || !points.Any());
        }

        public static void SelectAll(this IEnumerable<ISolidworksPoint> points)
        {
            foreach (var p in points)
            {
                p.Select();
            }
        }

        public static IEnumerable<ISolidworksPoint> RemoveDuplicates(this List<ISolidworksPoint> points)
        {
            var uniquePoints = new List<ISolidworksPoint>();

            for (var i = 0; i < points.Count(); i++)
            {
                var p = points[i];

                if (i == 0)
                {
                    uniquePoints.Add(p);
                }
                else
                {
                    if (uniquePoints.Any(x => Math.Abs(x.X - p.X) < .005 && Math.Abs(x.Y - p.Y) < .005)) continue;
                    
                    uniquePoints.Add(p);
                }
            }

            return uniquePoints;
        }

        public static IEnumerable<ISolidworksPoint> RemoveDuplicateXValues(this List<ISolidworksPoint> points)
        {
            var uniquePoints = new List<ISolidworksPoint>();

            for (var i = 0; i < points.Count(); i++)
            {
                var p = points[i];

                if (i == 0)
                {
                    uniquePoints.Add(p);
                }
                else
                {
                    if (uniquePoints.Any(x => Math.Abs(x.X - p.X) < .005)) continue;

                    uniquePoints.Add(p);
                }
            }

            return uniquePoints;
        }

        public static IEnumerable<ISolidworksPoint> RemoveDuplicateYValues(this List<ISolidworksPoint> points)
        {
            var uniquePoints = new List<ISolidworksPoint>();

            for (var i = 0; i < points.Count(); i++)
            {
                var p = points[i];

                if (i == 0)
                {
                    uniquePoints.Add(p);
                }
                else
                {
                    if (uniquePoints.Any(x => Math.Abs(x.Y - p.Y) < .005)) continue;

                    uniquePoints.Add(p);
                }
            }

            return uniquePoints;
        }

        public static bool PointsMatch(this ISolidworksPoint point, ISolidworksPoint pointToCompare,
            double tolerance = 0.005)
        {
            return Math.Abs(point.X - pointToCompare.X) < tolerance && Math.Abs(point.Y - pointToCompare.Y) < tolerance;
        }

        public static List<ISolidworksPoint> OrderPointsByXAndY(this List<ISolidworksPoint> collection)
        {
            return collection.OrderBy(x => x.X).ThenBy(x => x.Y).ToList();
        }
    }
}
