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
Imports Microsoft.ApplicationBlocks.Data
Imports DotNetNuke
Imports DotNetNuke.Modules.Map.Components
Imports DotNetNuke.Modules.Map.Data

Namespace DotNetNuke.Modules.Map.Data
    Public Class Standard
        Inherits DotNetNuke.Modules.Map.Components.SourceProvider

#Region "Private Members"
        Private Const ProviderType As String = "data"
        Private _ProviderConfiguration As Configuration
        Private _connectionString As String
#End Region

#Region "Configuration"
        Public Class Configuration

#Region "Private Variables"
            Private _connectionString As String = ""
            Private _connectionString_isName As Boolean
            Private _objectQualifier As String = ""
            Private _databaseOwner As String = ""
            Private _customquery As String = ""
            Private _renderType As Integer
            Private _queryvariables As New ArrayList
            Private _format As String = ""
            Private _summaryOnly As Boolean
#End Region
#Region "Public Properties"
            Public Property RenderType() As Integer
                Get
                    Return _renderType
                End Get
                Set(ByVal Value As Integer)
                    _renderType = Value
                End Set
            End Property
            Public Property Format() As String
                Get
                    If Not _format Is Nothing Then
                        Return _format
                    Else
                        Return ""
                    End If
                End Get
                Set(ByVal Value As String)
                    _format = Value
                End Set
            End Property
            Public Property SummaryOnly() As Boolean
                Get
                    Return _summaryOnly
                End Get
                Set(ByVal Value As Boolean)
                    _summaryOnly = Value
                End Set
            End Property
            Public Property CustomQuery() As String
                Get
                    If Not _customquery Is Nothing Then
                        Return _customquery
                    Else
                        Return ""
                    End If
                End Get
                Set(ByVal Value As String)
                    _customquery = Value
                End Set
            End Property
            Public Property ConnectionString() As String
                Get
                    If Not _connectionString Is Nothing Then
                        Return _connectionString
                    Else
                        Return ""
                    End If
                End Get
                Set(ByVal Value As String)
                    _connectionString = Value
                End Set
            End Property
            Public Property ConnectionStringIsName() As Boolean
                Get
                    Return _connectionString_isName
                End Get
                Set(ByVal Value As Boolean)
                    _connectionString_isName = Value
                End Set
            End Property
            Public Property ObjectQualifier() As String
                Get
                    If Not _objectQualifier Is Nothing Then
                        Return _objectQualifier
                    Else
                        Return ""
                    End If
                End Get
                Set(ByVal Value As String)
                    _objectQualifier = Value
                End Set
            End Property
            Public Property DatabaseOwner() As String
                Get
                    If Not _databaseOwner Is Nothing Then
                        Return _databaseOwner
                    Else
                        Return ""
                    End If
                End Get
                Set(ByVal Value As String)
                    _databaseOwner = Value
                End Set
            End Property
            Public Property QueryVariables() As ArrayList
                Get
                    Return _queryvariables
                End Get
                Set(ByVal Value As ArrayList)
                    _queryvariables = Value
                End Set
            End Property
#End Region
#Region "Public Methods"
            'THIS FUNCTION IS REQUIRED SO THAT REPLACEMENT FUNCTION REPLACE FROM THE LONGEST TEXT THE SHORTEST
            Public Sub SequenceQueryItems()
                If Not _queryvariables Is Nothing AndAlso _queryvariables.Count > 0 Then
                    Dim oQItems As New LengthSortableArrayList
                    Dim qVariable As QueryVariable
                    For Each qVariable In _queryvariables
                        oQItems.Add(qVariable.Target, qVariable)
                    Next
                    oQItems.Sort()
                    oQItems.Reverse()

                    _queryvariables = oQItems.ToArrayList

                    Dim i As Integer = 0
                    For Each qVariable In _queryvariables
                        qVariable.Index = i
                        i += 1
                    Next
                End If
            End Sub
#End Region

