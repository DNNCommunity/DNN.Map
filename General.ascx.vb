'
' DotNetNuke - http://www.dotnetnuke.com
' Copyright (c) 2002-2005
' by Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'
Imports DotNetNuke
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Modules.Map.Components
Imports DotNetNuke.Modules.Map.Data
Namespace DotNetNuke.Modules.Map.Controls
    Public Class MapGeneral
        Inherits ControlPanelBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents lblPanelName As System.Web.UI.WebControls.Label
        Protected WithEvents lblMapInfo As System.Web.UI.WebControls.Label
        Protected WithEvents rdoUseExisting As System.Web.UI.WebControls.RadioButton
        Protected WithEvents rdoCreateNew As System.Web.UI.WebControls.RadioButton
        Protected WithEvents lblExisting As System.Web.UI.WebControls.Label
        Protected WithEvents ddlExisting As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lblName As System.Web.UI.WebControls.Label
        Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblDynamicProvider As System.Web.UI.WebControls.Label
        Protected WithEvents ddlVisualProvider As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lblPanelNameSource As System.Web.UI.WebControls.Label
        Protected WithEvents lblSourceInfo As System.Web.UI.WebControls.Label
        Protected WithEvents rdoUseExistingSource As System.Web.UI.WebControls.RadioButton
        Protected WithEvents rdoCreateNewSource As System.Web.UI.WebControls.RadioButton
        Protected WithEvents lblExistingSource As System.Web.UI.WebControls.Label
        Protected WithEvents ddlExistingSource As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lblNameSource As System.Web.UI.WebControls.Label
        Protected WithEvents txtNameSource As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblDescriptionSource As System.Web.UI.WebControls.Label
        Protected WithEvents txtDescriptionSource As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblDynamicProviderSource As System.Web.UI.WebControls.Label
        Protected WithEvents ddlVisualProviderSource As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lblDynamicProviderGeoLocator As System.Web.UI.WebControls.Label
        Protected WithEvents ddlVisualProviderGeoLocator As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lnkSave As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lblInstructions As System.Web.UI.WebControls.Label


        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Localize(CType(lblPanelName, Object), "Text")
            Localize(CType(lblPanelNameSource, Object), "Text")
            Localize(CType(lblInstructions, Object), "Text")
            Localize(CType(rdoUseExisting, Object), "Text")
            Localize(CType(rdoCreateNew, Object), "Text")
            Localize(CType(lblExisting, Object), "Text")
            Localize(CType(lblName, Object), "Text")
            Localize(CType(lblDescription, Object), "Text")
            Localize(CType(lblDynamicProvider, Object), "Text")
            Localize(CType(lblMapInfo, Object), "Text")

            Localize(CType(rdoUseExistingSource, Object), "Text")
            Localize(CType(rdoCreateNewSource, Object), "Text")
            Localize(CType(lblExistingSource, Object), "Text")
            Localize(CType(lblNameSource, Object), "Text")
            Localize(CType(lblDescriptionSource, Object), "Text")
            Localize(CType(lblDynamicProviderSource, Object), "Text")
            Localize(CType(lblDynamicProviderGeoLocator, Object), "Text")

            Localize(CType(lnkCancel, Object), "Text")
            Localize(CType(lnkSave, Object), "Text")
        End Sub

        Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
            If Not isLoaded Then
                BindData_MapConfigurations()
                BindData_VisualProviders()

                BindData_SourceConfigurations()
                BindData_SourceProviders()

                BindData_GeoLocatorProviders()

                If Not MapConfiguration Is Nothing AndAlso Not MapConfiguration.Map Is Nothing Then
                    'LOAD CONFIGURATION SETTINGS
                    BindData_Map(MapConfiguration.Map)
                    BindData_MapConfiguration(MapConfiguration.Map.MapID)
                    If Not SourceConfiguration Is Nothing AndAlso Not SourceConfiguration.Source Is Nothing Then
                        BindData_Source(SourceConfiguration.Source)
                        BindData_SourceConfiguration(SourceConfiguration.Source.SourceID)
                    Else
                        BindData_Source(-1)
                        BindData_SourceConfiguration(-1)
                    End If
                Else
                    'LOAD DEFAULTS
                    BindData_Map(-1)
                    BindData_MapConfiguration(-1)

                    BindData_Source(-1)
                    BindData_SourceConfiguration(-1)
                End If
            End If
        End Sub

        Private Sub BindData_Map(ByRef mapinfo As DotNetNuke.Modules.Map.Data.MapInfo)
            If Not mapinfo Is Nothing AndAlso mapinfo.PortalID = ControlPanelModule.PortalId Then
                rdoCreateNew.Checked = False
                rdoUseExisting.Checked = True
                ddlExisting.Visible = rdoUseExisting.Checked
                lblExisting.Visible = rdoUseExisting.Checked

                txtName.Text = mapinfo.Name
                txtDescription.Text = mapinfo.Description
                If mapinfo.ProviderID > 0 Then
                    ddlVisualProvider.SelectedValue = mapinfo.ProviderID
                End If

                BindData_MapConfiguration(mapinfo.MapID)
            End If
        End Sub
        Private Sub BindData_Source(ByRef sourceinfo As DotNetNuke.Modules.Map.Data.SourceInfo)
            If Not sourceinfo Is Nothing AndAlso sourceinfo.PortalID = ControlPanelModule.PortalId Then
                rdoCreateNewSource.Checked = False
                rdoUseExistingSource.Checked = True
                ddlExistingSource.Visible = rdoUseExistingSource.Checked
                lblExistingSource.Visible = rdoUseExistingSource.Checked

                txtNameSource.Text = sourceinfo.Name
                txtDescriptionSource.Text = sourceinfo.Description
                If sourceinfo.ProviderID > 0 Then
                    ddlVisualProviderSource.SelectedValue = sourceinfo.ProviderID
                End If
                If sourceinfo.GeoLocatorProviderID > 0 Then
                    ddlVisualProviderGeoLocator.SelectedValue = sourceinfo.GeoLocatorProviderID
                End If

                BindData_SourceConfiguration(sourceinfo.SourceID)
            End If
        End Sub
        Private Sub BindData_Map(ByVal MapID As Integer)
            If MapID > 0 Then
                Dim mapDC As New MapController
                Dim mapInfo As DotNetNuke.Modules.Map.Data.MapInfo
                mapInfo = mapDC.GetMap(MapID)
                BindData_Map(mapInfo)
                mapInfo = Nothing
                mapDC = Nothing
            Else
                rdoCreateNew.Checked = True
                rdoUseExisting.Checked = False
                ddlExisting.Visible = rdoUseExisting.Checked
                lblExisting.Visible = rdoUseExisting.Checked
                ddlExisting.SelectedIndex = -1
            End If
        End Sub
        Private Sub BindData_Source(ByVal SourceID As Integer)
            If SourceID > 0 Then
                Dim mapDC As New MapController
                Dim sourceInfo As DotNetNuke.Modules.Map.Data.SourceInfo
                sourceInfo = mapDC.GetSource(SourceID)
                BindData_Source(sourceInfo)
                sourceInfo = Nothing
                mapDC = Nothing
            Else
                rdoCreateNewSource.Checked = True
                rdoUseExistingSource.Checked = False
                ddlExistingSource.Visible = rdoUseExistingSource.Checked
                lblExistingSource.Visible = rdoUseExistingSource.Checked
                ddlExistingSource.SelectedIndex = -1
            End If
        End Sub
        Private Sub BindData_MapConfigurations()
            Dim mapDC As New MapController
            Dim arr As ArrayList = mapDC.ListMaps(ControlPanelModule.PortalId)
            If Not arr Is Nothing Then
                ddlExisting.DataSource = arr
                ddlExisting.DataTextField = "Name"
                ddlExisting.DataValueField = "MapID"
                ddlExisting.DataBind()
            End If
        End Sub
        Private Sub BindData_SourceConfigurations()
            Dim mapDC As New MapController
            Dim arr As ArrayList = mapDC.ListSources(ControlPanelModule.PortalId)
            If Not arr Is Nothing Then
                ddlExistingSource.DataSource = arr
                ddlExistingSource.DataTextField = "Name"
                ddlExistingSource.DataValueField = "SourceID"
                ddlExistingSource.DataBind()
            End If
        End Sub
        Private Sub BindData_MapConfiguration(ByVal DefaultMapID As Integer)
            If DefaultMapID <= 0 Then
                If MapID > 0 AndAlso Me.MapConfiguration.Map.ProviderID > 0 Then
                    'Possibly MapID.
                    DefaultMapID = Me.MapConfiguration.Map.ProviderID
                Else
                    If ddlExisting.Items.Count > 0 Then
                        DefaultMapID = ddlExisting.Items(0).Value
                    End If
                End If
            End If
            If DefaultMapID > 0 Then
                ddlExisting.SelectedValue = DefaultMapID
            End If
        End Sub
        Private Sub BindData_SourceConfiguration(ByVal DefaultSourceID As Integer)
            If DefaultSourceID <= 0 Then
                If SourceID > 0 Then
                    'Possibly SourceID.
                    DefaultSourceID = SourceID
                Else
                    If ddlExistingSource.Items.Count > 0 Then
                        DefaultSourceID = ddlExistingSource.Items(0).Value
                    End If
                End If
            End If
            If DefaultSourceID > 0 Then
                ddlExistingSource.SelectedValue = DefaultSourceID
            End If
        End Sub
        Private Sub BindData_VisualProviders()
            If ddlVisualProvider.DataSource Is Nothing Then
                Dim mapDC As New MapController
                Dim originalValue As String
                Dim arr As ArrayList = mapDC.ListProviders(DotNetNuke.Modules.Map.Data.DynamicProviderInfo.DataType_Visual)
                originalValue = ddlVisualProvider.SelectedValue
                If Not arr Is Nothing Then
                    ddlVisualProvider.DataSource = arr
                    ddlVisualProvider.DataTextField = "Name"
                    ddlVisualProvider.DataValueField = "ProviderID"
                    ddlVisualProvider.DataBind()
                End If
                Try
                    If Not ddlVisualProvider.SelectedValue Is Nothing Then
                        ddlVisualProvider.SelectedValue = originalValue
                    End If
                Catch ex As Exception
                End Try
            End If
        End Sub
        Private Sub BindData_SourceProviders()
            If ddlVisualProviderSource.DataSource Is Nothing Then
                Dim mapDC As New MapController
                Dim originalValue As String
                Dim arr As ArrayList = mapDC.ListProviders(DotNetNuke.Modules.Map.Data.DynamicProviderInfo.DataType_Data)
                originalValue = ddlVisualProviderSource.SelectedValue
                If Not arr Is Nothing Then
                    ddlVisualProviderSource.DataSource = arr
                    ddlVisualProviderSource.DataTextField = "Name"
                    ddlVisualProviderSource.DataValueField = "ProviderID"
                    ddlVisualProviderSource.DataBind()
                End If
                Try
                    If Not ddlVisualProviderSource.SelectedValue Is Nothing Then
                        ddlVisualProviderSource.SelectedValue = originalValue
                    End If
                Catch ex As Exception
                End Try
            End If
        End Sub
        Private Sub BindData_GeoLocatorProviders()
            If ddlVisualProviderGeoLocator.DataSource Is Nothing Then
                Dim mapDC As New MapController
                Dim originalValue As String
                Dim arr As ArrayList = mapDC.ListProviders(DotNetNuke.Modules.Map.Data.DynamicProviderInfo.DataType_GeoLocator)
                originalValue = ddlVisualProviderGeoLocator.SelectedValue
                If Not arr Is Nothing Then
                    ddlVisualProviderGeoLocator.DataSource = arr
                    ddlVisualProviderGeoLocator.DataTextField = "Name"
                    ddlVisualProviderGeoLocator.DataValueField = "ProviderID"
                    ddlVisualProviderGeoLocator.DataBind()
                End If
                Try
                    If Not ddlVisualProviderGeoLocator.SelectedValue Is Nothing Then
                        ddlVisualProviderGeoLocator.SelectedValue = originalValue
                    End If
                Catch ex As Exception
                End Try
            End If
        End Sub

        Private Sub rdoUseExisting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoUseExisting.CheckedChanged
            rdoCreateNew.Checked = Not rdoUseExisting.Checked
            ddlExisting.Visible = rdoUseExisting.Checked
            lblExisting.Visible = rdoUseExisting.Checked
            If rdoUseExisting.Checked AndAlso ddlExisting.DataSource Is Nothing Then
                BindData_MapConfigurations()
                BindData_VisualProviders()
            End If
        End Sub


        Private Sub rdoUseExistingSource_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoUseExistingSource.CheckedChanged
            rdoCreateNewSource.Checked = Not rdoUseExistingSource.Checked
            ddlExistingSource.Visible = rdoUseExistingSource.Checked
            lblExistingSource.Visible = rdoUseExistingSource.Checked
            If rdoUseExistingSource.Checked AndAlso ddlExistingSource.DataSource Is Nothing Then
                BindData_SourceConfigurations()
                BindData_SourceProviders()
            End If
        End Sub

        Private Sub rdoCreateNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCreateNew.CheckedChanged
            rdoUseExisting.Checked = Not rdoCreateNew.Checked
            ddlExisting.Visible = rdoUseExisting.Checked
            lblExisting.Visible = rdoUseExisting.Checked
        End Sub

        Private Sub rdoCreateNewSource_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCreateNewSource.CheckedChanged
            rdoUseExistingSource.Checked = Not rdoCreateNewSource.Checked
            ddlExistingSource.Visible = rdoUseExistingSource.Checked
            lblExistingSource.Visible = rdoUseExistingSource.Checked
        End Sub

        Private Sub lnkSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSave.Click
            Dim iMapID As Integer = MapID
            Dim iSourceID As Integer = SourceID
            If rdoUseExisting.Checked Then
                'USE AN EXISTING MAP
                If IsNumeric(ddlExisting.Visible) Then
                    iMapID = CInt(ddlExisting.SelectedValue)
                End If
            Else
                iMapID = -1
            End If
            If rdoUseExistingSource.Checked Then
                'USE AN EXISTING SOURCE
                If IsNumeric(ddlExistingSource.Visible) Then
                    iSourceID = CInt(ddlExistingSource.SelectedValue)
                End If
            Else
                iSourceID = -1
            End If

            Dim iVisualProvider As Integer = 0
            Dim iDataProvider As Integer = 0
            Dim iGeoLocatorProvider As Integer = 0
            If Not ddlVisualProvider.SelectedValue Is Nothing Then
                iVisualProvider = ddlVisualProvider.SelectedValue
            End If
            If Not ddlVisualProviderSource.SelectedValue Is Nothing Then
                iDataProvider = ddlVisualProviderSource.SelectedValue
            End If
            If Not ddlVisualProviderGeoLocator.SelectedValue Is Nothing Then
                iGeoLocatorProvider = ddlVisualProviderGeoLocator.SelectedValue
            End If

            If SaveMap(iMapID, iVisualProvider, txtName.Text, txtDescription.Text) AndAlso SaveSource(iSourceID, iDataProvider, txtNameSource.Text, txtDescriptionSource.Text, iGeoLocatorProvider) Then
                Response.Redirect(MyBase.ControlPanelModule.EditUrl("ControlPanel"))
            End If
        End Sub

        Private Sub lnkCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCancel.Click
            MyBase.RemoveControl()
        End Sub

        Private Sub ddlExisting_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlExisting.SelectedIndexChanged
            BindData_Map(CInt(ddlExisting.SelectedValue))
            Me.ControlPanelModule.MapID = Me.MapID
            Me.ControlPanelModule.SourceID = Me.SourceID
        End Sub
        Private Sub ddlExistingSource_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlExistingSource.SelectedIndexChanged
            BindData_Source(CInt(ddlExistingSource.SelectedValue))
        End Sub
    End Class
End Namespace
