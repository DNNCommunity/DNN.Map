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
    Public Class MapControlBase
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
        Public ReadOnly Property GeoLocatorProviderSrc() As String
            Get
                'LOAD THE GEOLOCATOR PROVIDER INFORMATION (BUSINESS LOGIC) FOR THE CURRENT PROVIDER
                'IF NOT IS YET ASSIGNED, USE THE DEFAULT
                If Not _Interaction Is Nothing Then
                    Return _Interaction.GeolocatorProviderSrc
                Else
                    Return ""
                End If
            End Get
        End Property
#End Region

        Private _module As DotNetNuke.Entities.Modules.PortalModuleBase
        Private _modulesettings As DotNetNuke.Entities.Modules.ModuleSettingsBase
        Private _adminMode As Boolean
        Public Property AdminMode() As Boolean
            Get
                Return _adminMode
            End Get
            Set(ByVal Value As Boolean)
                _adminMode = Value
            End Set
        End Property
        Public Property [Module]() As DotNetNuke.Entities.Modules.PortalModuleBase
            Get
                Return _module
            End Get
            Set(ByVal Value As DotNetNuke.Entities.Modules.PortalModuleBase)
                _module = Value
            End Set
        End Property
        Public Property [ModuleSettings]() As DotNetNuke.Entities.Modules.ModuleSettingsBase
            Get
                Return _modulesettings
            End Get
            Set(ByVal Value As DotNetNuke.Entities.Modules.ModuleSettingsBase)
                _modulesettings = Value
            End Set
        End Property
        Public ReadOnly Property VisualRootPath() As String
            Get
                If Not _module Is Nothing Then
                    Return _module.ModulePath & "Visuals/"
                ElseIf Not _modulesettings Is Nothing Then
                    Return _modulesettings.ModulePath & "Visuals/"
                End If
                Return Nothing
            End Get
        End Property

        Private ReadOnly Property PortalPath() As String
            Get
                If Not _module Is Nothing Then
                    Return _module.PortalSettings.HomeDirectoryMapPath
                ElseIf Not _modulesettings Is Nothing Then
                    Return _modulesettings.PortalSettings.HomeDirectoryMapPath
                End If
                Return Nothing
            End Get
        End Property
        Public Function ImageURL(ByVal Src As String) As String
            Src = IIf(Src = "", "~/images/spacer.gif", Src)
            If Not Src Is Nothing Then
                If Not Src.IndexOf(":") >= 0 Then
                    If Src.StartsWith("~") Then
                        Src = DotNetNuke.Common.ResolveUrl(Src)
                    ElseIf Src.StartsWith("*") Then
                        Src = ModuleImageURL(Src.Remove(0, 1))
                    ElseIf Src.ToLower.StartsWith("fileid=") Then
                        Dim fileId As Integer = Integer.Parse(Src.Substring(7))
                        Dim objFileController As New DotNetNuke.Services.FileSystem.FileController
                        Dim objFileInfo As DotNetNuke.Services.FileSystem.FileInfo = objFileController.GetFileById(fileId, _module.PortalId)
                        If Not objFileInfo Is Nothing Then

                            Src = _module.PortalSettings.HomeDirectory & objFileInfo.Folder & objFileInfo.FileName
                        End If
                    Else
                        Src = _module.PortalSettings.HomeDirectory & Src
                    End If
                    Return Src
                Else
                    Return Src
                End If
            Else
                Return ""
            End If
        End Function

        Public Function ModuleImageURL(ByVal Src As String) As String
            If Not _module Is Nothing Then
                Return _module.ModulePath & Src
            ElseIf Not _modulesettings Is Nothing Then
                Return _modulesettings.ModulePath & Src
            End If
            Return ""
        End Function


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
        Public Property GeoLocatorConfiguration() As GeoLocatorConfiguration
            Get
                If Not _Interaction Is Nothing Then
                    Return _Interaction.GeoLocatorConfiguration
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal Value As GeoLocatorConfiguration)
                If Not _Interaction Is Nothing Then
                    _Interaction.GeoLocatorConfiguration = Value
                End If
            End Set
        End Property
        Public ReadOnly Property MapID() As Integer
            Get
                If _module.Settings.ContainsKey("MapID") Then
                    Return CType(_module.Settings("MapID"), Integer)
                Else
                    Return -1
                End If
            End Get
        End Property
        Public ReadOnly Property SourceID() As Integer
            Get
                If _module.Settings.ContainsKey("SourceID") Then
                    Return CType(_module.Settings("SourceID"), Integer)
                Else
                    Return -1
                End If
            End Get
        End Property
        Public ReadOnly Property GeoLocatorID() As Integer
            Get
                If _module.Settings.ContainsKey("GeoLocatorID") Then
                    Return CType(_module.Settings("GeoLocatorID"), Integer)
                Else
                    Return -1
                End If
            End Get
        End Property
#End Region

#Region "Page Events"
        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Init
            If _ResourceFileName Is Nothing Then
                _ResourceFileName = DotNetNuke.Services.Localization.Localization.GetResourceFile(Me, Me.GetType().Name.Remove(Me.GetType.Name.Length - 5, 5) & ".ascx")
            End If
            If MapID > 0 OrElse SourceID > 0 Then
                _Interaction = New Interaction(MapID, SourceID)
            End If

        End Sub
        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
#End Region
    End Class
End Namespace