

using System;
using System.Diagnostics;

namespace SleepApi.Controllers
{
    public class SleepService
    {
        public String pingServer()
        {
            var startInfo = new ProcessStartInfo(@"cmd.exe", "/c ping -n 8 http://20.106.201.151/")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            var pingProc = new Process { StartInfo = startInfo };
            pingProc.Start();

            pingProc.WaitForExit();

            var result = pingProc.StandardOutput.ReadToEnd();
            return result;
        }
    }
}