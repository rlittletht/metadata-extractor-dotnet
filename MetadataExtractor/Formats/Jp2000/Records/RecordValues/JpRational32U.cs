
using System.Buffers.Binary;
using System.Text;

namespace MetadataExtractor.Formats.Jp2000.Records.RecordValues;

public class JpRational32U : IRecordValue
{
    private UInt16 m_num;
    private UInt16 m_denom;

    public byte[]? Value { get; private set; }

    public void SetFromData(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        Value = new byte[data.Length];
        data.CopyTo(Value);

        m_num = isBigEndian
            ? BinaryPrimitives.ReadUInt16BigEndian(data)
            : BinaryPrimitives.ReadUInt16LittleEndian(data);
        m_denom = isBigEndian
            ? BinaryPrimitives.ReadUInt16BigEndian(data.Slice(2))
            : BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(2));
    }

    public bool IsEqual(ReadOnlySpan<byte> data)
    {
        if (Value is null)
            return false;

        return data.SequenceEqual(Value);
    }

    public JpRational32U(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        SetFromData(data, isBigEndian);
    }

    public IRecordValue Factory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return new JpRational32U(data, isBigEndian);
    }

    public static IRecordValue StaticFactory(ReadOnlySpan<byte> data, bool isBigEndian)
    {
        return new JpRational32U(data, isBigEndian);
    }

    public override string ToString()
    {
        return $"{((double)m_num) / ((double)m_denom)}";
    }
}

