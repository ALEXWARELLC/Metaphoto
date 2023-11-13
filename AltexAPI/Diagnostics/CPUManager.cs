using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.Versioning;

namespace Altex.Diagnostics;

public class CPUManager
{
    public static readonly CPUManager Instance;

    static CPUManager()
    {
        Instance = new CPUManager();
    }

    [Obsolete]
    public async Task<double> GetCpuUsageForProcess(Process Target)
    {
        var startTime = DateTime.UtcNow;
        var startCpuUsage = Target.TotalProcessorTime;
        await Task.Delay(500);

        var endTime = DateTime.UtcNow;
        var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
        var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
        var totalMsPassed = (endTime - startTime).TotalMilliseconds;
        var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
        return cpuUsageTotal * 100;
    }

    [Obsolete]
    [SupportedOSPlatform("Windows")]
    public async Task<float> GetCpuUsageAsync()
    {
        var tcs = new TaskCompletionSource<float>();

        var query = new SelectQuery("Win32_Processor", "ProcessorTime");
        var searcher = new ManagementObjectSearcher(query);

        ManagementObjectCollection? results = null;
        try
        {
            results = await Task.Run(() => searcher.Get());
            foreach (ManagementObject result in results)
            {
                // double check this conversion as I am not sure it's right
                var usage = (float)(Convert.ToUInt64(result.Properties["ProcessorTime"].Value)) / Environment.ProcessorCount;
                tcs.TrySetResult(usage);
                break; // We only need the first result
            }
        }
        catch (Exception ex)
        {
            tcs.TrySetException(ex);
        }
        finally
        {
            results?.Dispose();
            searcher?.Dispose();
        }

        return await tcs.Task;
    }
}