<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Google.Timelapse.Admin.ascx.vb" Inherits="DotNetNuke.Modules.Map.Visuals.Google.TimelapseAdmin" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="dnn" TagName="URL" Src="~/controls/URLControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>
<table align="center" class="FileManager_Explorer" cellSpacing="0" cellPadding="2" border="0" width="500">
	<tr>
		<td class="FileManager_Header" align="center"><asp:label id="lblPanelName" runat="server">Panel Name</asp:label></td>
	</tr>
	<tr>
		<td class="normal"><asp:Label CssClass="Normal" id="lblInstructions" runat="server"></asp:Label></td>
	</tr>
	<TR>
		<TD align=center>
			<TABLE class="Normal" cellSpacing="0" cellPadding="0" width="540" border="0">
				<TR>
					<TD class="SubHead" noWrap align="left"><asp:label id="lblLiceneKeys" runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<asp:repeater id="rptLicenseKeys" runat="server">
								<HeaderTemplate>
									<tr>
										<td width="70"><IMG height="1" src="/images/spacer.gif" width="70"></td>
										<td></td>
										<td></td>
									</tr>
									<tr class="DataGrid_Header">
										<td>
											<asp:label id="lblLKAction" runat="server">Action</asp:label></td>
										<td>
											<asp:label id="lblLKUrl" runat="server">Domain</asp:label></td>
										<td>
											<asp:label id="lblLKKey" runat="server">Key</asp:label></td>
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
										<td nowrap><%#Container.DataItem.Domain%>&nbsp;</td>
										<td><%#KeyFormat(Container.DataItem.Key)%></td>
									</tr>
								</ItemTemplate>
								<FooterTemplate>
									<tr class="DataGrid_Footer">
										<td align="center" colspan="3">
											<asp:LinkButton CssClass="CommandButton" CommandName="Add" ID="lnkAddLicenseKey" Visible="True"
												Runat="server">Add License Key</asp:LinkButton></td>
									</tr>
								</FooterTemplate>
							</asp:repeater></TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="center"><asp:panel id="pnlLicenseKeyEdit" Runat="server" Visible="False">
							<TABLE class="Normal" cellSpacing="1" cellPadding="0" border="0" width=100%>
								<TR>
									<TD class="FileManager_Header" colSpan="4">
										<asp:label id="lblKeyEditor" runat="server"></asp:label>
										<asp:Label id="lblKeyTarget" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 165px" noWrap>
										<asp:label id="lblKeyDomain" runat="server"></asp:label></TD>
									<TD colSpan="3">
										<asp:TextBox id="txtKeyDomain" Runat="server" Width="300"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 165px" noWrap>
										<asp:label id="lblKey" runat="server"></asp:label></TD>
									<TD colSpan="3">
										<asp:TextBox id="txtKeyLicense" Runat="server" Width="300"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="FileManager_Pager" noWrap align="center" colSpan="4">
										<asp:LinkButton id="lnkSaveKey" Runat="server" CssClass="CommandButton"></asp:LinkButton>|
										<asp:LinkButton id="lnkCancelKey" Runat="server" CssClass="CommandButton"></asp:LinkButton></TD>
								</TR>
							</TABLE>
						</asp:panel></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<tr>
		<td class="FileManager_Header" align="center"><asp:label id="lblPanelName2" runat="server">Panel Name</asp:label></td>
	</tr>
	<TR>
		<TD align=center>
			<TABLE class="Normal" cellSpacing="0" cellPadding="0" width="540" border="0">
				<TR>
					<TD>
						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
							<asp:repeater id="rptMarkers" runat="server">
								<HeaderTemplate>
									<tr>
										<td><IMG height="1" src='<%#ImageURL("~/images/spacer.gif")%>' width="40"></td>
										<td><IMG height="1" src='<%#ImageURL("~/images/spacer.gif")%>' width="40"></td>
										<td><IMG height="1" src='<%#ImageURL("~/images/spacer.gif")%>' width="30"></td>
										<td><IMG height="1" src='<%#ImageURL("~/images/spacer.gif")%>' width="40"></td>
										<td><IMG height="1" src='<%#ImageURL("~/images/spacer.gif")%>' width="40"></td>
										<td><IMG height="1" src='<%#ImageURL("~/images/spacer.gif")%>' width="30"></td>
										<td><IMG height="1" src='<%#ImageURL("~/images/spacer.gif")%>' width="40"></td>
										<td><IMG height="1" src='<%#ImageURL("~/images/spacer.gif")%>' width="40"></td>
										<td><IMG height="1" src='<%#ImageURL("~/images/spacer.gif")%>' width="40"></td>
										<td><IMG height="1" src='<%#ImageURL("~/images/spacer.gif")%>' width="40"></td>
										<td><IMG height="1" src='<%#ImageURL("~/images/spacer.gif")%>' width="40"></td>
										<td><IMG height="1" src='<%#ImageURL("~/images/spacer.gif")%>' width="40"></td>
									</tr>
									<tr class="DataGrid_Header" style="background: #EEEEEE;">
										<td class="NormalBold" style="border-left: 1px solid black; border-top: 1px solid black;">&nbsp;</td>
										<td class="NormalBold" style="border-top: 1px solid black;">&nbsp;</td>
										<td class="NormalBold" style="border-top: 1px solid black;">&nbsp;</td>
										<td class="NormalBold" style="border-top: 1px solid black;" colspan="2" align="center"><%=Locale("Icons_C_Size")%></td>
										<td class="NormalBold" style="border-top: 1px solid black;">&nbsp;</td>
										<td class="NormalBold" style="border-top: 1px solid black;" colspan="2" align="center"><%=Locale("Icons_C_Size")%></td>
										<td class="NormalBold" style="border-top: 1px solid black;" colspan="2" align="center"><%=Locale("Icons_C_Anchor")%></td>
										<td class="NormalBold" style="border-right: 1px solid black; border-top: 1px solid black;"
											align="center" colspan="2"><%=Locale("Icons_C_Info")%></td>
									</tr>
									<tr class="DataGrid_Header" style="background: #EEEEEE;">
										<td class="NormalBold" style="border-left: 1px solid black; border-bottom: 1px solid black;"
											align="center"><%=Locale("Icons_C_Index")%></td>
										<td class="NormalBold" style="border-bottom: 1px solid black;" align="center"><%=Locale("Icons_C_Task")%></td>
										<td class="NormalBold" style="border-bottom: 1px solid black;" align="center"><%=Locale("Icons_C_Icon")%></td>
										<td class="NormalBold" style="border-bottom: 1px solid black; border-left: 1px solid black; border-top: 1px solid black;"
											align="center"><%=Locale("Icons_C_Width")%></td>
										<td class="NormalBold" style="border-bottom: 1px solid black; border-right: 1px solid black; border-top: 1px solid black;"
											align="center"><%=Locale("Icons_C_Height")%></td>
										<td class="NormalBold" style="border-bottom: 1px solid black; " align="center"><%=Locale("Icons_Shadow")%></td>
										<td class="NormalBold" style="border-bottom: 1px solid black;  border-left: 1px solid black; border-top: 1px solid black;"
											align="center"><%=Locale("Icons_C_Width")%></td>
										<td class="NormalBold" style="border-bottom: 1px solid black;  border-right: 1px solid black; border-top: 1px solid black;"
											align="center"><%=Locale("Icons_C_Height")%></td>
										<td class="NormalBold" style="border-bottom: 1px solid black;  border-top: 1px solid black;"
											align="center"><%=Locale("Icons_C_X")%></td>
										<td class="NormalBold" style="border-bottom: 1px solid black;  border-right: 1px solid black; border-top: 1px solid black;"
											align="center"><%=Locale("Icons_C_Y")%></td>
										<td class="NormalBold" style="border-bottom: 1px solid black;  border-top: 1px solid black;"
											align="center"><%=Locale("Icons_C_X")%></td>
										<td class="NormalBold" style="border-right: 1px solid black; border-bottom: 1px solid black; border-top: 1px solid black;"
											align="center"><%=Locale("Icons_C_Y")%></td>
									</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<tr class="DataGrid_Item">
										<td class="Normal" style="border-bottom: 1px solid #EEEEEE; border-left: 1px solid black;"><%#Container.DataItem.Index%></td>
										<td>
											<table cellpadding="0" cellspacing="0" border="0">
												<tr>
													<td>
														<asp:ImageButton ImageUrl='<%#ImageURL("~/images/edit.gif")%>' ID="lnkEdit" CommandName="Edit" CommandArgument="<%#Container.DataItem.Index%>" Runat="server">
														</asp:ImageButton></td>
													<td>
														<asp:ImageButton ImageUrl='<%#ImageURL("~/images/up.gif")%>' ID="lnkUp" CommandName="Up" CommandArgument="<%#Container.DataItem.Index%>" Runat="server">
														</asp:ImageButton></td>
													<td>
														<asp:ImageButton ImageUrl='<%#ImageURL("~/images/dn.gif")%>' ID="lnkDown" CommandName="Down" CommandArgument="<%#Container.DataItem.Index%>" Runat="server">
														</asp:ImageButton></td>
													<td>
														<asp:ImageButton ImageUrl='<%#ImageURL("~/images/delete.gif")%>' ID="lnkDelete" CommandName="Delete" CommandArgument="<%#Container.DataItem.Index%>" Runat="server">
														</asp:ImageButton></td>
												</tr>
											</table>
										</td>
										<td class="Normal" style="border-bottom: 1px solid #EEEEEE; background: #EEEEEE;" align="center"><img src="<%#ImageURL(Container.DataItem.Icon)%>" border=0 style="border: 0px;"></td>
										<td class="Normal" style="border-bottom: 1px solid #EEEEEE; border-left: 1px solid black;"
											align="center"><%#Container.DataItem.IconWidth%></td>
										<td class="Normal" style="border-bottom: 1px solid #EEEEEE; border-right: 1px solid black;"
											align="center"><%#Container.DataItem.IconHeight%></td>
										<td class="Normal" style="border-bottom: 1px solid #EEEEEE; background: #EEEEEE;" align="center"><img src="<%#ImageURL(Container.DataItem.Shadow)%>" border=0 style="border: 0px;"></td>
										<td class="Normal" style="border-bottom: 1px solid #EEEEEE; border-left: 1px solid black;"
											align="center"><%#Container.DataItem.ShadowWidth%></td>
										<td class="Normal" style="border-bottom: 1px solid #EEEEEE; border-right: 1px solid black;"
											align="center"><%#Container.DataItem.ShadowHeight%></td>
										<td class="Normal" style="border-bottom: 1px solid #EEEEEE;" align="center"><%#Container.DataItem.AnchorX%></td>
										<td class="Normal" style="border-bottom: 1px solid #EEEEEE; border-right: 1px solid black;"
											align="center"><%#Container.DataItem.AnchorY%></td>
										<td class="Normal" style="border-bottom: 1px solid #EEEEEE;" align="center"><%#Container.DataItem.InfoX%></td>
										<td class="Normal" style="border-bottom: 1px solid #EEEEEE; border-right: 1px solid black;"
											align="center"><%#Container.DataItem.InfoY%></td>
									</tr>
								</ItemTemplate>
								<FooterTemplate>
									<tr class="DataGrid_Footer">
										<td align="center" colspan="12">
											<asp:LinkButton CssClass="CommandButton" CommandName="Add" ID="lnkAddMarker" Visible="True" Runat="server">Add Marker</asp:LinkButton></td>
									</tr>
								</FooterTemplate>
							</asp:repeater></TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="center"><asp:panel id="pnlEditMarker" Runat="server" Visible="False">
							<TABLE class="Normal" cellSpacing="1" cellPadding="0" border="0" width=100%>
								<TR>
									<TD class="FileManager_Header" colSpan="2">
										<asp:label id="lblEditMarker" runat="server"></asp:label>
										<asp:Label id="lblEditMarkerTarget" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 165px" noWrap>
										<asp:label id="lblMarkerIcon" runat="server"></asp:label></TD>
									<TD>
										<dnn:url id="ctlIcon" runat="server" width="300" showlog="False" ShowUrls = "False" ShowUpLoad = "False" ShowTrack = "False" ShowTabs = "False"></dnn:url>
										<asp:TextBox id="txtIcon" Runat="server" Width="300" Visible="false"></asp:TextBox>
										</TD>
								</TR>
								<TR>
									<TD style="WIDTH: 165px" noWrap>
										<asp:label id="lblMarkerShadow" runat="server"></asp:label></TD>
									<TD>
										<dnn:url id="ctlShadow" runat="server" width="300"  showlog="False" ShowUrls = "False" ShowUpLoad = "False" ShowTrack = "False" ShowTabs = "False"></dnn:url>
										<asp:TextBox id="txtShadow" Runat="server" Width="300" Visible="false"></asp:TextBox>
										</TD>
								</TR>
								<TR>
									<TD style="WIDTH: 165px" noWrap>
										<asp:label id="lblMarkerWH" runat="server"></asp:label></TD>
									<TD>
										<asp:TextBox id="txtIWidth" Runat="server" Width="50"></asp:TextBox>
										<asp:TextBox id="txtIHeight" Runat="server" Width="50"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 165px" noWrap>
										<asp:label id="lblMarkerShadowWH" runat="server"></asp:label></TD>
									<TD>
										<asp:TextBox id="txtSWidth" Runat="server" Width="50"></asp:TextBox>
										<asp:TextBox id="txtSHeight" Runat="server" Width="50"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 165px" noWrap>
										<asp:label id="lblMarkerAnchorXY" runat="server"></asp:label></TD>
									<TD>
										<asp:TextBox id="txtAPointX" Runat="server" Width="50"></asp:TextBox>
										<asp:TextBox id="txtAPointY" Runat="server" Width="50"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 165px" noWrap>
										<asp:label id="lblMarkerInfoXY" runat="server"></asp:label></TD>
									<TD>
										<asp:TextBox id="txtIPointX" Runat="server" Width="50"></asp:TextBox>
										<asp:TextBox id="txtIPointY" Runat="server" Width="50"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="FileManager_Pager" noWrap align="center" colSpan="2">
										<asp:LinkButton id="lnkSaveMarker" Runat="server" CssClass="CommandButton"></asp:LinkButton>|
										<asp:LinkButton id="lnkCancelMarker" Runat="server" CssClass="CommandButton"></asp:LinkButton></TD>
								</TR>
							</TABLE>
						</asp:panel></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<tr>
		<td class="FileManager_Header" align="center"><asp:label id="lblPanelName3" runat="server">Panel Name</asp:label></td>
	</tr>
	<tr>
		<td class="Normal" align=center>
			<asp:Panel ID="pnlMap" Runat="server" Visible="False">
				<DIV id="div_GMap" style="DISPLAY: block; WIDTH: 100%">
					<TABLE cellSpacing="2" cellPadding="2" width="540" border="0">
						<TR>
							<TD>
								<DIV id="map" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; WIDTH: 540px; BORDER-BOTTOM: black 1px solid; HEIGHT: 400px"></DIV>
							</TD>
						</TR>
					</TABLE>
					<DIV id="Status" style="DISPLAY: none" name="Status"></DIV>
				</DIV>
			</asp:Panel>
			<TABLE class="Normal" cellSpacing="0" cellPadding="0" width="540" border="0">
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblMapLatitude" Runat="server"></asp:label></TD>
					<TD><asp:textbox id="txtLatitude" Runat="server" Width="100"></asp:textbox><A class="CommandButton" href="javascript:setRecording('CENTER');"><asp:label id="lblSet1" Runat="server"></asp:label></A></TD>
				</TR>
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblMapLongitude" Runat="server"></asp:label></TD>
					<TD><asp:textbox id="txtLongitude" Runat="server" width="100"></asp:textbox></TD>
				</TR>
			</TABLE>
			<TABLE class="Normal" cellSpacing="0" cellPadding="0" width="540" border="0">
				<TR>
					<TD class="SubHead" noWrap colSpan="2"><asp:label id="lblPointDescription" Runat="server"></asp:label></TD>
				</TR>
				<TR>
					<TD colSpan="2"><dnn:texteditor id="txtPoint_Description" runat="server" HtmlEncode="False" width="100%" height="400"></dnn:texteditor></TD>
				</TR>
			</TABLE>
			<TABLE class="Normal" cellSpacing="0" cellPadding="0" width="540" border="0">
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblWidth" Runat="server"></asp:label></TD>
					<TD><asp:textbox id="txtWidth" Runat="server" width="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblHeight" Runat="server"></asp:label></TD>
					<TD><asp:textbox id="txtHeight" Runat="server" width="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblMapZoom" Runat="server"></asp:label></TD>
					<TD><asp:dropdownlist id="ddlZoom" Runat="server" Width="100">
							<asp:ListItem Value="0">0</asp:ListItem>
							<asp:ListItem Value="1">1</asp:ListItem>
							<asp:ListItem Value="2">2</asp:ListItem>
							<asp:ListItem Value="3">3</asp:ListItem>
							<asp:ListItem Value="4">4</asp:ListItem>
							<asp:ListItem Value="5">5</asp:ListItem>
							<asp:ListItem Value="6">6</asp:ListItem>
							<asp:ListItem Value="7">7</asp:ListItem>
							<asp:ListItem Value="8">8</asp:ListItem>
							<asp:ListItem Value="9">9</asp:ListItem>
							<asp:ListItem Value="10">10</asp:ListItem>
							<asp:ListItem Value="11">11</asp:ListItem>
							<asp:ListItem Value="12">12</asp:ListItem>
							<asp:ListItem Value="13">13</asp:ListItem>
							<asp:ListItem Value="14">14</asp:ListItem>
							<asp:ListItem Value="15">15</asp:ListItem>
							<asp:ListItem Value="16">16</asp:ListItem>
						</asp:dropdownlist><A class="CommandButton" href="javascript:setRecording('ZOOM');"><asp:label id="lblSet2" Runat="server"></asp:label></A></TD>
				</TR>
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblMapType" Runat="server"></asp:label></TD>
					<TD><asp:dropdownlist id="ddlMapType" runat="server" Width="100">
							<asp:ListItem Value="0">0</asp:ListItem>
							<asp:ListItem Value="1">1</asp:ListItem>
							<asp:ListItem Value="2">2</asp:ListItem>
						</asp:dropdownlist><A class="CommandButton" href="javascript:setRecording('TYPE');"><asp:label id="lblSet3" Runat="server"></asp:label></A></TD>
				</TR>
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblMapControlType" Runat="server"></asp:label></TD>
					<TD><asp:dropdownlist id="ddlMapControlType" runat="server" Width="100">
							<asp:ListItem Value="0">Off</asp:ListItem>
							<asp:ListItem Value="1">On</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblNavigationControl" Runat="server"></asp:label></TD>
					<TD><asp:dropdownlist id="ddlNavigationType" runat="server" Width="100" cssclass="MapBox">
							<asp:ListItem Value="0">0</asp:ListItem>
							<asp:ListItem Value="1">1</asp:ListItem>
							<asp:ListItem Value="2">2</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblOverviewMapControl" Runat="server"></asp:label></TD>
					<TD><asp:dropdownlist id="ddlOverviewMapType" runat="server" Width="100">
							<asp:ListItem Value="0">Off</asp:ListItem>
							<asp:ListItem Value="1">On</asp:ListItem>
						</asp:dropdownlist></TD>
				</TR>					
			</TABLE>
		</td>
	</tr>
	<tr>
		<td class="FileManager_Header" align="center"><asp:label id="lblPanelName4" runat="server">Panel Name</asp:label></td>
	</tr>
	<tr>
		<td align=center>
			<TABLE class="Normal" cellSpacing="0" cellPadding="0" width="540" border="0">
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblStandardInitialDelay" Runat="server"></asp:label></TD>
					<TD><asp:textbox id="txtInitialDelay" Runat="server" Width="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblStandardRetryDelay" Runat="server"></asp:label></TD>
					<TD><asp:textbox id="txtRetryDelay" Runat="server" Width="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblTimelineDelay" Runat="server"></asp:label></TD>
					<TD><asp:textbox id="txtTimelineDelay" Runat="server" Width="100"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap><asp:label id="lblTimeline" Runat="server"></asp:label></TD>
					<TD><asp:CheckBox id="chkTimeLineAutoPlayback" Runat="server"></asp:CheckBox></TD>
				</TR>
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap>&nbsp;</TD>
					<TD><asp:CheckBox id="chkTimeLineUseTimer" Runat="server"></asp:CheckBox></TD>
				</TR>
				<TR>
					<TD class="SubHead" style="WIDTH: 165px" noWrap>&nbsp;</TD>
					<TD><asp:CheckBox id="chkTimeLineDisplayTimer" Runat="server"></asp:CheckBox></TD>
				</TR>
			</TABLE>
		</td>
	</tr>
	<tr>
		<td class="FileManager_Pager" align="center"><asp:linkbutton id="lnkSave" runat="server" CssClass="CommandButton"></asp:linkbutton>|
			<asp:linkbutton id="lnkCancel" runat="server" CssClass="CommandButton"></asp:linkbutton></td>
	</tr>
</table>
<asp:literal id="ltlGoogleAPIScript" Runat="server"></asp:literal>
