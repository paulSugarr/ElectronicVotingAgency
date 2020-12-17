using System.Collections.Generic;
using ElectronicVoting.Extensions;
using ElectronicVotingAgency.Server;

namespace ElectronicVotingAgency.Commands
{
    public class SendElectorsCommand : ICommand
    {
        public string Type { get; }

        public int[] Electors { get; }

        public SendElectorsCommand(int[] electors)
        {
            Type = "electors";
            Electors = electors;
        }

        public SendElectorsCommand(Dictionary<string, object> info)
        {
            Type = "electors";
            Electors = info.GetIntArray("electors");
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("electors", Electors);
            return result;
        }

        public void Execute(AgencyContext context, string id)
        {

        }
    }
}