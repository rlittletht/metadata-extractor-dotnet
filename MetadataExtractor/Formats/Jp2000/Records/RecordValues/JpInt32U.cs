
using System.Buffers.Binary;
using System.Text;
using MetadataExtractor.Formats.Jp2000;

namespace MetadataExtractor.Formats.Jp2000.Records.RecordValues;

class JpInt32U : IRecordValue
{
    public UInt32? Value { get; private set; }

    public void SetFromData(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        Value =
            isBigEndian
                ? BinaryPrimitives.ReadUInt32BigEndian(data)
                : BinaryPrimitives.ReadUInt32LittleEndian(data);
    }

    public bool IsEqual(ReadOnlySpan<byte> other)
    {
        if (Value == null)
            return false;

        return other[0] == Value;
    }

    public JpInt32U(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        SetFromData(data, isBigEndian);
    }

    public IRecordValue Factory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return StaticFactory(data, isBigEndian);
    }

    public static IRecordValue StaticFactory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return new JpInt32U(data, isBigEndian);
    }

    public override string ToString()
    {
        if (Value == null)
            return "<null>";

        return $"0x{Value:x8} ({Value})";
    }
}

