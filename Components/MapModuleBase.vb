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
    Public Class MapModuleBase
        Inherits DotNetNuke.Entities.Modules.PortalModuleBase
        Implements DotNetNuke.Entities.Modules.IActionable
        Implements DotNetNuke.Entities.Modules.IPortable


#Region "Public Member"
        Public ReadOnly Property VisualProviderSrc() As String
            Get
                'LOAD THE VISUAL UI PROVIDER INFORMATION FOR THE CURRENT PROVIDER
                'IF NONE IS YET ASSIGNED, USE THE DEFAULT
                If Not _Interaction Is Nothing AndAlso Not _Interaction.VisualProviderSrc Is Nothing AndAlso _Interaction.VisualProviderSrc.Length > 0 Then
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
                If Not _Interaction Is Nothing AndAlso Not _Interaction.SourceProviderSrc Is Nothing AndAlso _Interaction.SourceProviderSrc.Length > 0 Then
                    Return _Interaction.SourceProviderSrc
                Else
                    Return Nothing
                End If
            End Get
        End Property
        Public ReadOnly Property GeoLocatorProviderSrc() As String
            Get
                'LOAD THE SOURCE PROVIDER INFORMATION (BUSINESS LOGIC) FOR THE CURRENT PROVIDER
                'IF NOT IS YET ASSIGNED, USE THE DEFAULT
                If Not _Interaction Is Nothing AndAlso Not _Interaction.GeolocatorProviderSrc Is Nothing AndAlso _Interaction.GeolocatorProviderSrc.Length > 0 Then
                    Return _Interaction.GeolocatorProviderSrc
                Else
                    Return Nothing
                End If
            End Get
        End Property
#End Region

#Region "Public Methods"
        Public Sub SetModuleConfiguration(ByVal config As Entities.Modules.ModuleInfo)
            ModuleConfiguration = config
        End Sub
#End Region

#Region "Public Properties"
        Private _Interaction As Interaction
        Private _ResourceFileName As String
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
        Public ReadOnly Property MapID() As Integer
            Get
                If Me.Settings.ContainsKey("MapID") Then
                    Return CType(Me.Settings("MapID"), Integer)
                Else
                    Return -1
                End If
            End Get
        End Property
        Public ReadOnly Property SourceID() As Integer
            Get
                If Me.Settings.ContainsKey("SourceID") Then
                    Return CType(Me.Settings("SourceID"), Integer)
                Else
                    Return -1
                End If
            End Get
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
#End Region

