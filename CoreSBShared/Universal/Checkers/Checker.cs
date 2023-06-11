using System;
using System.Threading.Tasks;

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
