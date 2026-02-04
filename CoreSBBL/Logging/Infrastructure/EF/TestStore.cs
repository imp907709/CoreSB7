using System;
using System.Linq;
using System.Threading.Tasks;
using CoreSBBL.LogingTest.Models;
using CoreSBShared.Universal.Infrastructure.EF.Store;
using Microsoft.EntityFrameworkCore;

namespace CoreSBBL.Logging.Infrastructure.EF;

public class TestStore : ITestStore
{
    private IEFStoreGK<TestContext> _context;
    
    public TestStore(IEFStoreGK<TestContext> context)
    {
        _context = context;
    }

    public async Task GO()
    {
        var rnd = new Random();

        var dt = DateTime.Now;
        var tests = Enumerable.Range(1, 150).Select(s => new LoginTest() {
            Name = $"name_{rnd.Next(1, 1000)}", CreatedAd = dt
        });
        
    }
}
