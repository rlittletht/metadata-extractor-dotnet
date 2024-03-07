using System.Text;
using MetadataExtractor.Formats.Jp2000;

namespace MetadataExtractor.Formats.Jp2000.Records.RecordValues;

class JpError: IRecordValue
{
    public string? Value { get; private set; }

    public bool IsEqual(ReadOnlySpan<byte> other)
    {
        return false;
    }

    public void SetFromData(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        throw new NotImplementedException();
    }

    public JpError(string value, bool isBigEndian)
    {
        Value = value;
    }

    public IRecordValue Factory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        if (Value is null)
            return "<null>";

        return Value;
    }
}

