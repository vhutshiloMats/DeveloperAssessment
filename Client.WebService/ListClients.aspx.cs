using Client.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Client.WebService
{
    public partial class AddClient : System.Web.UI.Page
    {
        ClientWebService clientService = new ClientWebService();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindClientGrid();

            }
        }

      

        protected void btnAddAddress_Click(object sender, EventArgs e)
        {
            
            int clientId = int.Parse(((Button)sender).CommandArgument);
            Response.Redirect($"AddAddress.aspx?ClientID={clientId}");
        }

        protected void btnAddContactInfo_Click(object sender, EventArgs e)
        {
            int clientId = int.Parse(((Button)sender).CommandArgument);
            Response.Redirect($"AddContactInfo.aspx?ClientID={clientId}");
        }

        protected void btnAddNewClient_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewClient.aspx");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            
            ClientWebService clientWebService = new ClientWebService();

            var clientsWithAddresses = clientWebService.GetAllClientsWithAddresses();
            Response.Clear();
            Response.ContentType = "text/plain"; 
            Response.AddHeader("Content-Disposition", "attachment;filename=ClientsWithAddresses.txt"); 

          
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ClientID,ClientName,AddressLine,City,PostalCode");

         
            foreach (var client in clientsWithAddresses)
            {
                foreach (var address in client.Addresses)
                {
                    
                    sb.AppendLine($"{client.ClientID},{client.Name},{address.AddressLine},{address.City},{address.PostalCode}");
                }
            }

            
            Response.Write(sb.ToString());
            Response.End(); 
        }


        protected void Edit_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;

            int clientId = int.Parse(linkButton.CommandArgument);
        
            Response.Redirect($"UpdateClient.aspx?ClientID={clientId}");
        }

        protected void View_Click(object sender, EventArgs e)
        {
            LinkButton btnView = (LinkButton)sender;
            string clientId = btnView.CommandArgument;
            Response.Redirect($"ViewClientDetails.aspx?ClientID={clientId}");
        }


        protected void Delete_Click(object sender, EventArgs e)
        {
            LinkButton deleteButton = (LinkButton)sender;
            int clientId = Convert.ToInt32(deleteButton.CommandArgument);
            clientService.DeleteClient(clientId);

         
            BindClientGrid();
        }

        private void BindClientGrid(string searchTerm = "")
        {
            List<ClientRepo> clients = clientService.GetAllClients();
            List<ClientRepo> filteredClients = new List<ClientRepo>();

            // Manually filter using a loop
            if (!string.IsNullOrEmpty(searchTerm))
            {
                foreach (var client in clients)
                {
                    if (client.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        filteredClients.Add(client);
                    }
                }
            }
            else
            {
                filteredClients = clients; 
            }

            GridViewClients.DataSource = filteredClients;
            GridViewClients.DataBind();
            lblRecordCount.Text = $"Total Records: {filteredClients.Count}";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            string searchTerm = txtSearch.Text.Trim();
            BindClientGrid(searchTerm);
        }












    }

}
