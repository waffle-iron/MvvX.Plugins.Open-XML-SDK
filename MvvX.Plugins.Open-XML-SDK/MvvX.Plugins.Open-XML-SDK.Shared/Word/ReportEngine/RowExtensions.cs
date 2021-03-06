﻿using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using MvvX.Plugins.OpenXMLSDK.Word.ReportEngine.BatchModels;
using MvvX.Plugins.OpenXMLSDK.Word.ReportEngine.Models;

namespace MvvX.Plugins.OpenXMLSDK.Platform.Word.ReportEngine
{
    public static class RowExtensions
    {
        public static TableRow Render(this Row row, OpenXmlElement parent, ContextModel context, OpenXmlPart documentPart, bool isHeader)
        {
            context.ReplaceItem(row);

            TableRow wordRow = new TableRow();

            TableRowProperties wordRowProperties = new TableRowProperties();
            if (isHeader)
            {                
                wordRowProperties.AppendChild(new TableHeader() { Val = OnOffOnlyValues.On });
            }
            wordRow.AppendChild(wordRowProperties);

            if (row.RowHeight.HasValue)
            {
                wordRowProperties.AppendChild(new TableRowHeight() { Val = UInt32Value.FromUInt32((uint)row.RowHeight.Value)});
            }

            foreach (var cell in row.Cells)
            {
                cell.InheritFromParent(row);
                wordRow.AppendChild(cell.Render(wordRow, context, documentPart));
            }

            return wordRow;
        }
    }
}
