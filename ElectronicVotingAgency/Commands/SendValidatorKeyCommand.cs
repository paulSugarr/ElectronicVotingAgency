using System.Collections.Generic;
using System.Text;
using ElectronicVoting.Extensions;
using ElectronicVotingValidator.Server;

namespace Networking.Commands
{
    public class SendValidatorKeyCommand : ICommand
    {
        public string Type { get; }

        public SendValidatorKeyCommand()
        {
            Type = "send_validator_key";
        }

        public SendValidatorKeyCommand(Dictionary<string, object> info)
        {
            Type = "send_validator_key";
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            return result;
        }

        public void Execute(AgencyContext context, string id)
        {

        }
    }
}