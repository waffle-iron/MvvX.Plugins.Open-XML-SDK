﻿using DocumentFormat.OpenXml.Packaging;
using MvvX.Plugins.OpenXMLSDK.Platform.Word.Extensions;
using MvvX.Plugins.OpenXMLSDK.Word.ReportEngine.BatchModels;
using MvvX.Plugins.OpenXMLSDK.Word.ReportEngine.Models;


namespace MvvX.Plugins.OpenXMLSDK.Platform.Word.ReportEngine
{
    /// <summary>
    /// Extension for style
    /// </summary>
    public static class StyleExtensions
    {
        /// <summary>
        /// Add a style into document
        /// </summary>
        /// <param name="style"></param>
        /// <param name="spart"></param>
        /// <param name="context"></param>
        public static void Render(this Style style, StyleDefinitionsPart spart, ContextModel context)
        {
            var oxstyle = new DocumentFormat.OpenXml.Wordprocessing.Style()
            {
                Type = style.Type.ToOOxml(),
                CustomStyle = style.CustomStyle,
                StyleId = style.StyleId,
                StyleName = new DocumentFormat.OpenXml.Wordprocessing.StyleName() { Val = style.StyleId }
            };
            DocumentFormat.OpenXml.Wordprocessing.StyleRunProperties srp = new DocumentFormat.OpenXml.Wordprocessing.StyleRunProperties();
            if (style.Bold.HasValue && style.Bold.Value)
                srp.Append(new DocumentFormat.OpenXml.Wordprocessing.Bold());
            if (style.Italic.HasValue && style.Italic.Value)
                srp.Append(new DocumentFormat.OpenXml.Wordprocessing.Italic());
            if (!string.IsNullOrWhiteSpace(style.FontName))
                srp.Append(new DocumentFormat.OpenXml.Wordprocessing.RunFonts() { Ascii = style.FontName, HighAnsi = style.FontName, EastAsia = style.FontName, ComplexScript = style.FontName });
            if (!string.IsNullOrWhiteSpace(style.FontSize))
                srp.Append(new DocumentFormat.OpenXml.Wordprocessing.FontSize() { Val = style.FontSize });
            if (!string.IsNullOrWhiteSpace(style.FontColor))
                srp.Append(new DocumentFormat.OpenXml.Wordprocessing.Color() { Val = style.FontColor });
            if (!string.IsNullOrWhiteSpace(style.Shading))
                srp.Append(new DocumentFormat.OpenXml.Wordprocessing.Shading() { Fill = style.Shading });

            if (!string.IsNullOrWhiteSpace(style.StyleBasedOn))
                oxstyle.Append(new DocumentFormat.OpenXml.Wordprocessing.BasedOn() { Val = style.StyleBasedOn });

            oxstyle.Append(srp);

            spart.Styles.Append(oxstyle);
        }
    }
}
