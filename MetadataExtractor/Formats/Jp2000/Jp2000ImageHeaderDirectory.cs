// Copyright (c) Drew Noakes and contributors. All Rights Reserved. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace MetadataExtractor.Formats.Jp2000;

/// <summary>Describes tags parsed from JPEG 2000 Header:ImageHeader, holding Image Height/Width, Number of
/// components, bits per component, and compression</summary>
/// <author>Robert Little https://github.com/rlittletht</author>
public class Jp2000ImageHeaderDirectory : Jp2000DirectoryBase
{
    private static readonly Dictionary<int, string> _tagNameMap = new();

    public Jp2000ImageHeaderDirectory() : base(_tagNameMap)
    {
        SetDescriptor(new TagDescriptor<Jp2000ImageHeaderDirectory>(this));
    }

    static Jp2000ImageHeaderDirectory()
    {
        AddJp2000TagNames(_tagNameMap);
    }
    public override string Name => "JP2000 Image Header";
}

