// Copyright (c) Drew Noakes and contributors. All Rights Reserved. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace MetadataExtractor.Formats.Jp2000;

/// <summary>Describes tags parsed from JPEG 2000 Header:Resolution:CaptureResolution box, holding resolution X/Y and X/Y units</summary>
/// <author>Robert Little https://github.com/rlittletht</author>
public class Jp2000CaptureResolutionDirectory : Directory
{
    /// <summary>Y Capture Resolution from JP2000 Image Header</summary>
    public const int TagResX= 1;

    /// <summary>Y Capture Resolution from JP2000 Image Header</summary>
    public const int TagResY= 2;

    /// <summary>X Unit from JP2000 Image Header</summary>
    public const int TagXUnit = 3;

    /// <summary>Y Unit from JP2000 Image Header</summary>
    public const int TagYUnit = 4;

    private static readonly Dictionary<int, string> _tagNameMap =
        new()
        {
            { TagResX, "X Resolution" },
            { TagResY, "Y Resolution" },
            { TagXUnit, "X Unit" },
            { TagYUnit, "Y Unit" }
        };

    public Jp2000CaptureResolutionDirectory() : base(_tagNameMap)
    {
        SetDescriptor(new TagDescriptor<Jp2000CaptureResolutionDirectory>(this));
    }

    public override string Name => "JP2000 Capture Resolution";
}

