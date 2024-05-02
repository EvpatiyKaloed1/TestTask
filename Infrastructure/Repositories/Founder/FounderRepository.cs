using Application.Commons.Interfaces;
using Domain.Common.ValueObjects;
using Infrastructure.Dto;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Founder;

public class FounderRepository : IFounderRepository
{
    private readonly Database _founderDatabase;

    public FounderRepository(Database founderDatabase)
    {
        _founderDatabase = founderDatabase;
    }

    public async Task<Domain.Founders.Founder> GetFounderByInnAsync(Inn inn, CancellationToken token)
    {
        var dbFounder = await _founderDatabase.Founders.FirstOrDefaultAsync(x => x.Inn == inn.InnValue, token);
        return dbFounder.Adapt<Domain.Founders.Founder>();
    }

    public async Task DeleteAsync(Guid id, CancellationToken token)
    {
        await _founderDatabase.Founders
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(token);
    }

    public async Task<IEnumerable<Domain.Founders.Founder>> GetFoundersAsync(IEnumerable<Guid> founders, CancellationToken cancellationToken)
    {
        var dbFounders = await _founderDatabase.Founders.Where(x => founders.Contains(x.Id)).ToListAsync(cancellationToken);
        return dbFounders.Adapt<IEnumerable<Domain.Founders.Founder>>();
    }

    public async Task<Domain.Founders.Founder> GetFounderByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var foundrt = await _founderDatabase.Founders.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return foundrt.Adapt<Domain.Founders.Founder>();
    }

    public async Task UpdateAsync(Domain.Founders.Founder founder, CancellationToken cancellationToken)
    {
        var dbFounders = founder.Adapt<FounderDto>();
        _founderDatabase.Founders.Update(dbFounders);
        await _founderDatabase.SaveChangesAsync(cancellationToken);
    }

    public async Task<Domain.Founders.Founder> CreateFounderAsync(Domain.Founders.Founder founder, CancellationToken cancellationToken)
    {
        await _founderDatabase.Founders.AddAsync(founder.Adapt<FounderDto>(), cancellationToken);
        await _founderDatabase.SaveChangesAsync(cancellationToken);
        return founder;
    }
}