#Region "ISerializable Members"
            Sub New()
                QueryVariables = New ArrayList
            End Sub
            Public Sub New(ByVal info As Hashtable)
                _connectionString = CType(info.Item("_connectionString"), String)
                _connectionString_isName = CType(info.Item("_connectionString_isName"), Boolean)
                _objectQualifier = CType(info.Item("_objectQualifier"), String)
                _databaseOwner = CType(info.Item("_databaseOwner"), String)
                _format = CType(info.Item("_format"), String)
                _summaryOnly = CType(info.Item("_summaryOnly"), Boolean)
                Try
                    _renderType = CType(info.Item("_renderType"), Integer)
                Catch ex As Exception
                    _renderType = 0
                End Try
                Try
                    _customquery = CType(info.Item("_customquery"), String)
                Catch ex As Exception
                    _customquery = ""
                End Try
                Try
                    _queryvariables = CType(QueryVariable.DeserializeArray(info.Item("_queryvariables")), ArrayList)
                Catch ex As Exception
                    _queryvariables = New ArrayList
                End Try
            End Sub
            Public Overridable Sub Settings(ByRef info As Hashtable)
                info.Add("_connectionString", ConnectionString)
                info.Add("_connectionString_isName", _connectionString_isName)
                info.Add("_objectQualifier", ObjectQualifier)
                info.Add("_databaseOwner", DatabaseOwner)
                info.Add("_renderType", RenderType)
                info.Add("_customquery", CustomQuery)
                info.Add("_format", Format)
                info.Add("_summaryOnly", _summaryOnly)
                info.Add("_queryvariables", QueryVariable.SerializeArray(_queryvariables))
            End Sub

            Public Shared Function Serialize(ByVal Value As Configuration) As String
                Dim hsh As New Hashtable
                Value.Settings(hsh)
                Return DotNetNuke.Common.SerializeHashTableXml(hsh)
            End Function
            Public Shared Function Deserialize(ByVal Value As String) As Configuration
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

            Public Class QueryVariable

                Public Index As Integer
                Private _VariableType As String
                Private _EscapeSingleQuotes As Boolean
                Private _TargetLeft As String
                Private _TargetRight As String
                Private _TargetEmpty As String
                Private _Source As String
                Private _Target As String

                Public Property VariableType() As String
                    Get
                        Return _VariableType
                    End Get
                    Set(ByVal Value As String)
                        _VariableType = Value
                    End Set
                End Property
                Public Property Source() As String
                    Get
                        Return _Source
                    End Get
                    Set(ByVal Value As String)
                        _Source = Value
                    End Set
                End Property
                Public Property Target() As String
                    Get
                        Return _Target
                    End Get
                    Set(ByVal Value As String)
                        _Target = Value
                    End Set
                End Property
                Public Property TargetLeft() As String
                    Get
                        Return _TargetLeft
                    End Get
                    Set(ByVal Value As String)
                        _TargetLeft = Value
                    End Set
                End Property
                Public Property TargetRight() As String
                    Get
                        Return _TargetRight
                    End Get
                    Set(ByVal Value As String)
                        _TargetRight = Value
                    End Set
                End Property
                Public Property TargetEmpty() As String
                    Get
                        Return _TargetEmpty
                    End Get
                    Set(ByVal Value As String)
                        _TargetEmpty = Value
                    End Set
                End Property
                Public Property EscapeSingleQuotes() As Boolean
                    Get
                        Return _EscapeSingleQuotes
                    End Get
                    Set(ByVal Value As Boolean)
                        _EscapeSingleQuotes = Value
                    End Set
                End Property

                Sub New()
                End Sub
                Public Sub New(ByVal info As Hashtable)
                    _VariableType = CType(info.Item("_VariableType"), String)
                    _EscapeSingleQuotes = CType(info.Item("_EscapeSingleQuotes"), Boolean)
                    _TargetLeft = CType(info.Item("_TargetLeft"), String)
                    _TargetRight = CType(info.Item("_TargetRight"), String)
                    _TargetEmpty = CType(info.Item("_TargetEmpty"), String)
                    _Source = CType(info.Item("_Source"), String)
                    _Target = CType(info.Item("_Target"), String)
                End Sub
                Public Sub Settings(ByRef info As Hashtable)
                    info.Add("_VariableType", _VariableType)
                    info.Add("_EscapeSingleQuotes", _EscapeSingleQuotes)
                    info.Add("_TargetLeft", _TargetLeft)
                    info.Add("_TargetRight", _TargetRight)
                    info.Add("_TargetEmpty", _TargetEmpty)
                    info.Add("_Source", _Source)
                    info.Add("_Target", _Target)
                End Sub

                Public Shared Function Serialize(ByVal Value As QueryVariable) As String
                    Dim hsh As New Hashtable
                    Value.Settings(hsh)
                    Return DotNetNuke.Common.SerializeHashTableXml(hsh)
                End Function
                Public Shared Function Deserialize(ByVal Value As String) As QueryVariable
                    Dim m As QueryVariable = Nothing
                    Try
                        Dim hsh As Hashtable
                        hsh = DotNetNuke.Common.DeserializeHashTableXml(Value)
                        m = New QueryVariable(hsh)
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
            Private Class LengthSortableArrayList : Inherits ArrayList
                Private Class SequenceItem
                    Implements IComparable

                    Public Text As String
                    Public Value As Object
                    Public Index As Integer
                    Public Length As Integer

                    Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
                        If TypeOf obj Is SequenceItem Then
                            Dim len As Integer = CType(obj, SequenceItem).Length
                            If len > Length Then
                                Return -1
                            ElseIf len < Length Then
                                Return 1
                            Else
                                If (CType(obj, SequenceItem).Text <> Text) Then
                                    Return -1 * String.Compare(CType(obj, SequenceItem).Text, Text)
                                Else
                                    Return -1 * Index.CompareTo((CType(obj, SequenceItem).Index))
                                End If
                            End If
                        Else
                            Return 0
                        End If
                    End Function
                End Class
                Public Shadows Sub Add(ByVal Text As String, ByVal Value As Object)
                    Dim obj As New SequenceItem
                    obj.Length = Text.Length
                    obj.Text = Text
                    obj.Value = Value
                    obj.Index = MyBase.Count
                    MyBase.Add(obj)
                End Sub
                Public Function ToArrayList() As ArrayList
                    Dim arr As New ArrayList
                    Dim objX As SequenceItem
                    For Each objX In MyBase.ToArray
                        arr.Add(objX.Value)
                    Next
                    Return arr
                End Function
            End Class
        End Class
