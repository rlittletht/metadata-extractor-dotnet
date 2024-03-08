// Copyright (c) Drew Noakes and contributors. All Rights Reserved. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace MetadataExtractor.Formats.Jp2000;

/// <summary>Base class for JPEG 2000 sub-directories</summary>
/// <author>Robert Little https://github.com/rlittletht</author>
public abstract class Jp2000DirectoryBase : Directory
{
    /// <summary>Major brand from JP2000 FileType</summary>
    public const int TagMajorBrand = 1;

    /// <summary>The version of the format (MM.m.m) from JP2000 FileType</summary>
    public const int TagMinorVersion = 2;

    /// <summary>Compatible brands from JP2000 FileType</summary>
    public const int TagCompatibleBrands = 3;

    /// <summary>Image Height from JP2000 Image Header</summary>
    public const int TagImageHeight = 11;

    /// <summary>Image Width from JP2000 Image Header</summary>
    public const int TagImageWidth = 12;

    /// <summary>Number of components from JP2000 Image Header</summary>
    public const int TagNumberOfComponents = 13;

    /// <summary>Bits per component from JP2000 Image Header</summary>
    public const int TagBitsPerComponent = 14;

    /// <summary>Compression type form JP2000 Image Header</summary>
    public const int TagCompression = 15;

    /// <summary>Y Capture Resolution from JP2000 Image Header</summary>
    public const int TagResX = 21;

    /// <summary>Y Capture Resolution from JP2000 Image Header</summary>
    public const int TagResY = 22;

    /// <summary>X Unit from JP2000 Image Header</summary>
    public const int TagXUnit = 23;

    /// <summary>Y Unit from JP2000 Image Header</summary>
    public const int TagYUnit = 24;

    protected Jp2000DirectoryBase(Dictionary<int, string> tagNameMap) : base(tagNameMap)
    {
    }

    protected static void AddJp2000TagNames(Dictionary<int, string> map)
    {
        map[TagMajorBrand] = "Major Brand";
        map[TagMinorVersion] = "Minor Version";
        map[TagCompatibleBrands] = "Compatible Brands";
        map[TagImageHeight] = "Image Height";
        map[TagImageWidth] = "Image Width";
        map[TagNumberOfComponents] = "Number Of Components";
        map[TagBitsPerComponent] = "Bits Per Component";
        map[TagCompression] = "Compression";
        map[TagResX] = "X Resolution";
        map[TagResY] = "Y Resolution";
        map[TagXUnit] = "X Unit";
        map[TagYUnit] = "Y Unit";
    }
}
