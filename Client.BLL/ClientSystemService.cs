using Client.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BLL
{
    public class ClientSystemService
    {

        private readonly ClientSystemRepository _clientRepository = new ClientSystemRepository();

        public List<ClientRepo> GetAllClients()
        {
            return _clientRepository.GetAllClients();
        }

        public void AddClient(ClientRepo client)
        {
            _clientRepository.AddClient(client);
        }

        public void AddAddress(Address address, int clientId)
        {
            address.ClientID = clientId;
            _clientRepository.AddAddress(address, clientId);
        }

        public List<Address> GetAddressesByClientId(int clientId)
        {
            return _clientRepository.GetAddressesByClientId(clientId);
        }

        public void AddContactInfo(ContactInfo contactInfo, int clientId)
        {
            contactInfo.ClientID = clientId;
            _clientRepository.AddContactInfo(contactInfo, clientId);
        }

        public List<ContactInfo> GetContactInfoByClientId(int clientId)
        {
            return _clientRepository.GetContactInfoByClientId(clientId);
        }

        public List<ClientWithAddresses> GetAllClientsWithAddresses()
        {
            return _clientRepository.GetAllClientsWithAddresses();
        }

        public ClientRepo GetClientById(int clientId)
        {
            return _clientRepository.GetClientById(clientId); 
        }

        public void UpdateClient(ClientRepo clientRepo, int clientId)
        {
            clientRepo.ClientID = clientId;
            _clientRepository.UpdateClient(clientId,clientRepo);
        }


        public void DeleteAddress(int addressId, int clientId)
        {
            _clientRepository.DeleteAddress(addressId, clientId);
        }

        public void UpdateAddress(Address address)
        {
            _clientRepository.UpdateAddress(address);
        }

        public ClientWithContactsAndAddresses GetClientDetailsWithContactsAndAddresses(int clientId)
        {
            return _clientRepository.GetClientDetailsWithContactsAndAddresses(clientId);
        }

        public void DeleteClient(int clientId)
        {
            _clientRepository.DeleteClient(clientId);
        }

    }

}

