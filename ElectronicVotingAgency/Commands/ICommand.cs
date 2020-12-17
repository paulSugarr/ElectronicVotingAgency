using System.Collections.Generic;
using ElectronicVotingAgency.Server;

namespace ElectronicVotingAgency.Commands
{
    public interface ICommand
    {
        string Type { get; }
        Dictionary<string, object> GetInfo();
        void Execute(AgencyContext context, string id);
    }
}