using System.Buffers.Binary;

namespace MetadataExtractor.Formats.Jp2000.Records.RecordValues;

class JpInt16U : IRecordValue
{
    public UInt16? Value { get; private set; }

    public void SetFromData(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        Value =
            isBigEndian
                ? BinaryPrimitives.ReadUInt16BigEndian(data)
                : BinaryPrimitives.ReadUInt16LittleEndian(data);
    }

    public bool IsEqual(ReadOnlySpan<byte> other)
    {
        if (Value == null)
            return false;

        return other[0] == Value;
    }

    public JpInt16U(UInt16 data, bool isBigEndian)
    {
        Value = data;
    }

    public JpInt16U(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        SetFromData(data, isBigEndian);
    }

    public IRecordValue Factory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return StaticFactory(data, isBigEndian);
    }

    public static IRecordValue StaticFactory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return new JpInt16U(data, isBigEndian);
    }

    public override string ToString()
    {
        if (Value == null)
            return "<null>";

        return $"0x{Value:x4} ({Value})";


    }
}
