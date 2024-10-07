using Client.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client.WebService
{
    public partial class AddAddress : System.Web.UI.Page
    {
        ClientWebService clientService = new ClientWebService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                int clientId;
                if (int.TryParse(Request.QueryString["ClientID"], out clientId))
                {
                    HiddenClientID.Value = clientId.ToString();
                }
            }



        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            lblSuccessMessage.Visible = false;
            lblErrorMessage.Visible = false;

            if (string.IsNullOrEmpty(ddlAddressType.SelectedValue) || string.IsNullOrEmpty(txtAddressLine.Text) || string.IsNullOrEmpty(txtCity.Text) || string.IsNullOrEmpty(txtPostalCode.Text))
            {
                lblErrorMessage.Text = "Please fill in all fields.";
                lblErrorMessage.Visible = true;
                return;
            }
            var address = new Address
            {
                AddressType = ddlAddressType.SelectedValue,
                AddressLine = txtAddressLine.Text,
                City = txtCity.Text,
                PostalCode = txtPostalCode.Text,
                ClientID = int.Parse(HiddenClientID.Value) 
            };

            try
            {


                clientService.AddAddress(address, address.ClientID);
                lblSuccessMessage.Text = "Address information added successfully.";
                lblSuccessMessage.Visible = true;
                ClearForm();

            }

            catch (Exception ex)
            {
               
                lblErrorMessage.Text = "An error occurred while adding the contact information." + ex.Message;
                lblErrorMessage.Visible = true;


            }
        }

        private void ClearForm()
        {
            ddlAddressType.SelectedIndex = 0;
            txtAddressLine.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            
        }

    }
}