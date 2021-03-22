namespace CovidMonitor
{
    using System.Collections.Generic;
    
    public interface IStore
    {
        string Name { get; }
        List<string> CheckAllLocations();
    }
}