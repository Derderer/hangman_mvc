using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hangman_mvc.App_Code
{
    public static class MyHtmlHelpers
    {
        public static MvcHtmlString Image(this HtmlHelper helper, 
            string imageUrl, string alt, string width, string height, string hidden)
        {
            if (!String.IsNullOrEmpty(imageUrl))
            {
                
                TagBuilder imageTag = new TagBuilder("img");
                imageTag.MergeAttribute("src", imageUrl);
                imageTag.MergeAttribute("alt", alt);
                imageTag.MergeAttribute("width", width);
                imageTag.MergeAttribute("height", height);
                if (!(hidden == null) && hidden.Equals("hidden"))
                {
                    imageTag.MergeAttribute("hidden", hidden);
                }
                return MvcHtmlString.Create(imageTag.ToString());
            }
            return null;
        }

        public static MvcHtmlString FormSubmit(this HtmlHelper helper,
                string type, string name, string value)
        {
            if (!String.IsNullOrEmpty(type))
            {
                TagBuilder submitTag = new TagBuilder("input");
                submitTag.MergeAttribute("type", type);
                submitTag.MergeAttribute("name", name);
                submitTag.MergeAttribute("value", value);
                return MvcHtmlString.Create(submitTag.ToString());
            }
            return null;
        }
    }
}