// Copyright (c) Drew Noakes and contributors. All Rights Reserved. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace MetadataExtractor.Formats.Jp2000;

/// <summary>Describes tags parsed from JPEG 2000 Header:ImageHeader, holding Image Height/Width, Number of
/// components, bits per component, and compression</summary>
/// <author>Robert Little https://github.com/rlittletht</author>
public class Jp2000ImageHeaderDirectory : Directory
{
    /// <summary>Image Height from JP2000 Image Header</summary>
    public const int TagImageHeight = 1;

    /// <summary>Image Width from JP2000 Image Header</summary>
    public const int TagImageWidth = 2;

    /// <summary>Number of components from JP2000 Image Header</summary>
    public const int TagNumberOfComponents = 3;

    /// <summary>Bits per component from JP2000 Image Header</summary>
    public const int TagBitsPerComponent = 4;

    /// <summary>Compression type form JP2000 Image Header</summary>
    public const int TagCompression = 5;

    private static readonly Dictionary<int, string> _tagNameMap =
        new()
        {
            { TagImageHeight, "Image Height" },
            { TagImageWidth, "Image Width" },
            { TagNumberOfComponents, "Number Of Components" },
            { TagBitsPerComponent, "Bits Per Component" },
            { TagCompression, "Compression" }
        };

    public Jp2000ImageHeaderDirectory() : base(_tagNameMap)
    {
        SetDescriptor(new TagDescriptor<Jp2000ImageHeaderDirectory>(this));
    }

    public override string Name => "JP2000 Image Header";
}