#End Region

#Region "Constructors"
        Public Sub New(ByRef Settings As Standard.Configuration)
            MyBase.New(Settings)
            If Not Settings Is Nothing Then
                _ProviderConfiguration = Settings
            Else
                _ProviderConfiguration = New Configuration
                _ProviderConfiguration.ConnectionStringIsName = False
                Settings = _ProviderConfiguration
                _ProviderConfiguration.RenderType = 1
            End If

            If _ProviderConfiguration.ConnectionStringIsName Then
                If Not _ProviderConfiguration.ConnectionString Is Nothing AndAlso _ProviderConfiguration.ConnectionString.IndexOf("=") > 0 Then
                    _connectionString = System.Configuration.ConfigurationManager.AppSettings(_ProviderConfiguration.ConnectionString)
                Else
                    _connectionString = DotNetNuke.Common.GetDBConnectionString() 'NOTE: Replaced for 4.x compliance - DotNetNuke.Common.GetDBConnectionString
                End If
            Else
                If Settings.RenderType = 3 Then
                    _connectionString = _ProviderConfiguration.ConnectionString
                Else
                    _connectionString = DotNetNuke.Common.GetDBConnectionString() 'NOTE: Replaced for 4.x compliance -  DotNetNuke.Common.GetDBConnectionString
                End If
            End If

        End Sub
#End Region

#Region "Properties"
        Public ReadOnly Property ConnectionString() As String
            Get
                Return _connectionString
            End Get
        End Property
#End Region

#Region "General Public Methods"
        Private Function GetNull(ByVal Field As Object) As Object
            Return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value)
        End Function
