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
Imports DotNetNuke.UI.Utilities
Namespace DotNetNuke.Modules.Map.Controls
    Public Class MapControlPanel
        Inherits DotNetNuke.Entities.Modules.PortalModuleBase
        Implements DotNetNuke.UI.Utilities.IClientAPICallbackEventHandler
        Implements DotNetNuke.Entities.Modules.IActionable

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents imgButton_General As System.Web.UI.WebControls.ImageButton
        Protected WithEvents lnk_General As System.Web.UI.WebControls.LinkButton
        Protected WithEvents imgButton_Interface As System.Web.UI.WebControls.ImageButton
        Protected WithEvents lnk_Interface As System.Web.UI.WebControls.LinkButton
        Protected WithEvents imgButton_DataSource As System.Web.UI.WebControls.ImageButton
        Protected WithEvents lnk_DataSource As System.Web.UI.WebControls.LinkButton
        Protected WithEvents imgButton_Data As System.Web.UI.WebControls.ImageButton
        Protected WithEvents lnk_Data As System.Web.UI.WebControls.LinkButton
        Protected WithEvents imgButton_GeoLocator As System.Web.UI.WebControls.ImageButton
        Protected WithEvents lnk_GeoLocator As System.Web.UI.WebControls.LinkButton

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object
        Protected WithEvents ctrlPanelTable As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents lblInstructions As System.Web.UI.WebControls.Label
        Private _interaction As Interaction

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()

            InitializeControls()

            Me.EnsureChildControls()
        End Sub

