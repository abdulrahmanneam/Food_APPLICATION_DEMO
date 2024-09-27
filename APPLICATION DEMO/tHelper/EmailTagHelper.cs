using Microsoft.AspNetCore.Razor.TagHelpers;

namespace APPLICATION_DEMO.tHelper
{
    public class EmailTagHelper : TagHelper
    {
        public string Address { get; set; }
        public string Email { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + Address); 
            output.Content.SetContent(Email);
        }
    }
}
