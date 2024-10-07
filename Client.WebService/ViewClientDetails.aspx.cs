using Client.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client.WebService
{
    public partial class ViewClientDetails : System.Web.UI.Page
    {
        private readonly ClientSystemService _clientService = new ClientSystemService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int clientId = Convert.ToInt32(Request.QueryString["ClientID"]);
              
                LoadClientDetails(clientId);
            }
        }

        private void LoadClientDetails(int clientId)
        {
            var clientDetails = _clientService.GetClientDetailsWithContactsAndAddresses(clientId);

            if (clientDetails != null)
            {
                lblName.Text = clientDetails.Name;
                lblGender.Text = clientDetails.Gender;
                lblAge.Text = clientDetails.Age.ToString();

                // Load addresses
                rptAddresses.DataSource = clientDetails.Addresses;
                rptAddresses.DataBind();

                // Check for addresses
                if (!clientDetails.Addresses.Any())
                {
                    lblNoAddresses.Visible = true; // Show no address message
                }
                else
                {
                    lblNoAddresses.Visible = false; // Hide message if addresses exist
                }

                // Load contact information
                rptContactInfos.DataSource = clientDetails.ContactInfos;
                rptContactInfos.DataBind();

                // Check for contact information
                if (!clientDetails.ContactInfos.Any())
                {
                    lblNoContacts.Visible = true; // Show no contact message
                }
                else
                {
                    lblNoContacts.Visible = false; // Hide message if contacts exist
                }
            }
            else
            {
                lblName.Text = "Client not found.";
                lblGender.Text = string.Empty;
                lblAge.Text = string.Empty;
                lblNoAddresses.Visible = false; // Hide address message
                lblNoContacts.Visible = false; // Hide contact message
            }
        }

    }
}