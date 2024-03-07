
using MetadataExtractor.Formats.Jp2000.Records;

namespace MetadataExtractor.Formats.Jp2000.Boxes;

public class Box
{
    public static int BoxHeaderLength = 8;

    public static byte[] BoxId_Ftyp = "ftyp"u8.ToArray();
    public static byte[] BoxId_Jp2Header = "jp2h"u8.ToArray();
    public static byte[] BoxId_Jp2Header_ImageHeader = "ihdr"u8.ToArray();
    public static byte[] BoxId_Jp2Header_Resolution = "res "u8.ToArray();
    public static byte[] BoxId_Jp2Header_Resolution_CaptureResolution = "resc"u8.ToArray();
    public static byte[] BoxId_Uuid = "uuid"u8.ToArray();

    public static Dictionary<byte[], BoxFactoryDelegate> RootBoxes =
        new()
        {
            { BoxId_Ftyp, Ftyp.StaticFactory },
            { BoxId_Jp2Header, Jp2Header.StaticFactory },
            { BoxId_Uuid, Uuid.StaticFactory },
            { Array.Empty<byte>(), Unknown.StaticFactory }
        };

    public static IBox? GetMatchingBoxHeader(Dictionary<byte[], BoxFactoryDelegate> boxes, BoxHeader header)
    {
        // now look for the right box type
        foreach (KeyValuePair<byte[], BoxFactoryDelegate> kvpBox in boxes)
        {
            if (header.MatchesID(kvpBox.Key))
                return kvpBox.Value();
        }

        return null;
    }

    public static IBox? CreateBox(Dictionary<byte[], BoxFactoryDelegate> boxes, ReadOnlySpan<byte> data, int start, int limit, bool isBigEndian)
    {
        BoxHeader header = new BoxHeader(data, start, limit, isBigEndian);

        IBox? box = GetMatchingBoxHeader(boxes, header);

        if (box != null)
        {
            box.IsBigEndian = isBigEndian;
            box.HeaderInternal = header;
        }

        return box;
    }

    /*----------------------------------------------------------------------------
        %%Function: CreateBox
        %%Qualified: Jp2000.Boxes.Box.CreateBox

        This will read the header and create the right box type. It does NOT read
        the data (since you might not want to handle it)
    ----------------------------------------------------------------------------*/
    public static IBox? CreateBox(Dictionary<byte[], BoxFactoryDelegate> boxes, IndexedReader reader, long offset)
    {
        BoxHeader header = new BoxHeader(reader, offset);

        IBox? box = GetMatchingBoxHeader(boxes, header);

        if (box != null)
        {
            box.IsBigEndian = reader.IsMotorolaByteOrder;
            box.HeaderInternal = header;
        }

        return box;
    }

    public static void ReadBoxesInRange(Dictionary<byte[], BoxFactoryDelegate> boxes, ReadOnlySpan<byte> data, int start, int limit, List<Directory> directories, bool isBigEndian)
    {
        int position = start;

        while (position < limit)
        {
            IBox? box = CreateBox(boxes, data, position, limit, isBigEndian);

            if (box is null)
                throw new Exception("couldn't create box");

            Console.WriteLine($"Box: {box.Header.Describe()}");

            box.Read(data);
            box.Parse(directories);

            position = (int)box.BoxLim;
        }
    }

    public static void ReadBoxesInRange(Dictionary<byte[], BoxFactoryDelegate> boxes, IndexedReader reader, long start, long length, List<Directory> directories)
    {
        long position = start;

        while (position < start + length)
        {
            IBox? box = Box.CreateBox(boxes, reader, position);

            if (box is null)
                throw new Exception("couldn't create box");

            Console.WriteLine($"Box: {box.Header.Describe()}");

            box.Read(reader);
            box.Parse(directories);


            position = box.BoxLim;
        }
    }

    public static void EnumerateValueMaps(Dictionary<string, Dictionary<string, IRecordValue?>> valueMaps, Action<string> onNewKey, Action<string, string, IRecordValue?> onValueMap)
    {
        foreach (KeyValuePair<string, Dictionary<string, IRecordValue?>> kvp in valueMaps)
        {
            onNewKey(kvp.Key);
            foreach (KeyValuePair<string, IRecordValue?> metadataPair in kvp.Value)
            {
                onValueMap(kvp.Key, metadataPair.Key, metadataPair.Value);
            }
        }
    }
}
