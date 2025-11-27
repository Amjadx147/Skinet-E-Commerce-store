using System;
using Core.Entities;
using Core.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T>(StorContext context) : IGenericRepository<T> where T : BaseEntitey
{
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public bool Exists(int id)
    {
        return context.Set<T>().Any(x=> x.Id == id);
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> Spec)
    {
        return await ApplySpecification(Spec).FirstOrDefaultAsync();
    }

    public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> Spec)
    {
        return await ApplySpecification(Spec).FirstOrDefaultAsync();
    }

    public async Task<T?> GetIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await context.Set<T>().ToArrayAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> Spec)
    {
        return await ApplySpecification(Spec).ToListAsync();
    }

    public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> Spec)
    {
        return await ApplySpecification(Spec).ToListAsync();
    }

    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0 ; 
    }

    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEviloter<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
    }

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
    {
        return SpecificationEviloter<T>.GetQuery<T, TResult>(context.Set<T>().AsQueryable(), spec);
    }
}
