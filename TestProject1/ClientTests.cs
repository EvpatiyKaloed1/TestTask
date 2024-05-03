using Domain.Clients;
using Domain.Clients.Exceptions;
using Domain.Clients.ValueObjects;
using Domain.Common.ValueObjects;
using Domain.Founders;
using Domain.Founders.ValueObjects;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tests;

public class ClientTests
{
    [Fact]
    public void Client_ShouldThrowInvalidClientTypeException_IfIndividualHasFounders()
    {
        var func = () => new Client(new Inn("1234567890"),
                                    ClientType.Individual,
                                    new Dates(DateTime.Now, null),
                                    new ClientName("clietnName"),
                                    founders: []);
        Assert.Throws<InvalidClientTypeException>(func);
    }
    [Fact]
    public void Client_ShouldThrowArgumentNullException_IfLegalEntityHasNoFounders()
    {
        var func = () => new Client(new Inn("1234567890"),
                                   ClientType.LegalEntity,
                                   new Dates(DateTime.Now, null),
                                   new ClientName("clietnName"),
                                   founders: null);
        Assert.Throws<ArgumentNullException>(func);
    }
    [Fact]
    public void Client_ShloudDeleteFounder_ByDefault()
    {
        var inn=new Inn("1234567890");
        var date=new Dates(DateTime.Now);
        var founderName = new FounderFullName("firstName", "LastName", "SurName");
        var founder1=new Founder(inn, founderName, date);
        var founder2 = new Founder(inn, founderName, date);
        var founder3 = new Founder(inn, founderName, date);
        var client = new Client(inn,
                                ClientType.LegalEntity,
                                date, new ClientName("clietnName"),
                                founders: [founder1,founder2,founder3]);

        client.DeleteFounders([founder1,founder2]);

        Assert.True(client.Founders.Count == 1);
        Assert.Equal(founder3, client.Founders[0]);
    }
}
