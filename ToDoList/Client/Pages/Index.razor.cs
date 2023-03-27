using ToDoList.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ToDoList.Client.Pages;

public partial class Index
{
    [Inject]
    public HttpClient Http { get; set; }
    public List<ToDoItem>? ToDoItems { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        ToDoItems = await Http.GetFromJsonAsync<List<ToDoItem>>("api/ToDoList/GetToDoList");
    }
}
