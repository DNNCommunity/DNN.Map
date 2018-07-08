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
Namespace DotNetNuke.Modules.Map.Data
    Public Class StandardAdmin
        Inherits DotNetNuke.Modules.Map.Components.ControlPanelBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents lnkSave As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkCancel As System.Web.UI.WebControls.LinkButton
        Protected lnkAddQueryVarable As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lblSourceInfo As System.Web.UI.WebControls.Label
        Protected WithEvents rdoDataSourceUserOnline As System.Web.UI.WebControls.RadioButton
        Protected WithEvents rdoDataSourceDataPoints As System.Web.UI.WebControls.RadioButton
        Protected WithEvents rdoDataSourceCustom As System.Web.UI.WebControls.RadioButton
        Protected WithEvents lblCustomQuery As System.Web.UI.WebControls.Label
        Protected WithEvents txtCustomQuery As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblCustomConnection As System.Web.UI.WebControls.Label
        Protected WithEvents txtCustomConnection As System.Web.UI.WebControls.TextBox
        Protected WithEvents rptQueryOptions As System.Web.UI.WebControls.Repeater
        Protected WithEvents lblQueryTarget As System.Web.UI.WebControls.Label
        Protected WithEvents ddlVariableType As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtQuerySource As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtQueryTarget As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtQueryTargetLeft As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtQueryTargetRight As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtQueryTargetEmpty As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkQuerySQLInjection As System.Web.UI.WebControls.CheckBox
        Protected WithEvents lnkSaveQueryOptions As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkCancelQueryOptions As System.Web.UI.WebControls.LinkButton
        Protected WithEvents pnlQueryEdit As System.Web.UI.WebControls.Panel
        Protected WithEvents pnlrdoDataSourceCustom As System.Web.UI.WebControls.Panel
        Protected WithEvents pnlrdoDataSourceUserOnline As System.Web.UI.WebControls.Panel
        Protected WithEvents pnlrdoDataSourceDataPoints As System.Web.UI.WebControls.Panel
        Protected WithEvents pnlService As System.Web.UI.WebControls.Panel
        Protected WithEvents lblQueryVariables As System.Web.UI.WebControls.Label
        Protected WithEvents lblQueryEditor As System.Web.UI.WebControls.Label
        Protected WithEvents lblQEType As System.Web.UI.WebControls.Label
        Protected WithEvents lblQESource As System.Web.UI.WebControls.Label
        Protected WithEvents lblQETarget As System.Web.UI.WebControls.Label
        Protected WithEvents lblQELeft As System.Web.UI.WebControls.Label
        Protected WithEvents lblQERight As System.Web.UI.WebControls.Label
        Protected WithEvents lblQEEmpty As System.Web.UI.WebControls.Label
        Protected WithEvents lblQESecurity As System.Web.UI.WebControls.Label
        Protected WithEvents lblPanelName As System.Web.UI.WebControls.Label
        Protected WithEvents lblInstructions As System.Web.UI.WebControls.Label
        Protected WithEvents lblPointContent As System.Web.UI.WebControls.Label
        Protected WithEvents lblCustomQueryInstructions As System.Web.UI.WebControls.Label
        Protected WithEvents lblUserOptionsInstructions As System.Web.UI.WebControls.Label
        Protected WithEvents lblServiceInstructions As System.Web.UI.WebControls.Label
        Protected WithEvents lblStandardOptions As System.Web.UI.WebControls.Label
        Protected WithEvents lblStandardInstructions As System.Web.UI.WebControls.Label
        Protected WithEvents lblService As System.Web.UI.WebControls.Label
        Protected WithEvents lblServiceBreak As System.Web.UI.WebControls.Label
        Protected WithEvents txtPointContent As UI.UserControls.TextEditor
        Protected WithEvents lblUserOptions As System.Web.UI.WebControls.Label
        Protected WithEvents lnkUserService As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkGeoService As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkClearService As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lblServiceBreakClear As System.Web.UI.WebControls.Label
        Protected WithEvents chkSummaryOnly As System.Web.UI.WebControls.CheckBox
        Protected WithEvents lblDataSourceCustom_Error As System.Web.UI.WebControls.Label
        Protected WithEvents pnlrdoDataSourceCustom_Error As System.Web.UI.WebControls.Panel

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
            Localize(CType(lblInstructions, Object), "Text")
            Localize(CType(rdoDataSourceDataPoints, Object), "Text")
            Localize(CType(rdoDataSourceUserOnline, Object), "Text")
            Localize(CType(rdoDataSourceCustom, Object), "Text")

            Localize(CType(chkQuerySQLInjection, Object), "Text")
            Localize(CType(lblCustomConnection, Object), "Text")
            Localize(CType(lblCustomQuery, Object), "Text")
            Localize(CType(lblQEEmpty, Object), "Text")
            Localize(CType(lblQELeft, Object), "Text")
            Localize(CType(lblQERight, Object), "Text")
            Localize(CType(lblQESecurity, Object), "Text")
            Localize(CType(lblQESource, Object), "Text")
            Localize(CType(lblQETarget, Object), "Text")
            Localize(CType(lblQEType, Object), "Text")
            Localize(CType(lblQueryEditor, Object), "Text")
            Localize(CType(lblQueryTarget, Object), "Text")
            Localize(CType(lblQueryVariables, Object), "Text")

            Localize(CType(lblPointContent, Object), "Text")

            Localize(CType(lblSourceInfo, Object), "Text")
            Localize(CType(lblUserOptions, Object), "Text")
            Localize(CType(lnkUserService, Object), "Text")
            Localize(CType(lnkGeoService, Object), "Text")

            Localize(CType(chkSummaryOnly, Object), "Text")

            Try
                Localize(CType(lnkAddQueryVarable, Object), "Text")
            Catch ex As Exception

            End Try

            Localize(CType(lnkCancelQueryOptions, Object), "Text")
            Localize(CType(lnkSaveQueryOptions, Object), "Text")

            Localize(CType(lblUserOptionsInstructions, Object), "Text")
            Localize(CType(lblServiceInstructions, Object), "Text")
            Localize(CType(lblCustomQueryInstructions, Object), "Text")
            Localize(CType(lblStandardOptions, Object), "Text")
            Localize(CType(lblStandardInstructions, Object), "Text")
            Localize(CType(lblServiceBreak, Object), "Text")
            Localize(CType(lblService, Object), "Text")
            Localize(CType(lblServiceBreakClear, Object), "Text")
            Localize(CType(lnkClearService, Object), "Text")
            Localize(CType(lblDataSourceCustom_Error, Object), "Text")


            Localize(CType(lnkCancel, Object), "Text")
            Localize(CType(lnkSave, Object), "Text")
        End Sub



        Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
            If Not isLoaded Then
                If Not SourceConfiguration.Source Is Nothing Then
                    BindData_Source(SourceConfiguration.Source, True)
                Else
                    BindData_Source(-1, True)
                End If
            End If
        End Sub

        Private Sub BindData_Source(ByRef sourceinfo As SourceInfo, ByVal LoadInterface As Boolean)
            If Not sourceinfo Is Nothing AndAlso sourceinfo.PortalID = ControlPanelModule.PortalId Then
                'LOAD THE SETTING
                pnlService.Enabled = False
                If Not sourceinfo.Settings Is Nothing Then
                    Try
                        Dim sinfo As Map.Data.Standard.Configuration = Map.Data.Standard.Configuration.Deserialize(sourceinfo.Settings)
                        SourceConfiguration.Settings = sinfo
                        If LoadInterface Then
                            rdoDataSourceDataPoints.Checked = False
                            rdoDataSourceUserOnline.Checked = False
                            pnlrdoDataSourceCustom.Visible = False
                            pnlrdoDataSourceUserOnline.Visible = False
                            pnlrdoDataSourceDataPoints.Visible = False
                            chkSummaryOnly.Checked = sinfo.SummaryOnly
                            txtPointContent.Text = sinfo.Format
                            lnkGeoService.Visible = True
                            Select Case sinfo.RenderType
                                Case 1
                                    lblDataSourceCustom_Error.Visible = False
                                    pnlrdoDataSourceCustom_Error.Visible = False
                                    rdoDataSourceDataPoints.Checked = True
                                    pnlrdoDataSourceDataPoints.Visible = True
                                    lnkUserService.Visible = False
                                    lblServiceBreak.Visible = False
                                    lblServiceBreakClear.Visible = True
                                    lnkClearService.Visible = True
                                Case 2
                                    lblDataSourceCustom_Error.Visible = False
                                    pnlrdoDataSourceCustom_Error.Visible = False
                                    rdoDataSourceUserOnline.Checked = True
                                    pnlrdoDataSourceUserOnline.Visible = True
                                    lnkUserService.Visible = True
                                    lblServiceBreak.Visible = True
                                    lblServiceBreakClear.Visible = True
                                    lnkClearService.Visible = True
                                Case 3
                                    'ONLY SUPER USERS CAN HAVE THIS AUTHORITY
                                    If Me.ControlPanelModule.UserInfo.IsSuperUser Then
                                        lblDataSourceCustom_Error.Visible = False
                                        pnlrdoDataSourceCustom_Error.Visible = False
                                        rdoDataSourceCustom.Checked = True
                                        txtCustomConnection.Text = sinfo.ConnectionString
                                        txtCustomQuery.Text = sinfo.CustomQuery
                                        pnlrdoDataSourceCustom.Visible = True
                                        lnkUserService.Visible = False
                                        lblServiceBreak.Visible = False
                                        lblServiceBreakClear.Visible = False
                                        lnkClearService.Visible = False
                                    Else
                                        rdoDataSourceCustom.Checked = True
                                        pnlrdoDataSourceCustom.Visible = False
                                        lnkUserService.Visible = False
                                        lblServiceBreak.Visible = False
                                        lblServiceBreakClear.Visible = False
                                        lnkClearService.Visible = False

                                        lblDataSourceCustom_Error.Visible = True
                                        pnlrdoDataSourceCustom_Error.Visible = True
                                    End If
                                Case Else
                            End Select

                            If sourceinfo.GeoLocatorProviderID > 0 Then
                                pnlService.Enabled = True
                            End If

                            BindData_QueryVariables(False)
                        End If
                    Catch ex As Exception
                        DotNetNuke.Services.Exceptions.ProcessModuleLoadException("Unable to deserialize the Map Source Configuration.", Me, ex, True)
                    End Try
                Else
                    SourceConfiguration.Settings = New Map.Data.Standard.Configuration
                    If LoadInterface Then
                        rdoDataSourceDataPoints.Checked = False
                        rdoDataSourceUserOnline.Checked = True
                        lnkUserService.Visible = True
                        lblServiceBreak.Visible = True
                        pnlService.Enabled = False
                        BindData_QueryVariables(False)
                    End If
                End If
            End If
        End Sub
        Private Sub BindData_Source(ByVal SourceID As Integer, ByVal LoadInterface As Boolean)
            If SourceID > 0 Then
                Dim mapDC As New MapController
                Dim sourceInfo As sourceInfo
                sourceInfo = mapDC.GetSource(SourceID)
                BindData_Source(sourceInfo, LoadInterface)
                sourceInfo = Nothing
                mapDC = Nothing
            Else
                If LoadInterface Then
                    rdoDataSourceDataPoints.Checked = True
                    rdoDataSourceUserOnline.Checked = False
                    lnkUserService.Visible = False
                    lblServiceBreak.Visible = False
                    pnlService.Enabled = False
                    BindData_QueryVariables(False)
                End If
            End If
        End Sub


        Private Sub lnkSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSave.Click
            BindData_Source(SourceID, False)
            Dim sinfo As Map.Data.Standard.Configuration
            If Me.SourceConfiguration.Settings Is Nothing Then
                sinfo = New Map.Data.Standard.Configuration
            Else
                sinfo = SourceConfiguration.Settings
            End If

            sinfo.Format = txtPointContent.Text

           If rdoDataSourceDataPoints.Checked = True Then
                sinfo.RenderType = 1
                sinfo.ConnectionStringIsName = False
                sinfo.CustomQuery = ""
                sinfo.ConnectionString = ""
                sinfo.SummaryOnly = False
            ElseIf rdoDataSourceUserOnline.Checked = True Then
                sinfo.RenderType = 2
                sinfo.ConnectionStringIsName = False
                sinfo.CustomQuery = ""
                sinfo.ConnectionString = ""
                sinfo.SummaryOnly = chkSummaryOnly.Checked
            ElseIf rdoDataSourceCustom.Checked = True Then
                'ONLY SUPER USERS CAN HAVE THIS AUTHORITY
                If Me.ControlPanelModule.UserInfo.IsSuperUser Then
                    sinfo.RenderType = 3
                    sinfo.ConnectionStringIsName = False
                    sinfo.CustomQuery = txtCustomQuery.Text
                    sinfo.ConnectionString = txtCustomConnection.Text

                    If ControlPanelModule.UserInfo.IsSuperUser Then
                        sinfo.ConnectionStringIsName = True
                    End If
                    sinfo.SummaryOnly = False
                End If
                sinfo.SummaryOnly = False
            End If

            Dim _providerConfiguration As Framework.Providers.ProviderConfiguration = Framework.Providers.ProviderConfiguration.GetProviderConfiguration("data")
            Dim objProvider As Framework.Providers.Provider = CType(_providerConfiguration.Providers(_providerConfiguration.DefaultProvider), Framework.Providers.Provider)

            sinfo.ObjectQualifier = objProvider.Attributes("objectQualifier")
            If sinfo.ObjectQualifier <> "" And sinfo.ObjectQualifier.EndsWith("_") = False Then
                sinfo.ObjectQualifier += "_"
            End If

            sinfo.DatabaseOwner = objProvider.Attributes("databaseOwner")
            If sinfo.DatabaseOwner <> "" And sinfo.DatabaseOwner.EndsWith(".") = False Then
                sinfo.DatabaseOwner += "."
            End If

            SourceConfiguration.Source.Settings = Standard.Configuration.Serialize(sinfo)

            SourceConfiguration.Save()
            MyBase.RemoveControl()

        End Sub

        Private Sub lnkCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCancel.Click
            MyBase.RemoveControl()
        End Sub
