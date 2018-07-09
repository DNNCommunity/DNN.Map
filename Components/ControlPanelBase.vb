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
Imports DotNetNuke.Modules.Map.Data
Namespace DotNetNuke.Modules.Map.Components
    Public Class ControlPanelBase
        Inherits System.Web.UI.UserControl

#Region "Public Member"
        Public ReadOnly Property VisualProviderSrc() As String
            Get
                'LOAD THE VISUAL UI PROVIDER INFORMATION FOR THE CURRENT PROVIDER
                'IF NONE IS YET ASSIGNED, USE THE DEFAULT
                If Not _Interaction Is Nothing Then
                    Return _Interaction.VisualProviderSrc
                Else
                    Return "~/DesktopModules/Map/Visuals/Standard.ascx"
                End If
            End Get
        End Property
        Public ReadOnly Property SourceProviderSrc() As String
            Get
                'LOAD THE SOURCE PROVIDER INFORMATION (BUSINESS LOGIC) FOR THE CURRENT PROVIDER
                'IF NOT IS YET ASSIGNED, USE THE DEFAULT
                If Not _Interaction Is Nothing Then
                    Return _Interaction.SourceProviderSrc
                Else
                    Return "~/DesktopModules/Map/Source/Standard.ascx"
                End If
            End Get
        End Property
#End Region

        Private _module As DotNetNuke.Entities.Modules.PortalModuleBase
        Public Property ControlPanelModule() As Map.Controls.MapControlPanel
            Get
                Return _module
            End Get
            Set(ByVal Value As Map.Controls.MapControlPanel)
                _module = Value
            End Set
        End Property

        Public ReadOnly Property ModulePath() As String
            Get
                Return _module.ModulePath
            End Get
        End Property

#Region "Public Properties"
        Private _Interaction As Interaction

        Public ReadOnly Property BasePage() As DotNetNuke.Framework.CDefault
            Get
                Return CType(Me.Page, DotNetNuke.Framework.CDefault)
            End Get
        End Property
        Public ReadOnly Property isLoaded() As Boolean
            Get
                If Me.ViewState.Item("isLoaded") Is Nothing Then
                    Me.ViewState.Item("isLoaded") = True
                    Return False
                Else
                    Return True
                End If
            End Get
        End Property
        Public Property MapConfiguration() As MapConfiguration
            Get
                If Not _Interaction Is Nothing Then
                    Return _Interaction.MapConfiguration
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal Value As MapConfiguration)
                If Not _Interaction Is Nothing Then
                    _Interaction.MapConfiguration = Value
                Else
                    Dim dc As New MapController
                    Value.Map.MapID = dc.AddMap(Value.Map)
                    Value.Save()
                End If
            End Set
        End Property
        Public Property SourceConfiguration() As SourceConfiguration
            Get
                If Not _Interaction Is Nothing Then
                    Return _Interaction.SourceConfiguration
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal Value As SourceConfiguration)
                If Not _Interaction Is Nothing Then
                    _Interaction.SourceConfiguration = Value
                Else
                    Dim dc As New MapController
                    Value.Source.SourceID = dc.AddSource(Value.Source)
                    Value.Save()
                End If
            End Set
        End Property
        Public Property GeolocatorConfiguration() As GeoLocatorConfiguration
            Get
                If Not _Interaction Is Nothing Then
                    Return _Interaction.GeoLocatorConfiguration
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal Value As GeolocatorConfiguration)
                If Not _Interaction Is Nothing Then
                    _Interaction.GeoLocatorConfiguration = Value
                End If
            End Set
        End Property
        Public Property MapID() As Integer
            Get
                If _module.Settings.ContainsKey("MapID") Then
                    Return CType(_module.Settings("MapID"), Integer)
                Else
                    Return -1
                End If
            End Get
            Set(ByVal Value As Integer)
                Dim mc As New DotNetNuke.Entities.Modules.ModuleController
                mc.UpdateModuleSetting(ControlPanelModule.ModuleConfiguration.ModuleID, "MapID", Value)
                _module.Settings("MapID") = Value
            End Set
        End Property
        Public Property SourceID() As Integer
            Get
                If _module.Settings.ContainsKey("SourceID") Then
                    Return CType(_module.Settings("SourceID"), Integer)
                Else
                    Return -1
                End If
            End Get
            Set(ByVal Value As Integer)
                Dim mc As New DotNetNuke.Entities.Modules.ModuleController
                mc.UpdateModuleSetting(ControlPanelModule.ModuleConfiguration.ModuleID, "SourceID", Value)
                _module.Settings("SourceID") = Value
            End Set
        End Property
#End Region

