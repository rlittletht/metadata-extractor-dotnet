// Copyright (c) Drew Noakes and contributors. All Rights Reserved. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace MetadataExtractor.Formats.Jp2000;

/// <summary>
/// Provides human-readable string representations of tag values stored in a <see cref="Jp2000CaptureResolutionDirectory"/>.
/// </summary>
/// <author>Kevin Mott https://github.com/kwhopper</author>
public sealed class Jp2000CaptureResolutionDescriptor(Jp2000CaptureResolutionDirectory directory)
    : TagDescriptor<Jp2000CaptureResolutionDirectory>(directory)
{
    public override string? GetDescription(int tagType)
    {
        return tagType switch
        {
            _ => base.GetDescription(tagType),
        };
    }
}
