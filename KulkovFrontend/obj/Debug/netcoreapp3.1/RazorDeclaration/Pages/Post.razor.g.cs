#pragma checksum "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Post.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7bc7f6891d563221eaae30ec2bd2985b66281537"
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
#line 3 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Post.razor"
using Kulkov.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Post.razor"
using Kulkov.Repository;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Post.razor"
using System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Post.razor"
using System.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Post.razor"
using System.Collections.Generic;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/post")]
    public partial class Post : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 57 "C:\Users\a7aro\source\repos\Kulkov\KulkovFrontend\Pages\Post.razor"
       

    public Kulkov.Data.Post post = new Kulkov.Data.Post();

    public List<Kulkov.Data.Post> posts;

    public bool[] vs;

    protected async Task Add()
    {
        await Service.AddPost(post);
        post = new Kulkov.Data.Post();
        await UpdateTable();
    }

    protected async Task Update(int row)
    {
        await Service.UpdatePost(posts[row].id_post, posts[row]);
        //await UpdateTable();
    }

    protected async Task Delete()
    {
        for (int i = 0; i < vs.Length; i++)
        {
            if (vs[i])
                await Service.RemovePost(posts[i].id_post);
        }
        await UpdateTable();
    }

    protected async Task UpdateTable()
    {
        var response = await Service.GetAllPosts();
        posts = response.ToList();
        vs = null;
        vs = new bool[posts.Count];
        response = null;
    }

    protected async override Task OnInitializedAsync()
    {
        var response = await Service.GetAllPosts();
        posts = response.ToList();
        vs = new bool[posts.Count];
        response = null;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IPostRepository Service { get; set; }
    }
}
#pragma warning restore 1591
