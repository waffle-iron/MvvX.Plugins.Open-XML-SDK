﻿using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using MvvX.Plugins.OpenXMLSDK.Word.ReportEngine.BatchModels;

namespace MvvX.Plugins.OpenXMLSDK.Platform.Word.ReportEngine
{
    /// <summary>
    /// Extension class for footers
    /// </summary>
    public static class FooterExtensions
    {
        /// <summary>
        /// Render the header of document
        /// </summary>
        /// <param name="header"></param>
        /// <param name="mainDocumentPart"></param>
        /// <param name="context"></param>
        public static void Render(this OpenXMLSDK.Word.ReportEngine.Models.Footer footer, MainDocumentPart mainDocumentPart, ContextModel context)
        {
            var footerPart = mainDocumentPart.AddNewPart<FooterPart>();

            footerPart.Footer = new Footer();

            foreach (var element in footer.ChildElements)
            {
                element.InheritFromParent(footer);
                element.Render(footerPart.Footer, context, footerPart);
            }

            string footerPartId = mainDocumentPart.GetIdOfPart(footerPart);
            if (!mainDocumentPart.Document.Body.Descendants<SectionProperties>().Any())
            {
                mainDocumentPart.Document.Body.AppendChild(new SectionProperties());
            }
            foreach (var section in mainDocumentPart.Document.Body.Descendants<SectionProperties>())
            {
                section.PrependChild(new FooterReference() { Id = footerPartId, Type = (HeaderFooterValues)(int)footer.Type });
            }

            if (footer.Type == OpenXMLSDK.Word.HeaderFooterValues.First)
            {
                mainDocumentPart.Document.Body.Descendants<SectionProperties>().First().PrependChild(new TitlePage());
            }
        }
    }
}
