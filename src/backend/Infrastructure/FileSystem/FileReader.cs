using Application.Abstractions.FileSystem;
using DocumentFormat.OpenXml.Packaging;
using Domain.ValueObjects;
using System.Text;
using System.Xml;

namespace Infrastructure.FileSystem;

internal sealed class FileReader : IFileReader
{
    public Task<string> ReadAllTextAsync(FileRequest file)
    {
        string fileExtension = Path.GetExtension(file.FileName);

        switch (fileExtension)
        {
            case ".docx":
                return ReadDocxFileAsync(file.Stream);
            case ".rtf":
                return ReadRtfFileAsync(file.Stream);
            default:
                return ReadTextFileAsync(file.Stream);
        }
    }

    public Task<FileContent[]> ReadAllTextsFromFilesRangeAsync(IEnumerable<FileRequest> files)
    {
        var tasks = files.Select(async f =>
        {
            string text = await ReadAllTextAsync(f);
            return new FileContent(f.FileName, text);
        });

        return Task.WhenAll(tasks);
    }

    private static Task<string> ReadDocxFileAsync(Stream stream)
    {
        StringBuilder sb = new StringBuilder();

        using (WordprocessingDocument wdDoc = WordprocessingDocument.Open(stream, false))
        {
            NameTable nt = new NameTable();

            XmlNamespaceManager nsManager = new XmlNamespaceManager(nt);
            nsManager.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

            XmlDocument xdoc = new XmlDocument(nt);

            if (wdDoc.MainDocumentPart is null)
            {
                return Task.FromResult(string.Empty);
            }

            xdoc.Load(wdDoc.MainDocumentPart.GetStream());

            XmlNodeList? paragraphNodes = xdoc.SelectNodes("//w:p", nsManager);

            if (paragraphNodes is null)
            {
                return Task.FromResult(string.Empty);
            }

            foreach (XmlNode paragraphNode in paragraphNodes)
            {
                XmlNodeList? textNodes = paragraphNode.SelectNodes(".//w:t", nsManager);

                if (textNodes is null)
                {
                    continue;
                }

                foreach (XmlNode textNode in textNodes)
                {
                    sb.Append(textNode.InnerText);
                }

                sb.Append(Environment.NewLine);
            }
        }

        return Task.FromResult(sb.ToString());
    }

    static Task<string> ReadRtfFileAsync(Stream stream)
    {
        StreamReader sr = new StreamReader(stream);

        string rtfText = sr.ReadToEnd();

        string text = RichTextStripper.StripRichTextFormat(rtfText);

        return Task.FromResult(text);
    }

    private static Task<string> ReadTextFileAsync(Stream stream)
    {
        using var streamReader = new StreamReader(stream);

        return streamReader.ReadToEndAsync();
    }
}
