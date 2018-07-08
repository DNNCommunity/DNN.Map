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
Imports DotNetNuke.Modules.Map.Components
Imports DotNetNuke

Namespace DotNetNuke.Modules.Map.Geolocator.Google
    Public Class GeoLocator
        Inherits GeoCoderBase
        Private _geolocatorsettings As DotNetNuke.Modules.Map.Geolocator.Google.Configuration
        Private Property GeoLocatorSettings() As DotNetNuke.Modules.Map.GeoLocator.Google.Configuration
            Get
                If _geolocatorsettings Is Nothing Then
                    If Not Me.GeoLocatorConfiguration Is Nothing AndAlso Not Me.GeoLocatorConfiguration.Settings Is Nothing Then
                        Try
                            Dim sinfo As DotNetNuke.Modules.Map.Geolocator.Google.Configuration = DotNetNuke.Modules.Map.Geolocator.Google.Configuration.Deserialize(Me.GeoLocatorConfiguration.Settings)
                            GeoLocatorConfiguration.Settings = sinfo
                        Catch ex As Exception
                            DotNetNuke.Services.Exceptions.LogException(New Exception("Unable to load the Map Geolocator configuration", ex))
                        End Try
                    End If
                    If Me.GeoLocatorConfiguration.Settings Is Nothing Then
                        _geolocatorsettings = New DotNetNuke.Modules.Map.GeoLocator.Google.Configuration
                    Else
                        _geolocatorsettings = GeoLocatorConfiguration.Settings
                    End If
                End If
                Return _geolocatorsettings
            End Get
            Set(ByVal Value As DotNetNuke.Modules.Map.GeoLocator.Google.Configuration)
                _geolocatorsettings = Value
            End Set
        End Property
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal GeoLocatorID As Integer)
            MyBase.New(GeoLocatorID)
        End Sub
        Public Overrides Function hasPermission() As Boolean
            Dim ballowed As Boolean = False
            Try
                Dim url As String = "maps.google.com"
                If System.Security.SecurityManager.IsGranted(New System.Net.WebPermission(System.Net.NetworkAccess.Connect, url)) Then
                    ballowed = True
                End If
            Catch ex As Exception
                DotNetNuke.Services.Exceptions.LogException(New Exception("MAP Module: Unable to execute GEO Coder address, please check your security settings. This call requires Server to Server URL contact. " & ex.ToString))
                ballowed = False
            End Try
            Return ballowed
        End Function
        Private ReadOnly Property APIKey() As String
            Get
                If Not GeoLocatorSettings.LicenseKey Is Nothing Then
                    Return GeoLocatorSettings.LicenseKey
                End If
                Return ""
            End Get
        End Property
        Private Sub Geocode(ByVal Address As String, ByRef Result As Location)
            Result.isSuccessful = False
            If hasPermission() Then
                Try
                    Dim str As String = "http://maps.google.com/maps/geo?output=xml&key=" & APIKey & "&q=" & Address.Replace(" ", "+")
                    Dim wc As New Net.WebClient
                    Dim instream As New IO.StreamReader(wc.OpenRead(str))
                    Dim instr As String = instream.ReadToEnd.ToUpper
                    instream.Close()
                    If instr.Length > 0 Then
                        'PARSE THE RESULT
                        'FIND THE <COORDINATES> TAG
                        Dim strICoordinates As Integer = instr.IndexOf("<COORDINATES>")
                        If strICoordinates > 0 Then
                            strICoordinates += 13
                            Dim strECoordinates As Integer = instr.IndexOf("</COORDINATES>", strICoordinates)
                            If strECoordinates > strICoordinates Then
                                Dim strCoordinates As String = instr.Substring(strICoordinates, strECoordinates - strICoordinates)
                                If strCoordinates.Length > 0 Then
                                    'THE FORMAT IS LONGITUDE,LATITUDE,INDEX
                                    Dim coords() As String = strCoordinates.Split(",")
                                    If coords.Length >= 2 Then
                                        Result.isSuccessful = True
                                        If Not Decimal.TryParse(coords(0), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, Result.Longitude) Then
                                            Result.Latitude = 0
                                        End If

                                        If Not Decimal.TryParse(coords(1), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, Result.Latitude) Then
                                            Result.Longitude = 0
                                        End If
                                        '                                        Result.Latitude = coords(1).ToString.Replace(",", ".")
                                    End If
                                Else
                                    Result.Status = "Coordinate value from google was in an unexpected format, format should be LONGITUDE,LATITUDE,INDEX"
                                End If
                            Else
                                Result.Status = "Unable to locate </COORDINATES> and <CODE>"
                            End If
                        Else
                            'CHECK FOR CODE 620 - IDENTIFYING THAT WE ARE GEOCODING TOO FAST
                            Dim strECoordinates As Integer
                            strICoordinates = instr.IndexOf("<CODE>")
                            If strICoordinates > 0 Then
                                strICoordinates += 6
                                strECoordinates = instr.IndexOf("</CODE>")
                                If strECoordinates > strICoordinates Then
                                    Dim strcoordinates As String = instr.Substring(strICoordinates, strECoordinates - strICoordinates)
                                    Select Case strcoordinates
                                        Case "200"
                                            Result.Status = "Unable to locate <COORDINATES> - Status was SUCCESS"
                                        Case "400"
                                            Result.Status = "Unable to locate <COORDINATES> - Status was BAD REQUEST"
                                        Case "500"
                                            Result.Status = "Unable to locate <COORDINATES> - Status was SERVER ERROR"
                                            Result.canRetry = True
                                        Case "601"
                                            Result.Status = "Unable to locate <COORDINATES> - Status was MISSING QUERY"
                                        Case "602"
                                            Result.Status = "Unable to locate <COORDINATES> - Status was MISSING ADDRESS"
                                        Case "603"
                                            Result.Status = "Unable to locate <COORDINATES> - Status was UNAVAILABLE ADDRESS"
                                        Case "604"
                                            Result.Status = "Unable to locate <COORDINATES> - Status was UNKNOWN DIRECTIONS"
                                        Case "610"
                                            Result.Status = "Unable to locate <COORDINATES> - Status was BAD KEY"
                                        Case "620"
                                            Result.Status = "Unable to locate <COORDINATES> - Status was TOO MANY QUERIES"
                                            Result.canRetry = True
                                            Result.canRepeat = True
                                        Case Else
                                    End Select
                                Else
                                    Result.Status = "Unable to locate <COORDINATES> and <CODE>"
                                End If
                            End If
                        End If
                    Else
                        Result.Status = "No Response was received from the Google Geocoder API"
                    End If
                Catch ex As Exception
                    Result.Status = "A Critical exception occurred while retrieving information from the Google API: " & ex.ToString
                End Try
            End If
        End Sub


        Public Overrides Function Address(ByVal Unit As String, ByVal Street As String, ByVal City As String, ByVal Region As String, ByVal PostalCode As String, ByVal Country As String) As String
            Dim result As String = ""
            With GeoLocatorSettings
                If .useUnit AndAlso Not Unit Is Nothing Then
                    result &= Unit & " "
                End If
                If .useStreet AndAlso Not Street Is Nothing Then
                    result &= Street & " "
                End If
                If .useCity AndAlso Not City Is Nothing Then
                    result &= City & " "
                End If
                If .useRegion AndAlso Not Region Is Nothing Then
                    result &= Region & " "
                End If
                If .usePostalCode AndAlso Not PostalCode Is Nothing Then
                    result &= PostalCode & " "
                End If
                If .useCountry AndAlso Not Country Is Nothing Then
                    result &= Country
                End If
            End With
            Return result
        End Function
        Public Overrides Function Lookup(ByVal Address As String) As Components.Location
            Dim result As New Location
            If Not Address Is Nothing AndAlso Address.Length > 0 Then
                Geocode(Address, result)
            End If
            Return result
        End Function

        Public Overrides Sub LoadConfiguration(ByVal Value As String)
            Try
                Dim sinfo As DotNetNuke.Modules.Map.Geolocator.Google.Configuration = DotNetNuke.Modules.Map.Geolocator.Google.Configuration.Deserialize(Value)
                GeoLocatorConfiguration.Settings = sinfo
                _geolocatorsettings = sinfo
            Catch ex As Exception
                DotNetNuke.Services.Exceptions.LogException(New Exception("Unable to load the Map Geolocator configuration", ex))
            End Try
        End Sub
    End Class
End Namespace