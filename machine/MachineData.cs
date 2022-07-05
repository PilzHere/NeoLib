using System.Runtime.InteropServices;

namespace NeoLib.machine;

public class MachineData
{
    // TODO: Might make this to Singleton or as static later.

    private OSPlatform _platform;
    private OperatingSystem _operatingSystem;
    private Architecture _osArchitecture;
    private int _cpuLogicCores;
    private string _osDescription;
    private string _dotNetVersion;
    private Architecture _appArchitecture;

    public OSPlatform Platform => _platform;

    public OperatingSystem OperatingSystem => _operatingSystem;

    public Architecture OsArchitecture => _osArchitecture;

    public int CpuLogicCores => _cpuLogicCores;

    public string OsDescription => _osDescription;

    public string DotNetVersion => _dotNetVersion;

    public Architecture AppArchitecture => _appArchitecture;

    public void CollectMachineData()
    {
        // * OS
        _platform = GetOperatingSystemPlatform();
        _operatingSystem = GetOperatingSystemVersion();
        _osArchitecture = GetOperatingSystemArchitecture();
        _cpuLogicCores = GetCpuLogicCoresCount();
        _osDescription = GetOperatingSystemDescription();

        // * .NET
        _dotNetVersion = GetDotNetVersion();

        // * APP
        _appArchitecture = GetAppArchitecture();
    }

    public void PrintMachineData()
    {
        Console.WriteLine("SYSTEM INFO:\nOS: " + _osDescription
                                               + "\nPlatform: " + _platform
                                               + "\nOS version: " + _operatingSystem
                                               + "\nOS arch: " + _osArchitecture
                                               + "\nCPU logic cores: " + _cpuLogicCores
                                               + "\n.NET: " + _dotNetVersion
                                               + "\nApp arch: " + _appArchitecture);
    }

    private Architecture GetAppArchitecture()
    {
        var appArchitecture = RuntimeInformation.ProcessArchitecture;

        return appArchitecture;
    }

    private string GetDotNetVersion()
    {
        var dotNetVersion = RuntimeInformation.FrameworkDescription;

        return dotNetVersion;
    }

    private string GetOperatingSystemDescription()
    {
        var osDescription = RuntimeInformation.OSDescription;

        return osDescription;
    }

    private int GetCpuLogicCoresCount()
    {
        var cpuLogicCores = Environment.ProcessorCount;

        return cpuLogicCores;
    }

    private Architecture GetOperatingSystemArchitecture()
    {
        var arch = RuntimeInformation.OSArchitecture;

        return arch;
    }

    private OSPlatform GetOperatingSystemPlatform()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return OSPlatform.Windows;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OSPlatform.OSX;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return OSPlatform.Linux;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            return OSPlatform.FreeBSD;
        }

        throw new Exception("WARNING: Can not determine operating system!");
    }

    private OperatingSystem GetOperatingSystemVersion()
    {
        return Environment.OSVersion;
    }
}