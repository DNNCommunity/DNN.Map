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
    Public Class Configuration

#Region "Private Variables"
        Private _MapType As Integer
        Private _Latitude As String = ""
        Private _Longitude As String = ""
        Private _Description As String = ""
        Private _Width As String = ""
        Private _Height As String = ""
        Private _DefaultType As String = ""
        Private _TypeDisplay As String = ""
        Private _NavigationDisplay As String = ""
        Private _OverviewMapDisplay As String = ""
        Private _Zoom As Integer
        Private _licenseKeys As New ArrayList
        Private _markers As New ArrayList
#End Region

#Region "Public Properties"
        Public Property MapType() As Integer
            Get
                Return _MapType
            End Get
            Set(ByVal Value As Integer)
                _MapType = Value
            End Set
        End Property
        Public Property Latitude() As String
            Get
                If _Latitude Is Nothing OrElse Not IsNumeric(_Latitude) Then
                    Return "0.0"
                End If
                Return _Latitude
            End Get
            Set(ByVal Value As String)
                _Latitude = Value
            End Set
        End Property
        Public Property Longitude() As String
            Get
                If _Longitude Is Nothing OrElse Not IsNumeric(_Longitude) Then
                    Return "0.0"
                End If
                Return _Longitude
            End Get
            Set(ByVal Value As String)
                _Longitude = Value
            End Set
        End Property
        Public Property Description() As String
            Get
                If _Description Is Nothing Then
                    Return ""
                Else
                    Return _Description
                End If
            End Get
            Set(ByVal Value As String)
                _Description = Value
            End Set
        End Property
        Public Property Width() As String
            Get
                If _Width Is Nothing Then
                    Return ""
                Else
                    Return _Width
                End If
            End Get
            Set(ByVal Value As String)
                _Width = Value
            End Set
        End Property
        Public Property Height() As String
            Get
                If _Height Is Nothing Then
                    Return ""
                Else
                    Return _Height
                End If
            End Get
            Set(ByVal Value As String)
                _Height = Value
            End Set
        End Property
        Public Property DefaultType() As String
            Get
                If _DefaultType Is Nothing Then
                    Return ""
                Else
                    Return _DefaultType
                End If
            End Get
            Set(ByVal Value As String)
                _DefaultType = Value
            End Set
        End Property
        Public Property TypeDisplay() As String
            Get
                If _TypeDisplay Is Nothing Then
                    Return ""
                Else
                    Return _TypeDisplay
                End If
            End Get
            Set(ByVal Value As String)
                _TypeDisplay = Value
            End Set
        End Property
        Public Property NavigationDisplay() As String
            Get
                If _NavigationDisplay Is Nothing Then
                    Return ""
                Else
                    Return _NavigationDisplay
                End If
            End Get
            Set(ByVal Value As String)
                _NavigationDisplay = Value
            End Set
        End Property
        Public Property OverviewMapDisplay() As String
            Get
                If _OverviewMapDisplay Is Nothing Then
                    Return ""
                Else
                    Return _OverviewMapDisplay
                End If
            End Get
            Set(ByVal Value As String)
                _OverviewMapDisplay = Value
            End Set
        End Property
        Public Property Zoom() As Integer
            Get
                Return _Zoom
            End Get
            Set(ByVal Value As Integer)
                _Zoom = Value
            End Set
        End Property
        Public Property LicenseKeys() As ArrayList
            Get
                If _licenseKeys Is Nothing Then
                    Return New ArrayList
                Else
                    Return _licenseKeys
                End If
            End Get
            Set(ByVal Value As ArrayList)
                _licenseKeys = Value
            End Set
        End Property
        Public Property Markers() As ArrayList
            Get
                If _markers Is Nothing Then
                    Return New ArrayList
                Else
                    Return _markers
                End If
            End Get
            Set(ByVal Value As ArrayList)
                _markers = Value
            End Set
        End Property
