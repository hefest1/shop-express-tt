using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public sealed class TasksEFCoreRepository : EFCoreRepository<ToDoTask>, ITaskRepository
{
    protected override DbSet<ToDoTask> TargetEntity { get; set; }

    public TasksEFCoreRepository(ToDoDBContext context) : base(context)
    {
        TargetEntity = Context.Tasks;
    }
}