using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private List<int> _candidates;

        public Agency(ICryptographyProvider cryptographyProvider, Dictionary<string, object> validatorPublicKey, int electorsCount)
        {
            _cryptographyProvider = cryptographyProvider;
            _validatorPublicKey = validatorPublicKey;
            _electorsCount = electorsCount;
            _random = new Random();
            _usedIds = new List<int>();
            _encryptedBulletins = new Dictionary<int, byte[]>();
            
            _candidates = new List<int>();
            
            _candidates.Add(0);
            _candidates.Add(0);
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

        public int DecryptBulletin(int id, Dictionary<string, object> privateKey)
        {
            var info = _cryptographyProvider.Decrypt(privateKey, _encryptedBulletins[id]);
            var stringResult = Encoding.UTF8.GetString(info);
            var choice = Convert.ToInt32(stringResult);
            _candidates[choice]++;
            return choice;
        }

        public int[] GetCandidates()
        {
            return _candidates.ToArray();
        }

        public int[] GetElectors()
        {
            return _encryptedBulletins.Keys.ToArray();
        }

        public int[] GetResults()
        {
            return _candidates.ToArray();
        }
    }
}