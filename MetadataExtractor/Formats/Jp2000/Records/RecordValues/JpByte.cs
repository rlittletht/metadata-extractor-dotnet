
using System.Text;
using MetadataExtractor.Formats.Jp2000;

namespace MetadataExtractor.Formats.Jp2000.Records.RecordValues;

class JpByte : IRecordValue
{
    public byte? Value { get; private set; }

    public void SetFromData(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        Value = data[0];
    }

    public bool IsEqual(ReadOnlySpan<byte> other)
    {
        if (Value == null)
            return false;

        return other[0] == Value;
    }

    public JpByte(byte data, bool isBigEndian)
    {
        Value = data;
    }

    public IRecordValue Factory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return StaticFactory(data, isBigEndian);
    }

    public static IRecordValue StaticFactory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return new JpByte(data[0], isBigEndian);
    }

    public override string ToString()
    {
        if (Value == null)
            return "<null>";

        return $"0x{Value:x2}";
    }
}
