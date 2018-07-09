<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Standard.Admin.ascx.vb" Inherits="DotNetNuke.Modules.Map.Data.StandardAdmin" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>


<table class="FileManager_Explorer" cellSpacing="0" cellPadding="2" border="0" width=500>
	<tr>
		<td align="center" colSpan="2" class="FileManager_Header"><asp:label id="lblPanelName" runat="server">Panel Name</asp:label></td>
	</tr>
	<tr>
		<td class="normal" colspan=2><asp:Label CssClass="Normal" id="lblInstructions" runat="server"></asp:Label></td>
    </tr>	
	<tr>
		<td class="SubHead" colspan=2><asp:label id="lblPointContent" runat="server"></asp:label></td>
    </tr>	
    <TR>
		<TD class="SubHead" colSpan="2">
				<dnn:texteditor id="txtPointContent" runat="server" HtmlEncode="False" width="100%" height="400"></dnn:texteditor>
	    </TD>
	</TR>
	<tr>
		<td class="SubHead" colspan=2><asp:label id="lblSourceInfo" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td colspan=2 class="Normal"><asp:radiobutton id="rdoDataSourceUserOnline" runat="server" AutoPostBack="True" GroupName="DataSourceEnum"></asp:radiobutton><br>
			<asp:radiobutton id="rdoDataSourceDataPoints" runat="server" AutoPostBack="True" GroupName="DataSourceEnum"
				Checked="True"></asp:radiobutton><br>
			<asp:radiobutton id="rdoDataSourceCustom" runat="server" AutoPostBack="True" GroupName="DataSourceEnum"
				Checked="False"></asp:radiobutton></td>
	</tr>
	
	<asp:panel id="pnlrdoDataSourceUserOnline" Visible="False" Runat="server">
		<TR>
			<TD class="SubHead" colSpan="2">
				<asp:label id="lblUserOptions" runat="server"></asp:label></TD>
		</TR>
	    <tr>
		    <td class="normal" colspan=2><asp:Label CssClass="Normal" id="lblUserOptionsInstructions" runat="server"></asp:Label></td>
        </tr>			
		<TR>
			<TD class="Normal" align=center colSpan="2">
				<asp:CheckBox id="chkSummaryOnly" Runat="server" CssClass="CommandButton"></asp:CheckBox></TD>
		</TR>		
    </asp:panel>
	<asp:panel id="pnlrdoDataSourceDataPoints" Visible="False" Runat="server">
		<TR>
			<TD class="SubHead" colSpan="2">
				<asp:label id="lblStandardOptions" runat="server"></asp:label></TD>
		</TR>
	    <tr>
		    <td class="normal" colspan=2><asp:Label CssClass="Normal" id="lblStandardInstructions" runat="server"></asp:Label></td>
        </tr>			
    </asp:panel>    
	<asp:panel id="pnlrdoDataSourceCustom" Visible="False" Runat="server">
		<TR>
			<TD class="SubHead" colSpan="2">
				<asp:label id="lblCustomQuery" runat="server"></asp:label></TD>
		</TR>
	    <tr>
		    <td class="normal" colspan=2><asp:Label CssClass="Normal" id="lblCustomQueryInstructions" runat="server"></asp:Label></td>
        </tr>					
		<TR>
			<TD class="SubHead" colSpan="2">
				<asp:TextBox id="txtCustomQuery" runat="server" TextMode="MultiLine" Width="100%" Height="300px"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD class="SubHead" colSpan="2">
				<asp:label id="lblCustomConnection" runat="server"></asp:label></TD>
		</TR>
		<TR>
			<TD class="SubHead" colSpan="2">
				<asp:TextBox id="txtCustomConnection" runat="server" Width="100%"></asp:TextBox></TD>
		</TR>
		<TR>
			<TD colSpan="2">
				<TABLE class="Normal" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="SubHead" noWrap align="left">
							<asp:label id="lblQueryVariables" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD>
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
								<asp:repeater id="rptQueryOptions" runat="server">
									<HeaderTemplate>
										<tr>
											<td width="70"><IMG height="1" src="/images/spacer.gif" width="70"></td>
											<td></td>
											<td></td>
											<td></td>
										</tr>
										<tr class="DataGrid_Header">
											<td>
												<asp:label id="lblQVAction" runat="server">Action</asp:label></td>
											<td>
												<asp:label id="lblQVType" runat="server">Type</asp:label></td>
											<td>
												<asp:label id="lblQVSource" runat="server">Source</asp:label></td>
											<td>
												<asp:label id="lblQVTarget" runat="server">Target</asp:label></td>
											<td>
												<asp:label id="lblQVSecure" runat="server">Secure</asp:label></td>
										</tr>
									</HeaderTemplate>
									<ItemTemplate>
										<tr class="DataGrid_Item">
											<td>
												<table cellpadding="0" cellspacing="0" border="0">
													<tr>
														<td>
															<asp:ImageButton ImageUrl='<%#ModuleImageURL("edit.gif")%>' ID="Imagebutton1" CommandName="Edit" CommandArgument="<%#Container.DataItem.Index%>" Runat="server">
															</asp:ImageButton></td>
														<td>
															<asp:ImageButton ImageUrl='<%#ModuleImageURL("delete.gif")%>' ID="Imagebutton4" CommandName="Delete" CommandArgument="<%#Container.DataItem.Index%>" Runat="server">
															</asp:ImageButton></td>
													</tr>
												</table>
											</td>
											<td><%#Convert.ToString(Container.DataItem.VariableType).Replace("<", "&lt;").Replace(">", "&gt;")%></td>
											<td><%#Container.DataItem.Source%></td>
											<td><%#Container.DataItem.Target%></td>
											<td><img src="<%#BoolURL(Container.DataItem.EscapeSingleQuotes)%>"></td>
										</tr>
									</ItemTemplate>
									<FooterTemplate>
										<tr class="DataGrid_Footer">
											<td align="center" colspan="5">
												<asp:LinkButton CssClass="CommandButton" CommandName="Add" ID="lnkAddQueryVarable" Visible="True"
													Runat="server">Add Query Variable</asp:LinkButton></td>
										</tr>
									</FooterTemplate>
								</asp:repeater></TABLE>
						</TD>
					</TR>
					<TR>
						<TD align="center">
							<asp:panel id="pnlQueryEdit" Runat="server" Visible="False">
								<TABLE class="Normal" cellSpacing="1" cellPadding="0" border="0">
									<TR>
										<TD class="FileManager_Header" colSpan="4">
											<asp:label id="lblQueryEditor" runat="server"></asp:label>
											<asp:Label id="lblQueryTarget" runat="server"></asp:Label></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 200px; TEXT-ALIGN: center" noWrap>
											<asp:label id="lblQEType" runat="server"></asp:label></TD>
										<TD colSpan="3">
											<asp:DropDownList id="ddlVariableType" Runat="server" Width="300">
												<asp:ListItem Value="&lt;Session&gt;">&lt;Session&gt;</asp:ListItem>
												<asp:ListItem Value="&lt;QueryString&gt;">&lt;QueryString&gt;</asp:ListItem>
												<asp:ListItem Value="&lt;Form&gt;">&lt;Form&gt;</asp:ListItem>
												<asp:ListItem Value="TabId">TabId</asp:ListItem>
												<asp:ListItem Value="ModuleId">ModuleId</asp:ListItem>
												<asp:ListItem Value="PortalAlias">PortalAlias</asp:ListItem>
												<asp:ListItem Value="PortalId">PortalId</asp:ListItem>
												<asp:ListItem Value="UserId">UserId</asp:ListItem>
												<asp:ListItem Value="Latitude">Latitude</asp:ListItem>
												<asp:ListItem Value="Longitude">Longitude</asp:ListItem>
												<asp:ListItem Value="MinLatitude">MinLatitude</asp:ListItem>
												<asp:ListItem Value="MinLongitude">MinLongitude</asp:ListItem>
												<asp:ListItem Value="MaxLatitude">MaxLatitude</asp:ListItem>
												<asp:ListItem Value="MaxLongitude">MaxLongitude</asp:ListItem>
											</asp:DropDownList></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 165px" noWrap>
											<asp:label id="lblQESource" runat="server"></asp:label></TD>
										<TD colSpan="3">
											<asp:TextBox id="txtQuerySource" Runat="server" Width="300"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 165px" noWrap>
											<asp:label id="lblQETarget" runat="server"></asp:label></TD>
										<TD colSpan="3">
											<asp:TextBox id="txtQueryTarget" Runat="server" Width="300"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 165px" noWrap>
											<asp:label id="lblQELeft" runat="server"></asp:label></TD>
										<TD style="WIDTH: 100px">
											<asp:TextBox id="txtQueryTargetLeft" Runat="server" Width="100px"></asp:TextBox></TD>
										<TD style="WIDTH: 98px; TEXT-ALIGN: center" noWrap>
											<asp:label id="lblQERight" runat="server"></asp:label></TD>
										<TD>
											<asp:TextBox id="txtQueryTargetRight" Runat="server" Width="100px"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 165px" noWrap>
											<asp:label id="lblQEEmpty" runat="server"></asp:label></TD>
										<TD colSpan="3">
											<asp:TextBox id="txtQueryTargetEmpty" Runat="server" Width="300"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 165px" noWrap>
											<asp:label id="lblQESecurity" runat="server"></asp:label></TD>
										<TD colSpan="3">
											<asp:CheckBox id="chkQuerySQLInjection" Runat="server" CssClass="Normal" Text=""></asp:CheckBox></TD>
									</TR>
									<TR>
										<TD class="FileManager_Pager" noWrap align="center" colSpan="4">
											<asp:LinkButton id="lnkSaveQueryOptions" Runat="server" CssClass="CommandButton"></asp:LinkButton>|
											<asp:LinkButton id="lnkCancelQueryOptions" Runat="server" CssClass="CommandButton"></asp:LinkButton></TD>
									</TR>
								</TABLE>
							</asp:panel></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</asp:panel>
	<asp:panel id="pnlrdoDataSourceCustom_Error" Visible="false" Runat="server">
	    <tr>
	        <td class="NormalRed">
	            <asp:label id="lblDataSourceCustom_Error" runat="server"></asp:label>
	        </td>
	    </tr>
	</asp:panel>
	<asp:panel id="pnlService" Visible="True" Enabled="False" Runat="server">
		<TR>
			<TD class="SubHead" colSpan="2">
					<asp:label id="lblService" runat="server"></asp:label></TD>
		</TR>	
    <tr>
	    <td class="normal" colspan=2><asp:Label CssClass="Normal" id="lblServiceInstructions" runat="server"></asp:Label></td>
    </tr>		
	<TR>
		<TD class="Normal" align=center colSpan="2">
			<asp:LinkButton id="lnkUserService" Runat="server" CssClass="CommandButton"></asp:LinkButton>
			<asp:label id="lblServiceBreak" runat="server"></asp:label>
			<asp:LinkButton id="lnkGeoService" Runat="server" CssClass="CommandButton"></asp:LinkButton>
            <asp:label id="lblServiceBreakClear" runat="server"></asp:label>
			<asp:LinkButton id="lnkClearService" Runat="server" CssClass="CommandButton"></asp:LinkButton>			
		</TD>
	</TR>	
	</asp:panel>
	<tr>
		<td align="center" colSpan="2" class="FileManager_Pager"><asp:linkbutton id="lnkSave" runat="server" CssClass="CommandButton"></asp:linkbutton>|
			<asp:linkbutton id="lnkCancel" runat="server" CssClass="CommandButton"></asp:linkbutton></td>
	</tr>
</table>
