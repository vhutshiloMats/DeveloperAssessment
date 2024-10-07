<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListClients.aspx.cs" Inherits="Client.WebService.AddClient" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client List</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />

    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f0f0;
            text-align: center;
        }

        .container {
            margin: 0 auto;
            width: 80%;
            background-color: #fff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
        }

         h2 {
            color: #333;
            font-size: 20px;
            font-weight: bold; 
        }

        .gridview {
            width: 100%;
            margin: 20px auto;
            border-collapse: collapse;
        }

        .gridview th, .gridview td {
            padding: 12px;
            border: 1px solid #ddd;
            text-align: left;
        }

        .gridview th {
            background-color: #f2f2f2;
        }

        .action-buttons {
            margin: 5px;
        }

        .button-container {
            margin-bottom: 20px;
            text-align: left; 
        }

        .button-container .btn {
            margin-right: 10px; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Clients</h2>
            
           
      <div class="button-container">
   
    <asp:Button ID="btnAddNewClient" runat="server" Text="Add New Client" CssClass="btn btn-primary" OnClick="btnAddNewClient_Click"/>
    <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-secondary" OnClick="btnExport_Click"/>
    
   
    <div style="float: right; display: inline-block;">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search clients..." 
                     style="display: inline-block; width: auto; margin-right: 5px;"/>
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-secondary" OnClick="btnSearch_Click"/>
    </div>
</div>

     
            <div class="grid-container">
                <asp:GridView ID="GridViewClients" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                    <Columns>
                        <asp:BoundField DataField="ClientID" HeaderText="Client ID" />
                        <asp:BoundField DataField="Name" HeaderText="Client Name" />
                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                        <asp:BoundField DataField="Age" HeaderText="Age" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="action-buttons">

               <asp:LinkButton ID="View" runat="server" CommandArgument='<%# Eval("ClientID") %>' 
                            OnClick="View_Click" CssClass="btn btn-info">
                <i class="fas fa-eye"></i> View
            </asp:LinkButton>

              <asp:LinkButton ID="Edit" runat="server" CommandArgument='<%# Eval("ClientID") %>' 
                    OnClick="Edit_Click" CssClass="btn btn-success">
               <i class="fas fa-edit"></i> Edit
          </asp:LinkButton>

               

                <asp:LinkButton ID="Delete" runat="server" CommandArgument='<%# Eval("ClientID") %>' 
                OnClick="Delete_Click" CssClass="btn btn-danger" 
                OnClientClick="return confirm('Are you sure you want to delete this client?');">
                <i class="fas fa-trash-alt"></i> Delete
            </asp:LinkButton>

                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
               <div class="record-count">
                <asp:Label ID="lblRecordCount" runat="server" Text="Total Records: "></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
