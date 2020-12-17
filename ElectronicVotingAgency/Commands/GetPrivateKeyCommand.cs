using System;
using System.Collections.Generic;
using System.Diagnostics;
using ElectronicVoting.Extensions;
using ElectronicVotingAgency.Server;

namespace ElectronicVotingAgency.Commands
{
    public class GetPrivateKeyCommand : ICommand
    {
        public string Type { get; }
        public int Id { get; }
        public Dictionary<string, object> PrivateKey { get; }

        public GetPrivateKeyCommand(Dictionary<string, object> info)
        {
            Type = "send_private";
            Id = info.GetInt("id");
            PrivateKey = info.GetDictionary("key");
        }

        public GetPrivateKeyCommand(int id, Dictionary<string, object> privateKey)
        {
            Type = "send_private";
            Id = id;
            PrivateKey = privateKey;
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("id", Id);
            result.Add("key", PrivateKey);
            return result;
        }

        public void Execute(AgencyContext context, string id)
        {
            var choice = context.Agency.DecryptBulletin(Id, PrivateKey);
            Console.WriteLine($"choice = {choice}");
        }
    }
}