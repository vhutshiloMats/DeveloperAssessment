<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddContactInfo.aspx.cs" Inherits="Client.WebService.AddContactInfo" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Contact Info</title>
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
        function validateContactNumber(event) {
            const input = event.target;
            const value = input.value;


            input.value = value.replace(/\D/g, '');


            if (input.value.length > 10) {
                input.value = input.value.slice(0, 10);
            }
        }

        function redirectToUpdatePage() {
            var clientId = document.getElementById('<%= hdnClientID.ClientID %>').value;
            window.location.href = 'UpdateClient.aspx?ClientID=' + clientId;
         }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return validatePostalCode();">
        <div class="container mt-5">
            <h2>Add Contact</h2>
            <div class="form-group">
                <label for="ContactType">Contact Type:</label>
                <asp:DropDownList ID="ddlContactType" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Type" Value="" />
                    <asp:ListItem Text="Cell Number" Value="Cell" />
                    <asp:ListItem Text="Work Number" Value="Work" />
                    <asp:ListItem Text="Home Number" Value="Home" />
                    <asp:ListItem Text="Other" Value="Other" />
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="ContactNumber">Contact Number:</label>
                <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" 
                             oninput="validateContactNumber(event)" MaxLength="10" />
                <span class="error-message" id="errorContactNumber" runat="server" visible="false">Please enter a valid 10-digit number.</span>
            </div>
        
      
            <asp:HiddenField ID="hdnClientID" runat="server" />
            <asp:Button ID="btnSubmit" runat="server" Text="Save Contact" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-secondary" 
            OnClientClick="redirectToUpdatePage(); return false;" />
            <div class="mt-3">
                <asp:Label ID="lblSuccessMessage" runat="server" CssClass="success-message" Visible="false"></asp:Label>
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>