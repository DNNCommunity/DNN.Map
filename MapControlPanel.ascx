<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ControlPanel.ascx.vb" Inherits="DotNetNuke.Modules.Map.Controls.MapControlPanel" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:Label CssClass="Normal" id="lblInstructions" runat="server"></asp:Label>
<table align=center style='width: 546px;' border="0" cellpadding="0" cellspacing="0" id="ctrlPanelTable" runat="server">
	<tr>
		<td align="center">
			<asp:ImageButton id="imgButton_General" runat="server"></asp:ImageButton><br/>
			<asp:LinkButton id="lnk_General" CssClass="CommandButton" runat="server"></asp:LinkButton></td>
		<td align="center">
			<asp:ImageButton id="imgButton_Interface" runat="server"></asp:ImageButton><br/>
			<asp:LinkButton id="lnk_Interface" CssClass="CommandButton" runat="server"></asp:LinkButton></td>
		<td align="center">
			<asp:ImageButton id="imgButton_DataSource" runat="server"></asp:ImageButton><br/>
			<asp:LinkButton id="lnk_DataSource" CssClass="CommandButton" runat="server"></asp:LinkButton></td>
		<td align="center">
			<asp:ImageButton id="imgButton_Data" runat="server"></asp:ImageButton><br/>
			<asp:LinkButton id="lnk_Data" CssClass="CommandButton" runat="server"></asp:LinkButton></td>
		<td align="center">
			<asp:ImageButton id="imgButton_GeoLocator" runat="server"></asp:ImageButton><br/>
			<asp:LinkButton id="lnk_GeoLocator" CssClass="CommandButton" runat="server"></asp:LinkButton></td>
	</tr>
</table>
<center>