﻿using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoList.Server.Models;
using ToDoList.Shared;

namespace ToDoList.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
        public DbSet<ChecklistItem> ChecklistItems => Set<ChecklistItem>();
    }
}