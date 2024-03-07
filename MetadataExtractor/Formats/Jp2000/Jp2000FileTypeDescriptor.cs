// Copyright (c) Drew Noakes and contributors. All Rights Reserved. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace MetadataExtractor.Formats.Jp2000;

/// <summary>
/// Provides human-readable string representations of tag values stored in a <see cref="Jp2000FileTypeDirectory"/>.
/// </summary>
/// <author>Kevin Mott https://github.com/kwhopper</author>
public sealed class Jp2000FileTypeDescriptor(Jp2000FileTypeDirectory directory)
    : TagDescriptor<Jp2000FileTypeDirectory>(directory)
{
    public override string? GetDescription(int tagType)
    {
        return tagType switch
        {
            _ => base.GetDescription(tagType),
        };
    }
}