#End Region
        Private Sub InitializeControls()
            If _ResourceFileName Is Nothing Then
                _ResourceFileName = DotNetNuke.Services.Localization.Localization.GetResourceFile(Me, "MapControlPanel.ascx")
            End If

            LoadInteraction()

            'ADD THE DATA MODULE
            If Not _DataControl Is Nothing Then
                Me.Controls.Add(_DataControl)
            End If
        End Sub
        Public Sub LoadInteraction()
            If MapID > 0 Or SourceID > 0 Then
                _interaction = New Interaction(MapID, SourceID)
            End If
        End Sub
        Private Property Current_ControlPanelItem() As String
            Get
                Return ViewState.Item("ControlPanel_CurrentItem")
            End Get
            Set(ByVal Value As String)
                ViewState.Item("ControlPanel_CurrentItem") = Value
            End Set
        End Property
        Private Property Current_ControlPanelItem_ID() As String
            Get
                Return ViewState.Item("ControlPanel_CurrentItemID")
            End Get
            Set(ByVal Value As String)
                ViewState.Item("ControlPanel_CurrentItemID") = Value
            End Set
        End Property
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            LocalizeInterface()

            LoadControlPanelItems()

            LoadCurrentControlSet()

            LoadDataIntegration()

        End Sub

        Private _ResourceFileName As String
        Protected Sub Localize(ByRef ControlToLocalize As Object, ByVal PropertyOfControl As String, ByVal ResourceKey As String)
            ControlToLocalize.GetType().GetProperty(PropertyOfControl).SetValue(ControlToLocalize, DotNetNuke.Services.Localization.Localization.GetString(ResourceKey, _ResourceFileName), Nothing)
        End Sub
        Protected Sub Localize(ByRef ControlToLocalize As Object, ByVal PropertyOfControl As String)
            ControlToLocalize.GetType().GetProperty(PropertyOfControl).SetValue(ControlToLocalize, DotNetNuke.Services.Localization.Localization.GetString(CType(ControlToLocalize, Web.UI.Control).ID, _ResourceFileName), Nothing)
        End Sub
        Protected Function Locale(ByVal Name As String) As String
            Return DotNetNuke.Services.Localization.Localization.GetString(Name, _ResourceFileName)
        End Function
        Private Sub LocalizeInterface()
            Localize(CType(lnk_General, Object), "Text", "lblGeneral")
            Localize(CType(imgButton_General, Object), "ImageUrl", "imgGeneral")
            Localize(CType(lnk_Data, Object), "Text", "lblData")
            Localize(CType(imgButton_Data, Object), "ImageUrl", "imgData")
            Localize(CType(lnk_DataSource, Object), "Text", "lblDataSource")
            Localize(CType(imgButton_DataSource, Object), "ImageUrl", "imgDataSource")
            Localize(CType(lnk_GeoLocator, Object), "Text", "lblGeoLocator")
            Localize(CType(imgButton_GeoLocator, Object), "ImageUrl", "imgGeoLocator")
            Localize(CType(lnk_Interface, Object), "Text", "lblInterface")
            Localize(CType(imgButton_Interface, Object), "ImageUrl", "imgInterface")
            Localize(CType(lblInstructions, Object), "Text", "lblInstructions")
        End Sub
        Public Property MapID() As Integer
            Get
                If Me.Settings.ContainsKey("MapID") Then
                    Return CType(Me.Settings("MapID"), Integer)
                Else
                    Return -1
                End If
            End Get
            Set(ByVal Value As Integer)
                Me.Settings.Item("MapID") = Value
            End Set
        End Property
        Public Property SourceID() As Integer
            Get
                If Me.Settings.ContainsKey("SourceID") Then
                    Return CType(Me.Settings("SourceID"), Integer)
                Else
                    Return -1
                End If
            End Get
            Set(ByVal Value As Integer)
                Me.Settings.Item("SourceID") = Value
            End Set
        End Property
        Public ReadOnly Property GeoLocatorID() As Integer
            Get
                If Me.Settings.ContainsKey("GeoLocatorID") Then
                    Return CType(Me.Settings("GeoLocatorID"), Integer)
                Else
                    Return -1
                End If
            End Get
        End Property

        Private Sub LoadControlPanelItems()
            If Not _interaction Is Nothing Then
                If _interaction.MapConfiguration Is Nothing OrElse _interaction.MapConfiguration.VisualProvider Is Nothing Then
                    ControlPanelItem_Enabled("Interface") = False
                Else
                    ControlPanelItem_Enabled("Interface") = True
                End If
                If _interaction.SourceConfiguration Is Nothing OrElse _interaction.SourceConfiguration.DataProvider Is Nothing Then
                    ControlPanelItem_Enabled("DataSource") = False
                    ControlPanelItem_Enabled("Data") = False
                    ControlPanelItem_Enabled("GeoLocator") = False
                Else
                    ControlPanelItem_Enabled("DataSource") = True
                    ControlPanelItem_Enabled("Data") = True
                    ControlPanelItem_Enabled("GeoLocator") = True
                End If
            Else
                ControlPanelItem_Enabled("Interface") = False
                ControlPanelItem_Enabled("DataSource") = False
                ControlPanelItem_Enabled("Data") = False
                ControlPanelItem_Enabled("Data") = False
                ControlPanelItem_Enabled("GeoLocator") = False
            End If
        End Sub
        Private Sub LoadCurrentControlSet()
            Try
                Dim currentControlPanelItem As String = Current_ControlPanelItem
                If Not currentControlPanelItem Is Nothing AndAlso currentControlPanelItem.Length > 0 Then
                    'SHOW THE CURRENT ITEM
                    Dim ctl As ControlPanelBase = Nothing
                    Select Case currentControlPanelItem
                        Case CPL_General
                            ctl = CType(Me.LoadControl("MapGeneral.ascx"), ControlPanelBase)
                        Case CPL_Data
                            ctl = CType(Me.LoadControl("MapData.ascx"), ControlPanelBase)
                        Case CPL_DataSource
                            ctl = CType(Me.LoadControl(_interaction.SourceAdminProviderSrc), ControlPanelBase)
                        Case CPL_GeoLocator
                            ctl = CType(Me.LoadControl(_interaction.GeolocatorAdminProviderSrc), ControlPanelBase)
                        Case CPL_Interface
                            ctl = CType(Me.LoadControl(_interaction.VisualAdminProviderSrc), ControlPanelBase)
                    End Select
                    ctl.ID = currentControlPanelItem
                    ctl.ControlPanelModule = Me
                    Me.Controls.Add(ctl)
                    Current_ControlPanelItem_ID = ctl.ID
                    Me.EnsureChildControls()
                End If
            Catch ex As Exception
                DotNetNuke.Services.Exceptions.ProcessModuleLoadException("The module failed to load the appropriate control panel item: " & ex.ToString, Me, ex, True)
            End Try
        End Sub

        Private Sub LoadDataIntegration()
            'REGISTER THE CLIENT API CALLBACK
            If Not _DataControl Is Nothing Then
                'these won't be necessary in next release after 3.2.0
                If ClientAPI.BrowserSupportsFunctionality(ClientAPI.ClientFunctionality.XMLHTTP) _
                  AndAlso ClientAPI.BrowserSupportsFunctionality(ClientAPI.ClientFunctionality.XML) Then
                    ClientAPI.RegisterClientReference(Me.Page, ClientAPI.ClientNamespaceReferences.dnn_xml)
                    ClientAPI.RegisterClientReference(Me.Page, ClientAPI.ClientNamespaceReferences.dnn_xmlhttp)

                    'Only this line will be necessary after 3.2
                    Dim readFunc As String = "function " & "MapRead" & "() {" & ClientAPI.GetCallbackEventReference(Me, _DataControl.JavascriptReadFunctionInit, JavascriptReadFunctionComplete, "this", JavascriptReadFunctionFailure) & "}"
                    Dim writeFunc As String = "function " & "MapWrite" & "() {" & ClientAPI.GetCallbackEventReference(Me, _DataControl.JavascriptWriteFunctionInit, JavascriptWriteFunctionComplete, "this", JavascriptWriteFunctionFailure) & "}"
                    Dim geoFunc As String = "function " & "MapGeo" & "() {" & ClientAPI.GetCallbackEventReference(Me, _DataControl.JavascriptGeoFunctionInit, JavascriptGeoFunctionComplete, "this", JavascriptGeoFunctionFailure) & "}"

                    If Me.Page.ClientScript.IsClientScriptBlockRegistered(_DataControl.JavascriptLibrary) = False Then
                        Me.Page.ClientScript.RegisterClientScriptBlock(GetType(String), _DataControl.JavascriptLibrary, "<script language=javascript src=""" & Me.ModulePath & "Sources/" & _DataControl.JavascriptLibrary & """></script>", False)
                    End If
                    If Me.Page.ClientScript.IsClientScriptBlockRegistered("MapCallback" & Me.ModuleId) = False Then
                        Me.Page.ClientScript.RegisterClientScriptBlock(GetType(String), "MapCallback" & Me.ModuleId, "<script language=javascript>" & readFunc & writeFunc & geoFunc & "</script>", False)
                    End If

                    setJavascript()
                End If
            End If
        End Sub

        Private m_DataControl As DataMapControlBase
        Private ReadOnly Property _DataControl() As DataMapControlBase
            Get
                If m_DataControl Is Nothing AndAlso Not _interaction Is Nothing AndAlso Not _interaction.SourceProviderSrc Is Nothing AndAlso _interaction.SourceProviderSrc.Length > 0 Then
                    Try
                        m_DataControl = CType(Me.LoadControl(_interaction.SourceProviderSrc), DataMapControlBase)
                        If Not m_DataControl Is Nothing Then
                            m_DataControl.Module = Me
                            m_DataControl.AdminMode = True
                        End If
                    Catch ex As Exception

                    End Try
                End If
                Return m_DataControl
            End Get
        End Property

        Public ReadOnly Property JavascriptReadFunctionComplete() As String
            Get
                Return "mapReadComplete"
            End Get
        End Property

        Public ReadOnly Property JavascriptReadFunctionFailure() As String
            Get
                Return "mapReadFailure"
            End Get
        End Property

        Public ReadOnly Property JavascriptGeoFunctionComplete() As String
            Get
                Return "mapGeoComplete"
            End Get
        End Property

        Public ReadOnly Property JavascriptGeoFunctionFailure() As String
            Get
                Return "mapGeoFailure"
            End Get
        End Property

        Public ReadOnly Property JavascriptWriteFunctionComplete() As String
            Get
                Return "mapWriteComplete"
            End Get
        End Property

        Public ReadOnly Property JavascriptWriteFunctionFailure() As String
            Get
                Return "mapWriteFailure"
            End Get
        End Property

        Public Function RaiseClientAPICallbackEvent(ByVal eventArgument As String) As String Implements UI.Utilities.IClientAPICallbackEventHandler.RaiseClientAPICallbackEvent
            Dim result As String = "No Source is available to return the data points"
            If Not _DataControl Is Nothing Then
                result = "Unknown Command, GEO, GET or SET anticipated - received: " & eventArgument
                If eventArgument.Length > 5 Then
                    Dim command As String = eventArgument.Substring(0, 4).ToUpper
                    eventArgument = eventArgument.Substring(4)
                    Select Case command
                        Case "GET:"
                            Return _DataControl.GetData(eventArgument)
                        Case "SET:"
                            Return _DataControl.SetData(eventArgument)
                        Case "GEO:"
                            Return _DataControl.GeoData(eventArgument)
                    End Select
                End If
            End If
            Return result
        End Function

        Public Sub RemoveControl(ByVal ID As String)
            Dim ctl As System.Web.UI.Control = FindControl(ID)
            If Not ctl Is Nothing Then
                Me.Controls.Remove(ctl)
                ctl.Dispose()
                Current_ControlPanelItem_ID = Nothing
                Current_ControlPanelItem = Nothing
                InitializeControls()
                Me.EnsureChildControls()
            End If
        End Sub
        Public Sub RemoveControl(ByRef ctl As ControlPanelBase)
            Me.Controls.Remove(ctl)
            ctl.Dispose()
            Current_ControlPanelItem_ID = Nothing
            Current_ControlPanelItem = Nothing
            InitializeControls()

            Me.EnsureChildControls()
        End Sub
#Region "Interaction - General"
        Private Sub imgButton_General_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgButton_General.Click
            onControlPanelItem_General()
        End Sub

        Private Sub lnk_General_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnk_General.Click
            onControlPanelItem_General()
        End Sub
#End Region
#Region "Interaction - Interface"
        Private Sub imgButton_Interface_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgButton_Interface.Click
            onControlPanelItem_Interface()
        End Sub

        Private Sub lnk_Interface_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnk_Interface.Click
            onControlPanelItem_Interface()
        End Sub
#End Region
#Region "Interaction - DataSource"
        Private Sub imgButton_DataSource_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgButton_DataSource.Click
            onControlPanelItem_DataSource()
        End Sub

        Private Sub lnk_DataSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnk_DataSource.Click
            onControlPanelItem_DataSource()
        End Sub
#End Region
#Region "Interaction - Data"
        Private Sub imgButton_Data_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgButton_Data.Click
            onControlPanelItem_Data()
        End Sub

        Private Sub lnk_Data_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnk_Data.Click
            onControlPanelItem_Data()
        End Sub

        Private Sub setJavascript()
            Dim sb As New Text.StringBuilder
            sb.Append("<SCRIPT language=""javascript"" src=""" & Me.ModulePath & "Map.js""></SCRIPT>" & vbCrLf)
            'sb.Append("<SCRIPT language=""javascript"" src=""" & GoMapPath & "Bi4ce.GoMap.Wizard.js""></SCRIPT>" & vbCrLf)
            sb.Append("<script language=""javascript"">" & vbCrLf)
            sb.Append("//<![CDATA[" & vbCrLf)

            'GENERATE REQUIRED FUNCTION
            sb.Append(" function " & Me.JavascriptReadFunctionComplete & "(result,ctx) { DATA" & ModuleId & "=result; Map_FetchEnd(" & ModuleId & ");}" & vbCrLf)
            sb.Append(" function " & Me.JavascriptReadFunctionFailure & "(result,ctx) { DATA" & ModuleId & "=result; Map_FetchEnd(" & ModuleId & ");}" & vbCrLf)
            sb.Append(" function " & Me.JavascriptWriteFunctionComplete & "(result,ctx) { DATA" & ModuleId & "=result; Map_WriteEnd(" & ModuleId & ");}" & vbCrLf)
            sb.Append(" function " & Me.JavascriptWriteFunctionFailure & "(result,ctx) { DATA" & ModuleId & "=result; Map_WriteEnd(" & ModuleId & ");}" & vbCrLf)
            sb.Append(" function " & Me.JavascriptGeoFunctionComplete & "(result,ctx) { DATA" & ModuleId & "=result; Map_FetchGeoEnd(" & ModuleId & ");}" & vbCrLf)
            sb.Append(" function " & Me.JavascriptGeoFunctionFailure & "(result,ctx) { DATA" & ModuleId & "=result; Map_FetchGeoEnd(" & ModuleId & ");}" & vbCrLf)

            'sb.Append(" function " & Me.JavascriptWriteFunctionFailure & "(result,ctx) { DATA" & ModuleId & "=result; Map_FetchEnd(" & ModuleId & ");}" & vbCrLf)


            sb.Append("//]]>" & vbCrLf)
            sb.Append("</script>" & vbCrLf)

            Page.ClientScript.RegisterClientScriptBlock(GetType(String), "MapControlPanel", sb.ToString)
            sb = Nothing
        End Sub
