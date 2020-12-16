using System.Collections.Generic;
using ElectronicVotingAgency.Server;

namespace ElectronicVotingAgency.Commands
{
    public interface ICommand
    {
        string Type { get; }
        void Execute(AgencyContext context, string id);
        Dictionary<string, object> GetInfo();
    }
}