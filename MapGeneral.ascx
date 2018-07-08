<%@ Control Language="vb" AutoEventWireup="false" Codebehind="General.ascx.vb" Inherits="DotNetNuke.Modules.Map.Controls.MapGeneral" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table class="FileManager_Explorer" cellSpacing="0" cellPadding="2" align="center" border="0" width=500>
	<tr>
		<td class="FileManager_Header" align="center" colSpan="2"><asp:label id="lblPanelName" runat="server">Panel Name</asp:label></td>
	</tr>
	<tr>
		<td class="normal" colSpan="2"><asp:label id="lblInstructions" runat="server" CssClass="Normal"></asp:label></td>
	</tr>
	<tr>
		<td class="SubHead" width="165"><asp:label id="lblMapInfo" runat="server"></asp:label></td>
		<td class="SubHead"><asp:radiobutton id="rdoUseExisting" runat="server" GroupName="NewExisting" AutoPostBack="True"></asp:radiobutton><asp:radiobutton id="rdoCreateNew" runat="server" GroupName="NewExisting" AutoPostBack="True" Checked="True"></asp:radiobutton></td>
	</tr>
	<tr>
		<td class="SubHead" style="HEIGHT: 18px" width="165"><asp:label id="lblExisting" runat="server" Visible="False"></asp:label></td>
		<td style="HEIGHT: 18px"><asp:dropdownlist id="ddlExisting" runat="server" AutoPostBack="True" Visible="False" width="300"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td class="SubHead" width="165"><asp:label id="lblName" runat="server"></asp:label></td>
		<td><asp:textbox id="txtName" runat="server" width="300" maxlength="100" cssclass="NormalTextBox"></asp:textbox></td>
	</tr>
	<tr>
		<td class="SubHead" width="165"><asp:label id="lblDescription" runat="server"></asp:label></td>
		<td><asp:textbox id="txtDescription" runat="server" width="300" maxlength="500" cssclass="NormalTextBox"
				rows="3" textmode="MultiLine"></asp:textbox></td>
	</tr>
	<tr>
		<td class="SubHead" width="165"><asp:label id="lblDynamicProvider" runat="server"></asp:label></td>
		<td><asp:dropdownlist id="ddlVisualProvider" runat="server" width="300"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td class="FileManager_Header" align="center" colSpan="2"><asp:label id="lblPanelNameSource" runat="server">Panel Name</asp:label></td>
	</tr>
	<tr>
		<td class="SubHead" width="165"><asp:label id="lblSourceInfo" runat="server"></asp:label></td>
		<td class="SubHead"><asp:radiobutton id="rdoUseExistingSource" runat="server" GroupName="NewExistingSource" AutoPostBack="True"></asp:radiobutton><asp:radiobutton id="rdoCreateNewSource" runat="server" GroupName="NewExistingSource" AutoPostBack="True"
				Checked="True"></asp:radiobutton></td>
	</tr>
	<tr>
		<td class="SubHead" style="HEIGHT: 18px" width="165"><asp:label id="lblExistingSource" runat="server" Visible="False"></asp:label></td>
		<td style="HEIGHT: 18px"><asp:dropdownlist id="ddlExistingSource" runat="server" AutoPostBack="True" Visible="False" width="300"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td class="SubHead" width="165"><asp:label id="lblNameSource" runat="server"></asp:label></td>
		<td><asp:textbox id="txtNameSource" runat="server" width="300" maxlength="100" cssclass="NormalTextBox"></asp:textbox></td>
	</tr>
	<tr>
		<td class="SubHead" width="165"><asp:label id="lblDescriptionSource" runat="server"></asp:label></td>
		<td><asp:textbox id="txtDescriptionSource" runat="server" width="300" maxlength="500" cssclass="NormalTextBox"
				rows="3" textmode="MultiLine"></asp:textbox></td>
	</tr>
	<tr>
		<td class="SubHead" width="165"><asp:label id="lblDynamicProviderSource" runat="server"></asp:label></td>
		<td><asp:dropdownlist id="ddlVisualProviderSource" runat="server" width="300"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td class="SubHead" width="165"><asp:label id="lblDynamicProviderGeoLocator" runat="server"></asp:label></td>
		<td><asp:dropdownlist id="ddlVisualProviderGeoLocator" runat="server" width="300"></asp:dropdownlist></td>
	</tr>
	<tr class="FileManager_Pager">
		<td align="center" colSpan="2"><asp:linkbutton id="lnkSave" runat="server" CssClass="CommandButton"></asp:linkbutton>|
			<asp:linkbutton id="lnkCancel" runat="server" CssClass="CommandButton"></asp:linkbutton></td>
	</tr>
</table>
