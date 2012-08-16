using System;
using System.Web.Mvc;

namespace SIGAPPBOM.Web.HtmlHelpers
{
    public static class GlyphImageHelper
    {
        public static MvcHtmlString GlyphtImage(this HtmlHelper helper, string imageConfiguration)
        {

            string iHtml = string.Empty;

            if (!String.IsNullOrEmpty(imageConfiguration))
            {
                var iTag = new TagBuilder("i");
                iTag.AddCssClass(imageConfiguration);
                iHtml = iTag.ToString();
            }

            return MvcHtmlString.Create(iHtml);
        }
    }
}