#Region "Page Events"
        Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Init
            If MapID > 0 AndAlso SourceID > 0 Then
                _Interaction = New Interaction(MapID, SourceID)
            End If
            If _ResourceFileName Is Nothing Then
                _ResourceFileName = DotNetNuke.Services.Localization.Localization.GetResourceFile(Me, Me.GetType().Name.Remove(Me.GetType.Name.Length - 5, 5) & ".ascx")
            End If
        End Sub
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

        Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
            Get
                Dim MyActions As New Entities.Modules.Actions.ModuleActionCollection
                If IsEditable Then
                    MyActions.Add(GetNextActionID, Locale("MenuEditMap"), "", Url:=EditUrl("ControlPanel"), Secure:=DotNetNuke.Security.SecurityAccessLevel.Edit, Visible:=True)
                End If
                Return MyActions
            End Get
        End Property

        Public Function ExportModule(ByVal ModuleID As Integer) As String Implements Entities.Modules.IPortable.ExportModule
            Dim strXML As String = ""
            Dim ModController As New DotNetNuke.Entities.Modules.ModuleController
            Dim settings As Hashtable = ModController.GetModuleSettings(ModuleID)
            strXML += "<map>"
            If settings.ContainsKey("MapID") Then
                Dim SourceID As Integer = Integer.Parse(settings.Item("SourceID"))
                Dim MapID As Integer = Integer.Parse(settings.Item("MapID"))
                If SourceID > 0 AndAlso MapID > 0 Then
                    Dim mcontroller As New Map.Data.MapController
                    Dim mdata As Map.Data.MapInfo = mcontroller.GetMap(MapID)
                    Dim sdata As Map.Data.SourceInfo = mcontroller.GetSource(SourceID)
                    If Not mdata Is Nothing AndAlso Not sdata Is Nothing Then
                        strXML += "<mapid>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(mdata.MapID) & "</mapid>"
                        strXML += "<mapdescription>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(mdata.Description) & "</mapdescription>"
                        strXML += "<mapname>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(mdata.Name) & "</mapname>"
                        strXML += "<mapportalid>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(mdata.PortalID) & "</mapportalid>"
                        strXML += "<mapproviderid>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(mdata.ProviderID) & "</mapproviderid>"
                        strXML += "<mapsettings>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(mdata.Settings) & "</mapsettings>"
                        strXML += "<sourceid>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(sdata.SourceID) & "</sourceid>"
                        strXML += "<sourcedescription>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(sdata.Description) & "</sourcedescription>"
                        strXML += "<sourcegeolocatorproviderid>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(sdata.GeoLocatorProviderID) & "</sourcegeolocatorproviderid>"
                        strXML += "<sourcegeolocatorsettings>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(sdata.GeoLocatorSettings) & "</sourcegeolocatorsettings>"
                        strXML += "<sourcename>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(sdata.Name) & "</sourcename>"
                        strXML += "<sourceproviderid>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(sdata.ProviderID) & "</sourceproviderid>"
                        strXML += "<sourcesettings>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(sdata.Settings) & "</sourcesettings>"
                    End If
                End If
            End If
            strXML += "</map>"
            Return strXML
        End Function

        Public Sub ImportModule(ByVal ModuleID As Integer, ByVal Content As String, ByVal Version As String, ByVal UserID As Integer) Implements Entities.Modules.IPortable.ImportModule
            Dim xmlMap As System.Xml.XmlNode
            xmlMap = DotNetNuke.Common.GetContent(Content, "map")
            Dim MapID As Integer = Integer.Parse(xmlMap.Item("mapid").InnerText)
            Dim MapName As String = xmlMap.Item("mapname").InnerText
            Dim SourceID As Integer = Integer.Parse(xmlMap.Item("sourceid").InnerText)
            Dim SourceName As String = xmlMap.Item("sourcename").InnerText
            Dim mcontroller As New Map.Data.MapController
            Dim minfo As Map.Data.MapInfo = Nothing
            Dim sinfo As Map.Data.SourceInfo = Nothing
            If MapID > 0 Then
                minfo = mcontroller.GetMap(MapID)
                If Not minfo Is Nothing AndAlso minfo.Name = MapName AndAlso minfo.PortalID = Me.PortalId Then
                    'MAP ALREADY EXISTS - LEAVE INTACT
                    minfo = Nothing
                Else
                    'MAP FAILS TO EXIST - NEW MAP
                    minfo = New Map.Data.MapInfo
                    minfo.MapID = -1
                    minfo.Description = xmlMap.Item("mapdescription").InnerText
                    minfo.Name = MapName
                    minfo.PortalID = Me.PortalId
                    minfo.ProviderID = Integer.Parse(xmlMap.Item("mapproviderid").InnerText)
                    minfo.Settings = xmlMap.Item("mapsettings").InnerText
                End If
            End If
            If SourceID > 0 Then
                sinfo = mcontroller.GetSource(SourceID)
                If Not sinfo Is Nothing AndAlso sinfo.Name = SourceName AndAlso sinfo.PortalID = Me.PortalId Then
                    'SOURCE ALREADY EXISTS - LEAVE INTACT
                    sinfo = Nothing
                Else
                    'SOURCE FAILS TO EXIST - NEW SOURCE
                    sinfo = New Map.Data.SourceInfo
                    sinfo.SourceID = -1
                    sinfo.Description = xmlMap.Item("sourcedescription").InnerText
                    sinfo.Name = SourceName
                    sinfo.PortalID = Me.PortalId
                    sinfo.ProviderID = Integer.Parse(xmlMap.Item("sourceproviderid").InnerText)
                    sinfo.GeoLocatorProviderID = Integer.Parse(xmlMap.Item("sourcegeolocatorproviderid").InnerText)
                    sinfo.GeoLocatorSettings = xmlMap.Item("sourcegeolocatorsettings").InnerText
                    sinfo.Settings = xmlMap.Item("sourcesettings").InnerText

                    SourceID = mcontroller.AddSource(sinfo)
                End If
            End If
            If SourceID > 0 AndAlso Not minfo Is Nothing Then
                minfo.SourceID = SourceID
                MapID = mcontroller.AddMap(minfo)
            End If
            Dim mccontroller As New DotNetNuke.Entities.Modules.ModuleController
            If SourceID > 0 Then
                mccontroller.UpdateModuleSetting(ModuleID, "SourceID", SourceID)
            End If
            If MapID > 0 Then
                mccontroller.UpdateModuleSetting(ModuleID, "MapID", MapID)
            End If
        End Sub
    End Class
End Namespace
