using System.Globalization;
using System.IO.Compression;
using System.Text;
using System.Xml.Linq;

namespace MiniSupermarket.WinForms.Services
{
    internal static class SpreadsheetExportService
    {
        private const string ProjectName = "Phần mềm Quản lý Cửa hàng Tiện lợi - Tạp hóa Phố Thị";
        private static readonly XNamespace MainNs = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        private static readonly XNamespace RelNs = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        private static readonly XNamespace PackageRelNs = "http://schemas.openxmlformats.org/package/2006/relationships";
        private static readonly XNamespace ContentNs = "http://schemas.openxmlformats.org/package/2006/content-types";

        public static void WriteXlsx(
            string path,
            string sheetName,
            IReadOnlyList<string> headers,
            IReadOnlyList<IReadOnlyList<object?>> rows,
            string? reportDetails = null)
        {
            var safeSheetName = string.Concat(sheetName.Select(ch => "[]:*?/\\".Contains(ch) ? '_' : ch));
            if (safeSheetName.Length > 31) safeSheetName = safeSheetName[..31];
            if (string.IsNullOrWhiteSpace(safeSheetName)) safeSheetName = "Sheet1";

            using var file = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            using var archive = new ZipArchive(file, ZipArchiveMode.Create);

            AddXml(archive, "[Content_Types].xml", new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(ContentNs + "Types",
                    new XElement(ContentNs + "Default", new XAttribute("Extension", "rels"), new XAttribute("ContentType", "application/vnd.openxmlformats-package.relationships+xml")),
                    new XElement(ContentNs + "Default", new XAttribute("Extension", "xml"), new XAttribute("ContentType", "application/xml")),
                    new XElement(ContentNs + "Override", new XAttribute("PartName", "/xl/workbook.xml"), new XAttribute("ContentType", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml")),
                    new XElement(ContentNs + "Override", new XAttribute("PartName", "/xl/worksheets/sheet1.xml"), new XAttribute("ContentType", "application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml")),
                    new XElement(ContentNs + "Override", new XAttribute("PartName", "/xl/styles.xml"), new XAttribute("ContentType", "application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml")))));

            AddXml(archive, "_rels/.rels", new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(PackageRelNs + "Relationships",
                    new XElement(PackageRelNs + "Relationship", new XAttribute("Id", "rId1"), new XAttribute("Type", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument"), new XAttribute("Target", "xl/workbook.xml")))));

            AddXml(archive, "xl/workbook.xml", new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(MainNs + "workbook",
                    new XAttribute(XNamespace.Xmlns + "r", RelNs),
                    new XElement(MainNs + "sheets",
                        new XElement(MainNs + "sheet", new XAttribute("name", safeSheetName), new XAttribute("sheetId", "1"), new XAttribute(RelNs + "id", "rId1"))))));

            AddXml(archive, "xl/_rels/workbook.xml.rels", new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(PackageRelNs + "Relationships",
                    new XElement(PackageRelNs + "Relationship", new XAttribute("Id", "rId1"), new XAttribute("Type", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet"), new XAttribute("Target", "worksheets/sheet1.xml")),
                    new XElement(PackageRelNs + "Relationship", new XAttribute("Id", "rId2"), new XAttribute("Type", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles"), new XAttribute("Target", "styles.xml")))));

            AddXml(archive, "xl/styles.xml", CreateStyles());
            AddXml(archive, "xl/worksheets/sheet1.xml", CreateSheet(sheetName, headers, rows, reportDetails));
        }

        private static XDocument CreateStyles()
        {
            XElement Xf(int numFmt, int font, int fill, int border, bool wrap = false, string? horizontal = null, bool bold = false) =>
                new(MainNs + "xf",
                    new XAttribute("numFmtId", numFmt),
                    new XAttribute("fontId", font),
                    new XAttribute("fillId", fill),
                    new XAttribute("borderId", border),
                    new XAttribute("xfId", "0"),
                    new XAttribute("applyFont", "1"),
                    new XAttribute("applyFill", "1"),
                    new XAttribute("applyBorder", "1"),
                    numFmt > 0 ? new XAttribute("applyNumberFormat", "1") : null,
                    wrap || horizontal != null
                        ? new XElement(MainNs + "alignment",
                            horizontal != null ? new XAttribute("horizontal", horizontal) : null,
                            new XAttribute("vertical", "center"),
                            wrap ? new XAttribute("wrapText", "1") : null)
                        : null);

            return new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(MainNs + "styleSheet",
                    new XElement(MainNs + "numFmts", new XAttribute("count", "3"),
                        new XElement(MainNs + "numFmt", new XAttribute("numFmtId", "164"), new XAttribute("formatCode", "#,##0 \"₫\"")),
                        new XElement(MainNs + "numFmt", new XAttribute("numFmtId", "165"), new XAttribute("formatCode", "dd/mm/yyyy")),
                        new XElement(MainNs + "numFmt", new XAttribute("numFmtId", "166"), new XAttribute("formatCode", "dd/mm/yyyy hh:mm"))),
                    new XElement(MainNs + "fonts", new XAttribute("count", "5"),
                        new XElement(MainNs + "font", new XElement(MainNs + "sz", new XAttribute("val", "10")), new XElement(MainNs + "color", new XAttribute("rgb", "FF334155")), new XElement(MainNs + "name", new XAttribute("val", "Calibri"))),
                        new XElement(MainNs + "font", new XElement(MainNs + "b"), new XElement(MainNs + "color", new XAttribute("rgb", "FFFFFFFF")), new XElement(MainNs + "sz", new XAttribute("val", "10")), new XElement(MainNs + "name", new XAttribute("val", "Calibri"))),
                        new XElement(MainNs + "font", new XElement(MainNs + "b"), new XElement(MainNs + "color", new XAttribute("rgb", "FF12304A")), new XElement(MainNs + "sz", new XAttribute("val", "16")), new XElement(MainNs + "name", new XAttribute("val", "Calibri"))),
                        new XElement(MainNs + "font", new XElement(MainNs + "b"), new XElement(MainNs + "color", new XAttribute("rgb", "FF1D4ED8")), new XElement(MainNs + "sz", new XAttribute("val", "12")), new XElement(MainNs + "name", new XAttribute("val", "Calibri"))),
                        new XElement(MainNs + "font", new XElement(MainNs + "color", new XAttribute("rgb", "FF64748B")), new XElement(MainNs + "sz", new XAttribute("val", "9")), new XElement(MainNs + "name", new XAttribute("val", "Calibri")))),
                    new XElement(MainNs + "fills", new XAttribute("count", "5"),
                        new XElement(MainNs + "fill", new XElement(MainNs + "patternFill", new XAttribute("patternType", "none"))),
                        new XElement(MainNs + "fill", new XElement(MainNs + "patternFill", new XAttribute("patternType", "gray125"))),
                        new XElement(MainNs + "fill", new XElement(MainNs + "patternFill", new XAttribute("patternType", "solid"), new XElement(MainNs + "fgColor", new XAttribute("rgb", "FF1E293B")), new XElement(MainNs + "bgColor", new XAttribute("indexed", "64")))),
                        new XElement(MainNs + "fill", new XElement(MainNs + "patternFill", new XAttribute("patternType", "solid"), new XElement(MainNs + "fgColor", new XAttribute("rgb", "FFEAF2FB")), new XElement(MainNs + "bgColor", new XAttribute("indexed", "64")))),
                        new XElement(MainNs + "fill", new XElement(MainNs + "patternFill", new XAttribute("patternType", "solid"), new XElement(MainNs + "fgColor", new XAttribute("rgb", "FFF1F5F9")), new XElement(MainNs + "bgColor", new XAttribute("indexed", "64"))))),
                    new XElement(MainNs + "borders", new XAttribute("count", "2"),
                        new XElement(MainNs + "border", new XElement(MainNs + "left"), new XElement(MainNs + "right"), new XElement(MainNs + "top"), new XElement(MainNs + "bottom"), new XElement(MainNs + "diagonal")),
                        new XElement(MainNs + "border",
                            new XElement(MainNs + "left", new XAttribute("style", "thin"), new XElement(MainNs + "color", new XAttribute("rgb", "FFE2E8F0"))),
                            new XElement(MainNs + "right", new XAttribute("style", "thin"), new XElement(MainNs + "color", new XAttribute("rgb", "FFE2E8F0"))),
                            new XElement(MainNs + "top", new XAttribute("style", "thin"), new XElement(MainNs + "color", new XAttribute("rgb", "FFE2E8F0"))),
                            new XElement(MainNs + "bottom", new XAttribute("style", "thin"), new XElement(MainNs + "color", new XAttribute("rgb", "FFE2E8F0"))),
                            new XElement(MainNs + "diagonal"))),
                    new XElement(MainNs + "cellStyleXfs", new XAttribute("count", "1"), Xf(0, 0, 0, 0)),
                    new XElement(MainNs + "cellXfs", new XAttribute("count", "17"),
                        Xf(0, 0, 0, 1, horizontal: "left"),                 // 0: body
                        Xf(0, 1, 2, 1, wrap: true, horizontal: "center"),   // 1: header
                        Xf(0, 2, 3, 0),                                     // 2: project title
                        Xf(0, 3, 3, 0),                                     // 3: report title
                        Xf(0, 4, 0, 0),                                     // 4: metadata
                        Xf(0, 0, 4, 1),                                     // 5: striped body
                        Xf(164, 0, 0, 1, horizontal: "right"),              // 6: currency
                        Xf(164, 0, 4, 1, horizontal: "right"),              // 7: striped currency
                        Xf(165, 0, 0, 1, horizontal: "center"),             // 8: date
                        Xf(165, 0, 4, 1, horizontal: "center"),             // 9: striped date
                        Xf(0, 0, 0, 1, horizontal: "right"),                // 10: number
                        Xf(0, 0, 4, 1, horizontal: "right"),                // 11: striped number
                        Xf(0, 3, 3, 1),                                     // 12: total label
                        Xf(164, 3, 3, 1, horizontal: "right"),              // 13: total currency
                        Xf(0, 3, 3, 1, horizontal: "right"),                // 14: total number
                        Xf(166, 0, 0, 1, horizontal: "center"),             // 15: date and time
                        Xf(166, 0, 4, 1, horizontal: "center")),            // 16: striped date and time
                    new XElement(MainNs + "cellStyles", new XAttribute("count", "1"), new XElement(MainNs + "cellStyle", new XAttribute("name", "Normal"), new XAttribute("xfId", "0"), new XAttribute("builtinId", "0")))));
        }

        private static XDocument CreateSheet(string sheetName, IReadOnlyList<string> headers, IReadOnlyList<IReadOnlyList<object?>> rows, string? reportDetails)
        {
            const int titleRowCount = 3;
            const int headerRowNumber = titleRowCount + 1;
            int reportWidth = Math.Max(headers.Count, 7);
            string lastColumn = ColumnName(Math.Max(1, headers.Count));
            string mergeLastColumn = ColumnName(reportWidth);
            int dataStartRow = headerRowNumber + 1;
            int dataEndRow = dataStartRow + rows.Count - 1;

            var sheetRows = new List<XElement>
            {
                new XElement(MainNs + "row", new XAttribute("r", "1"), new XAttribute("ht", "34"), new XAttribute("customHeight", "1"), CreateCell("A1", "TÊN DỰ ÁN  |  " + ProjectName, 2)),
                new XElement(MainNs + "row", new XAttribute("r", "2"), new XAttribute("ht", "27"), new XAttribute("customHeight", "1"), CreateCell("A2", sheetName.ToUpperInvariant(), 3)),
                new XElement(MainNs + "row", new XAttribute("r", "3"), new XAttribute("ht", "22"), new XAttribute("customHeight", "1"), CreateCell("A3", BuildMetadata(rows.Count, reportDetails), 4))
            };

            var headerCells = headers.Select((header, index) => CreateCell(ColumnName(index + 1) + headerRowNumber.ToString(CultureInfo.InvariantCulture), header, 1));
            sheetRows.Add(new XElement(MainNs + "row", new XAttribute("r", headerRowNumber), new XAttribute("ht", "26"), new XAttribute("customHeight", "1"), headerCells));

            for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                var cells = new List<XElement>();
                for (int colIndex = 0; colIndex < headers.Count; colIndex++)
                {
                    var value = colIndex < rows[rowIndex].Count ? rows[rowIndex][colIndex] : null;
                    string address = ColumnName(colIndex + 1) + (rowIndex + dataStartRow).ToString(CultureInfo.InvariantCulture);
                    cells.Add(CreateDataCell(address, value, headers[colIndex], rowIndex % 2 == 1));
                }
                sheetRows.Add(new XElement(MainNs + "row", new XAttribute("r", rowIndex + dataStartRow), new XAttribute("ht", "22"), new XAttribute("customHeight", "1"), cells));
            }

            var currencyColumns = headers.Select((header, index) => (header, index))
                .Where(item => IsCurrencyColumn(item.header))
                .ToList();
            bool hasTotals = rows.Count > 0 && currencyColumns.Count > 0;
            int totalRowNumber = dataStartRow + rows.Count;
            if (hasTotals)
            {
                var totalCells = new List<XElement> { CreateCell("A" + totalRowNumber.ToString(CultureInfo.InvariantCulture), "TỔNG CỘNG", 12) };
                for (int colIndex = 1; colIndex < headers.Count; colIndex++)
                {
                    if (currencyColumns.Any(item => item.index == colIndex))
                    {
                        decimal total = rows.Sum(row => colIndex < row.Count && TryGetDecimal(row[colIndex], out var number) ? number : 0m);
                        totalCells.Add(CreateCell(ColumnName(colIndex + 1) + totalRowNumber.ToString(CultureInfo.InvariantCulture), total, 13));
                    }
                }
                sheetRows.Add(new XElement(MainNs + "row", new XAttribute("r", totalRowNumber), new XAttribute("ht", "26"), new XAttribute("customHeight", "1"), totalCells));
            }

            var columns = Enumerable.Range(1, reportWidth).Select(index =>
            {
                int width = index <= headers.Count ? CalculateColumnWidth(headers[index - 1], index - 1, rows) : 12;
                return new XElement(MainNs + "col", new XAttribute("min", index), new XAttribute("max", index), new XAttribute("width", width), new XAttribute("customWidth", "1"));
            });

            int finalRow = hasTotals ? totalRowNumber : Math.Max(headerRowNumber, dataEndRow);
            var mergedCells = new XElement(MainNs + "mergeCells", new XAttribute("count", "3"),
                new XElement(MainNs + "mergeCell", new XAttribute("ref", "A1:" + mergeLastColumn + "1")),
                new XElement(MainNs + "mergeCell", new XAttribute("ref", "A2:" + mergeLastColumn + "2")),
                new XElement(MainNs + "mergeCell", new XAttribute("ref", "A3:" + mergeLastColumn + "3")));

            return new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(MainNs + "worksheet",
                    new XElement(MainNs + "dimension", new XAttribute("ref", "A1:" + mergeLastColumn + finalRow.ToString(CultureInfo.InvariantCulture))),
                    new XElement(MainNs + "sheetViews", new XElement(MainNs + "sheetView", new XAttribute("workbookViewId", "0"), new XAttribute("showGridLines", "0"),
                        new XElement(MainNs + "pane", new XAttribute("ySplit", headerRowNumber), new XAttribute("topLeftCell", "A" + dataStartRow.ToString(CultureInfo.InvariantCulture)), new XAttribute("activePane", "bottomLeft"), new XAttribute("state", "frozen")),
                        new XElement(MainNs + "selection", new XAttribute("pane", "bottomLeft"), new XAttribute("activeCell", "A" + dataStartRow.ToString(CultureInfo.InvariantCulture)), new XAttribute("sqref", "A" + dataStartRow.ToString(CultureInfo.InvariantCulture))))),
                    new XElement(MainNs + "sheetFormatPr", new XAttribute("defaultRowHeight", "22")),
                    new XElement(MainNs + "cols", columns),
                    new XElement(MainNs + "sheetData", sheetRows),
                    headers.Count > 0 ? new XElement(MainNs + "autoFilter", new XAttribute("ref", "A" + headerRowNumber.ToString(CultureInfo.InvariantCulture) + ":" + lastColumn + Math.Max(headerRowNumber, dataEndRow).ToString(CultureInfo.InvariantCulture))) : null,
                    mergedCells,
                    new XElement(MainNs + "pageMargins", new XAttribute("left", "0.3"), new XAttribute("right", "0.3"), new XAttribute("top", "0.5"), new XAttribute("bottom", "0.5"), new XAttribute("header", "0.2"), new XAttribute("footer", "0.2")),
                    new XElement(MainNs + "pageSetup", new XAttribute("orientation", "landscape"), new XAttribute("fitToWidth", "1"), new XAttribute("fitToHeight", "0"), new XAttribute("paperSize", "9"))));
        }

        private static XElement CreateDataCell(string address, object? value, string header, bool striped)
        {
            if (value is string text && IsDateColumn(header))
            {
                if (DateTime.TryParseExact(text, new[] { "dd/MM/yyyy", "d/M/yyyy", "dd/MM", "d/M" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    if (text.Length <= 5) parsedDate = new DateTime(DateTime.Today.Year, parsedDate.Month, parsedDate.Day);
                    value = parsedDate;
                }
            }

            int styleId;
            if (IsCurrencyColumn(header) && TryGetDecimal(value, out _))
                styleId = striped ? 7 : 6;
            else if (value is DateTime && (header.Contains("tạo", StringComparison.OrdinalIgnoreCase) || header.Contains("thời gian", StringComparison.OrdinalIgnoreCase)))
                styleId = striped ? 16 : 15;
            else if ((value is DateTime || IsDateColumn(header)) && (value is DateTime || value is string))
                styleId = striped ? 9 : 8;
            else if (value is byte or sbyte or short or ushort or int or uint or long or ulong or float or double or decimal)
                styleId = striped ? 11 : 10;
            else
                styleId = striped ? 5 : 0;

            return CreateCell(address, value, styleId);
        }

        private static string BuildMetadata(int rowCount, string? reportDetails)
        {
            string exportedAt = "Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.GetCultureInfo("vi-VN"));
            string count = "Số dòng dữ liệu: " + rowCount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
            return string.IsNullOrWhiteSpace(reportDetails)
                ? exportedAt + "     |     " + count
                : exportedAt + "     |     " + count + "     |     " + reportDetails;
        }

        private static bool IsDateColumn(string header) =>
            header.Contains("ngày", StringComparison.OrdinalIgnoreCase) || header.Contains("date", StringComparison.OrdinalIgnoreCase);

        private static bool IsCurrencyColumn(string header) =>
            header.Contains("doanh thu", StringComparison.OrdinalIgnoreCase) ||
            header.Contains("tổng tiền", StringComparison.OrdinalIgnoreCase) ||
            header.Contains("thành tiền", StringComparison.OrdinalIgnoreCase) ||
            header.Contains("(VNĐ)", StringComparison.OrdinalIgnoreCase) ||
            header.Contains("FinalAmount", StringComparison.OrdinalIgnoreCase);

        private static bool TryGetDecimal(object? value, out decimal number)
        {
            try
            {
                if (value == null || value is string) { number = 0m; return false; }
                number = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
                return true;
            }
            catch { number = 0m; return false; }
        }

        private static int CalculateColumnWidth(string header, int columnIndex, IReadOnlyList<IReadOnlyList<object?>> rows)
        {
            int width = Math.Clamp(header.Length + 4, 12, 28);
            foreach (var row in rows)
            {
                if (columnIndex < row.Count && row[columnIndex] is string text)
                    width = Math.Clamp(Math.Max(width, Math.Min(text.Length + 2, 42)), 12, 42);
            }
            return width;
        }

        private static XElement CreateCell(string address, object? value, int styleId)
        {
            var style = styleId > 0 ? new XAttribute("s", styleId.ToString(CultureInfo.InvariantCulture)) : null;
            if (value == null) return new XElement(MainNs + "c", new XAttribute("r", address), style);
            if (value is DateTime date) value = date.ToOADate();
            if (value is byte or sbyte or short or ushort or int or uint or long or ulong or float or double or decimal)
                return new XElement(MainNs + "c", new XAttribute("r", address), style, new XElement(MainNs + "v", Convert.ToString(value, CultureInfo.InvariantCulture)));
            return new XElement(MainNs + "c", new XAttribute("r", address), new XAttribute("t", "inlineStr"), style,
                new XElement(MainNs + "is", new XElement(MainNs + "t", new XAttribute(XNamespace.Xml + "space", "preserve"), Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty)));
        }
        private static string ColumnName(int number)
        {
            var result = new StringBuilder();
            while (number > 0)
            {
                number--;
                result.Insert(0, (char)('A' + number % 26));
                number /= 26;
            }
            return result.ToString();
        }

        private static void AddXml(ZipArchive archive, string path, XDocument document)
        {
            var entry = archive.CreateEntry(path, CompressionLevel.Optimal);
            using var stream = entry.Open();
            using var writer = new StreamWriter(stream, new UTF8Encoding(false));
            document.Save(writer);
        }
    }
}
