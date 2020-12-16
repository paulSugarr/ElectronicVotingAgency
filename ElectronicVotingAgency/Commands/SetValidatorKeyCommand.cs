﻿using System.Collections.Generic;
using ElectronicVoting.Extensions;
using ElectronicVotingAgency.Server;

namespace ElectronicVotingAgency.Commands
{
    public class SetValidatorKeyCommand : ICommand
    {
        public string Type { get; }
        public Dictionary<string, object> Key { get; }
        
        public SetValidatorKeyCommand(Dictionary<string, object> info)
        {
            Type = "set_validator_key";
            Key = info.GetDictionary("key");
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("key", Key);
            return result;
        }
        public void Execute(AgencyContext context, string id)
        {
            context.InitializeAgency(Key);
        }
    }
}