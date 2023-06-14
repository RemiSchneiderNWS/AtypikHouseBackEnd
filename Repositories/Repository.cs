using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public abstract class Repository<T> where T : DBModel
{
    protected AppDbContext Context;
    protected DbSet<T> Set = null!;

    protected Repository(AppDbContext context)
    {
        Context = context;
    }

    public IEnumerable<T> All()
    {
        return (Set);
    }

    public T FromId(int id)
    {
        return (Set.First(a => a.Id == id));
    }

    public void Add(T entity)
    {
        Set.Add(entity);
    }

    public void Save()
    {
        Context.SaveChanges();
    }
}