#End Region
#Region "Public Methods"
        'THIS FUNCTION IS ONLY USEFUL TO ORGANIZE THE ITEMS IN THE LIST
        Public Function CurrentLicenseKey(ByVal Domain As String) As String
            'FIND LICENSE KEY
            If Not _licenseKeys Is Nothing AndAlso _licenseKeys.Count > 0 Then
                Dim b As Boolean = False
                Dim match As LicenseKey = Nothing
                Dim i As Integer = 0
                If Domain.EndsWith("/") OrElse Domain.EndsWith("\") Then
                    Domain = Domain.Substring(0, Domain.Length - 1)
                End If
                If Domain.ToUpper.EndsWith(".ASPX") Then
                    Dim sposition As Integer = Domain.LastIndexOfAny(New Char() {"/", "\"})
                    If sposition > 0 Then
                        Domain = Domain.Substring(0, sposition)
                    End If
                End If
                i = 0
                b = False

                While i < _licenseKeys.Count AndAlso Not b
                    Dim lkey As LicenseKey
                    lkey = LicenseKeys(i)
                    If Not lkey.Domain Is Nothing AndAlso (lkey.Domain.EndsWith("/") OrElse lkey.Domain.EndsWith("\")) Then
                        lkey.Domain = lkey.Domain.Substring(0, lkey.Domain.Length - 1)
                    End If
                    If Not lkey.Domain Is Nothing AndAlso lkey.Domain.Replace("\", "/").ToUpper = Domain.Replace("\", "/").ToUpper OrElse Domain.Replace("\", "/").ToUpper.StartsWith(lkey.Domain.Replace("\", "/").ToUpper()) OrElse i = 0 Then
                        match = lkey
                        If lkey.Domain.Replace("\", "/").ToUpper = Domain.Replace("\", "/").ToUpper Then
                            b = True
                        End If
                    End If
                    i += 1
                End While
                Return match.Key
            End If
            Return Nothing
        End Function
        Public Sub SequenceKeys()
            If Not _licenseKeys Is Nothing AndAlso _licenseKeys.Count > 0 Then
                Dim lkValue As LicenseKey

                Dim i As Integer = 0
                For Each lkValue In _licenseKeys
                    lkValue.Index = i
                    i += 1
                Next
            End If
        End Sub
        Public Sub SequenceMarkers()
            If Not _markers Is Nothing AndAlso _markers.Count > 0 Then
                Dim mkValue As Marker

                Dim i As Integer = 0
                For Each mkValue In _markers
                    mkValue.Index = i
                    i += 1
                Next
            End If
        End Sub
        Public Sub SwapMarkers(ByVal IndexA As Integer, ByVal IndexB As Integer)
            Dim thisindex As Integer = 0
            Dim newlist As New ArrayList
            Dim markerA As Marker = _markers(IndexA)
            Dim markerB As Marker = _markers(IndexB)
            Dim markerC As Marker
            If Not markerA Is Nothing AndAlso Not markerB Is Nothing Then
                For Each markerC In _markers
                    If markerC.Index = IndexA Then
                        newlist.Add(markerB)
                    ElseIf markerC.Index = IndexB Then
                        newlist.Add(markerA)
                    Else
                        newlist.Add(markerC)
                    End If
                    thisindex += 1
                Next
                markerA.Index = IndexB
                markerB.Index = IndexA

                _markers = newlist
            End If
        End Sub
#End Region

#Region "Standard Variables and Public Properties"
        Private _delayInitial As Integer
        Private _delayRetry As Integer

        Public Property delayInitial() As Integer
            Get
                Return _delayInitial
            End Get
            Set(ByVal Value As Integer)
                _delayInitial = Value
            End Set
        End Property
        Public Property delayRetry() As Integer
            Get
                Return _delayRetry
            End Get
            Set(ByVal Value As Integer)
                _delayRetry = Value
            End Set
        End Property
#End Region

#Region "ISerializable Members"
        Sub New()
            LicenseKeys = New ArrayList
            Markers = New ArrayList
        End Sub
        Protected Function CheckNothing(ByVal Value As String) As String
            If Not Value Is Nothing Then Return Value
            Return ""
        End Function
        Public Sub New(ByVal hsh As Hashtable)
            _DefaultType = CType(hsh.Item("_DefaultType"), String)
            _Description = CType(hsh.Item("_Description"), String)
            _Height = CType(hsh.Item("_Height"), String)
            _Latitude = CType(hsh.Item("_Latitude"), String)
            _Longitude = CType(hsh.Item("_Longitude"), String)
            _NavigationDisplay = CType(hsh.Item("_NavigationDisplay"), String)
            _OverviewMapDisplay = CType(hsh.Item("_OverviewMapDisplay"), String)
            _TypeDisplay = CType(hsh.Item("_TypeDisplay"), String)
            _Width = CType(hsh.Item("_Width"), String)
            _Zoom = CType(hsh.Item("_Zoom"), Integer)
            _MapType = CType(hsh.Item("_MapType"), Integer)
            Try
                _licenseKeys = CType(LicenseKey.DeserializeArray(hsh.Item("_licenseKeys")), ArrayList)
            Catch ex As Exception
                _licenseKeys = New ArrayList
            End Try
            Try
                _markers = CType(Marker.DeserializeArray(hsh.Item("_markers")), ArrayList)
            Catch ex As Exception
                _markers = New ArrayList
            End Try

            _delayInitial = CType(hsh.Item("_delayInitial"), Integer)
            _delayRetry = CType(hsh.Item("_delayRetry"), Integer)
        End Sub
        Public Overridable Sub Settings(ByRef hsh As Hashtable)
            If _licenseKeys Is Nothing Then
                _licenseKeys = New ArrayList
            End If
            If _markers Is Nothing Then
                _markers = New ArrayList
            End If

            hsh.Add("_DefaultType", _DefaultType)
            hsh.Add("_Description", _Description)
            hsh.Add("_Height", _Height)
            hsh.Add("_Latitude", _Latitude)
            hsh.Add("_Longitude", _Longitude)
            hsh.Add("_NavigationDisplay", _NavigationDisplay)
            hsh.Add("_TypeDisplay", _TypeDisplay)
            hsh.Add("_OverviewMapDisplay", _OverviewMapDisplay)
            hsh.Add("_Width", _Width)
            hsh.Add("_Zoom", _Zoom)
            hsh.Add("_MapType", _MapType)
            hsh.Add("_licenseKeys", LicenseKey.SerializeArray(_licenseKeys))
            hsh.Add("_markers", Marker.SerializeArray(_markers))

            hsh.Add("_delayInitial", _delayInitial)
            hsh.Add("_delayRetry", _delayRetry)
        End Sub
#End Region

        Public Class LicenseKey
            Public Index As Integer
            Private _Domain As String
            Private _Key As String

            Public Property Domain() As String
                Get
                    Return _Domain
                End Get
                Set(ByVal Value As String)
                    _Domain = Value
                End Set
            End Property
            Public Property Key() As String
                Get
                    Return _Key
                End Get
                Set(ByVal Value As String)
                    _Key = Value
                End Set
            End Property

            Sub New()
            End Sub
            Public Sub New(ByVal hsh As Hashtable)
                _Domain = CType(hsh.Item("_Domain"), String)
                _Key = CType(hsh.Item("_Key"), String)
            End Sub
            Public Sub Settings(ByVal hsh As Hashtable)
                hsh.Add("_Domain", _Domain)
                hsh.Add("_Key", _Key)
            End Sub
            Public Shared Function Serialize(ByVal Value As LicenseKey) As String
                Dim hsh As New Hashtable
                Value.Settings(hsh)
                Return DotNetNuke.Common.SerializeHashTableXml(hsh)
            End Function
            Public Shared Function Deserialize(ByVal Value As String) As LicenseKey
                Dim m As LicenseKey = Nothing
                Try
                    Dim hsh As Hashtable
                    hsh = DotNetNuke.Common.DeserializeHashTableXml(Value)
                    m = New LicenseKey(hsh)
                Catch ex As Exception
                End Try
                Return m
            End Function

            Public Shared Function SerializeArray(ByVal Value As ArrayList) As String
                Dim hsh As Hashtable = New Hashtable
                If Value Is Nothing Then Value = New ArrayList
                hsh.Add("Count", Value.Count)
                Dim i As Integer
                For i = 0 To Value.Count - 1
                    Dim strXml As String = Serialize(Value(i))
                    hsh.Add("Item-" & i.ToString, strXml)
                Next
                Return DotNetNuke.Common.SerializeHashTableXml(hsh)
            End Function
            Public Shared Function DeserializeArray(ByVal Source As String) As ArrayList
                Dim arr As New ArrayList
                If Source Is Nothing OrElse Source.Length = 0 Then
                    Return arr
                Else
                    Dim hsh As Hashtable = DotNetNuke.Common.DeserializeHashTableXml(Source)
                    If Not hsh Is Nothing Then
                        Dim count As Integer = CInt(hsh.Item("Count"))
                        If count > 0 Then
                            Dim i As Integer
                            For i = 0 To count - 1
                                If hsh.ContainsKey("Item-" & i.ToString) Then
                                    arr.Add(Deserialize(hsh.Item("Item-" & i.ToString)))
                                End If
                            Next
                        End If
                    End If
                End If
                Return arr
            End Function
        End Class
        Public Class Marker
            Public Index As Integer
            Private _Icon As String
            Private _Shadow As String
            Private _IconWidth As Integer
            Private _IconHeight As Integer
            Private _ShadowWidth As Integer
            Private _ShadowHeight As Integer
            Private _AnchorX As Integer
            Private _AnchorY As Integer
            Private _InfoX As Integer
            Private _InfoY As Integer

            Public Property Icon() As String
                Get
                    Return _Icon
                End Get
                Set(ByVal Value As String)
                    _Icon = Value
                End Set
            End Property
            Public Property Shadow() As String
                Get
                    Return _Shadow
                End Get
                Set(ByVal Value As String)
                    _Shadow = Value
                End Set
            End Property
            Public Property IconWidth() As Integer
                Get
                    Return _IconWidth
                End Get
                Set(ByVal Value As Integer)
                    _IconWidth = Value
                End Set
            End Property
            Public Property IconHeight() As Integer
                Get
                    Return _IconHeight
                End Get
                Set(ByVal Value As Integer)
                    _IconHeight = Value
                End Set
            End Property
            Public Property ShadowWidth() As Integer
                Get
                    Return _ShadowWidth
                End Get
                Set(ByVal Value As Integer)
                    _ShadowWidth = Value
                End Set
            End Property
            Public Property ShadowHeight() As Integer
                Get
                    Return _ShadowHeight
                End Get
                Set(ByVal Value As Integer)
                    _ShadowHeight = Value
                End Set
            End Property
            Public Property AnchorX() As Integer
                Get
                    Return _AnchorX
                End Get
                Set(ByVal Value As Integer)
                    _AnchorX = Value
                End Set
            End Property
            Public Property AnchorY() As Integer
                Get
                    Return _AnchorY
                End Get
                Set(ByVal Value As Integer)
                    _AnchorY = Value
                End Set
            End Property
            Public Property InfoX() As Integer
                Get
                    Return _InfoX
                End Get
                Set(ByVal Value As Integer)
                    _InfoX = Value
                End Set
            End Property
            Public Property InfoY() As Integer
                Get
                    Return _InfoY
                End Get
                Set(ByVal Value As Integer)
                    _InfoY = Value
                End Set
            End Property

            Sub New()
            End Sub
            Public Sub New(ByVal hsh As Hashtable)
                _Icon = CType(hsh.Item("_Icon"), String)
                _Shadow = CType(hsh.Item("_Shadow"), String)
                _IconWidth = CType(hsh.Item("_IconWidth"), Integer)
                _IconHeight = CType(hsh.Item("_IconHeight"), Integer)
                _ShadowWidth = CType(hsh.Item("_ShadowWidth"), Integer)
                _ShadowHeight = CType(hsh.Item("_ShadowHeight"), Integer)
                _AnchorX = CType(hsh.Item("_AnchorX"), Integer)
                _AnchorY = CType(hsh.Item("_AnchorY"), Integer)
                _InfoX = CType(hsh.Item("_InfoX"), Integer)
                _InfoY = CType(hsh.Item("_InfoY"), Integer)
            End Sub
            Public Sub Settings(ByRef hsh As Hashtable)
                hsh.Add("_Icon", _Icon)
                hsh.Add("_Shadow", _Shadow)
                hsh.Add("_IconWidth", _IconWidth)
                hsh.Add("_IconHeight", _IconHeight)
                hsh.Add("_ShadowWidth", _ShadowWidth)
                hsh.Add("_ShadowHeight", _ShadowHeight)
                hsh.Add("_AnchorX", _AnchorX)
                hsh.Add("_AnchorY", _AnchorY)
                hsh.Add("_InfoX", _InfoX)
                hsh.Add("_InfoY", _InfoY)
            End Sub
            Public Shared Function Serialize(ByVal Value As Marker) As String
                Dim hsh As New Hashtable
                Value.Settings(hsh)
                Return DotNetNuke.Common.SerializeHashTableXml(hsh)
            End Function
            Public Shared Function Deserialize(ByVal Value As String) As Marker
                Dim m As Marker = Nothing
                Try
                    Dim hsh As Hashtable
                    hsh = DotNetNuke.Common.DeserializeHashTableXml(Value)
                    m = New Marker(hsh)
                Catch ex As Exception
                End Try
                Return m
            End Function

            Public Shared Function SerializeArray(ByVal Value As ArrayList) As String
                Dim hsh As Hashtable = New Hashtable
                If Value Is Nothing Then Value = New ArrayList
                hsh.Add("Count", Value.Count)
                Dim i As Integer
                For i = 0 To Value.Count - 1
                    Dim strXml As String = Serialize(Value(i))
                    hsh.Add("Item-" & i.ToString, strXml)
                Next
                Return DotNetNuke.Common.SerializeHashTableXml(hsh)
            End Function
            Public Shared Function DeserializeArray(ByVal Source As String) As ArrayList
                Dim arr As New ArrayList
                If Source Is Nothing OrElse Source.Length = 0 Then
                    Return arr
                Else
                    Dim hsh As Hashtable = DotNetNuke.Common.DeserializeHashTableXml(Source)
                    If Not hsh Is Nothing Then
                        Dim count As Integer = CInt(hsh.Item("Count"))
                        If count > 0 Then
                            Dim i As Integer
                            For i = 0 To count - 1
                                arr.Add(Deserialize(hsh.Item("Item-" & i.ToString)))
                            Next
                        End If
                    End If
                End If
                Return arr
            End Function
        End Class
    End Class
#End Region

End Namespace

