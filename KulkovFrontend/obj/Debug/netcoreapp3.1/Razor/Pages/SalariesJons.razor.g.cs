#pragma checksum "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ca4258aaec1c9fc6ba89d6db73e2e7df361fcc59"
// <auto-generated/>
#pragma warning disable 1591
namespace KulkovFrontend.Pages
{
    #line hidden
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\_Imports.razor"
using KulkovFrontend;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\_Imports.razor"
using KulkovFrontend.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
using Kulkov.Repository;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
using System.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
using System.Collections.Generic;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/salj")]
    public partial class SalariesJons : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Операции над зарплатами</h1>\r\n\r\n\r\n");
            __builder.OpenElement(1, "input");
            __builder.AddAttribute(2, "type", "button");
            __builder.AddAttribute(3, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 12 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
                               IndexSalaries

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "value", "Проиндексировать зарплаты");
            __builder.CloseElement();
            __builder.AddMarkupContent(5, "\r\n\r\n");
#nullable restore
#line 14 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
 if (id1 == id2)
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(6, "    ");
            __builder.AddMarkupContent(7, "<p>Индексы не могут быть одинаковы</p>\r\n");
#nullable restore
#line 17 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(8, "    ");
            __builder.OpenElement(9, "input");
            __builder.AddAttribute(10, "type", "button");
            __builder.AddAttribute(11, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 20 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
                                   TransactionExample

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(12, "value", "Пример транзакции с rollback");
            __builder.CloseElement();
            __builder.AddMarkupContent(13, "\r\n");
#nullable restore
#line 21 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
}

#line default
#line hidden
#nullable disable
            __builder.OpenElement(14, "select");
            __builder.AddAttribute(15, "class", "form-control");
            __builder.AddAttribute(16, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 22 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
                                     id1

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(17, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => id1 = __value, id1));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddMarkupContent(18, "\r\n\r\n    ");
            __builder.OpenElement(19, "option");
            __builder.AddAttribute(20, "value", "-1");
            __builder.AddMarkupContent(21, "Выберите сотрудника");
            __builder.CloseElement();
            __builder.AddMarkupContent(22, "\r\n");
#nullable restore
#line 25 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
     if (employees != null)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
         foreach (var row in employees)
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(23, "            ");
            __builder.OpenElement(24, "option");
            __builder.AddAttribute(25, "value", 
#nullable restore
#line 29 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
                            row.id_emp

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(26, " ");
            __builder.AddContent(27, 
#nullable restore
#line 29 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
                                          row.last_name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(28, "\r\n");
#nullable restore
#line 30 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
         
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(29, "\r\n");
            __builder.OpenElement(30, "select");
            __builder.AddAttribute(31, "class", "form-control");
            __builder.AddAttribute(32, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 33 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
                                     id2

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(33, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => id2 = __value, id2));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddMarkupContent(34, "\r\n\r\n    ");
            __builder.OpenElement(35, "option");
            __builder.AddAttribute(36, "value", "-2");
            __builder.AddMarkupContent(37, "Выберите сотрудника");
            __builder.CloseElement();
            __builder.AddMarkupContent(38, "\r\n");
#nullable restore
#line 36 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
     if (employees != null)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 38 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
         foreach (var row in employees)
        {

#line default
#line hidden
#nullable disable
            __builder.AddContent(39, "            ");
            __builder.OpenElement(40, "option");
            __builder.AddAttribute(41, "value", 
#nullable restore
#line 40 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
                            row.id_emp

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(42, " ");
            __builder.AddContent(43, 
#nullable restore
#line 40 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
                                          row.last_name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(44, "\r\n");
#nullable restore
#line 41 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
         
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(45, "\r\n\r\n");
#nullable restore
#line 45 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
 if (state)
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(46, "    ");
            __builder.AddMarkupContent(47, "<p>Зарплаты проиндексированы</p>\r\n");
#nullable restore
#line 48 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
}
else
{

}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 55 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\SalariesJons.razor"
       
    [Parameter]
    public bool state { get; set; } = false;
    [Parameter]
    public int id1 { get; set; } = -1;
    [Parameter]
    public int id2 { get; set; } = -2;

    private List<Kulkov.Data.Employee> employees;

    protected async Task TransactionExample()
    {
        await Service.TransactionExample(id1, id2);
    }

    protected async Task IndexSalaries()
    {
        state = await Service.IndexSalaries();
    }

    protected async override Task OnInitializedAsync()
    {
        employees = (await GetterEmp.GetAllEmployees()).ToList();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IEmployeeRepository GetterEmp { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ISalaryRepository Service { get; set; }
    }
}
#pragma warning restore 1591