#End Region

        Public Function MapServices_Users(ByVal SourceID As Integer, ByVal LastUserID As Integer) As IDataReader
            Return SqlHelper.ExecuteReader(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_GetSyncUsers", SourceID, LastUserID)
        End Function
        Public Overrides Function AddPoint(ByVal SourceID As Integer, ByVal Address As String, ByVal Description As String, ByVal GUID As String, ByVal IconIndex As Integer, ByVal Longitude As Double, ByVal Latitude As Double, ByVal SequenceNumber As Integer, ByVal SequenceInfo As String, ByVal ZoomShow As Integer, ByVal ZoomHide As Integer, ByVal ActionPlot As String, ByVal ActionClick As String, ByVal ActionOpen As String, ByVal SummaryOnly As Boolean) As Integer
            Return CType(SqlHelper.ExecuteScalar(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_AddPoint", SourceID, GUID, Address, Description, IconIndex, Longitude, Latitude, SequenceNumber, SequenceInfo, ZoomShow, ZoomHide, ActionPlot, ActionClick, ActionOpen, SummaryOnly), Integer)
        End Function

        Public Overrides Sub DeletePoint(ByVal SourceID As Integer, ByVal PointID As Integer)
            SqlHelper.ExecuteScalar(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_DeletePoint", PointID)
        End Sub
        Public Overrides Sub DeletePoints(ByVal SourceID As Integer)
            SqlHelper.ExecuteScalar(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_DeletePoints", SourceID)
        End Sub

        Public Overloads Overrides Function GetPoint(ByVal PointID As Integer) As Data.DataPoint
            Return DotNetNuke.Common.Utilities.CBO.FillObject(SqlHelper.ExecuteReader(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_GetPoint", PointID), GetType(DataPoint))
        End Function
        Public Overloads Function GetPoint(ByVal SourceID As Integer, ByVal GUID As String) As DataPoint
            Return DotNetNuke.Common.Utilities.CBO.FillObject(SqlHelper.ExecuteReader(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_GetPoint_ByGUID", SourceID, GUID), GetType(Data.DataPoint))
        End Function
        Public Function GetPoints_ByAddress(ByVal SourceID As Integer, ByVal Address As String) As ArrayList
            Return DotNetNuke.Common.Utilities.CBO.FillCollection(SqlHelper.ExecuteReader(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_GetPoint_ByAddress", SourceID, Address), GetType(Data.DataPoint))
        End Function
        Public Function GetPoints_Unassigned(ByVal SourceID As Integer) As ArrayList
            Dim filter As New PointFilterInformation
            filter.SourceID = SourceID
            filter.SourceInformation = New Hashtable
            filter.SourceInformation("NoLocation") = True
            Return GetPoints(filter)
        End Function

        Public Overrides Function GetPoints_ByProviderName_NoGeo(ByVal ProviderName As String, ByVal IsUser As Boolean, Optional ByVal SourceID As Integer = -1) As IDataReader
            Return SqlHelper.ExecuteReader(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_GetPoints_ByProviderName_NoGeo", ProviderName, IsUser, SourceID)
        End Function
        Public Sub UpdatePoint_Location(ByVal PointID As Integer, ByVal FailedGeo As Boolean, ByVal Longitude As Double, ByVal Latitude As Double)
            SqlHelper.ExecuteNonQuery(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_UpdatePoint_Location", PointID, FailedGeo, Longitude, Latitude)
        End Sub

        Public Overrides Function GetPoints(ByVal PointFilter As PointFilterInformation) As ArrayList
            Dim Zoom As Integer
            Dim Page As Integer = 0
            Dim PerPage As Integer = 0
            Dim SourceID As Integer = PointFilter.SourceID
            If Not PointFilter.SourceInformation Is Nothing AndAlso Not PointFilter.SourceInformation.ContainsKey("CustomQuery") AndAlso PointFilter.SourceInformation.ContainsKey("distance".ToLower) AndAlso IsNumeric(PointFilter.SourceInformation("distance".ToLower)) AndAlso CType(PointFilter.SourceInformation("distance".ToLower), Integer) > 0 Then
                Dim Distance As Double
                Dim Latitude As Double
                Dim Longitude As Double
                Dim Scale As String = ""

                If PointFilter.SourceInformation.ContainsKey("latitude".ToLower) Then
                    Latitude = Utility.FormatNumber(PointFilter.SourceInformation("latitude".ToLower), False, False)
                End If
                If PointFilter.SourceInformation.ContainsKey("longitude".ToLower) Then
                    Longitude = Utility.FormatNumber(PointFilter.SourceInformation("longitude".ToLower), False, False)
                End If
                If PointFilter.SourceInformation.ContainsKey("distance".ToLower) Then
                    Distance = Utility.FormatNumber(PointFilter.SourceInformation("distance".ToLower), False, False)
                End If
                If PointFilter.SourceInformation.ContainsKey("scale".ToLower) Then
                    Scale = PointFilter.SourceInformation("scale".ToLower)
                End If
                If PointFilter.SourceInformation.ContainsKey("Zoom") Then
                    Zoom = PointFilter.SourceInformation("Zoom")
                End If
                If PointFilter.SourceInformation.ContainsKey("Page") Then
                    Page = PointFilter.SourceInformation("Page")
                End If
                If PointFilter.SourceInformation.ContainsKey("PerPage") Then
                    PerPage = PointFilter.SourceInformation("PerPage")
                End If
                'Return DotNetNuke.Common.Utilities.CBO.FillCollection(SqlHelper.ExecuteReader(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_GetPoints_ByLocation", SourceID, Zoom, Latitude, Longitude, Distance, Scale), GetType(Data.DataPoint))
                Return FillCollection(SqlHelper.ExecuteReader(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_GetPoints_ByLocation", SourceID, Zoom, Latitude, Longitude, Distance, Scale), PerPage, Page)
            ElseIf Not PointFilter.SourceInformation Is Nothing AndAlso Not PointFilter.SourceInformation.ContainsKey("CustomQuery") AndAlso PointFilter.SourceInformation.ContainsKey("NoLocation") Then
                'Return DotNetNuke.Common.Utilities.CBO.FillCollection(SqlHelper.ExecuteReader(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_GetPoints_NoLocation", SourceID), GetType(Data.DataPoint))
                If PointFilter.SourceInformation.ContainsKey("Page") Then
                    Page = PointFilter.SourceInformation("Page")
                End If
                If PointFilter.SourceInformation.ContainsKey("PerPage") Then
                    PerPage = PointFilter.SourceInformation("PerPage")
                End If
                Return FillCollection(SqlHelper.ExecuteReader(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_GetPoints_NoLocation", SourceID), PerPage, Page)
            ElseIf Not PointFilter.SourceInformation Is Nothing AndAlso PointFilter.SourceInformation.ContainsKey("CustomQuery") Then
                If PointFilter.SourceInformation.ContainsKey("Page") Then
                    Page = PointFilter.SourceInformation("Page")
                End If
                If PointFilter.SourceInformation.ContainsKey("PerPage") Then
                    PerPage = PointFilter.SourceInformation("PerPage")
                End If
                If PointFilter.SourceInformation.ContainsKey("CustomConnection") Then
                    'Return DotNetNuke.Common.Utilities.CBO.FillCollection(SqlHelper.ExecuteReader(PointFilter.SourceInformation("CustomConnection"), CommandType.Text, PointFilter.SourceInformation("CustomQuery")), GetType(Map.Data.DataPoint))
                    Return FillCollection(SqlHelper.ExecuteReader(PointFilter.SourceInformation("CustomConnection"), CommandType.Text, PointFilter.SourceInformation("CustomQuery")), PerPage, Page)
                Else
                    Return FillCollection(SqlHelper.ExecuteReader(ConnectionString, CommandType.Text, PointFilter.SourceInformation("CustomQuery")), PerPage, Page)
                    'Return DotNetNuke.Common.Utilities.CBO.FillCollection(SqlHelper.ExecuteReader(ConnectionString, CommandType.Text, PointFilter.SourceInformation("CustomQuery")), GetType(Map.Data.DataPoint))
                End If
            Else
                If PointFilter.SourceInformation.ContainsKey("Page") Then
                    Page = PointFilter.SourceInformation("Page")
                End If
                If PointFilter.SourceInformation.ContainsKey("PerPage") Then
                    PerPage = PointFilter.SourceInformation("PerPage")
                End If
                If PointFilter.SourceInformation.ContainsKey("Zoom") Then
                    Zoom = PointFilter.SourceInformation("Zoom")
                End If
                'Return DotNetNuke.Common.Utilities.CBO.FillCollection(SqlHelper.ExecuteReader(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_GetPoints", SourceID, Zoom), GetType(Map.Data.DataPoint))
                Return FillCollection(SqlHelper.ExecuteReader(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_GetPoints", SourceID, Zoom), PerPage, Page)
            End If
        End Function

        Private Function FillCollection(ByVal iDR As IDataReader, ByVal RecordsPerPage As Integer, ByVal PageIndex As Integer) As ArrayList
            Dim rowPosition As Integer = 1
            Dim arr As New ArrayList
            Dim hsh As New Hashtable

            While (RecordsPerPage = 0 Or rowPosition < (PageIndex + 1) * RecordsPerPage) And iDR.Read
                If Not rowPosition < (PageIndex) * RecordsPerPage Then
                    Dim Point As New Data.DataPoint
                    If hsh.Count = 0 Then
                        Dim feildcount As Integer = iDR.FieldCount - 1
                        While feildcount >= 0
                            hsh.Add(iDR.GetName(feildcount).ToLower, feildcount)
                            feildcount -= 1
                        End While
                    End If

                    SetPointValue("ActionClick", Point, iDR, hsh)
                    SetPointValue("ActionOpen", Point, iDR, hsh)
                    SetPointValue("ActionPlot", Point, iDR, hsh)
                    SetPointValue("Address", Point, iDR, hsh)
                    SetPointValue("Description", Point, iDR, hsh)
                    SetPointValue("Distance", Point, iDR, hsh)
                    SetPointValue("GUID", Point, iDR, hsh)
                    SetPointValue("IconIndex", Point, iDR, hsh)
                    SetPointValue("Latitude", Point, iDR, hsh)
                    SetPointValue("Longitude", Point, iDR, hsh)
                    SetPointValue("PointID", Point, iDR, hsh)
                    SetPointValue("SequenceInfo", Point, iDR, hsh)
                    SetPointValue("SequenceNumber", Point, iDR, hsh)
                    SetPointValue("SummaryCount", Point, iDR, hsh)
                    SetPointValue("ZoomHide", Point, iDR, hsh)
                    SetPointValue("ZoomShow", Point, iDR, hsh)
                    arr.Add(Point)
                End If
                rowPosition += 1
            End While
            While iDR.Read
                rowPosition += 1
            End While
            iDR.Close()
            iDR.Dispose()
            arr.Capacity = rowPosition
            Return arr
        End Function
        Private Sub SetPointValue(ByVal Name As String, ByRef Point As Data.DataPoint, ByRef DataRow As IDataReader, ByRef Fields As Hashtable)
            Dim value As Object = Nothing
            If Fields.ContainsKey(Name.ToLower) Then
                Dim index As Integer = -1
                index = CType(Fields.Item(Name.ToLower), Integer)
                If Not IsDBNull(DataRow(index)) Then
                    value = DataRow(index)
                End If
            End If
            If Not value Is Nothing Then
                Select Case Name.ToLower
                    Case "ActionClick".ToLower
                        Point.ActionClick = value
                    Case "ActionOpen".ToLower
                        Point.ActionOpen = value
                    Case "ActionPlot".ToLower
                        Point.ActionPlot = value
                    Case "Address".ToLower
                        Point.Address = value
                    Case "Description".ToLower
                        Point.Description = value
                    Case "Distance".ToLower
                        Point.Distance = value
                    Case "GUID".ToLower
                        Point.GUID = value
                    Case "IconIndex".ToLower
                        Point.IconIndex = value
                    Case "Latitude".ToLower
                        Point.Latitude = value
                    Case "Longitude".ToLower
                        Point.Longitude = value
                    Case "PointID".ToLower
                        Point.PointID = value
                    Case "SequenceInfo".ToLower
                        Point.SequenceInfo = value
                    Case "SequenceNumber".ToLower
                        Point.SequenceNumber = value
                    Case "SummaryCount".ToLower
                        Point.SummaryCount = value
                    Case "ZoomHide".ToLower
                        Point.ZoomHide = value
                    Case "ZoomShow".ToLower
                        Point.ZoomShow = value
                End Select
            End If
        End Sub

        Public Overrides Sub UpdatePoint(ByVal PointID As Integer, ByVal SourceID As Integer, ByVal GUID As String, ByVal Address As String, ByVal Description As String, ByVal IconIndex As Integer, ByVal Longitude As Double, ByVal Latitude As Double, ByVal SequenceNumber As Integer, ByVal SequenceInfo As String, ByVal ZoomShow As Integer, ByVal ZoomHide As Integer, ByVal ActionPlot As String, ByVal ActionClick As String, ByVal ActionOpen As String)
            SqlHelper.ExecuteNonQuery(ConnectionString, _ProviderConfiguration.DatabaseOwner & _ProviderConfiguration.ObjectQualifier & "Map_UpdatePoint", PointID, SourceID, GUID, Address, Description, IconIndex, Longitude, Latitude, SequenceNumber, SequenceInfo, ZoomShow, ZoomHide, ActionPlot, ActionClick, ActionOpen)
        End Sub

    End Class
End Namespace
