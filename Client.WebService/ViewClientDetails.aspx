<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewClientDetails.aspx.cs" Inherits="Client.WebService.ViewClientDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client Details</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: flex-start; 
            height: 100vh;
        }
        .container {
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            width: 90%;
            max-width: 800px;
            padding: 20px;
            margin: 20px auto;
            max-height: 80vh; 
            overflow: auto; 
        }
        .panel {
            border: 1px solid #ddd;
            border-radius: 4px;
            padding: 15px;
            margin-bottom: 15px;
        }
        h2, h3 {
            color: #333;
        }
        .back-button {
            display: inline-block;
            margin-bottom: 15px;
            padding: 8px 15px;
            font-size: 14px;
            color: #333;
            text-decoration: none;
            background-color: #eee;
            border: 1px solid #ccc;
            border-radius: 4px;
            cursor: pointer;
        }
        .back-button:hover {
            background-color: #ddd;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
            table-layout: fixed; /* Ensure equal column widths */
        }
        th, td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
            overflow: hidden; 
            text-overflow: ellipsis;
            white-space: nowrap; 
        }
        th {
            background-color: #f2f2f2;
        }
        tr:hover {
            background-color: #f1f1f1;
        }
        .no-data {
            color: #888;
            text-align: center;
            margin: 10px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <a href="javascript:history.back();" class="back-button">← Back</a>
            
            <h2>Client Details</h2>
            <div class="panel">
                <p><strong>Name:</strong> <asp:Label ID="lblName" runat="server"></asp:Label></p>
                <p><strong>Gender:</strong> <asp:Label ID="lblGender" runat="server"></asp:Label></p>
                <p><strong>Age:</strong> <asp:Label ID="lblAge" runat="server"></asp:Label></p>
            </div>

            <h3>Addresses</h3>
            <div class="panel">
                <asp:Label ID="lblNoAddresses" runat="server" CssClass="no-data" Visible="false">No address found.</asp:Label>
                <table>
                    <thead>
                        <tr>
                            <th>Address</th>
                            <th>City</th>
                            <th>Postal Code</th>
                            <th>Type</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptAddresses" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("AddressLine") %></td>
                                    <td><%# Eval("City") %></td>
                                    <td><%# Eval("PostalCode") %></td>
                                    <td><%# Eval("AddressType") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>

            <h3>Contact Information</h3>
            <div class="panel">
                <asp:Label ID="lblNoContacts" runat="server" CssClass="no-data" Visible="false">No contact information found.</asp:Label>
                <table>
                    <thead>
                        <tr>
                            <th>Contact Type</th>
                            <th>Contact Number</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptContactInfos" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("ContactType") %></td>
                                    <td><%# Eval("ContactNumber") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
