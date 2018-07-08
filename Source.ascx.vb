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
    Public Class MapSource
        Inherits ControlPanelBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblName As System.Web.UI.WebControls.Label
        Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
        Protected WithEvents lblExisting As System.Web.UI.WebControls.Label
        Protected WithEvents ddlExisting As System.Web.UI.WebControls.DropDownList
        Protected WithEvents rdoUseExisting As System.Web.UI.WebControls.RadioButton
        Protected WithEvents rdoCreateNew As System.Web.UI.WebControls.RadioButton
        Protected WithEvents ddlVisualProvider As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lblMapInfo As System.Web.UI.WebControls.Label
        Protected WithEvents lblDynamicProvider As System.Web.UI.WebControls.Label
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

            Localize(CType(rdoUseExisting, Object), "Text", "rdoUseExisting")
            Localize(CType(rdoCreateNew, Object), "Text", "rdoCreateNew")
            Localize(CType(lblExisting, Object), "Text", "lblExisting")
            Localize(CType(lblName, Object), "Text", "lbName")
            Localize(CType(lblDescription, Object), "Text", "lblDescription")
            Localize(CType(lblDynamicProvider, Object), "Text", "lblDynamicProvider")
            Localize(CType(lblMapInfo, Object), "Text", "lblMapInfo")
            Localize(CType(lblInstructions, Object), "Text", "lblInstructions")
        End Sub

        Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
            If Not MapConfiguration Is Nothing Then
                'LOAD CONFIGURATION SETTINGS
            Else
                'LOAD DEFAULTS
            End If
            BindData_MapConfigurations()
            BindData_VisualProviders()
        End Sub

        Private Sub BindData_MapConfigurations()
            If ddlExisting.DataSource Is Nothing Then
                Dim mapDC As New MapController
                Dim arr As ArrayList = mapDC.ListMaps(ControlPanelModule.PortalId)
                If Not arr Is Nothing Then
                    ddlExisting.DataSource = arr
                    ddlExisting.DataTextField = "Name"
                    ddlExisting.DataValueField = "MapID"
                    ddlExisting.DataBind()
                End If
            End If
        End Sub
        Private Sub BindData_VisualProviders()
            If ddlVisualProvider.DataSource Is Nothing Then
                Dim mapDC As New MapController
                Dim arr As ArrayList = mapDC.ListProviders(DotNetNuke.Modules.Map.Data.DynamicProviderInfo.DataType_Visual)
                If Not arr Is Nothing Then
                    ddlVisualProvider.DataSource = arr
                    ddlVisualProvider.DataTextField = "Name"
                    ddlVisualProvider.DataValueField = "ProviderID"
                    ddlVisualProvider.DataBind()
                End If
            End If
        End Sub
    End Class
End Namespace
