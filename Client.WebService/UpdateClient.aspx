<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateClient.aspx.cs" Inherits="Client.WebService.UpdateClient" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Client</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            padding: 20px;
            background-color: #f8f9fa;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }
        .section {
            margin-bottom: 30px;
            padding: 15px;
            background-color: #ffffff;
            border-radius: 5px;
            box-shadow: 0 1px 3px rgba(0, 0, 0, 0.2);
        }
       
        h2 {
    color: #333;
    font-size: 1.5rem; 
    margin-bottom: 10px; 
    padding: 5px; 
}

        .btn {
            margin-top: 10px;
        }
        .error-message {
            color: red;
        }
        .success-message {
            color: green;
        }

        .table {
            margin-top: 20px;
        }
        .form-control {
            width: 100%;
        }
        .btn-sm {
            padding: 5px 10px;
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnClientID" runat="server" Value='<%= Request.QueryString["ClientID"] %>' />
         <div class="container mt-3">
            <asp:Button ID="btnBack" runat="server" Text="Back to List Clients" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
        </div>
        <div class="mt-3">
            <asp:Label ID="lblSuccessMessage" runat="server" CssClass="success-message" Visible="false"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
        </div>
        <div class="container mt-5">
        
            <div class="section">
                <h2>Update Client Details</h2>
                <div class="form-group">
                    <label for="ClientName">Client Name:</label>
                    <asp:TextBox ID="txtClientName" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="Age">Age:</label>
                    <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" />
                </div>
                <label for="Gender">Gender:</label>
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Gender" Value="" />
                    <asp:ListItem Text="Female" Value="Female" />
                    <asp:ListItem Text="Male" Value="Male" />
                </asp:DropDownList>

                <div class="mt-4">
                    <asp:Button ID="btnSubmitDetails" runat="server" Text="Update Client Details" CssClass="btn btn-primary" OnClick="btnSaveDetails_Click" />
                </div>
            </div>

            <div class="section mt-5">
                <h2>Addresses</h2>
                
              
                <div>
                    <asp:Button ID="btnAddAddress" runat="server" Text="Add Address" CssClass="btn btn-success" OnClick="btnAddAddress_Click" />
                </div>
                
                <asp:Panel ID="pnlAddresses" runat="server" CssClass="container">
                    <asp:GridView ID="gvAddresses" runat="server" AutoGenerateColumns="False" DataKeyNames="AddressID"
                                  CssClass="table table-bordered table-striped table-hover"
                                  OnRowEditing="gvAddresses_RowEditing" OnRowUpdating="gvAddresses_RowUpdating" OnRowCancelingEdit="gvAddresses_RowCancelingEdit" OnRowDeleting="gvAddresses_RowDeleting">
                        <Columns>
                         
                            <asp:TemplateField HeaderText="Address Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddressType" runat="server" Text='<%# Eval("AddressType") %>' CssClass="form-control"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlAddressType" runat="server" SelectedValue='<%# Bind("AddressType") %>' CssClass="form-control">
                                        <asp:ListItem Text="Residential" Value="Residential" />
                                        <asp:ListItem Text="Work" Value="Work" />
                                        <asp:ListItem Text="Postal" Value="Postal" />
                                        <asp:ListItem Text="Other" Value="Other" />
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>

                          
                            <asp:TemplateField HeaderText="Address Line">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddressLine" runat="server" Text='<%# Eval("AddressLine") %>' CssClass="form-control"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAddressLine" runat="server" Text='<%# Bind("AddressLine") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                         
                            <asp:TemplateField HeaderText="City">
                                <ItemTemplate>
                                    <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>' CssClass="form-control"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCity" runat="server" Text='<%# Bind("City") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                          
                            <asp:TemplateField HeaderText="Postal Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostalCode" runat="server" Text='<%# Eval("PostalCode") %>' CssClass="form-control"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPostalCode" runat="server" Text='<%# Bind("PostalCode") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
            <div class="section mt-5">
                <h2>Contact Information</h2>
                
                <div>
                    <asp:Button ID="btnAddContact" runat="server" Text="Add Contact" CssClass="btn btn-success" onClick="btnAddContactInfo_Click" />
                </div>
                
                <asp:Panel ID="pnlContacts" runat="server" CssClass="container">
                    <asp:GridView ID="gvContacts" runat="server" AutoGenerateColumns="False" DataKeyNames="ContactID"
                                  CssClass="table table-bordered table-striped table-hover">
                        <Columns>
                           
                            <asp:TemplateField HeaderText="Contact Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblContactType" runat="server" Text='<%# Eval("ContactType") %>' CssClass="form-control"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlContactType" runat="server" SelectedValue='<%# Bind("ContactType") %>' CssClass="form-control">
                                        <asp:ListItem Text="Cell" Value="Cell" />
                                        <asp:ListItem Text="Work" Value="Work" />
                                        <asp:ListItem Text="Home" Value="Home" />
                                        <asp:ListItem Text="Other" Value="Other" />
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhoneNumber" runat="server" Text='<%# Eval("ContactNumber") %>' CssClass="form-control"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtContactNumber" runat="server" Text='<%# Bind("ContactNumber") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
