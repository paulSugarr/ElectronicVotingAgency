using System.Collections.Generic;
using ElectronicVotingValidator.Server;

namespace Networking.Commands
{
    public interface ICommand
    {
        string Type { get; }
        void Execute(AgencyContext context, string id);
        Dictionary<string, object> GetInfo();
    }
}