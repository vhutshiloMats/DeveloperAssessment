using Client.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client.WebService
{
    public partial class AddContactInfo : System.Web.UI.Page
    {
        ClientWebService clientService = new ClientWebService();
        protected void Page_Load(object sender, EventArgs e)
        {
            int clientId;
            if (int.TryParse(Request.QueryString["ClientID"], out clientId))
            {
                hdnClientID.Value = clientId.ToString();
            }

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblSuccessMessage.Visible = false;
            lblErrorMessage.Visible = false;

          
            if (string.IsNullOrEmpty(ddlContactType.SelectedValue) || string.IsNullOrEmpty(txtContactNumber.Text))
            {
                lblErrorMessage.Text = "Please fill in all fields.";
                lblErrorMessage.Visible = true;
                return;
            }

            var contactInfo = new ContactInfo
            {
                ContactType = ddlContactType.SelectedValue,
                ContactNumber = txtContactNumber.Text,
                ClientID = int.Parse(hdnClientID.Value) 
            };

            try
            {
               
                clientService.AddContactInfo(contactInfo, contactInfo.ClientID);
                lblSuccessMessage.Text = "Contact information added successfully.";
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
            ddlContactType.SelectedIndex = 0;
            txtContactNumber.Text = string.Empty;
        }




    }
}