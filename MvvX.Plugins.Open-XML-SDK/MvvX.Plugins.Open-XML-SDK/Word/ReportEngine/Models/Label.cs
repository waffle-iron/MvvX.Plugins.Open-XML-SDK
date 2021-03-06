﻿namespace MvvX.Plugins.OpenXMLSDK.Word.ReportEngine.Models
{
    /// <summary>
    /// Model for a label
    /// </summary>
    public class Label : BaseElement
    {
        /// <summary>
        /// Label content (can contains #key# from context)
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// if true, the label contains the page number
        /// </summary>
        public bool IsPageNumber { get; set; }

        /// <summary>
        /// Indicate if the engine must preserve empty space or space before or after text in generation
        /// </summary>
        public SpaceProcessingModeValues SpaceProcessingModeValue { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Label()
            : base(typeof(Label).Name)
        {
        }
    }
}
