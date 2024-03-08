// Copyright (c) Drew Noakes and contributors. All Rights Reserved. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace MetadataExtractor.Formats.Jp2000;

/// <summary>Describes tags parsed from JPEG 2000 Header:Resolution:CaptureResolution box, holding resolution X/Y and X/Y units</summary>
/// <author>Robert Little https://github.com/rlittletht</author>
public class Jp2000CaptureResolutionDirectory : Jp2000DirectoryBase
{
    private static readonly Dictionary<int, string> _tagNameMap = new();

    public Jp2000CaptureResolutionDirectory() : base(_tagNameMap)
    {
        SetDescriptor(new TagDescriptor<Jp2000CaptureResolutionDirectory>(this));
    }

    static Jp2000CaptureResolutionDirectory()
    {
        AddJp2000TagNames(_tagNameMap);
    }

    public override string Name => "JP2000 Capture Resolution";
}

