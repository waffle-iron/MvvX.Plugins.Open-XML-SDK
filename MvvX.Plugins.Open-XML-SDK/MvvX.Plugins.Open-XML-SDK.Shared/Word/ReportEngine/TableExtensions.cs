﻿using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using MvvX.Plugins.OpenXMLSDK.Platform.Word.Extensions;
using MvvX.Plugins.OpenXMLSDK.Word.ReportEngine.BatchModels;

namespace MvvX.Plugins.OpenXMLSDK.Platform.Word.ReportEngine
{
    public static class TableExtensions
    {
        /// <summary>
        /// Render a table element
        /// </summary>
        /// <param name="table"></param>
        /// <param name="parent"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Table Render(this OpenXMLSDK.Word.ReportEngine.Models.Table table, OpenXmlElement parent, ContextModel context, OpenXmlPart documentPart)
        {
            context.ReplaceItem(table);

            Table wordTable = new Table();

            TableProperties wordTableProperties = new TableProperties();
            var tableLook = new TableLook()
            {
                FirstRow = OnOffValue.FromBoolean(false),
                LastRow = OnOffValue.FromBoolean(false),
                FirstColumn = OnOffValue.FromBoolean(false),
                LastColumn = OnOffValue.FromBoolean(false),
                NoHorizontalBand = OnOffValue.FromBoolean(false),
                NoVerticalBand = OnOffValue.FromBoolean(false)
            };
            wordTableProperties.AppendChild(tableLook);
            //The type must be Dxa, if the value is set to Pct : the open xml engine will ignored it !
            wordTableProperties.TableIndentation = new TableIndentation() { Width = table.TableIndentation.Width, Type = TableWidthUnitValues.Dxa };
            wordTable.AppendChild(wordTableProperties);

            if (table.Borders != null)
            {
                TableBorders borders = table.Borders.Render();

                wordTableProperties.AppendChild(borders);
            }

            // add column width definitions
            if (table.ColsWidth != null)
            {
                wordTable.AppendChild(new TableLayout() { Type = TableLayoutValues.Fixed });

                TableGrid tableGrid = new TableGrid();
                foreach (int width in table.ColsWidth)
                {
                    tableGrid.AppendChild(new GridColumn() { Width = width.ToString(CultureInfo.InvariantCulture) });
                }
                wordTable.AppendChild(tableGrid);
            }

            if (table.TableWidth != null)
            {
                wordTable.AppendChild(new TableWidth() { Width = table.TableWidth.Width, Type = table.TableWidth.Type.ToOOxml() });
            }

            // add header row
            if (table.HeaderRow != null)
            {
                wordTable.AppendChild(table.HeaderRow.Render(wordTable, context, documentPart, true));

                tableLook.FirstRow = OnOffValue.FromBoolean(true);
            }

            // add content rows
            if (!string.IsNullOrEmpty(table.DataSourceKey))
            {
                if (context.ExistItem<DataSourceModel>(table.DataSourceKey))
                {
                    var datasource = context.GetItem<DataSourceModel>(table.DataSourceKey);

                    foreach (ContextModel item in datasource.Items)
                    {
                        var row = table.RowModel.Clone();
                        row.InheritFromParent(table);
                        wordTable.AppendChild(row.Render(wordTable, item, documentPart, false));
                    }
                }
            }
            else
            {
                foreach (var row in table.Rows)
                {
                    row.InheritFromParent(table);
                    wordTable.AppendChild(row.Render(wordTable, context, documentPart, false));
                }
            }


            // add footer row
            if (table.FooterRow != null)
            {
                wordTable.AppendChild(table.FooterRow.Render(wordTable, context, documentPart, false));

                tableLook.LastRow = OnOffValue.FromBoolean(true);
            }
            parent.AppendChild(wordTable);
            return wordTable;
        }
    }
}
