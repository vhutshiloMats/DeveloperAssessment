using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DAL
{
    public class ClientSystemRepository
    {

    
        private string connectionString = ConfigurationManager.ConnectionStrings["ClientDB"].ConnectionString;

        // Get all clients
        public List<ClientRepo> GetAllClients()
        {
            List<ClientRepo> clients = new List<ClientRepo>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Clients", conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    clients.Add(new ClientRepo
                    {
                        ClientID = (int)reader["ClientID"],
                        Name = reader["Name"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Age = (int)reader["Age"]
                    });
                }
            }
            return clients;
        }

     
        public void AddClient(ClientRepo client)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Clients (Name, Gender, Age) VALUES (@Name, @Gender, @Age)", conn);
                cmd.Parameters.AddWithValue("@Name", client.Name);
                cmd.Parameters.AddWithValue("@Gender", client.Gender);
                cmd.Parameters.AddWithValue("@Age", client.Age);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public ClientRepo GetClientById(int clientId)
        {
            ClientRepo client = null; 

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT Name, Gender, Age FROM Clients WHERE ClientID = @ClientID", conn);
                cmd.Parameters.AddWithValue("@ClientID", clientId);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) 
                    {
                        client = new ClientRepo
                        {
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Age = Convert.ToInt32(reader["Age"])
                        };
                    }
                }
            }

            return client; 
        }

        public void UpdateClient(int clientId, ClientRepo client)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Clients SET Name = @Name, Gender = @Gender, Age = @Age WHERE ClientID = @ClientID", conn);
                    cmd.Parameters.AddWithValue("@Name", client.Name);
                    cmd.Parameters.AddWithValue("@Gender", client.Gender);
                    cmd.Parameters.AddWithValue("@Age", client.Age);
                    cmd.Parameters.AddWithValue("@ClientID", clientId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Handle exception (log it, rethrow it, or display a message)
                throw new Exception("An error occurred while updating the client details.", ex);
            }
        }




        public void AddContactInfo(ContactInfo contactInfo, int clientId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO ContactInfo (ContactType, ContactNumber, ClientID) VALUES (@ContactType, @ContactNumber, @ClientID)", conn);
                cmd.Parameters.AddWithValue("@ContactType", contactInfo.ContactType);
                cmd.Parameters.AddWithValue("@ContactNumber", contactInfo.ContactNumber);
                cmd.Parameters.AddWithValue("@ClientID", clientId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddAddress(Address address, int clientId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Addresses (ClientID, AddressLine, City, PostalCode, AddressType) VALUES (@ClientID, @AddressLine, @City, @PostalCode, @AddressType)", conn);
                cmd.Parameters.AddWithValue("@ClientID", clientId);
                cmd.Parameters.AddWithValue("@AddressLine", address.AddressLine);
                cmd.Parameters.AddWithValue("@City", address.City);
                cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                cmd.Parameters.AddWithValue("@AddressType", address.AddressType);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public ClientWithContactsAndAddresses GetClientDetailsWithContactsAndAddresses(int clientId)
        {
            ClientWithContactsAndAddresses clientDetails = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT c.ClientID, c.Name, c.Gender, c.Age,
                   a.AddressID, a.AddressLine, a.City, a.PostalCode, a.AddressType,
                   ci.ContactID, ci.ContactType, ci.ContactNumber
            FROM Clients c
            LEFT JOIN Addresses a ON c.ClientID = a.ClientID
            LEFT JOIN ContactInfo ci ON c.ClientID = ci.ClientID
            WHERE c.ClientID = @ClientID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ClientID", clientId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (clientDetails == null)
                    {
                        clientDetails = new ClientWithContactsAndAddresses
                        {
                            ClientID = (int)reader["ClientID"],
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Age = (int)reader["Age"],
                            Addresses = new List<Address>(),
                            ContactInfos = new List<ContactInfo>()
                        };
                    }

                    // Add address details if present
                    if (!reader.IsDBNull(reader.GetOrdinal("AddressID")))
                    {
                        clientDetails.Addresses.Add(new Address
                        {
                            AddressID = (int)reader["AddressID"],
                            AddressLine = reader["AddressLine"].ToString(),
                            City = reader["City"].ToString(),
                            PostalCode = reader["PostalCode"].ToString(),
                            AddressType = reader["AddressType"].ToString(),
                            ClientID = (int)reader["ClientID"]
                        });
                    }

                    // Add contact info details if present
                    if (!reader.IsDBNull(reader.GetOrdinal("ContactID")))
                    {
                        clientDetails.ContactInfos.Add(new ContactInfo
                        {
                            ContactID = (int)reader["ContactID"],
                            ContactType = reader["ContactType"].ToString(),
                            ContactNumber = reader["ContactNumber"].ToString(),
                            ClientID = (int)reader["ClientID"]
                        });
                    }
                }
            }

            return clientDetails;
        }


        // Get addresses for a specific client
        public List<Address> GetAddressesByClientId(int clientId)
        {
            List<Address> addresses = new List<Address>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Addresses WHERE ClientID = @ClientID", conn);
                cmd.Parameters.AddWithValue("@ClientID", clientId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    addresses.Add(new Address
                    {
                        AddressID = (int)reader["AddressID"],
                        ClientID = (int)reader["ClientID"],
                        AddressLine = reader["AddressLine"].ToString(),
                        City = reader["City"].ToString(),
                        PostalCode = reader["PostalCode"].ToString(),
                        AddressType = reader["AddressType"].ToString()
                    });
                }
            }
            return addresses;
        }

    
        public List<ContactInfo> GetContactInfoByClientId(int clientId)
        {
            List<ContactInfo> contactInfo = new List<ContactInfo>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ContactInfo WHERE ClientID = @ClientID", conn);
                cmd.Parameters.AddWithValue("@ClientID", clientId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    contactInfo.Add(new ContactInfo
                    {
                        ContactID = (int)reader["ContactID"],
                        ClientID = (int)reader["ClientID"],
                        ContactNumber = reader["ContactNumber"].ToString(),
                        ContactType = reader["ContactType"].ToString()
                        
                    });
                }
            }
            return contactInfo;
        }

        public List<ClientWithAddresses> GetAllClientsWithAddresses()
        {
            List<ClientWithAddresses> clientsWithAddresses = new List<ClientWithAddresses>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT c.ClientID, c.Name, c.Gender, c.Age, 
                           a.AddressID, a.AddressLine, a.City, a.PostalCode, a.AddressType
                    FROM Clients c
                    LEFT JOIN Addresses a ON c.ClientID = a.ClientID";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    ClientWithAddresses client = clientsWithAddresses
                        .FirstOrDefault(cl => cl.ClientID == (int)reader["ClientID"]);

                    if (client == null)
                    {
                        client = new ClientWithAddresses
                        {
                            ClientID = (int)reader["ClientID"],
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Age = (int)reader["Age"],
                            Addresses = new List<Address>() 
                        };
                        clientsWithAddresses.Add(client);
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("AddressID")))
                    {
                        client.Addresses.Add(new Address
                        {
                            AddressID = (int)reader["AddressID"],
                            AddressLine = reader["AddressLine"].ToString(),
                            City = reader["City"].ToString(),
                            PostalCode = reader["PostalCode"].ToString(),
                            AddressType = reader["AddressType"].ToString(),
                            ClientID = (int)reader["ClientID"]
                        });
                    }
                }
            }

            return clientsWithAddresses;
        }


        public void DeleteAddress(int addressId, int clientId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Addresses WHERE AddressID = @AddressID AND ClientID = @ClientID", conn);

                cmd.Parameters.AddWithValue("@AddressID", addressId);
                cmd.Parameters.AddWithValue("@ClientID", clientId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateAddress(Address address)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Addresses SET AddressLine = @AddressLine, City = @City, PostalCode = @PostalCode, AddressType = @AddressType WHERE AddressID = @AddressID", conn);

                cmd.Parameters.AddWithValue("@AddressLine", address.AddressLine);
                cmd.Parameters.AddWithValue("@City", address.City);
                cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                cmd.Parameters.AddWithValue("@AddressType", address.AddressType);
                cmd.Parameters.AddWithValue("@AddressID", address.AddressID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteContactInfo(int contactId, int clientId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM ContactInfo WHERE ContactID = @ContactID AND ClientID = @ClientID", conn);
                cmd.Parameters.AddWithValue("@ContactID", contactId);
                cmd.Parameters.AddWithValue("@ClientID", clientId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteClient(int clientId)
        {
            // First, delete all associated addresses
            var addresses = GetAddressesByClientId(clientId);
            foreach (var address in addresses)
            {
                DeleteAddress(address.AddressID, clientId);
            }

            // Then, delete all associated contact information
            var contactInfos = GetContactInfoByClientId(clientId);
            foreach (var contact in contactInfos)
            {
                DeleteContactInfo(contact.ContactID, clientId);
            }

            // Finally, delete the client
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Clients WHERE ClientID = @ClientID", conn);
                cmd.Parameters.AddWithValue("@ClientID", clientId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }




}

#region properties
public class ClientRepo
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }


    public class Address
    {
        public int AddressID { get; set; }
        public string AddressType { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int ClientID { get; set; }

    }

    public class ContactInfo
    {
        public int ContactID { get; set; }
        public string ContactType { get; set; }
        public string ContactNumber { get; set; }
        public int ClientID { get; set; }

    }

    public class ClientWithAddresses
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public List<Address> Addresses { get; set; } 
    }

public class ClientWithContactsAndAddresses
{
    public int ClientID { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public List<Address> Addresses { get; set; }
    public List<ContactInfo> ContactInfos { get; set; }
}

#endregion


