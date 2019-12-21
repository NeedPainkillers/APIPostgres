#pragma checksum "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Salary.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b3f26938c6a2e6dae21ac0cad3842019db4a7d58"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace KulkovFrontend.Pages
{
    #line hidden
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
#line 3 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Salary.razor"
using Kulkov.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Salary.razor"
using Kulkov.Repository;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Salary.razor"
using System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Salary.razor"
using System.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Salary.razor"
using System.Collections.Generic;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/sal")]
    public partial class Salary : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 82 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Salary.razor"
       
    public List<Kulkov.Data.Employee> employees;

    public List<Kulkov.Data.Salary> salaries;


    public Kulkov.Data.Salary salary = new Kulkov.Data.Salary();

    public bool[] vs;

    protected async Task Add()
    {
        await Service.AddSalary(salary);
        salary = new Kulkov.Data.Salary();
        await UpdateTable();
    }

    protected async Task Update(int row)
    {
        await Service.UpdateSalary(employees[row].id_emp, employees[row].salary);
        //await UpdateTable();
    }

    protected async Task Delete()
    {
        for (int i = 0; i < vs.Length; i++)
        {
            if (vs[i])
                await Service.RemoveSalary(employees[i].id_emp);
        }
        await UpdateTable();
    }

    protected async Task UpdateTable()
    {
        var response = await EmpGetter.GetAllEmployees(2, false);
        employees = response.ToList();
        vs = null;
        vs = new bool[employees.Count];
        response = null;
    }

    protected async override Task OnInitializedAsync()
    {
        var response = await EmpGetter.GetAllEmployees(2, false);
        employees = response.ToList();
        vs = new bool[employees.Count];
        response = null;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ISalaryRepository Service { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IEmployeeRepository EmpGetter { get; set; }
    }
}
#pragma warning restore 1591