#Region "Query Variables"

        Private Property EditItem() As Integer
            Get
                Return viewstate.Item("EditItem")
            End Get
            Set(ByVal Value As Integer)
                viewstate.Item("EditItem") = Value
            End Set
        End Property

        Public Function ModuleImageURL(ByVal Src As String) As String
            If Not ModulePath Is Nothing Then
                If ModulePath.EndsWith("/") Or ModulePath.EndsWith("\") Then
                    Return ModulePath & Src
                Else
                    Return ModulePath & "/" & Src
                End If
            Else
                Return "~/images/" & Src
            End If
        End Function
        Public Function BoolURL(ByVal Value As Boolean) As String
            If Value Then
                Return ModuleImageURL("checked.gif")
            Else
                Return ModuleImageURL("unchecked.gif")
            End If
        End Function


        Private Function Build_QueryVariable() As Standard.Configuration.QueryVariable
            Dim qvobj As New Standard.Configuration.QueryVariable

            qvobj.Source = txtQuerySource.Text
            qvobj.Target = txtQueryTarget.Text
            qvobj.VariableType = ddlVariableType.SelectedValue
            qvobj.TargetLeft = txtQueryTargetLeft.Text
            qvobj.TargetRight = txtQueryTargetRight.Text
            qvobj.TargetEmpty = txtQueryTargetEmpty.Text
            qvobj.EscapeSingleQuotes = chkQuerySQLInjection.Checked

            Return qvobj
        End Function


        Private Sub BindData_QueryVariables(ByVal ForceSave As Boolean)
            Dim src As Map.Data.Standard.Configuration
            If Me.SourceConfiguration Is Nothing Then
                BindData_Source(SourceID, False)
            End If

            If Me.SourceConfiguration.Settings Is Nothing Then
                src = New Map.Data.Standard.Configuration
            Else
                src = CType(SourceConfiguration.Settings, Standard.Configuration)
            End If

            src.SequenceQueryItems()

            If ForceSave Then
                SourceConfiguration.Source.Settings = Standard.Configuration.Serialize(src)
                SourceConfiguration.Save()
            End If

            rptQueryOptions.DataSource = src.QueryVariables
            rptQueryOptions.DataBind()
        End Sub

        Private Sub rptQueryOptions_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptQueryOptions.ItemCommand
            BindData_Source(SourceID, False)
            Dim src As Standard.Configuration = CType(SourceConfiguration.Settings, Standard.Configuration)
            src.SequenceQueryItems()

            Dim index As Integer
            If Not e.CommandArgument Is Nothing AndAlso IsNumeric(e.CommandArgument) Then
                index = CInt(e.CommandArgument)
                If index >= 0 And index < src.QueryVariables.Count Then
                    Select Case e.CommandName
                        Case "Edit"
                            'Edit the item
                            Dim tbiMe As Standard.Configuration.QueryVariable = src.QueryVariables(index)
                            EditQueryOptionItem(tbiMe)
                            pnlQueryEdit.Visible = True
                        Case "Delete"
                            'Delete the item
                            src.QueryVariables.RemoveAt(index)

                            BindData_QueryVariables(True)
                    End Select
                End If
            Else
                If Not e.CommandName Is Nothing Then
                    Select Case e.CommandName
                        Case "Add"
                            pnlQueryEdit.Visible = True

                            txtQuerySource.Text = ""
                            txtQueryTarget.Text = ""
                            lblQueryTarget.Text = ""
                            txtQueryTargetEmpty.Text = ""
                            txtQueryTargetLeft.Text = ""
                            txtQueryTargetRight.Text = ""

                            chkQuerySQLInjection.Checked = True

                            ddlVariableType.SelectedIndex = -1

                            BindData_QueryVariables(True)

                            EditItem = src.QueryVariables.Count + 1
                    End Select
                End If
            End If
        End Sub

        Private Sub EditQueryOptionItem(ByRef Value As Standard.Configuration.QueryVariable)
            Try
                EditItem = Value.Index + 1
                lblQueryTarget.Text = " - [" + Value.Target + "]"
                txtQuerySource.Text = Value.Source
                txtQueryTarget.Text = Value.Target

                If Value.TargetEmpty Is Nothing Then
                    Value.TargetEmpty = ""
                End If
                If Value.TargetLeft Is Nothing Then
                    Value.TargetLeft = ""
                End If
                If Value.TargetRight Is Nothing Then
                    Value.TargetRight = ""
                End If
                txtQueryTargetEmpty.Text = Value.TargetEmpty
                txtQueryTargetLeft.Text = Value.TargetLeft
                txtQueryTargetRight.Text = Value.TargetRight

                chkQuerySQLInjection.Checked = Value.EscapeSingleQuotes

                ddlVariableType.SelectedIndex = -1
                ddlVariableType.SelectedValue = Value.VariableType
            Catch ex As Exception
                DotNetNuke.Services.Exceptions.LogException(ex)
            End Try

        End Sub

        Private Sub lnkSaveQueryOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSaveQueryOptions.Click
            BindData_Source(SourceID, False)

            Dim src As Standard.Configuration = CType(SourceConfiguration.Settings, Standard.Configuration)
            src.SequenceQueryItems()

            Dim tbi As Standard.Configuration.QueryVariable = Build_QueryVariable()
            Dim index As Integer = CInt(EditItem)
            If index > src.QueryVariables.Count Then
                'NEW ITEM
                tbi.Index = src.QueryVariables.Count
                src.QueryVariables.Add(tbi)
            Else
                'EXISTING ITEM
                tbi.Index = index - 1
                src.QueryVariables(index - 1) = tbi
            End If

            BindData_QueryVariables(True)

            pnlQueryEdit.Visible = False
        End Sub

        Private Sub lnkCancelQueryOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCancelQueryOptions.Click
            BindData_Source(SourceID, False)
            BindData_QueryVariables(False)

            pnlQueryEdit.Visible = False
        End Sub
#End Region

        Private Sub rdoDataSourceUserOnline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoDataSourceUserOnline.CheckedChanged
            If rdoDataSourceUserOnline.Checked Then
                lblDataSourceCustom_Error.Visible = False
                pnlrdoDataSourceCustom_Error.Visible = False

                pnlrdoDataSourceUserOnline.Visible = True
                pnlrdoDataSourceCustom.Visible = False
                pnlrdoDataSourceDataPoints.Visible = False
                lnkUserService.Visible = True
                lblServiceBreak.Visible = True
                lblServiceBreakClear.Visible = True
                lnkClearService.Visible = True
            Else
                lnkUserService.Visible = False
                lblServiceBreak.Visible = False
                pnlrdoDataSourceUserOnline.Visible = False
            End If
        End Sub

        Private Sub rdoDataSourceDataPoints_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoDataSourceDataPoints.CheckedChanged
            If rdoDataSourceDataPoints.Checked Then
                lblDataSourceCustom_Error.Visible = False
                pnlrdoDataSourceCustom_Error.Visible = False

                pnlrdoDataSourceCustom.Visible = False
                pnlrdoDataSourceUserOnline.Visible = False
                pnlrdoDataSourceDataPoints.Visible = True
                lnkUserService.Visible = False
                lblServiceBreak.Visible = False
                lblServiceBreakClear.Visible = True
                lnkClearService.Visible = True
            Else
                pnlrdoDataSourceDataPoints.Visible = False
            End If
        End Sub

        Private Sub rdoDataSourceCustom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoDataSourceCustom.CheckedChanged
            If rdoDataSourceCustom.Checked Then
                If Me.ControlPanelModule.UserInfo.IsSuperUser Then
                    lblDataSourceCustom_Error.Visible = False
                    pnlrdoDataSourceCustom_Error.Visible = False
                    pnlrdoDataSourceCustom.Visible = True
                    pnlrdoDataSourceUserOnline.Visible = False
                    pnlrdoDataSourceDataPoints.Visible = False
                    lnkUserService.Visible = False
                    lblServiceBreak.Visible = False
                    lblServiceBreakClear.Visible = False
                    lnkClearService.Visible = False
                Else
                    pnlrdoDataSourceCustom.Visible = False
                    pnlrdoDataSourceUserOnline.Visible = False
                    pnlrdoDataSourceDataPoints.Visible = False
                    lnkUserService.Visible = False
                    lblServiceBreak.Visible = False
                    lblServiceBreakClear.Visible = False
                    lnkClearService.Visible = False

                    lblDataSourceCustom_Error.Visible = True
                    pnlrdoDataSourceCustom_Error.Visible = True
                End If
            Else
                pnlrdoDataSourceCustom.Visible = False
            End If
        End Sub
        Private Sub RunService(ByVal serviceType As String)
            If serviceType Is Nothing OrElse serviceType = "USER" Then
                Dim srv As New AutoUserCoder
                srv.Start(Me.SourceID)
            ElseIf serviceType = "CLEAR" Then
                BindData_Source(SourceID, False)
                If Not Me.SourceConfiguration.Settings Is Nothing Then
                    Dim s As New Standard(SourceConfiguration.Settings)
                    s.DeletePoints(SourceID)
                End If
                pnlService.Enabled = True
            Else
                Dim srv As New AutoGeoCoder
                srv.Start(Me.SourceID)
            End If
        End Sub

        Private Sub lnkUserService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkUserService.Click
            RunService("USER")
        End Sub

        Private Sub lnkGeoService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkGeoService.Click
            RunService("GEO")
        End Sub

        Private Sub lnkClearService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkClearService.Click
            RunService("CLEAR")
        End Sub
    End Class
End Namespace
