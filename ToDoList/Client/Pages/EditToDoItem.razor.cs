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
    public ToDoItem ToDoItem { get; set; } = new ToDoItem();

    protected override async Task OnInitializedAsync()
    {
        ToDoItem = await Http.GetFromJsonAsync<ToDoItem>($"api/todoitems/{Id}");
        UserToDoItem = ToDoItem;
    }

    private async Task HandleValidSubmit()
    {
        ToDoItem updateUserToDoItem = new ToDoItem
        {
            Id = ToDoItem.Id,
            ApplicationUserId = ToDoItem.ApplicationUserId,
            Title = UserToDoItem.Title,
            DueDate = UserToDoItem.DueDate,
            Completed = UserToDoItem.Completed,
            Notes = UserToDoItem.Notes
        };
        var response = await Http.PutAsJsonAsync($"api/todoitems/{Id}", updateUserToDoItem);
        if (response.IsSuccessStatusCode)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
