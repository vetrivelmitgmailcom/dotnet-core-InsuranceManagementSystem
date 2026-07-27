//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Microsoft.AspNetCore.Razor.TagHelpers;
//using Microsoft.AspNetCore.Mvc.Rendering;


//namespace InsuranceManagementSystemMVC.Helper
//{
//    [HtmlTargetElement("div", Attributes = ValidationForAttributeName)]
//    public class ValidationDivTagHelper : TagHelper
//    {
//        private const string ValidationForAttributeName = "asp-validation-for";

//        [HtmlAttributeName(ValidationForAttributeName)]
//        public ModelExpression For { get; set; }

//        [HtmlAttributeName("class")]
//        public string CssClass { get; set; } = "text-danger fst-italic text-end";

//        public override void Process(TagHelperContext context, TagHelperOutput output)
//        {
//            if (For == null || For.ModelState.IsValid)
//            {
//                return;
//            }

//            output.TagName = "div";
//            output.TagMode = TagMode.StartTagAndEndTag;
//            output.Attributes.Add("class", CssClass);

//            var errorMessage = For.ModelState.Values
//                .SelectMany(v => v.Errors)
//                .FirstOrDefault()?.ErrorMessage;

//            if (string.IsNullOrEmpty(errorMessage))
//            {
//                return;
//            }

//            output.Content.SetContent(errorMessage);
//        }
//    }
//}

