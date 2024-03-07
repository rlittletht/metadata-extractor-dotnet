
using System.Text;
using MetadataExtractor.Formats.Jp2000;

namespace MetadataExtractor.Formats.Jp2000.Records.RecordValues;

class Bytes : IRecordValue
{
    public byte[]? Value { get; private set; }

    public void SetFromData(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        Value = new byte[data.Length];
        data.CopyTo(Value);
    }

    public bool IsEqual(ReadOnlySpan<byte> other)
    {
        if (Value is null)
            return false;
        if (Value.Length != other.Length)
            return false;

        return other.SequenceEqual(Value);
    }

    public Bytes(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        SetFromData(data, isBigEndian);
    }

    public IRecordValue Factory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return StaticFactory(data, isBigEndian);
    }

    public static IRecordValue StaticFactory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return new Bytes(data, isBigEndian);
    }

    public override string ToString()
    {
        if (Value is null)
            return "<null>";

        return Encoding.UTF8.GetString(Value);
    }
}
