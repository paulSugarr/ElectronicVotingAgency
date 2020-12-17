using System.Collections.Generic;
using ElectronicVoting.Extensions;
using ElectronicVotingAgency.Server;

namespace ElectronicVotingAgency.Commands
{
    public class GetElectorsCommand : ICommand
    {
        public string Type { get; }

        public GetElectorsCommand()
        {
            Type = "get_electors";
        }

        public GetElectorsCommand(Dictionary<string, object> info)
        {
            Type = "get_electors";
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            return result;
        }

        public void Execute(AgencyContext context, string id)
        {
            var electors = context.Agency.GetElectors();
            var command = new SendElectorsCommand(electors);
            context.Server.SendCommand(command, id);
        }
    }
}