#End Region
#Region "Interaction - GeoLocator"
        Private Sub imgButton_GeoLocator_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgButton_GeoLocator.Click
            onControlPanelItem_GeoLocator()
        End Sub

        Private Sub lnk_GeoLocator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnk_GeoLocator.Click
            onControlPanelItem_GeoLocator()
        End Sub
#End Region

#Region "Interaction Handlers"
        Private Const CPL_General As String = "ControlPanelItem_General"
        Private Const CPL_Interface As String = "ControlPanelItem_Interface"
        Private Const CPL_DataSource As String = "ControlPanelItem_DataSource"
        Private Const CPL_Data As String = "ControlPanelItem_Data"
        Private Const CPL_GeoLocator As String = "ControlPanelItem_GeoLocator"
        Private Function CheckFooterIsVisible(ByVal name As String) As Boolean
            If Not name Is Nothing Then
                If Not Me.FindControl(CPL_General) Is Nothing Then
                    Return True
                End If
            Else
                If Not Me.FindControl(name) Is Nothing Then
                    Return True
                End If
            End If
            Return False
        End Function
        Private Sub onControlPanelItem_Toggle(ByVal Name As String)
            If Not Current_ControlPanelItem_ID Is Nothing Then
                RemoveControl(Current_ControlPanelItem_ID)
            End If
            If Not Name Is Nothing Then
                Current_ControlPanelItem = Name
            Else
                Current_ControlPanelItem = Nothing
            End If
            LoadCurrentControlSet()
        End Sub
        Private Sub onControlPanelItem_General()
            onControlPanelItem_Toggle(CPL_General)
        End Sub
        Private Sub onControlPanelItem_Interface()
            onControlPanelItem_Toggle(CPL_Interface)
        End Sub
        Private Sub onControlPanelItem_DataSource()
            onControlPanelItem_Toggle(CPL_DataSource)
        End Sub
        Private Sub onControlPanelItem_Data()
            onControlPanelItem_Toggle(CPL_Data)
        End Sub
        Private Sub onControlPanelItem_GeoLocator()
            onControlPanelItem_Toggle(CPL_GeoLocator)
        End Sub
        Private Property ControlPanelItem_Enabled(ByVal Name As String) As Boolean
            Get
                Return CType(FindControlByNameType(Name, "imgButton"), Web.UI.WebControls.ImageButton).Enabled
            End Get
            Set(ByVal Value As Boolean)
                CType(FindControlByNameType(Name, "imgButton"), Web.UI.WebControls.ImageButton).Enabled = Value
                CType(FindControlByNameType(Name, "lnk"), Web.UI.WebControls.LinkButton).Enabled = Value
            End Set
        End Property
        Protected Overridable Function FindControlByNameType(ByVal Name As String, ByVal Type As String) As System.Web.UI.Control
            Dim propInfo As System.Reflection.PropertyInfo
            propInfo = Me.GetType().GetProperty(Type & "_" & Name, System.Reflection.BindingFlags.IgnoreCase Or System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Public)
            If Not propInfo Is Nothing Then
                Dim value As Object = propInfo.GetValue(Me, Nothing)
                If TypeOf value Is System.Web.UI.Control Then
                    Return CType(value, System.Web.UI.Control)
                End If
            End If
            Return Nothing
        End Function
#End Region

        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim MyActions As New Entities.Modules.Actions.ModuleActionCollection
                If IsEditable Then
                    MyActions.Add(GetNextActionID, Locale("MenuViewMap"), "", Url:=DotNetNuke.Common.NavigateURL(Me.TabId), Secure:=DotNetNuke.Security.SecurityAccessLevel.View, Visible:=True)
                End If
                Return MyActions
            End Get
        End Property
    End Class
End Namespace