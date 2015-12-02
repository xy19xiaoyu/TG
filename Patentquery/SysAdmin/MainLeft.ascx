<%@ Control Language="C#" AutoEventWireup="true" Inherits="MainLeft" Codebehind="MainLeft.ascx.cs" %>
<asp:TreeView ID="tTV" runat="server" ImageSet="XPFileExplorer" NodeIndent="15">
    <ParentNodeStyle Font-Bold="False" />
    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
    <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
        VerticalPadding="0px" />
    <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
        NodeSpacing="0px" VerticalPadding="2px" />
</asp:TreeView>
