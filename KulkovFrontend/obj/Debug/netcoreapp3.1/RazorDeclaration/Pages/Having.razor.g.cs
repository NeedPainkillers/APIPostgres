#pragma checksum "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\Pages\Having.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a856980437dc5dc6f6d1089c2f017a7d1de791ad"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace KulkovFrontend.Pages
{
    #line hidden
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\_Imports.razor"
using KulkovFrontend;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\_Imports.razor"
using KulkovFrontend.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\Pages\Having.razor"
using Kulkov.Repository;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\Pages\Having.razor"
using System.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\Pages\Having.razor"
using System.Collections.Generic;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/have")]
    public partial class Having : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 39 "C:\Users\apcxo\source\repos\APIPostges\KulkovFrontend\Pages\Having.razor"
       

    private List<Kulkov.Data.Department> departments;


    protected async override Task OnInitializedAsync()
    {
        departments = (await Service.GetDepartmentsHaving()).ToList();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IDepartmentRepository Service { get; set; }
    }
}
#pragma warning restore 1591
