<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Google.Geolocator.Admin.ascx.vb" Inherits="DotNetNuke.Modules.Map.Geolocator.Google.GoogleAdmin" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table class="FileManager_Explorer" cellSpacing="0" cellPadding="2" border="0" width="500">
	<tr>
		<td class="FileManager_Header" align="center" colSpan="2"><asp:label id="lblPanelName" runat="server">Panel Name</asp:label></td>
	</tr>
	<tr>
		<td class="normal"><asp:Label CssClass="Normal" id="lblInstructions" runat="server"></asp:Label></td>
	</tr>
	<TR>
		<TD class="SubHead" colSpan="2"><asp:label id="lblAPIKey" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD class="SubHead" colSpan="2"><asp:textbox id="txtAPIKey" runat="server" Width="100%"></asp:textbox></TD>
	</TR>
	<TR>
		<TD class="SubHead" colSpan="2"><asp:checkbox id="chkUseCountry" runat="server"></asp:checkbox><br>
			<asp:CheckBox id="chkUseRegion" runat="server"></asp:CheckBox><br>
			<asp:CheckBox id="chkUsePostalCode" runat="server"></asp:CheckBox><br>
			<asp:CheckBox id="chkUseCity" runat="server"></asp:CheckBox><br>
			<asp:CheckBox id="chkUseStreet" runat="server"></asp:CheckBox><br>
			<asp:CheckBox id="chkUseUnit" runat="server"></asp:CheckBox></TD>
	</TR>
	<tr>
		<td align="center" colSpan="2" class="FileManager_Pager"><asp:linkbutton id="lnkSave" runat="server" CssClass="CommandButton"></asp:linkbutton>|
			<asp:linkbutton id="lnkCancel" runat="server" CssClass="CommandButton"></asp:linkbutton></td>
	</tr>
</table>
