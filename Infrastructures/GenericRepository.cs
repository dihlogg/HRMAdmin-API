namespace AdminHRM.Server.Infrastructures;

using AdminHRM.Server.DataContext;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Infrastructures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntities
{
    protected readonly HrmDbContext _hrmDbContext;

    public GenericRepository(HrmDbContext hrmDbContext)
    {
        _hrmDbContext = hrmDbContext;
    }

    public T? GetById(Guid id)
    {
        return _hrmDbContext.Set<T>().Find(id);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _hrmDbContext.Set<T>().FindAsync(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _hrmDbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _hrmDbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public IEnumerable<T> Get(Expression<Func<T, bool>> expression)
    {
        return _hrmDbContext.Set<T>().Where(expression);
    }

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression)
    {
        var resultData = await _hrmDbContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        return resultData;
    }

    public T? GetObject(Expression<Func<T, bool>> expression)
    {
        return _hrmDbContext.Set<T>().Find(expression);
    }

    public async Task<T?> GetObjectAsync(Expression<Func<T, bool>> expression)
    {
        return await _hrmDbContext.Set<T>().FindAsync(expression);
    }

    private bool Insert(T pObj)
    {
        try
        {
            _hrmDbContext.Entry(pObj).State = EntityState.Added;
            return true;
        }
        catch
        {
            throw;
        }
    }

    public async Task<T> AddReturnModelAsync(T entity)
    {
        await using var transaction = await _hrmDbContext.Database.BeginTransactionAsync();
        try
        {
            bool isOk = Insert(entity);
            if (!isOk)
            {
                return entity;
            }
            await _hrmDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return entity;
        }
        catch
        {
            await _hrmDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public bool Add(T entity)
    {
        using var transaction = _hrmDbContext.Database.BeginTransaction();
        try
        {
            bool isOk = Insert(entity);
            if (!isOk)
            {
                return false;
            }
            _hrmDbContext.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch
        {
            _hrmDbContext.Database.RollbackTransaction();
            throw;
        }
    }

    public async Task<bool> AddAsync(T entity)
    {
        await using var transaction = await _hrmDbContext.Database.BeginTransactionAsync();
        try
        {
            bool isOk = Insert(entity);
            if (!isOk)
            {
                return false;
            }
            await _hrmDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await _hrmDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public bool AddMany(IEnumerable<T> entity)
    {
        using var transaction = _hrmDbContext.Database.BeginTransaction();
        try
        {
            _hrmDbContext.Set<T>().AddRange(entity);
            _hrmDbContext.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch
        {
            _hrmDbContext.Database.RollbackTransaction();
            throw;
        }
    }

    public async Task<bool> AddManyAsync(IEnumerable<T> entity)
    {
        await using var transaction = await _hrmDbContext.Database.BeginTransactionAsync();
        try
        {
            await _hrmDbContext.Set<T>().AddRangeAsync(entity);
            await _hrmDbContext.SaveChangesAsync();

            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await _hrmDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public T? GetObject(params object[] pKeys)
    {
        return _hrmDbContext.Set<T>().Find(pKeys);
    }

    public async Task<T?> GetObjectAsync(params object[] pKeys)
    {
        return await _hrmDbContext.Set<T>().FindAsync(pKeys);
    }

    private bool UpdateWithObject(T pObj, params string[] pNotUpdatedProperties)
    {
        try
        {
            var keyNames = GetPrimaryKey();
            var keyValues = keyNames.Select(p => pObj.GetType().GetProperty(p)?.GetValue(pObj, null)).ToArray();
            if (keyValues != null)
            {
                T exist = GetObject(keyValues!)!;
                if (exist != null)
                {
                    _hrmDbContext.Entry(exist).State = EntityState.Detached;
                    _hrmDbContext.Attach(pObj);
                    var entry = _hrmDbContext.Entry(pObj);
                    entry.State = EntityState.Modified;

                    if (pNotUpdatedProperties.Any())
                    {
                        foreach (string prop in pNotUpdatedProperties)
                        {
                            entry.Property(prop).IsModified = false;
                        }
                    }

                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        catch
        {
            throw;
        }
    }

    public bool Update(T pObj)
    {
        return UpdateWithTransaction(pObj, "CreateDate", "CreateBy");
    }

    public async Task<bool> UpdateAsync(T pObj)
    {
        return await UpdateWithTransactionAsync(pObj, "CreateDate", "CreateBy");
    }

    public async Task<bool> UpdateStatusAsync(T pObj)
    {
        return await UpdateWithTransactionAsync(pObj, "CreateDate", "CreateBy");
    }

    private bool UpdateWithTransaction(T pObj, params string[] pNotUpdatedProperties)
    {
        using var transaction = _hrmDbContext.Database.BeginTransaction();
        try
        {
            bool isOk = UpdateWithObject(pObj, pNotUpdatedProperties);
            if (isOk)
            {
                _hrmDbContext.SaveChanges();
                transaction.Commit();
            }

            return isOk;
        }
        catch (Exception ex)
        {
            _hrmDbContext.Database.RollbackTransaction();
            throw;
        }
    }

    private async Task<bool> UpdateWithTransactionAsync(T pObj, params string[] pNotUpdatedProperties)
    {
        await using var transaction = await _hrmDbContext.Database.BeginTransactionAsync();
        try
        {
            bool isOk = UpdateWithObject(pObj, pNotUpdatedProperties);
            if (isOk)
            {
                await _hrmDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }

            return isOk;
        }
        catch
        {
            await _hrmDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public bool UpdateMany(IEnumerable<T> entity)
    {
        using var transaction = _hrmDbContext.Database.BeginTransaction();
        try
        {
            _hrmDbContext.Set<T>().UpdateRange(entity);
            _hrmDbContext.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch
        {
            _hrmDbContext.Database.RollbackTransaction();
            throw;
        }
    }

    public async Task<bool?> DeleteByKey(Guid pKey)
    {
        await using var transaction = _hrmDbContext.Database.BeginTransaction();
        try
        {
            var obj = GetById(pKey);
            if (obj == null)
            {
                return null;
            }
            _ = _hrmDbContext.Remove(obj);
            await _hrmDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await _hrmDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    private string[] GetPrimaryKey()
    {
        return _hrmDbContext.Model?.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.Select(x => x.Name)?.ToArray() ?? [];
    }
}