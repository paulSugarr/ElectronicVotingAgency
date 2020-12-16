using System;
using System.Collections.Generic;
using ElectronicVoting.Agencies;
using ElectronicVoting.Cryptography;
using Factory;

namespace ElectronicVotingAgency.Server
{
    public class AgencyContext
    {
        public AgencyContext(ServerModel server)
        {
            CryptographyProvider = new RSACryptography();


            Server = server;

            MainFactory = new MainFactory();
            MainFactory.RegisterTypes();

            RegisteredUsers = new RegisteredUsers();
            RegisteredUsers.RegisterUsers();
        }

        public ICryptographyProvider CryptographyProvider { get; }
        public Agency Agency { get; private set; }
        public ServerModel Server { get; }
        public MainFactory MainFactory { get; }
        public RegisteredUsers RegisteredUsers { get; }

        public void InitializeAgency(Dictionary<string, object> validatorKey)
        {
            Agency = new Agency(CryptographyProvider, validatorKey, 1);
            Console.WriteLine("agency created");
        }
    }
}