using System;
using System.Collections.Generic;
using ElectronicVoting.Cryptography;

namespace ElectronicVoting.Agencies
{
    public class Agency
    {
        private Dictionary<string, object> _validatorPublicKey;
        private ICryptographyProvider _cryptographyProvider;
        private Random _random;
        private int _electorsCount;

        private List<int> _usedIds;
        private Dictionary<int, byte[]> _encryptedBulletins;

        public Agency(ICryptographyProvider cryptographyProvider, Dictionary<string, object> validatorPublicKey, int electorsCount)
        {
            _cryptographyProvider = cryptographyProvider;
            _validatorPublicKey = validatorPublicKey;
            _electorsCount = electorsCount;
            _random = new Random();
            _usedIds = new List<int>();
            _encryptedBulletins = new Dictionary<int, byte[]>();
        }

        public int GetUniqueId()
        {
            var id = _random.Next(0, _electorsCount * _electorsCount);
            if (_usedIds.Contains(id))
            {
                return GetUniqueId();
            }
            _usedIds.Add(id);
            return id;
        }

        public void AddBulletin(byte[] validatorSigned, byte[] encryptedBulletin, int electorsId)
        {
            if (_cryptographyProvider.VerifyData(_validatorPublicKey, encryptedBulletin, validatorSigned))
            {
                Console.WriteLine("bulletin added");
                _encryptedBulletins.Add(electorsId, encryptedBulletin);
            }
        }
    }
}