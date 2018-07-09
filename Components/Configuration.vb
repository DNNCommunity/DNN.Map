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
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters
Imports DotNetNuke
Imports DotNetNuke.Modules.Map.Data

Namespace DotNetNuke.Modules.Map.Components
#Region "Configuration"
    Public Class MapConfiguration
#Region "Private Variables"
        Private _Map As Data.MapInfo
        Private _Visual As Data.DynamicProviderInfo
        Private _Settings As Object
#End Region
#Region "Public Properties"
        Public Property Map() As Data.MapInfo
            Get
                Return _Map
            End Get
            Set(ByVal Value As Data.MapInfo)
                _Map = Value
            End Set
        End Property
        Public Property VisualProvider() As Data.DynamicProviderInfo
            Get
                Return _Visual
            End Get
            Set(ByVal Value As Data.DynamicProviderInfo)
                _Visual = Value
            End Set
        End Property
        Public Property Settings() As Object
            Get
                Return _Settings
            End Get
            Set(ByVal Value As Object)
                _Settings = Value
            End Set
        End Property
        Public Sub New()
        End Sub
        Public Sub New(ByVal MapID As Integer)
            Load(MapID)
        End Sub
        Public Sub New(ByVal Map As Data.MapInfo)
            Load(Map)
        End Sub
#End Region
#Region "Public Methods"
        Public Sub Load(ByVal MapID As Integer)
            Dim mapDC As New MapController
            _Map = mapDC.GetMap(MapID)
            Load(_Map)
        End Sub
        Public Sub Load(ByVal Map As Data.MapInfo)
            _Map = Map
            Load_MapDependencies()
        End Sub
        Public Sub Save()
            Dim dc As New MapController
            dc.UpdateMap(_Map)
        End Sub
#End Region
#Region "Private Methods"
        Private Sub Load_MapDependencies()
            If Not _Map Is Nothing Then
                Dim mapDC As New MapController
                _Visual = mapDC.GetProvider(_Map.ProviderID)
            End If
        End Sub
#End Region
    End Class
    Public Class SourceConfiguration
#Region "Private Variables"
        Private _Source As Data.SourceInfo
        Private _Data As Data.DynamicProviderInfo
        Private _Settings As Object
#End Region
#Region "Public Properties"
        Public Property Source() As Data.SourceInfo
            Get
                Return _Source
            End Get
            Set(ByVal Value As Data.SourceInfo)
                _Source = Value
            End Set
        End Property
        Public Property DataProvider() As Data.DynamicProviderInfo
            Get
                Return _Data
            End Get
            Set(ByVal Value As Data.DynamicProviderInfo)
                _Data = Value
            End Set
        End Property
        Public Property Settings() As Object
            Get
                Return _Settings
            End Get
            Set(ByVal Value As Object)
                _Settings = Value
            End Set
        End Property
        Public Sub New()
        End Sub
        Public Sub New(ByVal SourceID As Integer)
            Load(SourceID)
        End Sub
        Public Sub New(ByVal Source As Data.SourceInfo)
            Load(Source)
        End Sub
#End Region
#Region "Public Methods"
        Public Sub Load(ByVal SourceID As Integer)
            Dim mapDC As New MapController
            _Source = mapDC.GetSource(SourceID)
            Load(_Source)
        End Sub
        Public Sub Load(ByVal Source As Data.SourceInfo)
            _Source = Source
            Load_SourceDependencies()
        End Sub
        Public Sub Save()
            Dim dc As New MapController
            dc.UpdateSource(_Source)
        End Sub
#End Region
#Region "Private Methods"
        Private Sub Load_SourceDependencies()
            If Not _Source Is Nothing Then
                Dim mapDC As New MapController
                _Source = mapDC.GetSource(_Source.SourceID)
                If _Source.ProviderID > 0 Then
                    _Data = mapDC.GetProvider(_Source.ProviderID)
                End If
            End If
        End Sub
#End Region
    End Class
    Public Class GeoLocatorConfiguration
#Region "Private Variables"
        Private _Provider As Data.DynamicProviderInfo
        Private _Settings As Object
        Private _Source As SourceInfo
#End Region
#Region "Public Properties"

        Public Property GeoLocatorProvider() As Data.DynamicProviderInfo
            Get
                Return _Provider
            End Get
            Set(ByVal Value As Data.DynamicProviderInfo)
                _Provider = Value
            End Set
        End Property
        Public Property Settings() As Object
            Get
                Return _Settings
            End Get
            Set(ByVal Value As Object)
                _Settings = Value
                If TypeOf Value Is String Then
                    If Not _Source Is Nothing Then
                        _Source.GeoLocatorSettings = CType(Value, String)
                    End If
                End If
            End Set
        End Property

        Public Sub New()
        End Sub
        Public Sub New(ByVal SourceID As Integer)
            Load(SourceID)
        End Sub
        Public Sub New(ByVal Source As Data.SourceInfo)
            Load(Source)
        End Sub
#End Region
#Region "Public Methods"
        Public Sub Load(ByVal SourceID As Integer)
            Dim mapDC As New MapController
            _Source = mapDC.GetSource(SourceID)
            Load(_Source)
        End Sub
        Public Sub Load(ByVal Source As Data.SourceInfo)
            _Source = Source
            Load_SourceDependencies()
        End Sub
        Public Sub Save()
            Dim dc As New MapController

            dc.UpdateGeoLocator(_Source)
        End Sub
#End Region
#Region "Private Methods"
        Private Sub Load_SourceDependencies()
            If Not _Source Is Nothing Then
                Dim mapDC As New MapController
                If _Source.GeoLocatorProviderID > 0 Then
                    _Provider = mapDC.GetProvider(_Source.GeoLocatorProviderID)
                End If
            End If
        End Sub
#End Region
    End Class
#End Region
End Namespace
