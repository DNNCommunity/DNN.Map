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

Namespace DotNetNuke.Modules.Map.Geolocator.Google
#Region "Configuration"
    Public Class Configuration

#Region "Private Variables"
        Private _licenseKey As String
        Private _useStreet As Boolean
        Private _useUnit As Boolean
        Private _useCountry As Boolean
        Private _useCity As Boolean
        Private _useRegion As Boolean
        Private _usePostalCode As Boolean
#End Region

#Region "Public Properties"
        Public Property LicenseKey() As String
            Get
                Return _licenseKey
            End Get
            Set(ByVal Value As String)
                _licenseKey = Value
            End Set
        End Property
        Public Property useStreet() As Boolean
            Get
                Return _useStreet
            End Get
            Set(ByVal Value As Boolean)
                _useStreet = Value
            End Set
        End Property
        Public Property useUnit() As Boolean
            Get
                Return _useUnit
            End Get
            Set(ByVal Value As Boolean)
                _useUnit = Value
            End Set
        End Property
        Public Property useCountry() As Boolean
            Get
                Return _useCountry
            End Get
            Set(ByVal Value As Boolean)
                _useCountry = Value
            End Set
        End Property
        Public Property useCity() As Boolean
            Get
                Return _useCity
            End Get
            Set(ByVal Value As Boolean)
                _useCity = Value
            End Set
        End Property
        Public Property useRegion() As Boolean
            Get
                Return _useRegion
            End Get
            Set(ByVal Value As Boolean)
                _useRegion = Value
            End Set
        End Property
        Public Property usePostalCode() As Boolean
            Get
                Return _usePostalCode
            End Get
            Set(ByVal Value As Boolean)
                _usePostalCode = Value
            End Set
        End Property
#End Region
#Region "ISerializable Members"
        Sub New()
        End Sub
        Public Sub New(ByVal info As Hashtable)
            _licenseKey = CType(info.Item("_licenseKey"), String)
            _useCountry = CType(info.Item("_useCountry"), Boolean)
            _usePostalCode = CType(info.Item("_usePostalCode"), Boolean)
            _useRegion = CType(info.Item("_useRegion"), Boolean)
            _useStreet = CType(info.Item("_useStreet"), Boolean)
            _useCity = CType(info.Item("_useCity"), Boolean)
            _useUnit = CType(info.Item("_useUnit"), Boolean)
        End Sub
        Public Sub Settings(ByRef info As Hashtable)
            info.Add("_licenseKey", _licenseKey)
            info.Add("_useCountry", _useCountry)
            info.Add("_usePostalCode", _usePostalCode)
            info.Add("_useRegion", _useRegion)
            info.Add("_useStreet", _useStreet)
            info.Add("_useCity", _useCity)
            info.Add("_useUnit", _useUnit)
        End Sub
        'Public Shared Function Serialize(ByVal Value As Configuration) As Byte()
        '    Dim Formatter As New Binary.BinaryFormatter
        '    Dim MS As New IO.MemoryStream
        '    Formatter.Serialize(MS, Value)
        '    Dim data As Byte() = MS.ToArray
        '    MS.Close()
        '    MS = Nothing
        '    Return data
        'End Function
        'Public Shared Function Deserialize(ByVal Value As Byte()) As Configuration
        '    Dim Formatter As New Binary.BinaryFormatter
        '    Dim MS As New System.IO.MemoryStream(Value)
        '    Dim Config As Configuration = CType(Formatter.Deserialize(MS), Configuration)
        '    MS.Close()
        '    MS = Nothing
        '    Return Config
        'End Function
        Public Shared Function Serialize(ByVal Value As Configuration) As String
            'Dim Formatter As New Binary.BinaryFormatter
            'Dim MS As New IO.MemoryStream
            'Formatter.Serialize(MS, Value)
            'Dim data As Byte() = MS.ToArray
            'MS.Close()
            'MS = Nothing
            'Return data
            Dim hsh As New Hashtable
            Value.Settings(hsh)
            Return DotNetNuke.Common.SerializeHashTableXml(hsh)
        End Function
        Public Shared Function Deserialize(ByVal Value As String) As Configuration
            'Dim Formatter As New Binary.BinaryFormatter
            'Dim MS As New System.IO.MemoryStream(Value)
            'Dim Config As LicenseKey = CType(Formatter.Deserialize(MS), LicenseKey)
            'MS.Close()
            'MS = Nothing
            'Return Config
            Dim m As Configuration = Nothing
            Try
                Dim hsh As Hashtable
                hsh = DotNetNuke.Common.DeserializeHashTableXml(Value)
                m = New Configuration(hsh)
            Catch ex As Exception
            End Try
            Return m
        End Function
#End Region
    End Class
#End Region

End Namespace

