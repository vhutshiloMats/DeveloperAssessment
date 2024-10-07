using Client.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client.WebService
{
    public partial class AddNewClient : System.Web.UI.Page
    {
        ClientWebService clientService = new ClientWebService();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblSuccessMessage.Visible = false;
            lblErrorMessage.Visible = false;

            var clientRepo = new ClientRepo
            {
                Name = txtName.Text,
                Gender = ddlGender.SelectedValue,
                Age = int.Parse(txtAge.Text)
            };

            try
            {
                clientService.AddClient(clientRepo);

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
            ddlGender.SelectedIndex = 0;
            txtName.Text = string.Empty;
            txtAge.Text = string.Empty;
        }
    }
}
