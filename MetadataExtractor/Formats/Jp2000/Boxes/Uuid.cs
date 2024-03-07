using MetadataExtractor.Formats.Jp2000;
using MetadataExtractor.Formats.Jp2000.Records;
using MetadataExtractor.Formats.Jp2000.Records.RecordValues;
using System.Text;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.Iptc;
using MetadataExtractor.Formats.Jp2000.Boxes;
using MetadataExtractor.Formats.Xmp;

namespace MetadataExtractor.Formats.Jp2000.Boxes;

public class Uuid : BoxBase, IBox
{
    private readonly byte[] ID = Box.BoxId_Uuid;

    ReadOnlySpan<byte> IBox.ID => ID;
    ReadOnlySpan<byte> IBox.BoxData => BoxData;
    public bool IsBigEndian { get; set; } = false;

    public string Name { get; set; } =  "Uuid";

    private readonly Dictionary<IRecordValue, string> m_uuidDirectoryMap =
        new()
        {
            { new Bytes("JpgTiffExif->JP2"u8.ToArray(), false), "EXIF" },
            { new Bytes(new byte[] {0x05,0x37,0xcd,0xab,0x9d,0x0c,0x44,0x31,0xa7,0x2a,0xfa,0x56,0x1f,0x2a,0x11,0x3e }, false), "EXIF-Adobe" },
            { new Bytes(new byte[] {0x33,0xc7,0xa4,0xd2,0xb8,0x1d,0x47,0x23,0xa0,0xba,0xf1,0xa3,0xe0,0x97,0xad,0x38 }, false), "IPTC" },
            { new Bytes(new byte[] {0x09,0xa1,0x4e,0x97,0xc0,0xb4,0x42,0xe0,0xbe,0xbf,0x36,0xdf,0x6f,0x0c,0xe3,0x6f }, false), "IPTC-Adobe" },
            { new Bytes(new byte[] {0xbe,0x7a,0xcf,0xcb,0x97,0xa9,0x42,0xe8,0x9c,0x71,0x99,0x94,0x91,0xe3,0xaf,0xac }, false), "XMP" },
            { new Bytes(new byte[] {0xb1,0x4b,0xf8,0xbd,0x08,0x3d,0x4b,0x43,0xa5,0xae,0x8c,0xd7,0xd5,0xa6,0xce,0x03 }, false), "GeoJP2" },
            { new Bytes(new byte[] {0x2c,0x4c,0x01,0x00,0x85,0x04,0x40,0xb9,0xa0,0x3e,0x56,0x21,0x48,0xd6,0xdf,0xeb }, false), "Photoshop" },
            { new Bytes(new byte[] {(byte)'c', (byte)'2', (byte)'c', (byte)'s',0x00,0x11,0x00,0x10,0x80,0x00,0x00,0xaa,0x00,0x38,0x9b,0x71 }, false ), "C2PAClaimSignature" },
            { new Bytes(new byte[] {(byte)'c', (byte)'a', (byte)'s', (byte)'g',0x00,0x11,0x00,0x10,0x80,0x00,0x00,0xaa,0x00,0x38,0x9b,0x71 }, false ), "Signature" },
        };

    string? GetUuidType()
    {
        foreach (KeyValuePair<IRecordValue, string> kv in m_uuidDirectoryMap)
        {
            if (kv.Key.IsEqual(BoxData.AsSpan(0, 16)))
                return kv.Value;
        }

        return null;
    }

    public bool Parse(List<Directory> directories)
    {
        if (BoxData is null)
            return true;

        // This box has several sub-boxes
        Dictionary<string, Dictionary<string, IRecordValue?>> valuesMaps = new();

        // examine the first 16 bytes of the box data to see what kind of
        // directory we are going to find

        // map the record value to something
        string? uuidType = GetUuidType();

        if (uuidType is null)
        {
            Console.WriteLine($"Skipping unknown UUID box: {Encoding.UTF8.GetString(BoxData.AsSpan(0, 16))}");
            return true;
        }

        switch (uuidType)
        {
            case "EXIF":
                Name = "EXIF";
                int exifStart =
                    BoxData.AsSpan(16, 6).SequenceEqual("Exif\0\0"u8.ToArray())
                        ? 22 // broken Digikam structure
                        : 16;

                ExifReader exifReader = new ExifReader();

                directories.AddRange(exifReader.Extract(new ByteArrayReader(BoxData, exifStart, IsBigEndian), 0));
                break;
            case "IPTC":
                Name = "IPTC";
                int iptcStart = 16;

                IptcReader iptcReader = new IptcReader();
                IptcDirectory iptcDir = iptcReader.Extract(new SequentialByteArrayReader(BoxData, iptcStart, IsBigEndian), BoxLength - iptcStart);

                directories.Add(iptcDir);
                break;
            case "XMP":
                Name = "XMP";
                int xmpStart = 16;

                byte[] xmpData = new byte[BoxLength - xmpStart];
                BoxData.AsSpan(xmpStart, BoxLength - xmpStart).CopyTo(xmpData);

                XmpReader xmpReader = new XmpReader();
                XmpDirectory xmpDir = xmpReader.Extract(xmpData);

                directories.Add(xmpDir);
                break;
        }

        return true;
    }

    public static IBox StaticFactory()
    {
        return new Uuid();
    }
}

