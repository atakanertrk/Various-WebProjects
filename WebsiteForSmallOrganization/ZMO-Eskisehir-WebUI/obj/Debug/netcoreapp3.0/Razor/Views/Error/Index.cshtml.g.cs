#pragma checksum "C:\Users\ataka\Desktop\ziraat-odasi\ZMO-Eskisehir-WebProject\ZMO-Eskisehir-Solution\ZMO-Eskisehir-WebUI\Views\Error\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5f72c287bbd1771c1ca6ce9bf44ef699d640b6b9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Error_Index), @"mvc.1.0.view", @"/Views/Error/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\ataka\Desktop\ziraat-odasi\ZMO-Eskisehir-WebProject\ZMO-Eskisehir-Solution\ZMO-Eskisehir-WebUI\Views\_ViewImports.cshtml"
using ZMO_Eskisehir_WebUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ataka\Desktop\ziraat-odasi\ZMO-Eskisehir-WebProject\ZMO-Eskisehir-Solution\ZMO-Eskisehir-WebUI\Views\_ViewImports.cshtml"
using ZMO_Eskisehir_WebUI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f72c287bbd1771c1ca6ce9bf44ef699d640b6b9", @"/Views/Error/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ae8b2843d67c690df2dc5d79c2d54a65e0e84a28", @"/Views/_ViewImports.cshtml")]
    public class Views_Error_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ZMO_Eskisehir_WebUI.Models.ErrorModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    <h3>Bir şeyler ters gitti...</h3>\r\n    <h4>hata mesajı:</h4>\r\n    <h5>");
#nullable restore
#line 5 "C:\Users\ataka\Desktop\ziraat-odasi\ZMO-Eskisehir-WebProject\ZMO-Eskisehir-Solution\ZMO-Eskisehir-WebUI\Views\Error\Index.cshtml"
   Write(Model.ErrorMessage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n    <h5>");
#nullable restore
#line 6 "C:\Users\ataka\Desktop\ziraat-odasi\ZMO-Eskisehir-WebProject\ZMO-Eskisehir-Solution\ZMO-Eskisehir-WebUI\Views\Error\Index.cshtml"
   Write(Model.ExtraMessage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ZMO_Eskisehir_WebUI.Models.ErrorModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
