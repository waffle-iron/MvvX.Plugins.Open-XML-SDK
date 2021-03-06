﻿using MvvX.Plugins.OpenXMLSDK.Word.ReportEngine.Models.Attributes;
using MvvX.Plugins.OpenXMLSDK.Word.Tables;

namespace MvvX.Plugins.OpenXMLSDK.Word.ReportEngine.Models
{
    /// <summary>
    /// Table Cell
    /// </summary>
    public class Cell : BaseElement
    {
        /// <summary>
        /// Borders
        /// </summary>
        public BorderModel Borders { get; set; }

        /// <summary>
        /// Colspan
        /// </summary>
        public int ColSpan { get; set; }

        /// <summary>
        /// Row Span : Cell is merged vertically : it's the first merged cell
        /// </summary>
        public bool Fusion { get; set; }

        /// <summary>
        /// Cell is a hidden merged part of a rowspan
        /// </summary>
        public bool FusionChild { get; set; }

        /// <summary>
        /// Justification/ horizontal alignment of cells content
        /// </summary>
        public JustificationValues? Justification { get; set; }

        /// <summary>
        /// Vertical alignment of cell content
        /// </summary>
        public TableVerticalAlignmentValues? VerticalAlignment { get; set; }

        /// <summary>
        /// Text direction in cell
        /// </summary>
        public TextDirectionValues? TextDirection { get; set; }

        /// <summary>
        /// Margins in cell
        /// </summary>
        public MarginModel Margin { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Cell()
            : base(typeof(Cell).Name)
        {
        }
    }
}
