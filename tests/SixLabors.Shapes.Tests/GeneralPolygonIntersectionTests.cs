﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Xunit;

namespace SixLabors.Shapes.Tests
{
    public class GeneralClosedPolygonIntersectionTests
    {

        static Dictionary<string, IPath> shapes = new Dictionary<string, IPath> {
            {"ellispeWithHole", new ComplexPolygon(new SixLabors.Shapes.Ellipse(new Vector2(603), 161f), new SixLabors.Shapes.Ellipse(new Vector2(603), 61f)) },
            { "largeEllipse", new SixLabors.Shapes.Ellipse(new Vector2(603), 603f-60) },
            { "iris_0", Shapes.IrisSegment(0) },
            { "iris_1", Shapes.IrisSegment(1) },
            { "iris_2", Shapes.IrisSegment(2) },
            { "iris_3", Shapes.IrisSegment(3) },
            { "iris_4", Shapes.IrisSegment(4) },
            { "iris_5", Shapes.IrisSegment(5) },
            { "iris_6", Shapes.IrisSegment(6) },

            { "scaled_300_iris_0", Shapes.IrisSegment(300, 0) },
            { "scaled_300_iris_1", Shapes.IrisSegment(300, 1) },
            { "scaled_300_iris_2", Shapes.IrisSegment(300, 2) },
            { "scaled_300_iris_3", Shapes.IrisSegment(300, 3) },
            { "scaled_300_iris_4", Shapes.IrisSegment(300, 4) },
            { "scaled_300_iris_5", Shapes.IrisSegment(300, 5) },
            { "scaled_300_iris_6", Shapes.IrisSegment(300, 6) },

            { "clippedRect",   new Rectangle(10, 10, 40, 40).Clip(new Rectangle(20, 0, 20, 20))     },

            { "hourGlass", Shapes.HourGlass().AsClosedPath() }
};

        public static TheoryData<string> polygonsTheoryData = new TheoryData<string> {
            { "ellispeWithHole" },
            { "largeEllipse" },
            { "iris_0" },
            { "iris_1" },
            { "iris_2" },
            { "iris_3" },
            { "iris_4" },
            { "iris_5" },
            { "iris_6" },

            { "scaled_300_iris_0" },
            { "scaled_300_iris_1" },
            { "scaled_300_iris_2" },
            { "scaled_300_iris_3" },
            { "scaled_300_iris_4" },
            { "scaled_300_iris_5" },
            { "scaled_300_iris_6" },

            { "clippedRect" },

            { "hourGlass" },
        };

        [Theory]
        [MemberData(nameof(polygonsTheoryData))]
        public void ShapeMissingEdgeHits(string name)
        {
            var polygon = shapes[name];
            var top = (int)Math.Ceiling(polygon.Bounds.Top);
            var bottom = (int)Math.Floor(polygon.Bounds.Bottom);

            for (var y = top; y <= bottom; y++)
            {
                var intersections = polygon.FindIntersections(new Vector2(polygon.Bounds.Left - 1, y), new Vector2(polygon.Bounds.Right + 1, y));
                if (intersections.Count() % 2 != 0)
                {
                    Assert.True(false, $"crosssections at '{y}' produced {intersections.Count()} number of intersections");
                }
            }
        }

        public static TheoryData<string, int> specificErrors = new TheoryData<string, int>
        {
            { "ellispeWithHole", 603 },
            { "ellispeWithHole", 442 },
            { "iris_5", 694 },
            { "iris_2", 512 },
            { "scaled_300_iris_3", 135 },
            { "scaled_300_iris_0", 165 },
            { "clippedRect", 20},
            
            { "hourGlass", 25 },
        };

        [Theory]
        [MemberData(nameof(specificErrors))]
        public void SpecificMisses(string name, int yScanLine)
        {
            var polygon = shapes[name];

            var intersections = polygon.FindIntersections(new Vector2(polygon.Bounds.Left - 1, yScanLine), new Vector2(polygon.Bounds.Right + 1, yScanLine));

            Assert.True(intersections.Count() % 2 == 0, $"crosssections at '{yScanLine}' produced odd number of intersections");
        }
    }
}