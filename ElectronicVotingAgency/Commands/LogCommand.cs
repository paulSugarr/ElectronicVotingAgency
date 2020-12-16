﻿using System;
using System.Collections.Generic;
using ElectronicVoting.Extensions;
using ElectronicVotingAgency.Server;

namespace ElectronicVotingAgency.Commands
{
    public class LogCommand : ICommand
    {
        public string Type { get; }
        public string Message { get; }

        public LogCommand(string message)
        {
            Type = "log";
            Message = message;
        }
        public LogCommand(string type, Dictionary<string, object> info)
        {
            Type = "log";
            Message = info.GetString("message");
        }

        public Dictionary<string, object> GetInfo()
        {
            var result = new Dictionary<string, object>();
            result.Add("type", Type);
            result.Add("message", Message);
            return result;
        }
        public void Execute(AgencyContext context, string id)
        {
            Console.WriteLine(Message);
        }
    }
}