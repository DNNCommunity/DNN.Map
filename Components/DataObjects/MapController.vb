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
Imports DotNetNuke.Modules.Map
Imports DotNetNuke.Common.Utilities
Namespace DotNetNuke.Modules.Map.Data
    Public Class MapController
#Region "Public Methods"
#Region "Map Methods"
        Public Function GetMap(ByVal MapID As Integer) As Data.MapInfo
            Dim obj As Data.MapInfo
            obj = DotNetNuke.Services.Cache.CachingProvider.Instance.GetItem("Map" & MapID)
            If obj Is Nothing Then
                obj = CType(CBO.FillObject(Data.DataProvider.Instance().GetMap(MapID), GetType(Data.MapInfo)), Data.MapInfo)
                If Not obj Is Nothing Then
                    DotNetNuke.Services.Cache.CachingProvider.Instance.Insert("Map" & MapID, obj, False)
                End If
            End If
            Return obj
        End Function
        Public Function ListMaps(ByVal PortalID As Integer) As ArrayList
            Return CBO.FillCollection(Data.DataProvider.Instance().GetMaps(PortalID), GetType(Data.MapInfo))
        End Function
        Public Function ListMaps(ByVal ProviderName As String) As ArrayList
            Return CBO.FillCollection(Data.DataProvider.Instance().GetMaps_ByProviderName(ProviderName), GetType(Data.MapInfo))
        End Function
        Public Function AddMap(ByVal obj As Data.MapInfo) As Integer
            Return CType(Data.DataProvider.Instance().AddMap(obj.PortalID, obj.ProviderID, obj.SourceID, obj.Name, obj.Description, obj.Settings), Integer)
        End Function
        Public Function UpdateMap(ByVal obj As Data.MapInfo) As Integer
            If obj.MapID <= 0 Then
                DotNetNuke.Services.Cache.CachingProvider.Instance.Remove("Map" & obj.MapID)
                Return AddMap(obj)
            Else
                Data.DataProvider.Instance().UpdateMap(obj.PortalID, obj.MapID, obj.ProviderID, obj.SourceID, obj.Name, obj.Description, obj.Settings)
                DotNetNuke.Services.Cache.CachingProvider.Instance.Remove("Map" & obj.MapID)
                Return obj.MapID
            End If
        End Function
        Public Sub DeleteMap(ByVal MapID As Integer)
            Data.DataProvider.Instance().DeleteMap(MapID)
        End Sub
#End Region
#Region "Source Methods"
        Public Function GetSource(ByVal SourceID As Integer) As Data.SourceInfo
            Dim obj As Data.SourceInfo
            obj = DotNetNuke.Services.Cache.CachingProvider.Instance.GetItem("MapSource" & SourceID)
            If obj Is Nothing Then
                obj = CType(CBO.FillObject(Data.DataProvider.Instance().GetSource(SourceID), GetType(Data.SourceInfo)), Data.SourceInfo)
                If Not obj Is Nothing Then
                    DotNetNuke.Services.Cache.CachingProvider.Instance.Insert("MapSource" & SourceID, obj, False)
                End If
            End If
            Return obj
        End Function
        Public Function ListSources(ByVal PortalID As Integer) As ArrayList
            Return CBO.FillCollection(Data.DataProvider.Instance().GetSources(PortalID), GetType(Data.SourceInfo))
        End Function
        Public Function ListSources(ByVal ProviderName As String) As ArrayList
            Return CBO.FillCollection(Data.DataProvider.Instance().GetSources_ByProviderName(ProviderName), GetType(Data.SourceInfo))
        End Function
        Public Function AddSource(ByVal obj As Data.SourceInfo) As Integer
            Return CType(Data.DataProvider.Instance().AddSource(obj.PortalID, obj.ProviderID, obj.Name, obj.Description, obj.Settings, obj.GeoLocatorProviderID), Integer)
        End Function
        Public Function UpdateGeoLocator(ByVal obj As Data.SourceInfo) As Integer
            Data.DataProvider.Instance().UpdateSourceGeoLocator(obj.PortalID, obj.SourceID, obj.GeoLocatorSettings)
            Return obj.SourceID
        End Function
        Public Function UpdateSource(ByVal obj As Data.SourceInfo) As Integer
            If obj.SourceID <= 0 Then
                DotNetNuke.Services.Cache.CachingProvider.Instance.Remove("MapSource" & obj.SourceID)
                Return AddSource(obj)
            Else
                Data.DataProvider.Instance().UpdateSource(obj.SourceID, obj.PortalID, obj.ProviderID, obj.Name, obj.Description, obj.Settings, obj.GeoLocatorProviderID)
                DotNetNuke.Services.Cache.CachingProvider.Instance.Remove("MapSource" & obj.SourceID)
                Return obj.SourceID
            End If
        End Function
        Public Sub UpdateSource_Service(ByVal SourceID As Integer, ByVal ServiceFlag As Integer)
            Data.DataProvider.Instance().UpdateSourceService(SourceID, ServiceFlag)
        End Sub
        Public Function GetSource_Service(ByVal SourceID As Integer) As Integer
            Return Data.DataProvider.Instance().GetSourceService(SourceID)
        End Function
        Public Sub DeleteSource(ByVal SourceID As Integer)
            Data.DataProvider.Instance().DeleteSource(SourceID)
        End Sub
#End Region
#Region "Provider Methods"
        Public Function GetProvider(ByVal ProviderID As Integer) As Data.DynamicProviderInfo
            Dim obj As Data.DynamicProviderInfo
            obj = DotNetNuke.Services.Cache.CachingProvider.Instance.GetItem("MapProvider" & ProviderID)
            If obj Is Nothing Then
                obj = CType(CBO.FillObject(Data.DataProvider.Instance().GetProvider(ProviderID), GetType(Data.DynamicProviderInfo)), Data.DynamicProviderInfo)
                If Not obj Is Nothing Then
                    DotNetNuke.Services.Cache.CachingProvider.Instance.Insert("MapProvider" & ProviderID, obj, False)
                End If
            End If
            Return obj
        End Function
        Public Function ListProviders(ByVal ProviderType As String) As ArrayList
            Return CBO.FillCollection(Data.DataProvider.Instance().GetProviders(ProviderType), GetType(Data.DynamicProviderInfo))
        End Function
#End Region
#End Region
#Region "Private Methods"

#End Region
    End Class
End Namespace