#Region "Page Events"
        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If mResourceFileName Is Nothing Then
                mResourceFileName = DotNetNuke.Services.Localization.Localization.GetResourceFile(Me, Me.GetType().Name.Remove(Me.GetType.Name.Length - 5, 5) & ".ascx")
            End If
            If MapID > 0 OrElse SourceID > 0 Then
                _Interaction = New Interaction(MapID, SourceID)
            End If
        End Sub

        Public mResourceFileName As String
        Protected Sub Localize(ByRef ControlToLocalize As Object, ByVal PropertyOfControl As String, ByVal ResourceKey As String)
            ControlToLocalize.GetType().GetProperty(PropertyOfControl).SetValue(ControlToLocalize, DotNetNuke.Services.Localization.Localization.GetString(ResourceKey, mResourceFileName), Nothing)
        End Sub
        Protected Sub Localize(ByRef ControlToLocalize As Object, ByVal PropertyOfControl As String)
            ControlToLocalize.GetType().GetProperty(PropertyOfControl).SetValue(ControlToLocalize, DotNetNuke.Services.Localization.Localization.GetString(CType(ControlToLocalize, Web.UI.Control).ID, mResourceFileName), Nothing)
        End Sub
        Protected Function Locale(ByVal ResourceKey As String) As String
            Return DotNetNuke.Services.Localization.Localization.GetString(ResourceKey, mResourceFileName)
        End Function

        Public Sub RemoveControl()
            ControlPanelModule.RemoveControl(Me)
        End Sub

#End Region

#Region "Public Methods"
        Public Function SaveMap(ByVal iMapID As Integer, ByVal VisualProviderID As Integer, ByVal Name As String, ByVal Description As String) As Boolean
            Dim returnValue As Boolean = False
            Dim mc As New MapController
            Dim mi As Map.Data.MapInfo
            If iMapID > 0 Then
                'USE AN EXISTING MAP
                MapID = iMapID
                mi = mc.GetMap(MapID)
                returnValue = True
            Else
                mi = New Map.Data.MapInfo
                mi.MapID = -1
                mi.Settings = ""
                'CREATE A NEW MAP
            End If
            If Name.Length > 0 Then
                returnValue = True
                mi.Name = Name
                mi.Description = Description
                mi.PortalID = ControlPanelModule.PortalId
                mi.ProviderID = VisualProviderID
            End If

            If returnValue Then
                MapID = mc.UpdateMap(mi)
            End If
            Return returnValue
        End Function
        Public Function SaveSource(ByVal iSourceID As Integer, ByVal DataProviderID As Integer, ByVal Name As String, ByVal Description As String, ByVal iGeoLocatorProviderID As Integer) As Boolean
            Dim returnValue As Boolean = False
            Dim mc As New MapController
            Dim mi As Map.Data.SourceInfo
            If iSourceID > 0 Then
                'USE AN EXISTING MAP
                SourceID = iSourceID
                mi = mc.GetSource(SourceID)
                returnValue = True
            Else
                mi = New Map.Data.SourceInfo
                mi.SourceID = -1
                'CREATE A NEW MAP
                mi.Settings = ""
            End If
            If Name.Length > 0 Then
                returnValue = True
                mi.Name = Name
                mi.Description = Description
                mi.PortalID = ControlPanelModule.PortalId
                mi.ProviderID = DataProviderID
                mi.GeoLocatorProviderID = iGeoLocatorProviderID
            End If

            If returnValue Then
                SourceID = mc.UpdateSource(mi)
            End If
            Return returnValue
        End Function
        Protected Sub setUrlControl(ByRef Control As DotNetNuke.UI.UserControls.UrlControl, ByRef TextControl As System.Web.UI.WebControls.TextBox, ByVal value As String)
            'CHECK VERSION
            Dim displayversion As Boolean = False
            If CInt(Me.ControlPanelModule.PortalSettings.Version.Replace(".", "")) > 451 Then
                displayversion = True
            End If
            If displayversion Then
                Control.Url = value
                TextControl.Visible = False
                Control.Visible = True
            Else
                TextControl.Text = value
                TextControl.Visible = True
                Control.Visible = False
            End If
        End Sub
        Protected Function getUrlControl(ByRef Control As DotNetNuke.UI.UserControls.UrlControl, ByRef TextControl As System.Web.UI.WebControls.TextBox) As String
            Dim displayversion As Boolean = False
            If CInt(Me.ControlPanelModule.PortalSettings.Version.Replace(".", "")) > 451 Then
                displayversion = True
            End If

            If displayversion Then
                Return Control.Url
            Else
                Return TextControl.Text
            End If
        End Function

#End Region
    End Class
    Public Class URLControlReplacement
        Inherits DotNetNuke.UI.UserControls.UrlControl
        Private WithEvents txt As New System.Web.UI.WebControls.TextBox
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub ClearBase()
            If Me.Controls.Count > 0 Then
                Me.Controls.Clear()
                txt.Width = Me.Width
                Me.Controls.Add(txt)
            End If
        End Sub
    End Class
End Namespace