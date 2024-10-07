using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client.WebService
{
    public partial class UpdateClient : System.Web.UI.Page
    {
        ClientWebService clientService = new ClientWebService();
        int clientId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int clientId;
                if (int.TryParse(Request.QueryString["ClientID"], out clientId))
                {
                    hdnClientID.Value = clientId.ToString();
                    ViewState["ClientID"] = clientId;
                }

                ClientRepo client = clientService.GetClientById(clientId);
                if (client != null)
                {
                    txtClientName.Text = client.Name;
                    txtAge.Text = client.Age.ToString();
                    ddlGender.SelectedValue = client.Gender;
                }

                BindAddresses();
                BindContactInfo();
            }
        }

        //protected void btnSaveDetails_Click(object sender, EventArgs e)
        //{
        //    lblSuccessMessage.Visible = false;
        //    lblErrorMessage.Visible = false;

        //    try
        //    {
        //        // Retrieve the client details from the form fields
        //        string name = txtClientName.Text.Trim();
        //        string gender = ddlGender.SelectedValue;
        //        int age = int.Parse(txtAge.Text);

        //        // Assume you have the clientId from a hidden field or query string
        //        int clientId = int.Parse(Request.QueryString["ClientID"]);

        //        // Create a ClientRepo object with the updated values
        //        ClientRepo updatedClient = new ClientRepo
        //        {
        //            Name = name,
        //            Gender = gender,
        //            Age = age
        //        };

        //      // Assuming you have a service instance
        //        clientService.UpdateClient(updatedClient, clientId); // Call your service method

        //        // Display success message
        //        lblSuccessMessage.Text = "Client details updated successfully.";
        //        lblSuccessMessage.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions that occur during the save operation
        //        lblErrorMessage.Text = "An error occurred while updating the client details: " + ex.Message;
        //        lblErrorMessage.Visible = true;
        //    }
        //}

        protected void btnSaveDetails_Click(object sender, EventArgs e)
        {
            lblSuccessMessage.Visible = false;
            lblErrorMessage.Visible = false;

            try
            {
                string name = txtClientName.Text.Trim();
                string gender = ddlGender.SelectedValue;
                int age = int.Parse(txtAge.Text);

                // Log the values being retrieved
                Console.WriteLine("Client Name: " + name);
                Console.WriteLine("Gender: " + gender);
                Console.WriteLine("Age: " + age);

                int clientId = int.Parse(Request.QueryString["ClientID"]);  // Log clientId value
                Console.WriteLine("ClientID: " + clientId);

                ClientRepo updatedClient = new ClientRepo
                {
                    Name = name,
                    Gender = gender,
                    Age = age
                };

                // Log before calling the service
                Console.WriteLine("Calling clientService.UpdateClient...");
                clientService.UpdateClient(updatedClient, clientId);

                lblSuccessMessage.Text = "Client details updated successfully.";
                lblSuccessMessage.Visible = true;
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine("Exception: " + ex.Message);
                lblErrorMessage.Text = "An error occurred while updating the client details: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }



        //protected void btnAddAddress_Click(object sender, EventArgs e)
        //{

        //    lblSuccessMessage.Visible = false;
        //    lblErrorMessage.Visible = false;

        //    if (string.IsNullOrEmpty(ddlAddressType.SelectedValue) || string.IsNullOrEmpty(txtAddressLine.Text) || string.IsNullOrEmpty(txtCity.Text) || string.IsNullOrEmpty(txtPostalCode.Text))
        //    {
        //        lblErrorMessage.Text = "Please fill in all fields.";
        //        lblErrorMessage.Visible = true;
        //        return;
        //    }
        //    var address = new Address
        //    {
        //        AddressType = ddlAddressType.SelectedValue,
        //        AddressLine = txtAddressLine.Text,
        //        City = txtCity.Text,
        //        PostalCode = txtPostalCode.Text,
        //        ClientID = int.Parse(hdnClientID.Value)
        //    };

        //    try
        //    {


        //        clientService.AddAddress(address, address.ClientID);
        //        lblSuccessMessage.Text = "Address information added successfully.";
        //        lblSuccessMessage.Visible = true;


        //    }

        //    catch (Exception ex)
        //    {

        //        lblErrorMessage.Text = "An error occurred while adding the contact information." + ex.Message;
        //        lblErrorMessage.Visible = true;


        //    }


        //}
        protected void btnAddAddress_Click(object sender, EventArgs e)
        {
            string clientId = Request.QueryString["ClientID"];
            Response.Redirect($"AddAddress.aspx?ClientID={clientId}");
        }

        protected void btnAddContactInfo_Click(object sender, EventArgs e)
        {
            string clientId = Request.QueryString["ClientID"];
            Response.Redirect($"AddContactInfo.aspx?ClientID={clientId}");
        }


        private void BindAddresses()
        {
            try
            {
             

                List<Address> addresses = clientService.GetAddressesByClientId(clientId);
                gvAddresses.DataSource = addresses;
                gvAddresses.DataBind();
                gvAddresses.Visible = addresses.Any();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred while loading the grid." + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }


        private void BindContactInfo()
        {
            try
            {


                List<ContactInfo> contactInfo = clientService.GetContactInfoByClientId(clientId);
                gvContacts.DataSource = contactInfo;
                gvContacts.DataBind();
                gvContacts.Visible = contactInfo.Any();
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred while loading the grid" + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }

        protected void gvAddresses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int addressId = Convert.ToInt32(gvAddresses.DataKeys[e.RowIndex].Value);

            int clientIdQuery = Convert.ToInt32(Request.QueryString["ClientID"]);

            clientService.DeleteAddress(addressId, clientIdQuery);

            BindAddresses(); 
        }

        protected void gvAddresses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAddresses.EditIndex = -1;
            BindAddresses(); 
        }


        protected void gvAddresses_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int addressId = Convert.ToInt32(gvAddresses.DataKeys[e.RowIndex].Value);

         
            GridViewRow row = gvAddresses.Rows[e.RowIndex];
            DropDownList ddlAddressType = (DropDownList)row.FindControl("ddlAddressType");
            TextBox txtAddressLine = (TextBox)row.FindControl("txtAddressLine");
            TextBox txtCity = (TextBox)row.FindControl("txtCity");
            TextBox txtPostalCode = (TextBox)row.FindControl("txtPostalCode");

          
            Address updatedAddress = new Address
            {
                AddressID = addressId,
                AddressType = ddlAddressType.SelectedValue,
                AddressLine = txtAddressLine.Text,
                City = txtCity.Text,
                PostalCode = txtPostalCode.Text
            };

            clientService.UpdateAddress(updatedAddress);

            gvAddresses.EditIndex = -1; 
            BindAddresses(); 
        }

        protected void gvAddresses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int rowIndex = e.NewEditIndex;
            gvAddresses.EditIndex = rowIndex;

           // BindAddresses();

           
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListClients.aspx");
        }


    }
}