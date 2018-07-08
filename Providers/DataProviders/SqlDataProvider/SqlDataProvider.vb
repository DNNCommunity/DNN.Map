'
' DotNetNuke -  http://www.dotnetnuke.com
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
'-------------------------------------------------------------------------

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports DotNetNuke

Namespace DotNetNuke.Modules.Map.Data

    Public Class SqlDataProvider
        Inherits DotNetNuke.Modules.Map.Data.DataProvider



#Region "Private Members"
        Private Const ProviderType As String = "data"
        Private _providerConfiguration As Framework.Providers.ProviderConfiguration = Framework.Providers.ProviderConfiguration.GetProviderConfiguration(ProviderType)
        Private _connectionString As String
        Private _providerPath As String
        Private _objectQualifier As String
        Private _databaseOwner As String
#End Region

#Region "Constructors"
        Public Sub New()

            Dim objProvider As Framework.Providers.Provider = CType(_providerConfiguration.Providers(_providerConfiguration.DefaultProvider), Framework.Providers.Provider)

            If objProvider.Attributes("connectionStringName") <> "" AndAlso _
            System.Configuration.ConfigurationManager.AppSettings(objProvider.Attributes("connectionStringName")) <> "" Then
                _connectionString = System.Configuration.ConfigurationManager.AppSettings(objProvider.Attributes("connectionStringName"))
            Else
                _connectionString = objProvider.Attributes("connectionString")
            End If

            _providerPath = objProvider.Attributes("providerPath")

            _objectQualifier = objProvider.Attributes("objectQualifier")
            If _objectQualifier <> "" And _objectQualifier.EndsWith("_") = False Then
                _objectQualifier += "_"
            End If

            _databaseOwner = objProvider.Attributes("databaseOwner")
            If _databaseOwner <> "" And _databaseOwner.EndsWith(".") = False Then
                _databaseOwner += "."
            End If

        End Sub
#End Region

#Region "Properties"
        Public ReadOnly Property ConnectionString() As String
            Get
                Return _connectionString
            End Get
        End Property

        Public ReadOnly Property ProviderPath() As String
            Get
                Return _providerPath
            End Get
        End Property

        Public ReadOnly Property ObjectQualifier() As String
            Get
                Return _objectQualifier
            End Get
        End Property

        Public ReadOnly Property DatabaseOwner() As String
            Get
                Return _databaseOwner
            End Get
        End Property
#End Region

#Region "General Public Methods"
        Private Function GetNull(ByVal Field As Object) As Object
            Return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value)
        End Function
#End Region

#Region "Maps Methods"
        Public Overrides Function GetMap(ByVal MapID As Integer) As System.Data.IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_GetMap", MapID), IDataReader)
        End Function

        Public Overrides Function GetMaps(ByVal PortalID As Integer) As System.Data.IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_GetMaps", PortalID), IDataReader)
        End Function
        Public Overrides Function GetMaps_ByProviderName(ByVal ProviderName As String) As System.Data.IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_GetMaps_ByProviderName", ProviderName), IDataReader)
        End Function
        Public Overrides Function AddMap(ByVal PortalID As Integer, ByVal ProviderID As Integer, ByVal SourceID As Integer, ByVal Name As String, ByVal Description As String, ByVal Settings As String) As Integer
            Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_AddMap", PortalID, ProviderID, SourceID, Name, Description, Settings), Integer)
        End Function

        Public Overrides Sub UpdateMap(ByVal PortalID As Integer, ByVal MapID As Integer, ByVal ProviderID As Integer, ByVal SourceID As Integer, ByVal Name As String, ByVal Description As String, ByVal Settings As String)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_UpdateMap", PortalID, MapID, ProviderID, SourceID, Name, Description, Settings)
        End Sub

        Public Overrides Function GetProvider(ByVal ProviderID As Integer) As System.Data.IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_GetProvider", ProviderID), IDataReader)
        End Function

        Public Overrides Function GetProviders(ByVal Type As String) As System.Data.IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_GetProvidersByType", Type), IDataReader)
        End Function

        Public Overrides Function GetSource(ByVal SourceID As Integer) As System.Data.IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_GetSource", SourceID), IDataReader)
        End Function

        Public Overrides Sub DeleteMap(ByVal MapID As Integer)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_DeleteMap", MapID)
        End Sub
        Public Overrides Sub DeleteSource(ByVal SourceID As Integer)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_DeleteSource", SourceID)
        End Sub

        Public Overrides Function GetSources(ByVal PortalID As Integer) As System.Data.IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_GetSources", PortalID), IDataReader)
        End Function
        Public Overrides Function GetSources_ByProviderName(ByVal ProviderName As String) As System.Data.IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_GetSources_ByProviderName", ProviderName), IDataReader)
        End Function

        Public Overrides Function AddSource(ByVal PortalID As Integer, ByVal ProviderID As Integer, ByVal Name As String, ByVal Description As String, ByVal Settings As String, ByVal GeoLocatorProviderID As Integer) As Integer
            Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_AddSource", PortalID, ProviderID, Name, Description, Settings, GeoLocatorProviderID), Integer)
        End Function

        Public Overrides Sub UpdateSource(ByVal SourceID As Integer, ByVal PortalID As Integer, ByVal ProviderID As Integer, ByVal Name As String, ByVal Description As String, ByVal Settings As String, ByVal GeoLocatorProviderID As Integer)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_UpdateSource", SourceID, PortalID, ProviderID, Name, Description, Settings, GeoLocatorProviderID)
        End Sub
        Public Overrides Sub UpdateSourceGeoLocator(ByVal PortalID As Integer, ByVal SourceID As Integer, ByVal Settings As String)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_UpdateSourceGeoLocator", PortalID, SourceID, Settings)
        End Sub
        Public Overrides Sub UpdateSourceService(ByVal SourceID As Integer, ByVal ServiceFlag As Integer)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_UpdateSource_Service", SourceID, ServiceFlag)
        End Sub
        Public Overrides Function GetSourceService(ByVal SourceID As Integer) As Integer
            Return SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & "Map_GetSource_Service", SourceID)
        End Function
#End Region
    End Class

End Namespace
