using System.Collections.Generic;
using ElectronicVoting.Extensions;
using ElectronicVotingAgency.Server;

namespace ElectronicVotingAgency.Commands
{
    public class SendResultsCommand : ICommand
    {
        public string Type { get; }

        public int[] Candidates { get; }

        public SendResultsCommand(int[] candidates)
        {
            Type = "results";
            Candidates = candidates;
        }

        public SendResultsCommand(Dictionary<string, object> info)
        {
            Type = "results";
            Candidates = info.GetIntArray("candidates");
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("candidates", Candidates);
            return result;
        }

        public void Execute(AgencyContext context, string id)
        {

        }
    }
}