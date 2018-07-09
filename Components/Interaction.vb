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
Namespace DotNetNuke.Modules.Map.Components
    Public Class Interaction
        Private _mapID As Integer
        Private _sourceID As Integer
        Public Sub New(ByVal MapID As Integer, ByVal SourceID As Integer)
            _mapID = MapID
            _sourceID = SourceID
            If MapID > 0 Then
                MapConfiguration = Map(MapID)
            End If
            If SourceID > 0 Then
                SourceConfiguration = Source(SourceID)
                GeoLocatorConfiguration = GeoLocator(SourceID)
            End If
        End Sub
#Region "Public Member"
        Public ReadOnly Property VisualProviderSrc() As String
            Get
                'LOAD THE VISUAL UI PROVIDER INFORMATION FOR THE CURRENT PROVIDER
                'IF NONE IS YET ASSIGNED, USE THE DEFAULT
                If Not _mapconfiguration Is Nothing Then
                    Return _mapconfiguration.VisualProvider.Path
                Else
                    Return "~/DesktopModules/Map/Visuals/Standard.ascx"
                End If
            End Get
        End Property
        Public ReadOnly Property VisualAdminProviderSrc() As String
            Get
                'LOAD THE VISUAL UI PROVIDER INFORMATION FOR THE CURRENT PROVIDER
                'IF NONE IS YET ASSIGNED, USE THE DEFAULT
                If Not _mapconfiguration Is Nothing AndAlso Not _mapconfiguration.VisualProvider Is Nothing Then
                    Return _mapconfiguration.VisualProvider.AdminPath
                Else
                    Return "~/DesktopModules/Map/Visuals/Standard.ascx"
                End If
            End Get
        End Property
        Public ReadOnly Property SourceProviderSrc() As String
            Get
                'LOAD THE SOURCE PROVIDER INFORMATION (BUSINESS LOGIC) FOR THE CURRENT PROVIDER
                'IF NOT IS YET ASSIGNED, USE THE DEFAULT
                If Not _sourceconfiguration Is Nothing AndAlso Not _sourceconfiguration.DataProvider Is Nothing Then
                    Return _sourceconfiguration.DataProvider.Path
                Else
                    Return Nothing '"~/DesktopModules/Map/Sources/Standard.ascx"
                End If
            End Get
        End Property
        Public ReadOnly Property SourceAdminProviderSrc() As String
            Get
                'LOAD THE VISUAL UI PROVIDER INFORMATION FOR THE CURRENT PROVIDER
                'IF NONE IS YET ASSIGNED, USE THE DEFAULT
                If Not _sourceconfiguration Is Nothing AndAlso Not _sourceconfiguration.DataProvider Is Nothing Then
                    Return _sourceconfiguration.DataProvider.AdminPath
                Else
                    Return "~/DesktopModules/Map/Sources/Standard.ascx"
                End If
            End Get
        End Property
        Public ReadOnly Property GeolocatorProviderSrc() As String
            Get
                'LOAD THE SOURCE PROVIDER INFORMATION (BUSINESS LOGIC) FOR THE CURRENT PROVIDER
                'IF NOT IS YET ASSIGNED, USE THE DEFAULT
                If Not _geolocatorconfiguration Is Nothing AndAlso Not _geolocatorconfiguration.GeoLocatorProvider Is Nothing Then
                    Return _geolocatorconfiguration.GeoLocatorProvider.Path
                Else
                    Return Nothing '"~/DesktopModules/Map/Source/Standard.ascx"
                End If
            End Get
        End Property
        Public ReadOnly Property GeolocatorAdminProviderSrc() As String
            Get
                'LOAD THE VISUAL UI PROVIDER INFORMATION FOR THE CURRENT PROVIDER
                'IF NONE IS YET ASSIGNED, USE THE DEFAULT
                If Not _geolocatorconfiguration Is Nothing AndAlso Not _geolocatorconfiguration.GeoLocatorProvider Is Nothing Then
                    Return _geolocatorconfiguration.GeoLocatorProvider.AdminPath
                Else
                    Return "~/DesktopModules/Map/Source/Standard.ascx"
                End If
            End Get
        End Property
#End Region

#Region "Public Properties"
        Private _mapconfiguration As MapConfiguration
        Private _sourceconfiguration As SourceConfiguration
        Private _geolocatorconfiguration As GeoLocatorConfiguration

        Public Property MapConfiguration() As MapConfiguration
            Get
                Return _mapconfiguration
            End Get
            Set(ByVal Value As MapConfiguration)
                _mapconfiguration = Value
            End Set
        End Property
        Public Property SourceConfiguration() As SourceConfiguration
            Get
                Return _sourceconfiguration
            End Get
            Set(ByVal Value As SourceConfiguration)
                _sourceconfiguration = Value
            End Set
        End Property
        Public Property GeoLocatorConfiguration() As GeoLocatorConfiguration
            Get
                Return _geolocatorconfiguration
            End Get
            Set(ByVal Value As GeoLocatorConfiguration)
                _geolocatorconfiguration = Value
            End Set
        End Property

        Public ReadOnly Property MapID() As Integer
            Get
                Return _mapID
            End Get
        End Property
        Public ReadOnly Property SourceID() As Integer
            Get
                Return _sourceID
            End Get
        End Property
#End Region


#Region "Private Methods"
        Private Property Map(ByVal MapId As Integer) As MapConfiguration
            Get
                Dim config As New MapConfiguration(MapId)
                Return config
            End Get
            Set(ByVal Value As MapConfiguration)
                Value.Save()
            End Set
        End Property
        Private Property Source(ByVal SourceID As Integer) As SourceConfiguration
            Get
                Dim config As New SourceConfiguration(SourceID)
                Return config
            End Get
            Set(ByVal Value As SourceConfiguration)
                Value.Save()
            End Set
        End Property
        Private Property GeoLocator(ByVal SourceID As Integer) As GeoLocatorConfiguration
            Get
                Dim config As New GeoLocatorConfiguration(SourceID)
                Return config
            End Get
            Set(ByVal Value As GeoLocatorConfiguration)
                Value.Save()
            End Set
        End Property
#End Region
    End Class
    Public Class Utility
        Public Shared Function FormatNumber(ByVal Source As String, ByVal isIncoming As Boolean, ByVal forDisplay As Boolean) As Double
            Return Double.Parse(Source, System.Globalization.CultureInfo.InvariantCulture)
        End Function
    End Class
End Namespace
