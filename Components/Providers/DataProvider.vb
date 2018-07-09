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
Namespace DotNetNuke.Modules.Map.Data
    Public MustInherit Class DataProvider

#Region "Shared/Static Methods"

        Private Shared objProvider As DataProvider = Nothing

        Shared Sub New()
            CreateProvider()
        End Sub

        Private Shared Sub CreateProvider()
            objProvider = CType(Framework.Reflection.CreateObject("data", "DotNetNuke.Modules.Map.Data", "DotNetNuke.Modules.Map"), DataProvider)
        End Sub

        Public Shared Shadows Function Instance() As DataProvider
            Return objProvider
        End Function
#End Region

#Region "Abstract Methods"
#Region "Map_Maps Methods"
        Public MustOverride Function AddMap(ByVal PortalID As Integer, ByVal ProviderID As Integer, ByVal SourceID As Integer, ByVal Name As String, ByVal Description As String, ByVal Settings As String) As Integer
        Public MustOverride Sub UpdateMap(ByVal PortalID As Integer, ByVal MapID As Integer, ByVal ProviderID As Integer, ByVal SourceID As Integer, ByVal Name As String, ByVal Description As String, ByVal Settings As String)
        Public MustOverride Function GetMaps_ByProviderName(ByVal ProviderName As String) As IDataReader
        Public MustOverride Sub DeleteMap(ByVal MapID As Integer)
        Public MustOverride Function GetMap(ByVal MapID As Integer) As IDataReader
        Public MustOverride Function GetMaps(ByVal PortalID As Integer) As IDataReader
#End Region
#Region "Map_Sources"
        Public MustOverride Function AddSource(ByVal PortalID As Integer, ByVal Provider As Integer, ByVal Name As String, ByVal Description As String, ByVal Settings As String, ByVal GeoLocatorProviderID As Integer) As Integer
        Public MustOverride Sub UpdateSourceGeoLocator(ByVal PortalID As Integer, ByVal SourceID As Integer, ByVal Settings As String)
        Public MustOverride Sub UpdateSourceService(ByVal SourceID As Integer, ByVal ServiceFlag As Integer)
        Public MustOverride Sub UpdateSource(ByVal SourceID As Integer, ByVal PortalID As Integer, ByVal ProviderID As Integer, ByVal Name As String, ByVal Description As String, ByVal Settings As String, ByVal GeoLocatorProviderID As Integer)
        Public MustOverride Sub DeleteSource(ByVal SourceID As Integer)
        Public MustOverride Function GetSource(ByVal SourceID As Integer) As IDataReader
        Public MustOverride Function GetSources(ByVal PortalID As Integer) As IDataReader
        Public MustOverride Function GetSources_ByProviderName(ByVal ProviderName As String) As IDataReader
        Public MustOverride Function GetSourceService(ByVal SourceID As Integer) As Integer
#End Region
#Region "Map_Providers"
        Public MustOverride Function GetProvider(ByVal ProviderID As Integer) As IDataReader
        Public MustOverride Function GetProviders(ByVal Type As String) As IDataReader
#End Region

#End Region

    End Class
End Namespace
