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

Namespace DotNetNuke.Modules.Map.Visuals
#Region "Configuration"
    'This class is the configuration class used by ALL default Visuals. This may or may not contain the items required, and differ by the Map Type itself.
    Public Class StandardConfiguration
        Inherits Configuration


#Region "ISerializable Members"
        Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal hsh As Hashtable)
            MyBase.New(hsh)
        End Sub
        Public Overrides Sub Settings(ByRef hsh As Hashtable)
            MyBase.Settings(hsh)
        End Sub

        Public Shared Function Serialize(ByVal Value As StandardConfiguration) As String
            Try
                Dim hsh As New Hashtable
                Value.Settings(hsh)
                Return DotNetNuke.Common.SerializeHashTableXml(hsh)
            Catch
                Return ""
            End Try
        End Function
        Public Shared Function Deserialize(ByVal Value As String) As StandardConfiguration
            Dim m As StandardConfiguration = Nothing
            Try
                Dim hsh As Hashtable
                hsh = DotNetNuke.Common.DeserializeHashTableXml(Value)
                m = New StandardConfiguration(hsh)
            Catch ex As Exception
            End Try
            Return m
        End Function
#End Region
    End Class
#End Region

End Namespace