using System.Buffers.Binary;
using System.Text;
using MetadataExtractor.Formats.Jp2000;

namespace MetadataExtractor.Formats.Jp2000.Boxes;

public class BoxHeader
{
    private byte[]? _data;
    private byte[] _id = null!;
    private int _headerLength = 0;
    private long _boxLength = 0;
    private bool _bigEndian;

    private static readonly int BoxIdSize = 4;

    public ReadOnlySpan<byte> ID => _id;
    public int HeaderLength => _headerLength;
    public long BoxLengthLong => _boxLength;

    // this is the starting offset relative to the beginning of _data
    public long HeaderStart;
    public long BoxStart => HeaderStart + HeaderLength;
    public long BoxLim => BoxStart + BoxLength;

    public int BoxLength
    {
        get
        {
            if (_boxLength > int.MaxValue)
                throw new Exception("Can't operate on boxes > 2gb in length");

            return (int)_boxLength;
        }
    }

    public bool MatchesID(ReadOnlySpan<byte> id)
    {
        if (id.Length == 0)
            return true;

        return id.SequenceEqual(ID);
    }

    void SetFromData(ReadOnlySpan<byte> data, int start, int maxBoxLength)
    {
        _boxLength =
            _bigEndian
                ? BinaryPrimitives.ReadUInt32BigEndian(data.Slice(start))
                : BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(start));

        _headerLength = 8;
        _id = new byte[BoxIdSize];
        data.Slice(start + 4, BoxIdSize).CopyTo(_id);

        if (_boxLength == 1)
        {
            _boxLength =
                _bigEndian
                    ? BinaryPrimitives.ReadUInt32BigEndian(data.Slice(start + 8))
                    : BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(start + 8));

            _headerLength += 8;
        }

        if (_boxLength == 0)
        {
            _boxLength = maxBoxLength - start;
        }

        _boxLength -= HeaderLength;
    }

    public BoxHeader(ReadOnlySpan<byte> data, int start, int maxLength, bool isBigEndian)
    {
        _bigEndian = isBigEndian;
        HeaderStart = start;
        SetFromData(data, start, maxLength);
    }

    public BoxHeader(IndexedReader reader, long start)
    {
        HeaderStart = start;
        int offset = (int)start;
        _bigEndian = reader.IsMotorolaByteOrder;

        ushort check = reader.GetUInt16(offset);

        if (check != 1)
        {
            _data = reader.GetBytes(offset, 8);
            offset += 8;
        }
        else
        {
            _data = reader.GetBytes(offset, 16);
            offset += 16;
        }

        SetFromData(_data, 0, (int)reader.Length);
    }

    public string Describe()
    {
        return $"Box: {Encoding.UTF8.GetString(ID)}: Header: {HeaderStart:x8}:{HeaderStart + HeaderLength:x8}, Box: {BoxStart:x8}:{BoxStart + BoxLength:x8}";
    }
}
