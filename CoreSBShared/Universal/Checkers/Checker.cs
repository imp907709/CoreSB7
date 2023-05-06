using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreSBShared.Universal.Checkers
{

    public class AsyncExeptionsCheck
    {
        private AsyncExeptionsCheck inst;

        public async Task GO()
        {
            try
            {
                inst = new AsyncExeptionsCheck();
                inst.ThrowErr();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            try
            {
                await inst.ThrowErrTask();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                throw;
            }
        }
        
        public async Task ThrowErrTask()
        {
            await Task.Delay(1);
            throw new Exception("Task exp");
        }
        
        public async void ThrowErr()
        {
            await Task.Delay(1);
            throw new Exception("Err exp");
        }
    }
}