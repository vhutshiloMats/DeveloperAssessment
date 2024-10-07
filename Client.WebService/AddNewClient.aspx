<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewClient.aspx.cs" Inherits="Client.WebService.AddNewClient" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Client</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        .container {
            margin: 0 auto;
            width: 50%;
            background-color: #fff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
        }
        h2 {
            color: #333;
        }
        .success-message {
            color: green;
            margin-top: 20px;
        }
        .error-message {
            color: red;
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2>Add New Client</h2>
            <div class="form-group">
                <label for="txtName">Client Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="ddlGender">Gender</label>
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                    <asp:ListItem Value="" Text="Select Gender" />
                    <asp:ListItem Value="Male" Text="Male" />
                    <asp:ListItem Value="Female" Text="Female" />
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="txtAge">Age</label>
                <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" TextMode="Number" />
            </div>

            <asp:Button ID="btnSubmit" runat="server" Text="Add Client" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" 
                OnClientClick="window.location='ListClients.aspx'; return false;" />

           
            <div class="mt-3">
                <asp:Label ID="lblSuccessMessage" runat="server" CssClass="success-message" Visible="false"></asp:Label>
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
