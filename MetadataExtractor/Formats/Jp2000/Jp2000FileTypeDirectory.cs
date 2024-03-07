// Copyright (c) Drew Noakes and contributors. All Rights Reserved. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace MetadataExtractor.Formats.Jp2000;

/// <summary>Describes tags parsed from JPEG 2000 FileType box, holding major brand and minor version
/// numbers as well as compatible formats</summary>
/// <author>Robert Little https://github.com/rlittletht</author>
public class Jp2000FileTypeDirectory : Directory
{
    /// <summary>Major brand</summary>
    public const int TagMajorBrand = 1;

    /// <summary>The version of the format (MM.m.m)</summary>
    public const int TagMinorVersion = 2;

    /// <summary>Compatible brands</summary>
    public const int TagCompatibleBrands = 3;

    private static readonly Dictionary<int, string> _tagNameMap =
        new() { { TagMajorBrand, "Major Brand" }, { TagMinorVersion, "Minor Version" }, { TagCompatibleBrands, "Compatible Brands" } };

    public Jp2000FileTypeDirectory() : base(_tagNameMap)
    {
        SetDescriptor(new TagDescriptor<Jp2000FileTypeDirectory>(this));
    }

    public override string Name => "JP2000 FileType";
}
