// Copyright (c) Drew Noakes and contributors. All Rights Reserved. Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.IO;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.FileSystem;

namespace MetadataExtractor.Formats.Jp2000
{
    /// <summary>Obtains all available metadata from JPEG 2000 formatted files.</summary>
    /// <remarks>
    /// Obtains all available metadata from JPEG 2000 formatted files. These files are organized into
    /// boxes, some of which invoke the EXIF and IPTC parsers as well
    /// </remarks>
    /// <author>Darren Salomons</author>
    /// <author>Drew Noakes https://drewnoakes.com</author>
    public static class Jp2000MetadataReader
    {
        public static byte[] Magic1 = new byte[] { 0x00, 0x00, 0x00, 0x0c, (byte)'j', (byte)'P', 0x20, 0x20, 0x0d, 0x0a, 0x87, 0x0a };
        public static byte[] Magic2 = new byte[] { 0x00, 0x00, 0x00, 0x0c, (byte)'j', (byte)'P', 0x1a, 0x1a, 0x0d, 0x0a, 0x87, 0x0a };

        public static int MagicLength = Math.Max(Magic1.Length, Magic2.Length);

        static bool FCheckMagicNumber(IndexedReader reader)
        {
            byte[] magic = reader.GetBytes(0, 12);

            ReadOnlySpan<byte> span = magic;

            if (!span.SequenceEqual(Magic1) && !span.SequenceEqual(Magic2))
                return false;

            return true;
        }

        static void ReadMetadataFromReader(IndexedReader reader, List<Directory> directories)
        {
            if (!FCheckMagicNumber(reader))
            {
                ErrorDirectory error = new ErrorDirectory();
                error.AddError("Magic number doesn't match. Can't parse JP2000 stream");
                directories.Add(error);
                return;
            }

            Boxes.Box.ReadBoxesInRange(Boxes.Box.RootBoxes, reader, MagicLength, reader.Length - MagicLength, directories);
        }

        /// <exception cref="IOException"/>
        /// <exception cref="Jp2000ProcessingException"/>
        public static IReadOnlyList<Directory> ReadMetadata(string filePath)
        {
            var directories = new List<Directory>();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.RandomAccess))
            {
                IndexedSeekingReader reader = new IndexedSeekingReader(stream);

                ReadMetadataFromReader(reader, directories);
            }

            directories.Add(new FileMetadataReader().Read(filePath));

            return directories;
        }

        /// <exception cref="IOException"/>
        /// <exception cref="Jp2000ProcessingException"/>
        public static IReadOnlyList<Directory> ReadMetadata(Stream stream)
        {
            // JP2000 processing requires random access, as directories can be scattered throughout the byte sequence.
            // Stream does not support seeking backwards, so we wrap it with IndexedCapturingReader, which
            // buffers data from the stream as we seek forward.
            var directories = new List<Directory>();

            using var reader = new IndexedCapturingReader(stream);
            ReadMetadataFromReader(reader, directories);

            return directories;
        }
    }
}
