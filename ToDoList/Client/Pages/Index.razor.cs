using ToDoList.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace ToDoList.Client.Pages;

public partial class Index
{
    [Inject]
    public HttpClient Http { get; set; }
    [Inject]
    public AuthenticationStateProvider authenticationStateProvider { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public List<ToDoItem>? ToDoItems { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        var UserAuth = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
        if (UserAuth is not null && UserAuth.IsAuthenticated)
        {
            ToDoItems = await Http.GetFromJsonAsync<List<ToDoItem>>("api/ToDoList/GetToDoList");
        }
    }
    private async Task EditNote(ToDoItem? todoItem)
    {
        NavigationManager.NavigateTo($"/edit-todo-item/{todoItem.Id}");
    }

    private async Task AddNote()
    {
        ToDoItem? todoItem = new ToDoItem()
        {
            Id = 0
        };
        NavigationManager.NavigateTo($"/edit-todo-item/{todoItem.Id}");
    }
}
