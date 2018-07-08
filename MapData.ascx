<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Data.ascx.vb" Inherits="DotNetNuke.Modules.Map.Controls.MapData" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE align=center width=100% class="FileManager_Explorer" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td class="FileManager_Header" align="center"><asp:label id="lblPanelName" runat="server">Panel Name</asp:label></td>
	</tr>
	<tr>
		<td class="normal"><asp:Label CssClass="Normal" id="lblInstructions" runat="server"></asp:Label></td>
	</tr>
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class=FileManager_Pager align="center" width="100%">
						<DIV id="pgs" name="pgs"></DIV>
					</TD>
				</TR>
				<TR>
					<TD class=FileManager_Item>
						<DIV id="tbl" name="tbl"></DIV>
					</TD>
				</TR>
				<TR>
					<TD class=FileManager_StatusBar align="center">
						<DIV id="Status" name="Status"></DIV>
					</TD>
				</TR>
			</TABLE>
			<asp:Literal id="ltlAjaxDataWizard" Runat="server"></asp:Literal></TD>
	</TR>
</TABLE>
<asp:literal id="ltlJavascript" Runat="server"></asp:literal>