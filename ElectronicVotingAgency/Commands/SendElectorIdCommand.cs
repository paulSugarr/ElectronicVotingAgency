using System.Collections.Generic;
using ElectronicVoting.Extensions;
using ElectronicVotingAgency.Server;

namespace ElectronicVotingAgency.Commands
{
    public class SendElectorIdCommand : ICommand
    {
        public string Type { get; }

        public int Id { get; }

        public SendElectorIdCommand(int id)
        {
            Type = "send_id";
            Id = id;
        }
        public SendElectorIdCommand(Dictionary<string, object> info)
        {
            Type = info.GetString("type");
            Id = info.GetInt("id");
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("id", Id);
            return result;
        }

        public void Execute(AgencyContext context, string id)
        {
            
        }
    }
}