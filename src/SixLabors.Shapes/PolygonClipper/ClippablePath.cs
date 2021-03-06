﻿// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

namespace SixLabors.Shapes.PolygonClipper
{
    /// <summary>
    /// Represents a shape and its type for when clipping is applies.
    /// </summary>
    public readonly struct ClippablePath
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClippablePath" /> struct.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="type">The type.</param>
        public ClippablePath(IPath path, ClippingType type)
        {
            this.Path = path;
            this.Type = type;
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        public IPath Path { get; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ClippingType Type { get; }
    }
}
