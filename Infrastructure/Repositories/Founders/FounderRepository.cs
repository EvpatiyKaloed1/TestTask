using Application.Commons.Interfaces;
using Domain.Common.ValueObjects;
using Domain.Founders;
using Infrastructure.Dto;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Founders;

public class FounderRepository : IFounderRepository
{
    private readonly Database _founderDatabase;

    public FounderRepository(Database founderDatabase)
    {
        _founderDatabase = founderDatabase;
    }

    public async Task<Founder> GetFounderByInnAsync(Inn inn, CancellationToken token)
    {
        var dbFounder = await _founderDatabase.Founders.FirstOrDefaultAsync(x => x.Inn == inn.InnValue, token);
        return dbFounder.Adapt<Founder>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken token)
    {
        await _founderDatabase.Founders
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(token);
    }

    public async Task<IEnumerable<Founder>> GetFoundersAsync(IEnumerable<Guid> founders, CancellationToken cancellationToken)
    {
        var dbFounders = await _founderDatabase.Founders
            .AsNoTracking()
            .Where(x => founders.Contains(x.Id))
            .ToListAsync(cancellationToken);
        return dbFounders.Adapt<IEnumerable<Founder>>();
    }

    public async Task<Founder> GetFounderByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var founder = await _founderDatabase.Founders
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return founder.Adapt<Founder>();
    }

    public async Task UpdateAsync(Founder founder, CancellationToken cancellationToken)
    {
        var dbFounders = founder.Adapt<FounderDto>();
        _founderDatabase.Founders.Update(dbFounders);
        await _founderDatabase.SaveChangesAsync(cancellationToken);
    }

    public async Task<Founder> CreateFounderAsync(Founder founder, CancellationToken cancellationToken)
    {
        await _founderDatabase.Founders.AddAsync(founder.Adapt<FounderDto>(), cancellationToken);
        await _founderDatabase.SaveChangesAsync(cancellationToken);
        return founder;
    }

    public async Task<IEnumerable<Founder>> GetAllAsync(CancellationToken token)
    {
        var dbFounders = await _founderDatabase.Founders.ToListAsync(token);

        return dbFounders.Adapt<IEnumerable<Founder>>();
    }
}