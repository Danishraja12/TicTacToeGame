<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="TicTacToe.aspx.cs" Inherits="TicTacToe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tic Tac Toe</title>
    <style>
        .cell {
            width: 50px;
            height: 50px;
            font-size: 24px;
        }
        .current-player {
            font-size: 18px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblSize" runat="server" Text="Enter board size (3-10):"></asp:Label>
            <asp:TextBox ID="txtSize" runat="server" placeholder="Enter board size" type="number" min="3" max="10"></asp:TextBox>
            <asp:Button ID="btnStart" runat="server" Text="Start Game" OnClick="btnStart_Click" />
            <br /><br />
            <asp:Panel ID="boardContainer" runat="server"></asp:Panel>
            <br />
            <asp:Label ID="lblGameStatus" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblCurrentPlayer" runat="server" CssClass="current-player" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>

