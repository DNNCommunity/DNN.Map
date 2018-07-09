<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Source.ascx.vb" Inherits="DotNetNuke.Modules.Map.Controls.MapGeneral" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table class="Settings" cellspacing="0" cellpadding="2" align="center" border="0">
	<tr>
		<td class="normal" colspan="2"><asp:label id="lblInstructions" runat="server" CssClass="Normal"></asp:label></td>
	</tr>
	<tr>
		<td class="SubHead" width="165"><asp:label id="lblMapInfo" runat="server"></asp:label></td>
		<td class="SubHead"><asp:radiobutton id="rdoUseExisting" runat="server" GroupName="NewExisting"></asp:radiobutton><asp:radiobutton id="rdoCreateNew" runat="server" GroupName="NewExisting"></asp:radiobutton></td>
	</tr>
	<tr>
		<td class="SubHead" width="165"><asp:label id="lblExisting" runat="server"></asp:label></td>
		<td><asp:dropdownlist id="ddlExisting" runat="server" width="300"></asp:dropdownlist></td>
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
		<td class="SubHead" width="165"><asp:Label id="lblDynamicProvider" runat="server"></asp:Label></td>
		<td>
			<asp:DropDownList id="ddlVisualProvider" runat="server" width="300"></asp:DropDownList></td>
	</tr>
</table>
