<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAddress.aspx.cs" Inherits="Client.WebService.AddAddress" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Address</title>
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
        .success-message {
            color: green;
            margin-top: 20px;
        }
        .error-message {
            color: red;
            margin-top: 20px;
        }
    </style>
    <script type="text/javascript">
        function validatePostalCode() {
            var postalCode = document.getElementById('<%= txtPostalCode.ClientID %>').value;
            if (!/^\d+$/.test(postalCode)) {
                alert("Postal code must be numeric.");
                return false;
            }
            return true;
        }

        function redirectToUpdatePage() {
            var clientId = document.getElementById('<%= HiddenClientID.ClientID %>').value;
                window.location.href = 'UpdateClient.aspx?ClientID=' + clientId;
            }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return validatePostalCode();">
        <div class="container mt-5">
            <h2>Add Address</h2>
            <div class="form-group">
                <label for="AddressType">Address Type:</label>
                <asp:DropDownList ID="ddlAddressType" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Address Type" Value="" />
                    <asp:ListItem Text="Residential Address" Value="Residential" />
                    <asp:ListItem Text="Postal Address" Value="Postal" />
                    <asp:ListItem Text="Work Address" Value="Work" />
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="AddressLine">Address Line:</label>
                <asp:TextBox ID="txtAddressLine" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="City">City:</label>
                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="PostalCode">Postal Code:</label>
                <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" TextMode="Number" />
            </div>
            <asp:HiddenField ID="HiddenClientID" runat="server" />
            <asp:Button ID="btnSubmit" runat="server" Text="Save Address" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-secondary" OnClientClick="redirectToUpdatePage(); return false;" />

           
            <div class="mt-3">
                <asp:Label ID="lblSuccessMessage" runat="server" CssClass="success-message" Visible="false"></asp:Label>
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
