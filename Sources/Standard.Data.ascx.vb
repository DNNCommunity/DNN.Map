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
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Modules.Map.Components
Imports DotNetNuke.Modules.Map.Data
Namespace DotNetNuke.Modules.Map.Data
    Public Class StandardData
        Inherits DotNetNuke.Modules.Map.Components.DataMapControlBase


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private m_GeoControl As GeoCoderBase
        Private ReadOnly Property _GeolocatorControl() As GeoCoderBase
            Get
                If m_GeoControl Is Nothing AndAlso Not Me.GeoLocatorProviderSrc Is Nothing AndAlso Me.GeoLocatorProviderSrc.Length > 0 Then
                    Try
                        'm_GeoControl = me.load CType(Me.LoadControl(_interaction.GeolocatorProviderSrc), GeoCoderBase)
                        Dim oByName As Object = System.Activator.CreateInstance(Type.GetType(Me.GeoLocatorProviderSrc))
                        If Not oByName Is Nothing Then
                            m_GeoControl = CType(oByName, GeoCoderBase)
                            m_GeoControl.Load(Me.SourceID)

                            m_GeoControl.LoadConfiguration(Me.SourceConfiguration.Source.GeoLocatorSettings)
                        End If
                    Catch ex As Exception
                        Dim str As String = ex.ToString
                    End Try
                End If
                Return m_GeoControl
            End Get
        End Property

        Public ReadOnly Property ReadFunction() As String
            Get
                Return "MapRead" & Me.Module.ModuleId
            End Get
        End Property
        Public ReadOnly Property WriteFunction() As String
            Get
                Return "MapWrite" & Me.Module.ModuleId
            End Get
        End Property
        Public ReadOnly Property GeoFunction() As String
            Get
                Return "MapGeo" & Me.Module.ModuleId
            End Get
        End Property

        Public Overrides ReadOnly Property JavascriptLibrary() As String
            Get
                Return "Dotnetnuke.Map.Standard.js"
            End Get
        End Property

        Public Overrides ReadOnly Property JavascriptReadFunctionInit() As String
            Get
                Return "mapReadData(" & Me.Module.ModuleId & ")"
            End Get
        End Property

        Public Overrides ReadOnly Property JavascriptWriteFunctionInit() As String
            Get
                Return "mapWriteData(" & Me.Module.ModuleId & ")"
            End Get
        End Property

        Public Overrides ReadOnly Property JavascriptGeoFunctionInit() As String
            Get
                Return "mapGeoData(" & Me.Module.ModuleId & ")"
            End Get
        End Property

        Private ReadOnly Property Settings() As Map.Data.Standard.Configuration
            Get
                If SourceConfiguration.Settings Is Nothing Then
                    If Not SourceConfiguration.Source Is Nothing Then
                        'LOAD THE SETTING
                        If Not SourceConfiguration.Source.Settings Is Nothing Then
                            Try
                                Dim sinfo As Map.Data.Standard.Configuration = Standard.Configuration.Deserialize(SourceConfiguration.Source.Settings)
                                SourceConfiguration.Settings = sinfo
                            Catch ex As Exception
                            End Try
                        End If
                    End If
                End If
                If Not SourceConfiguration.Settings Is Nothing Then
                    Return SourceConfiguration.Settings
                End If
                Return Nothing
            End Get
        End Property
        Public Overloads Overrides Function ServiceData(ByVal Argument As String) As String
            Try
                Dim lastType As String = Application.Item("MAPLASTSERVICETYPE")
                If lastType Is Nothing OrElse lastType = "GEO" Then
                    Application.Set("MAPLASTSERVICETYPE", "USER")
                    Dim srv As New AutoUserCoder
                    srv.Start(Me.SourceID)
                Else
                    Application.Set("MAPLASTSERVICETYPE", "GEO")
                    Dim srv As New AutoGeoCoder
                    srv.Start()
                End If
            Catch ex As Exception
            End Try
            Return ""
        End Function
        Public Overloads Overrides Function GeoData(ByVal Argument As String) As String
            If Not _GeolocatorControl Is Nothing Then

                Dim pipePosition As Integer = Argument.IndexOf("|")
                Dim variables As New SortedList
                If pipePosition > -1 Then
                    Dim strFilter As String = Argument.Substring(pipePosition + 1)
                    If strFilter.Length > 0 Then
                        Dim Pairs As String() = strFilter.Split("&")
                        Dim strP As String
                        For Each strP In Pairs
                            Dim strI As String() = strP.Split("=")
                            If strI.Length = 2 Then
                                variables.Add(strI(0), strI(1))
                            End If
                        Next
                    End If
                End If

                Dim Address As String
                Dim Unit As String
                Dim Country As String
                Dim City As String
                Dim Region As String
                Dim Zip As String

                If variables.ContainsKey("Address") Then
                    Address = variables("Address")
                Else
                    Address = ""
                End If
                If variables.ContainsKey("Unit") Then
                    Unit = variables("Unit")
                Else
                    Unit = ""
                End If
                If variables.ContainsKey("Country") Then
                    Country = variables("Country")
                Else
                    Country = ""
                End If
                If variables.ContainsKey("City") Then
                    City = variables("City")
                Else
                    City = ""
                End If
                If variables.ContainsKey("Region") Then
                    Region = variables("Region")
                Else
                    Region = ""
                End If
                If variables.ContainsKey("Zip") Then
                    Zip = variables("Zip")
                Else
                    Zip = ""
                End If

                Dim fullAddress As String = _GeolocatorControl.Address(Unit, Address, City, Region, Zip, Country)
                Dim mmcl As Modules.Map.Components.Location = _GeolocatorControl.Lookup(fullAddress)
                If Not mmcl Is Nothing Then
                    Return "results = new Array(" & mmcl.isSuccessful.ToString.ToLower & "," & mmcl.Latitude.ToString.Replace(",", ".") & "," & mmcl.Longitude.ToString.Replace(",", ".") & ",'" & MyBase.JavascriptFormatString(fullAddress) & "');"
                End If
            End If
            Return "results = new Array(false,0,0);"
        End Function
        Public Overloads Overrides Function GetData(ByVal Argument As String) As String
            Dim result As String = Nothing
            Dim sdata As New DotNetNuke.Modules.Map.Data.Standard(Settings)
            Dim sinfo As Map.Data.Standard.Configuration = SourceConfiguration.Settings
            If Not Settings Is Nothing Then

                Dim pipePosition As Integer = Argument.IndexOf("|")
                Dim variables As New SortedList
                If pipePosition > -1 Then
                    Dim strFilter As String = Argument.Substring(pipePosition + 1)
                    If strFilter.Length > 0 Then
                        Dim Pairs As String() = strFilter.Split("&")
                        Dim strP As String
                        For Each strP In Pairs
                            Dim strI As String() = strP.Split("=")
                            If strI.Length = 2 Then
                                variables.Add(strI(0), strI(1))
                            End If
                        Next
                    End If
                End If

                Dim Page As Integer = 0
                Dim RecordsPerPage As Integer = 0


                Select Case Settings.RenderType
                    Case 1 'POINTS
                        Dim filter As New PointFilterInformation
                        filter.SourceID = Me.SourceID
                        filter.SourceInformation = New Hashtable
                        filter.SourceInformation.Add("Zoom", variables("Zoom"))
                        If variables.ContainsKey("distance") Then
                            If CInt(variables("distance")) > 0 Then
                                filter.SourceInformation.Add("latitude", variables("latitude"))
                                filter.SourceInformation.Add("longitude", variables("longitude"))
                                filter.SourceInformation.Add("distance", variables("distance"))
                                filter.SourceInformation.Add("scale", variables("scale"))
                            End If
                        End If
                        If variables.ContainsKey("Page") Then
                            filter.SourceInformation.Add("Page", variables("Page"))
                            Page = variables("Page")
                        End If
                        If variables.ContainsKey("PerPage") Then
                            filter.SourceInformation.Add("PerPage", variables("PerPage"))
                            RecordsPerPage = variables("PerPage")
                        End If
                        Dim reader As ArrayList = Nothing
                        Dim length As Integer = 0
                        Try
                            reader = sdata.GetPoints(filter)
                        Catch ex As Exception

                        End Try
                        'result = "results = new Array(" & RenderPoints(reader, Page, RecordsPerPage) & ");"
                        result = "results = new Array(" & RenderPoints(reader, 0, 0) & ");"
                        If RecordsPerPage > 0 Then
                            result &= "DATALENGTH"
                            If Not AdminMode Then
                                result &= Me.Module.ModuleId
                            End If
                            result &= "=" & reader.Capacity & ";"
                        End If
                    Case 2 'USERS
                        Dim filter As New PointFilterInformation
                        filter.SourceID = Me.SourceID
                        filter.SourceInformation = New Hashtable
                        filter.SourceInformation.Add("Zoom", variables("Zoom"))
                        If variables.ContainsKey("distance") Then
                            If CInt(variables("distance")) > 0 Then
                                filter.SourceInformation.Add("latitude", variables("latitude"))
                                filter.SourceInformation.Add("longitude", variables("longitude"))
                                filter.SourceInformation.Add("distance", variables("distance"))
                                filter.SourceInformation.Add("scale", variables("scale"))
                            End If
                        End If
                        If variables.ContainsKey("Page") Then
                            filter.SourceInformation.Add("Page", variables("Page"))
                        End If
                        If variables.ContainsKey("PerPage") Then
                            filter.SourceInformation.Add("PerPage", variables("PerPage"))
                            RecordsPerPage = variables("PerPage")
                        End If
                        Dim reader As ArrayList = sdata.GetPoints(filter)
                        'result = "results = new Array(" & RenderPoints(reader, Page, RecordsPerPage) & ");"
                        result = "results = new Array(" & RenderPoints(reader, 0, 0) & ");"
                        If RecordsPerPage > 0 Then
                            result &= "DATALENGTH"
                            If Not AdminMode Then
                                result &= Me.Module.ModuleId
                            End If
                            result &= "=" & reader.Capacity & ";"
                        End If
                    Case 3 'CUSTOM
                        Dim filter As New PointFilterInformation
                        filter.SourceID = Me.SourceID
                        filter.SourceInformation = New Hashtable
                        filter.SourceInformation.Add("Zoom", variables("Zoom"))
                        If variables.ContainsKey("distance") Then
                            If CInt(variables("distance")) > 0 Then
                                filter.SourceInformation.Add("latitude", variables("latitude"))
                                filter.SourceInformation.Add("longitude", variables("longitude"))
                                filter.SourceInformation.Add("distance", variables("distance"))
                                filter.SourceInformation.Add("scale", variables("scale"))
                            End If
                        End If
                        If variables.ContainsKey("Page") Then
                            filter.SourceInformation.Add("Page", variables("Page"))
                        End If
                        If variables.ContainsKey("PerPage") Then
                            filter.SourceInformation.Add("PerPage", variables("PerPage"))
                            RecordsPerPage = variables("PerPage")
                        End If
                        Dim qVariable As Standard.Configuration.QueryVariable
                        Dim Query As String = Settings.CustomQuery
                        For Each qVariable In Settings.QueryVariables
                            Dim sValue As String = Nothing
                            Select Case qVariable.VariableType.ToLower
                                Case "userid"
                                    sValue = Me.Module.UserId
                                Case "tabid"
                                    sValue = Me.Module.TabId
                                Case "moduleid"
                                    sValue = Me.Module.ModuleId
                                Case "tabmoduleid"
                                    sValue = Me.Module.TabModuleId
                                Case "pagenumber"
                                    'TODO: pagesize
                                Case "pagesize"
                                    'TODO: pagenumber
                                Case "owner"
                                    'TODO: owner
                                Case "latitude"
                                    If Not variables("latitude") Is Nothing Then
                                        sValue = variables("latitude")
                                    End If
                                Case "longitude"
                                    If Not variables("longitude") Is Nothing Then
                                        sValue = variables("longitude")
                                    End If
                                Case "distance"
                                    If Not variables("distance") Is Nothing Then
                                        sValue = variables("distance")
                                    End If
                                Case "scale"
                                    If Not variables("scale") Is Nothing Then
                                        sValue = variables("scale")
                                    End If
                                Case "portalalias"
                                    sValue = Me.Module.PortalAlias.HTTPAlias
                                Case "portalid"
                                    sValue = Me.Module.PortalId
                                Case "qualifier"
                                    'TODO: qualifier
                                Case "<session>"
                                    If Not Me.Session(qVariable.Source) Is Nothing Then
                                        sValue = Me.Session(qVariable.Source)
                                    End If
                                Case "<querystring>"
                                    If Not variables("Q" & qVariable.Source) Is Nothing Then
                                        sValue = variables("Q" & qVariable.Source)
                                    End If
                                Case "<form>"
                                    If Not variables("F" & qVariable.Source) Is Nothing Then
                                        sValue = variables("F" & qVariable.Source)
                                    End If
                            End Select
                            If Not sValue Is Nothing AndAlso sValue.Length > 0 Then
                                sValue = sValue.Replace("'", "''")
                                If Not qVariable.TargetLeft Is Nothing Then
                                    sValue = qVariable.TargetLeft & sValue
                                End If
                                If Not qVariable.TargetRight Is Nothing Then
                                    sValue = sValue & qVariable.TargetRight
                                End If
                            Else
                                sValue = qVariable.TargetEmpty
                            End If
                            Query = Query.Replace(qVariable.Target, sValue)
                            sValue = Nothing
                        Next

                        filter.SourceInformation.Add("CustomQuery", Query)
                        If Not Settings.ConnectionString Is Nothing AndAlso Settings.ConnectionString.Length > 0 Then
                            If Settings.ConnectionStringIsName Then
                                filter.SourceInformation.Add("CustomConnection", System.Configuration.ConfigurationManager.AppSettings.Item(Settings.ConnectionString))
                            Else
                                filter.SourceInformation.Add("CustomConnection", Settings.ConnectionString)
                            End If
                        End If

                        Dim reader As ArrayList = sdata.GetPoints(filter)
                        result = "results = new Array(" & RenderPoints(reader, 0, 0) & "); "
                        If RecordsPerPage > 0 Then
                            result &= "DATALENGTH"
                            If Not AdminMode Then
                                result &= Me.Module.ModuleId
                            End If
                            result &= "=" & reader.Capacity & ";"
                        End If
                End Select
            End If
            Return result
        End Function

        Public Overloads Overrides Function SetData(ByVal Argument As String) As String
            Dim result As String = Nothing
            Dim sdata As New DotNetNuke.Modules.Map.Data.Standard(Settings)
            Dim sinfo As Map.Data.Standard.Configuration = SourceConfiguration.Settings
            If Not Settings Is Nothing Then
                Dim pipePosition As Integer = Argument.IndexOf("|")
                Dim variables As New SortedList
                If pipePosition > -1 Then
                    Dim strFilter As String = Argument.Substring(pipePosition + 1)
                    If strFilter.Length > 0 Then
                        Dim Pairs As String() = strFilter.Split("&")
                        Dim strP As String
                        For Each strP In Pairs
                            Dim equalPosition As Integer = strP.IndexOf("=")
                            If equalPosition > 0 Then
                                Dim leftVal As String = strP.Substring(0, equalPosition)
                                Dim rightVal As String = ""
                                If strP.Length > equalPosition Then
                                    rightVal = Web.HttpUtility.UrlDecode(strP.Substring(equalPosition + 1))
                                End If
                                variables.Add(leftVal, rightVal)
                            End If
                        Next
                    End If
                End If

                Dim point As New Data.DataPoint
                If variables.ContainsKey("index") Then
                    point.PointID = variables("index")
                End If
                If variables.ContainsKey("description") Then
                    point.Description = variables("description")
                End If
                If variables.ContainsKey("address") Then
                    point.Address = variables("address")
                End If
                If variables.ContainsKey("guid") Then
                    point.GUID = variables("guid")
                End If
                If variables.ContainsKey("iconindex") AndAlso IsNumeric(variables("iconindex")) Then
                    point.IconIndex = variables("iconindex")
                Else
                    point.IconIndex = 0
                End If
                If variables.ContainsKey("latitude") AndAlso IsNumeric(variables("latitude")) Then
                    point.Latitude = Double.Parse(variables("latitude"), System.Globalization.CultureInfo.InvariantCulture)
                Else
                    point.Latitude = -1
                End If
                If variables.ContainsKey("longitude") AndAlso IsNumeric(variables("longitude")) Then
                    point.Longitude = Double.Parse(variables("longitude"), System.Globalization.CultureInfo.InvariantCulture)
                Else
                    point.Longitude = -1
                End If
                If variables.ContainsKey("sequenceinfo") Then
                    point.SequenceInfo = variables("sequenceinfo")
                End If
                If variables.ContainsKey("sequence") AndAlso IsNumeric(variables("sequence")) Then
                    point.SequenceNumber = variables("sequence")
                Else
                    point.SequenceNumber = 0
                End If
                If variables.ContainsKey("zoomshow") AndAlso IsNumeric(variables("zoomshow")) Then
                    point.ZoomShow = variables("zoomshow")
                Else
                    point.ZoomShow = 0
                End If
                If variables.ContainsKey("zoomhide") AndAlso IsNumeric(variables("zoomhide")) Then
                    point.ZoomHide = variables("zoomhide")
                Else
                    point.ZoomHide = 0
                End If

                If point.IconIndex = -9000 Then
                    'DELETE
                    sdata.DeletePoint(Me.SourceID, point.PointID)
                Else
                    If point.PointID > -1 Then
                        Dim dp As Data.DataPoint = sdata.GetPoint(point.PointID)
                        sdata.UpdatePoint(point.PointID, Me.SourceID, point.GUID, point.Address, point.Description, point.IconIndex, point.Longitude, point.Latitude, point.SequenceNumber, point.SequenceInfo, point.ZoomShow, point.ZoomHide, "", "", "")
                    Else
                        sdata.AddPoint(Me.SourceID, point.Address, point.Description, point.GUID, point.IconIndex, point.Longitude, point.Latitude, point.SequenceNumber, point.SequenceInfo, point.ZoomShow, point.ZoomHide, "", "", "", False)
                    End If
                End If
            End If
            Return result
        End Function

        Private Sub LoadJavascript_ExtendedData()
            Dim sb As New Text.StringBuilder
            sb.Append("<script language=javascript>")
            If Me.AdminMode Then
                sb.Append("var ADMINMODE" & Me.Module.ModuleId & " = true;" & vbCrLf)
            Else
                sb.Append("var ADMINMODE" & Me.Module.ModuleId & " = false;" & vbCrLf)
            End If
            sb.Append("function mapReadDataExtended" & Me.Module.ModuleId & "() {" & vbCrLf)
            sb.Append("var strValue = ''; " & vbCrLf)
            If Not Settings Is Nothing Then
                Select Case Settings.RenderType
                    Case 3 'CUSTOM
                        If Not Settings.QueryVariables Is Nothing AndAlso Settings.QueryVariables.Count > 0 Then
                            Dim qvObject As Standard.Configuration.QueryVariable
                            For Each qvObject In Settings.QueryVariables
                                If qvObject.VariableType = "<Form>" Then
                                    sb.Append("if (strValue.length > 0) { strValue += '&' }" & vbCrLf & " strValue += Map_GetFormValue('" + qvObject.Source + "');" & vbCrLf)
                                ElseIf qvObject.VariableType = "<QueryString>" Then
                                    sb.Append("if (strValue.length > 0) { strValue += '&' }" & vbCrLf & " strValue += Map_GetQueryStringValue('" + qvObject.Source + "');" & vbCrLf)
                                End If
                            Next
                        End If
                End Select
            End If
            sb.Append("return strValue;" & vbCrLf)
            sb.Append("}")
            sb.Append("</script>")
            Me.Page.ClientScript.RegisterClientScriptBlock(GetType(String), "Dotnetnuke.Map.Standard.Extended" & Me.Module.ModuleId, sb.ToString)
            sb = Nothing
        End Sub

        Private Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Me.Page.ClientScript.IsClientScriptBlockRegistered(GetType(String), "Dotnetnuke.Map.Standard.Extended" & Me.Module.ModuleId) = False Then
                LoadJavascript_ExtendedData()
            End If
        End Sub

        Public Overrides ReadOnly Property JavascriptServiceFunctionComplete() As String
            Get
                Return "mapServiceDataComplete"
            End Get
        End Property

        Public Overrides ReadOnly Property JavascriptServiceFunctionFailure() As String
            Get
                Return "mapServiceDataComplete"
            End Get
        End Property

        Public Overrides ReadOnly Property JavascriptServiceFunctionInit() As String
            Get
                Return "mapServiceData(" & Me.Module.ModuleId & ")"
            End Get
        End Property

        Public Overrides ReadOnly Property JavascriptPointFormatValue() As String
            Get
                If Not Settings Is Nothing Then
                    If Not Settings.Format Is Nothing Then
                        Return Settings.Format.Replace(vbCr, "\n").Replace(vbLf, "\n").Replace("'", "\'")
                    Else
                        Return ""
                    End If
                End If
                Return ""
            End Get
        End Property
    End Class
End Namespace
