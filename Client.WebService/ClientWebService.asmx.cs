using Client.BLL;
using Client.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Client.WebService
{
    /// <summary>
    /// Summary description for ClientWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ClientWebService : System.Web.Services.WebService
    {
        private readonly ClientSystemService _clientService = new ClientSystemService();

        [WebMethod]
        public List<ClientRepo> GetAllClients()
        {
            return _clientService.GetAllClients();
        }

        [WebMethod]
        public void AddClient(ClientRepo client)
        {
            _clientService.AddClient(client);
        }

    

        [WebMethod]
        public List<Address> GetAddressesByClientId(int clientId)
        {
            return _clientService.GetAddressesByClientId(clientId);
        }

        [WebMethod]
        public void AddAddress(Address address, int clientId)
        {
            _clientService.AddAddress(address, clientId); 
        }

        [WebMethod]
        public void AddContactInfo(ContactInfo contactInfo, int clientId)
        {
            _clientService.AddContactInfo(contactInfo, clientId); 
        }

        [WebMethod]
        public List<ContactInfo> GetContactInfoByClientId(int clientId)
        {
            return _clientService.GetContactInfoByClientId(clientId);
        }

        [WebMethod]
        public List<ClientWithAddresses> GetAllClientsWithAddresses()
        {
            return _clientService.GetAllClientsWithAddresses();
        }

        [WebMethod]
        public ClientRepo GetClientById(int clientId)
        {
            return _clientService.GetClientById(clientId); 
        }

        [WebMethod]
        public void UpdateClient(ClientRepo clientRepo, int clientId)
        {
            _clientService.UpdateClient(clientRepo, clientId); 
        }


        [WebMethod]
        public void DeleteAddress(int addressId, int clientId)
        {
            _clientService.DeleteAddress(addressId, clientId);
        }

        [WebMethod]
        public void UpdateAddress(Address address)
        {
            _clientService.UpdateAddress(address);
        }

        [WebMethod]
        public ClientWithContactsAndAddresses GetClientDetailsWithContactsAndAddresses(int clientId)
        {
            return _clientService.GetClientDetailsWithContactsAndAddresses(clientId);
        }

        [WebMethod]
        public void DeleteClient(int clientId)
        {
            _clientService.DeleteClient(clientId);
        }


    }
}
