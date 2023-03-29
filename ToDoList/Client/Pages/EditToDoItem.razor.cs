using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using ToDoList.Shared;

namespace ToDoList.Client.Pages;

public partial class EditToDoItem
{
    [Inject]
    public HttpClient Http { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Parameter]
    public int Id { get; set; }
    public ToDoItem UserToDoItem { get; set; } = new ToDoItem();

    protected override async Task OnInitializedAsync()
    {
        if (Id != 0)
        {
            UserToDoItem = await Http.GetFromJsonAsync<ToDoItem>($"api/todoitems/{Id}");
        }
    }

    private async Task HandleValidSubmit()
    {
        HttpResponseMessage? response = new();
        if (Id == 0)
        {
            response = await Http.PostAsJsonAsync($"api/todoitems", UserToDoItem);
        } else
        {
            response = await Http.PutAsJsonAsync($"api/todoitems/{Id}", UserToDoItem);
        }

        if (response.IsSuccessStatusCode)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
