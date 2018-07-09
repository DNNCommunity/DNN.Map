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
    Public Class Location
        Public Latitude As Decimal
        Public Longitude As Decimal
        Public Status As String
        Public canRetry As Boolean = False
        Public canRepeat As Boolean = False
        Public isSuccessful As Boolean = False
    End Class
    Public MustInherit Class GeoCoderBase
        Private _Interaction As Interaction
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

        Public MustOverride Function Address(ByVal Unit As String, ByVal Street As String, ByVal City As String, ByVal Region As String, ByVal PostalCode As String, ByVal Country As String) As String
        Public MustOverride Function Lookup(ByVal Address As String) As Location
        Public MustOverride Function hasPermission() As Boolean
        Public MustOverride Sub LoadConfiguration(ByVal Value As String)


        Public Sub New()
        End Sub
        Public Sub Load(ByVal SourceID As Integer)
            If SourceID > 0 Then
                _Interaction = New Interaction(-1, SourceID)
            End If
        End Sub
        Public Sub New(ByVal SourceID As Integer)
            Load(SourceID)
        End Sub
    End Class
End Namespace
