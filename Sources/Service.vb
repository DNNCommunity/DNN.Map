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
Namespace DotNetNuke.Modules.Map.Data
    Public Class AutoGeoCoder
        Private _SourceID As Integer
        Public Sub New()
        End Sub
        Public Sub Start(Optional ByVal SourceID As Integer = -1)
            _Start(SourceID)
        End Sub
        Private Sub _Start(Optional ByVal DefaultSourceID As Integer = -1)
            Try
                'THIS PROCESS WILL AUTOMATICALLY RUN UNTIL THE THREAD IS TERMINATED, THE REQUEST TIMES OUT, OR 100 RECORDS ARE UPDATED
                Dim ireader As IDataReader
                Dim s As New Standard(Nothing)
                Dim g As DotNetNuke.Modules.Map.Components.GeoCoderBase = Nothing
                ireader = s.GetPoints_ByProviderName_NoGeo("DotNetNuke.Map.Basic", False, DefaultSourceID)
                While ireader.Read
                    Dim bRepeat As Boolean = True
                    While bRepeat

                        bRepeat = False
                        Dim sourceID As Integer = ireader(0)
                        Dim pointID As Integer = ireader(1)
                        Dim address As String = ireader(2)


                        'LOAD the GEO Coder from the First SourceID
                        Dim mapDC As New MapController
                        Dim sourceInfo As SourceInfo
                        sourceInfo = mapDC.GetSource(sourceID)

                        Try
                            'm_GeoControl = me.load CType(Me.LoadControl(_interaction.GeolocatorProviderSrc), GeoCoderBase)
                            Dim geoInfo As DotNetNuke.Modules.Map.Data.DynamicProviderInfo
                            geoInfo = mapDC.GetProvider(sourceInfo.GeoLocatorProviderID)
                            Dim oByName As Object = System.Activator.CreateInstance(Type.GetType(geoInfo.Path))
                            If Not oByName Is Nothing Then
                                g = CType(oByName, DotNetNuke.Modules.Map.Components.GeoCoderBase)
                                g.Load(sourceID)

                                g.LoadConfiguration(sourceInfo.GeoLocatorSettings)
                            End If
                            geoInfo = Nothing
                        Catch ex As Exception

                        End Try

                        sourceInfo = Nothing
                        mapDC = Nothing

                        If Not g Is Nothing Then
                            Dim location As DotNetNuke.Modules.Map.Components.Location
                            Try
                                location = g.Lookup(address)
                            Catch ex As Exception
                                location = New DotNetNuke.Modules.Map.Components.Location
                                location.isSuccessful = False
                            End Try
                            If location.isSuccessful Then
                                'UPDATE THE POINT
                                s.UpdatePoint_Location(pointID, False, location.Longitude, location.Latitude)
                            Else
                                If location.canRetry = False Then
                                    'SET AS FAILURE
                                    s.UpdatePoint_Location(pointID, True, 0, 0)
                                Else
                                    'THE FAILURE IS NOT PERMANENT
                                    If location.canRepeat Then
                                        'THE FAILURE WAS DUE TO TOO MANY REQUESTS TO QUICKLY - WAIT FOR A SHORT TIME
                                        Threading.Thread.Sleep(100)
                                        bRepeat = True
                                    End If
                                End If
                            End If
                        End If
                    End While
                End While
            Catch ex As Exception

            End Try
        End Sub
    End Class
    Public Class AutoUserCoder
        Public Sub New()
        End Sub

        Public Sub Start(ByVal SourceID As Integer)
            Try
                'THIS PROCESS WILL AUTOMATICALLY RUN UNTIL THE THREAD IS TERMINATED, THE REQUEST TIMES OUT, OR 100 RECORDS ARE UPDATED
                Dim ireader As IDataReader
                'Dim s As New Standard(Nothing)
                Dim g As DotNetNuke.Modules.Map.Components.GeoCoderBase = Nothing

                Dim dnn As New DotNetNuke.Entities.Users.UserController
                Dim dnnM As New DotNetNuke.Entities.Modules.ModuleController
                Dim mapDC As New MapController
                Dim sourceInfo As SourceInfo
                Dim actualinfo As Map.Data.Standard.Configuration = Nothing
                sourceInfo = mapDC.GetSource(SourceID)

                Dim summaryOnly As Boolean = False

                If Not sourceInfo Is Nothing Then
                    'LOAD THE SETTING
                    If Not sourceInfo.Settings Is Nothing Then
                        Try
                            actualinfo = Standard.Configuration.Deserialize(sourceInfo.Settings)

                            summaryOnly = actualinfo.SummaryOnly
                        Catch ex As Exception
                        End Try
                    End If
                End If

                If Not actualinfo Is Nothing AndAlso actualinfo.RenderType = 2 Then
                    Dim s As New Standard(actualinfo)
                    'GET THE NEXT USER
                    Dim lastuserid As Integer = mapDC.GetSource_Service(SourceID)
                    ireader = s.MapServices_Users(SourceID, lastuserid)

                    Try
                        'm_GeoControl = me.load CType(Me.LoadControl(_interaction.GeolocatorProviderSrc), GeoCoderBase)
                        Dim geoInfo As DotNetNuke.Modules.Map.Data.DynamicProviderInfo
                        geoInfo = mapDC.GetProvider(sourceInfo.GeoLocatorProviderID)
                        Dim oByName As Object = System.Activator.CreateInstance(Type.GetType(geoInfo.Path))
                        If Not oByName Is Nothing Then
                            g = CType(oByName, DotNetNuke.Modules.Map.Components.GeoCoderBase)
                            g.Load(SourceID)
                            g.LoadConfiguration(sourceInfo.GeoLocatorSettings)
                        End If
                        geoInfo = Nothing
                    Catch ex As Exception

                    End Try
                    sourceInfo = Nothing


                    While ireader.Read
                        Dim currentuserID As Integer = ireader(0)
                        Dim portalId As Integer = ireader(1)

                        If Not g Is Nothing Then
                            'WE HAVE THE USERS FOR THE PORTAL THAT HAVE BEEN UPDATED
                            Dim userInfo As DotNetNuke.Entities.Users.UserInfo = Nothing
                            userInfo = dnn.GetUser(portalId, currentuserID)

                            Dim address As String = g.Address(userInfo.Profile.Unit, userInfo.Profile.Street, userInfo.Profile.City, userInfo.Profile.Region, userInfo.Profile.PostalCode, userInfo.Profile.Country)
                            Dim location As DotNetNuke.Modules.Map.Components.Location
                            location = g.Lookup(address)
                            If location.isSuccessful Then
                                'ADD THE POINT
                                s.AddPoint(SourceID, address, userInfo.FullName, userInfo.UserID, 0, location.Longitude, location.Latitude, userInfo.UserID, userInfo.Membership.CreatedDate, 0, 0, "", "", "", summaryOnly)
                            Else
                                'SET AS FAILURE
                            End If

                            'UPDATE SYSTEM
                            mapDC.UpdateSource_Service(SourceID, currentuserID)
                        End If
                    End While
                End If
            Catch ex As Exception

            End Try
        End Sub
    End Class
End Namespace