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
Namespace DotNetNuke.Modules.Map.Visuals.Google
    Public Class Timelapse
        Inherits DotNetNuke.Modules.Map.Components.VisualMapControlBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ltlMapData As System.Web.UI.WebControls.Literal
        Protected WithEvents ltlGoogleMapScript As System.Web.UI.WebControls.Literal
        Protected WithEvents ltlGoogleMap As System.Web.UI.WebControls.Literal
        Protected WithEvents ltlJavascript As System.Web.UI.WebControls.Literal

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object
#End Region
#Region "Public Methods"

        Public Overrides ReadOnly Property JavascriptReadFunctionComplete() As String
            Get
                Return "mapReadComplete" & Me.Module.ModuleId
            End Get
        End Property

        Public Overrides ReadOnly Property JavascriptReadFunctionFailure() As String
            Get
                Return "mapReadFailure" & Me.Module.ModuleId
            End Get
        End Property

        Public Overrides ReadOnly Property JavascriptWriteFunctionComplete() As String
            Get
                Return "mapWriteComplete" & Me.Module.ModuleId
            End Get
        End Property

        Public Overrides ReadOnly Property JavascriptWriteFunctionFailure() As String
            Get
                Return "mapWriteFailure" & Me.Module.ModuleId
            End Get
        End Property


#End Region
#Region "Private Methods"
        Private _mapsettings As TimelapseConfiguration
        Private Property MapSettings() As TimelapseConfiguration
            Get
                If _mapsettings Is Nothing Then
                    If Not Me.MapConfiguration.Map Is Nothing AndAlso Not Me.MapConfiguration.Map.Settings Is Nothing Then
                        Try
                            Dim sinfo As TimelapseConfiguration = TimelapseConfiguration.Deserialize(Me.MapConfiguration.Map.Settings)
                            MapConfiguration.Settings = sinfo
                        Catch ex As Exception
                            DotNetNuke.Services.Exceptions.ProcessModuleLoadException("Unable to load the Map configuration", Me, ex, False)
                        End Try
                    End If
                    If Me.MapConfiguration.Settings Is Nothing Then
                        _mapsettings = New TimelapseConfiguration
                    Else
                        _mapsettings = MapConfiguration.Settings
                    End If
                End If
                Return _mapsettings
            End Get
            Set(ByVal Value As TimelapseConfiguration)
                _mapsettings = Value
            End Set
        End Property

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()

        End Sub
        Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
            If MyBase.MapID > 0 Then
                If Not MapConfiguration.Map Is Nothing Then
                    BindData_Map(MapConfiguration.Map, True)
                Else
                    BindData_Map(MyBase.MapID, True)
                End If
            Else
                ltlGoogleMap.Text = "<div style='width: 100%; font-size: 15px; font-color: red; font-family: arial; font-weight: bold;'><center>" & Locale("New") & "</center></div>"
            End If
        End Sub

        Private ReadOnly Property APIKey() As String
            Get
                Static Dim _APIKey As String
                If _APIKey Is Nothing Then _APIKey = MapSettings.CurrentLicenseKey(Request.Url.Host & Request.RawUrl)
                Return _APIKey
            End Get
        End Property

        Private Sub BindData_Map(ByVal MapID As Integer, ByVal LoadInterface As Boolean)
            If MapID > 0 Then
                Dim mapDC As New Data.MapController
                Dim mapInfo As Data.MapInfo
                mapInfo = mapDC.GetMap(MapID)
                BindData_Map(mapInfo, LoadInterface)
                mapInfo = Nothing
                mapDC = Nothing
            End If
        End Sub
        Private Sub BindData_Map(ByRef mapinfo As Data.MapInfo, ByVal LoadInterface As Boolean)
            If Not MapSettings Is Nothing Then
                If Not APIKey() Is Nothing AndAlso APIKey.Length > 0 Then
                    If Me.Page.ClientScript.IsClientScriptBlockRegistered("Dotnetnuke.Map.Google") = False Then
                        Me.Page.ClientScript.RegisterClientScriptBlock(GetType(String), "Dotnetnuke.Map.Google", "<script src='http://maps.google.com/maps?file=api&amp;v=2&amp;key=" & APIKey() & "' type=""text/javascript""></script>", False)
                    End If
                Else
                    ltlGoogleMap.Text = "<div style='width: 100%; font-size: 15px; font-color: red; font-family: arial; font-weight: bold;'><center>" & Locale("New") & "</center></div>"
                End If
                Try
                    setJavascript()
                    Dim twidth As String = MapSettings.Width
                    Dim theight As String = MapSettings.Height
                    If Not twidth Is Nothing AndAlso twidth.Length > 0 AndAlso Not twidth.EndsWith("%") And Not twidth.ToUpper.EndsWith("PX") Then
                        twidth &= "px"
                    End If
                    If Not theight Is Nothing AndAlso theight.Length > 0 AndAlso Not theight.EndsWith("%") And Not theight.ToUpper.EndsWith("PX") Then
                        theight &= "px"
                    End If
                    If twidth Is Nothing OrElse twidth.Length = 0 Then
                        twidth = "100%"
                    End If
                    If theight Is Nothing OrElse theight.Length = 0 Then
                        theight = "300px"
                    End If
                    ltlGoogleMap.Text = "<div id=""map" & Me.Module.ModuleId & """ style=""display: block; width:" & twidth & "; height:" & theight & ";""></div>" & vbCrLf

                    'TIMELAPSE
                    ltlGoogleMap.Text &= GeneratePlayback("", "") & vbCrLf

                    'STATUS
                    ltlGoogleMap.Text &= GenerateStatus() & vbCrLf

                Catch ex As Exception
                    DotNetNuke.Services.Exceptions.ProcessModuleLoadException(Me, ex)
                End Try
            Else
                ltlGoogleMap.Text = "Not Loaded" & vbCrLf
            End If
        End Sub
#End Region

#Region "Javascript Rendering Engine"
        Private Function GeneratePlayback(ByVal TimeStart As String, ByVal TimeEnd As String) As String
            Dim str As String = ""
            Dim twidth As String = MapSettings.Width
            Dim theight As String = MapSettings.Height
            If Not twidth.EndsWith("%") Then
                twidth &= "px"
            End If
            If Not theight.EndsWith("%") Then
                theight &= "px"
            End If
            str &= "<div id=""mapplayer" & Me.Module.ModuleId & """ style=""WIDTH: " & twidth & ";"
            str &= "display: block;"
            str &= """>"

            str &= "<table width=100% border=0 cellpadding=0 cellspacing=0 ><tr>"
            str &= "<td><img title=""Rewind"" src='" & Me.VisualRootPath & "rewind.gif' onclick='CURRENT" & Me.Module.ModuleId & "=0;PLAY" & Me.Module.ModuleId & "=0;Map_Reset(" & Me.Module.ModuleId & ");'></td>"
            str &= "<td><img title=""Stop"" src='" & Me.VisualRootPath & "stop.gif' onclick='PLAY" & Me.Module.ModuleId & "=0;'></td>"
            str &= "<td><img title=""Play"" src='" & Me.VisualRootPath & "play.gif' onclick='PLAY" & Me.Module.ModuleId & "=1; Map_Play(" & Me.Module.ModuleId & ");'></td>"
            str &= "<td><img title=""" & TimeStart & """ src='" & Me.VisualRootPath & "left.gif'></td>"
            str &= "<td width=100% style=""background: url(" & Me.VisualRootPath & "back.gif) repeat;""><img title=""" & TimeStart & """ style=""position: relative; left: 0;"" id=""imgScroller" & Me.Module.ModuleId & """ width=12 height=14 src=""" & Me.VisualRootPath & "scroll.gif""></td>"
            str &= "<td><img title=""" & TimeEnd & """ src='" & Me.VisualRootPath & "right.gif'></td>"
            str &= "</tr></table>"
            str &= "</div>"

            Return str
        End Function
        Private Function GenerateStatus() As String
            Dim str As String = ""
            Dim twidth As String = MapSettings.Width
            Dim theight As String = MapSettings.Height
            If Not twidth.EndsWith("%") Then
                twidth &= "px"
            End If
            If Not theight.EndsWith("%") Then
                theight &= "px"
            End If
            str &= "<div id=""mapstatus" & Me.Module.ModuleId & """ style=""display: block; font-family: arial; font-color: #555555; font-size: 11px; background: #CCCCCC; WIDTH: " & twidth & "; HEIGHT: 14px;text-align: center; border-top: 1px solid black;"">Loading...</div>"
            Return str
        End Function

        Private Sub setJavascript_Variables(ByRef SB As Text.StringBuilder, ByVal tid As Integer)
            SB.Append("	var MAP" & tid & ";" & vbCrLf)
            SB.Append(" var MAPLoad" & tid & "='Map_FetchEnd';" & vbCrLf)
            SB.Append(" var WURL" & tid & "='" & VisualRootPath & "';" & vbCrLf)
            SB.Append("	var DETECT" & tid & " = navigator.userAgent.toLowerCase();" & vbCrLf)
            SB.Append("	var SPINWAIT" & tid & " = " & MapSettings.delayInitial & ";" & vbCrLf)
            SB.Append(" var SHOWALL" & tid & " = true; " & vbCrLf)
            SB.Append("	var SCROLLER" & tid & ";" & vbCrLf)
            SB.Append("	var STATUS" & tid & ";" & vbCrLf)
            SB.Append("	var TMID" & tid & " = " & tid & ";" & vbCrLf)
            SB.Append("	var PLAY" & tid & " = 0;" & vbCrLf)
            SB.Append("	var CURRENT" & tid & " = 0;" & vbCrLf)
            SB.Append("	var XML" & tid & " = false;" & vbCrLf)
            SB.Append("	var DATA" & tid & " = false;" & vbCrLf)
            SB.Append("	var XMLA" & tid & " = false;" & vbCrLf)
            SB.Append("	var DATAA" & tid & " = false;" & vbCrLf)
            SB.Append("	var ICONS" & tid & " = false;" & vbCrLf)
            'GENERATE REQUIRED FUNCTION
            SB.Append(" function " & Me.JavascriptReadFunctionComplete & "(result,ctx) { DATA" & tid & "=result; Map_FetchEnd(" & tid & ");}" & vbCrLf)
            SB.Append(" function " & Me.JavascriptReadFunctionFailure & "(result,ctx) { DATA" & tid & "=result; Map_FetchEnd(" & tid & ");}" & vbCrLf)

            If MapSettings.TimelineDisplayTimer = True Then
                SB.Append(" var SHOWTIMER" & tid & " = false;" & vbCrLf)
            Else
                SB.Append(" var SHOWTIMER" & tid & " = true;" & vbCrLf)
            End If
            If MapSettings.TimelineUseTimer = True Then
                SB.Append(" var USETIMER" & tid & " = true;" & vbCrLf)
            Else
                SB.Append(" var USETIMER" & tid & " = false;" & vbCrLf)
            End If

            'TIMELAPSE
            SB.Append("	var ANIMATEDELAY" & tid & " = " & MapSettings.delayTimeline & ";" & vbCrLf)

            'TIMELAPSE
            SB.Append("	var ANIMATEGROUP" & tid & " = 1;" & vbCrLf)

            SB.Append("	var STARTDELAY" & tid & " = " & MapSettings.delayInitial & ";" & vbCrLf)

            Dim decCulture As New System.Globalization.CultureInfo("en-US")
            SB.Append("	var DLAT" & tid & " = " & (MapSettings.Latitude.ToString.Replace(",", ".")) & ";" & vbCrLf)
            SB.Append("	var DLON" & tid & " = " & (MapSettings.Longitude.ToString.Replace(",", ".")) & ";" & vbCrLf)
            SB.Append("	var CLAT" & tid & " = " & (MapSettings.Latitude.ToString.Replace(",", ".")) & ";" & vbCrLf)
            SB.Append("	var CLON" & tid & " = " & (MapSettings.Longitude.ToString.Replace(",", ".")) & ";" & vbCrLf)
            SB.Append("	var CDIS" & tid & " = -1;" & vbCrLf)
            SB.Append("	var CSCALE" & tid & " = 'M';" & vbCrLf)
            SB.Append("	var CZOOM" & tid & " = " & MapSettings.Zoom & ";" & vbCrLf)
            SB.Append("	var XZOOM" & tid & " = " & MapSettings.Zoom & ";" & vbCrLf)
            SB.Append("	var DDESC" & tid & " = '" & MapSettings.Description.Replace(vbCrLf, "\n").Replace("'", "\'") & "';" & vbCrLf)
            SB.Append("	var DZOOM" & tid & " = " & MapSettings.Zoom & ";" & vbCrLf)

            SB.Append("	var DICON" & tid & " = 0;" & vbCrLf)

            SB.Append("	var HASSTARTED" & tid & " = false;" & vbCrLf)

            'TIMELAPSE
            If MapSettings.TimelineAutoStart = True Then
                SB.Append(" var AUTOSTART" & tid & " = true;" & vbCrLf)
            Else
                SB.Append(" var AUTOSTART" & tid & " = false;" & vbCrLf)
            End If

            SB.Append(" var LISTENERS" & tid & " = new Array();" & vbCrLf)

            SB.Append("	CURRENTPAGE" & tid & "		=	0;" & vbCrLf)
            SB.Append("	DATALENGTH" & tid & "		=	0;" & vbCrLf)

            SB.Append("	var RPP" & tid & "		=	0;" & vbCrLf)

            SB.Append("	var DATALENGTH" & tid & "		=	-1;" & vbCrLf)

        End Sub
        Private Sub setJavascript_Startup(ByRef SB As Text.StringBuilder, ByVal tid As Integer)
            SB.Append(" function start" & tid & "() {" & vbCrLf)
            SB.Append(" if (window.Map_Startup) {" & vbCrLf)
            SB.Append(" Map_Startup(" & tid & ");" & vbCrLf)
            SB.Append(" } else {" & vbCrLf)
            SB.Append(" window.setTimeout('start" & tid & "',250);}" & vbCrLf)
            SB.Append(" }" & vbCrLf)
            SB.Append(" start" & tid & "();" & vbCrLf)
        End Sub
        Private Function FormatJSString(ByVal value As String) As String
            Return value.Replace(Chr(10), "\n").Replace(Chr(13), "").Replace("\", "\\").Replace("'", "\'")
        End Function

        Private Sub setJavascript_Icons(ByRef SB As Text.StringBuilder, ByVal tid As Integer)
            SB.Append("	function Map_LoadIcons" & tid & "()" & vbCrLf)
            SB.Append("	{" & vbCrLf)
            SB.Append("		if (!ICONS" & tid & ")" & vbCrLf)
            SB.Append("		{" & vbCrLf)
            SB.Append("			ICONS" & tid & " = new Array();" & vbCrLf)

            Dim ico As Configuration.Marker
            MapSettings.SequenceMarkers()
            For Each ico In MapSettings.Markers
                SB.Append("         icon" & ico.Index & " = new GIcon();" & vbCrLf)
                SB.Append("			icon" & ico.Index & ".image = '" & FormatJSString(ImageURL(ico.Icon)) & "';" & vbCrLf)
                SB.Append("			icon" & ico.Index & ".shadow = '" & FormatJSString(ImageURL(ico.Shadow)) & "';" & vbCrLf)
                SB.Append("			icon" & ico.Index & ".iconSize = new GSize(" & ico.IconWidth & ", " & ico.IconHeight & ");" & vbCrLf)
                SB.Append("			icon" & ico.Index & ".shadowSize = new GSize(" & ico.ShadowWidth & ", " & ico.ShadowHeight & ");" & vbCrLf)
                SB.Append("			icon" & ico.Index & ".iconAnchor = new GPoint(" & ico.AnchorX & ", " & ico.AnchorY & ");" & vbCrLf)
                SB.Append("			icon" & ico.Index & ".infoWindowAnchor = new GPoint(" & ico.InfoX & ", " & ico.InfoY & ");" & vbCrLf)

                SB.Append("			ICONS" & tid & "[" & ico.Index & "] = icon" & ico.Index & ";" & vbCrLf)
            Next
            If MapSettings.Markers.Count = 0 Then
                SB.Append("     ICONS" & tid & "[0] = new GIcon(G_DEFAULT_ICON);" & vbCrLf)
            End If
            SB.Append("		}" & vbCrLf)
            SB.Append("	}" & vbCrLf)
        End Sub
        Private Sub setJavascript_MapControls(ByRef SB As Text.StringBuilder, ByVal tid As Integer)
            SB.Append("	function Map_SetDirectory" & tid & "(override)" & vbCrLf)
            SB.Append("	{	" & vbCrLf)
            
            SB.Append("	}	" & vbCrLf)

            SB.Append("	function Map_SetControls" & tid & "()" & vbCrLf)
            SB.Append("	{	" & vbCrLf)

            SB.Append("		//NAVIGATION" & vbCrLf)
            Select Case MapSettings.NavigationDisplay
                Case "2"
                    SB.Append("		MAP" & tid & ".addControl(new GLargeMapControl());" & vbCrLf)
                Case "1"
                    SB.Append("		MAP" & tid & ".addControl(new GSmallMapControl());" & vbCrLf)
                Case "0"
            End Select

            SB.Append("		//MAPTYPE" & vbCrLf)
            Select Case MapSettings.TypeDisplay
                Case "0"
                Case "1"
                    SB.Append("		MAP" & tid & ".addControl(new GMapTypeControl());" & vbCrLf)
            End Select

            Select Case MapSettings.DefaultType
                Case "2"
                    SB.Append("     MAP" & tid & ".setMapType(G_HYBRID_MAP);" & vbCrLf)
                Case "1"
                    SB.Append("     MAP" & tid & ".setMapType(G_SATELLITE_MAP);" & vbCrLf)
                Case "0"
                    SB.Append("     MAP" & tid & ".setMapType(G_NORMAL_MAP);" & vbCrLf)
            End Select

            If Not MapSettings.OverviewMapDisplay Is Nothing Then
                SB.Append("		//OVERVIEWMAP" & vbCrLf)
                Select Case MapSettings.OverviewMapDisplay
                    Case "0"
                    Case "1"
                        SB.Append("		MAP" & tid & ".addControl(new GOverviewMapControl());" & vbCrLf)
                End Select
            End If


            SB.Append("		//ANIMATION" & vbCrLf)
            SB.Append("		SCROLLER" & tid & "	=	document.getElementById(""imgScroller" & tid & """);" & vbCrLf)
            SB.Append("		//STATUS" & vbCrLf)
            SB.Append("		STATUS" & tid & "		=	document.getElementById(""mapstatus" & tid & """);" & vbCrLf)

            SB.Append("     LOCALE_PAGEFIRST	    = '" & Locale("SCRIPT_PAGEFIRST") & "';" & vbCrLf)
            SB.Append("     LOCALE_PAGELAST		= '" & Locale("SCRIPT_PAGELAST") & "';" & vbCrLf)
            SB.Append("     LOCALE_PAGENEXT		= '" & Locale("SCRIPT_PAGENEXT") & "';" & vbCrLf)
            SB.Append("     LOCALE_PAGEBACK		= '" & Locale("SCRIPT_PAGEBACK") & "';" & vbCrLf)
            SB.Append("     LOCALE_STATUS1		= '" & Locale("SCRIPT_STATUS1") & "';" & vbCrLf)
            SB.Append("     LOCALE_STATUS2		= '" & Locale("SCRIPT_STATUS2") & "';" & vbCrLf)
            SB.Append("     LOCALE_STATUS3		= '" & Locale("SCRIPT_STATUS3") & "';" & vbCrLf)
            SB.Append("     LOCALE_STATUS4		= '" & Locale("SCRIPT_STATUS4") & "';" & vbCrLf)
            SB.Append("     LOCALE_STATUS5		= '" & Locale("SCRIPT_STATUS5") & "';" & vbCrLf)
            SB.Append("     LOCALE_STATUS6		= '" & Locale("SCRIPT_STATUS6") & "';" & vbCrLf)
            SB.Append("     LOCALE_STATUS7		= '" & Locale("SCRIPT_STATUS7") & "';" & vbCrLf)
            SB.Append("     LOCALE_STATUS8		= '" & Locale("SCRIPT_STATUS8") & "';" & vbCrLf)
            SB.Append("     LOCALE_STATUS9		= '" & Locale("SCRIPT_STATUS9") & "';" & vbCrLf)
            SB.Append("     LOCALE_STATUS10		= '" & Locale("SCRIPT_STATUS10") & "';" & vbCrLf)

            SB.Append("Map_SetDirectory" & tid & "(true);" & vbCrLf)
            SB.Append("	}" & vbCrLf)
        End Sub
        Private Function BuildItemFormat(ByVal Source As String) As String
            Dim str As String = Source.Replace(vbCr, "\n").Replace(vbLf, "\n").Replace("'", "\'")
            Return str
        End Function

        Private Sub setJavascript()
            Dim sb As New Text.StringBuilder
            sb.Append("<SCRIPT language=""javascript"" src=""" & Me.VisualRootPath & "Google.Standard.js""></SCRIPT>" & vbCrLf)
            sb.Append("<script language=""javascript"">" & vbCrLf)
            sb.Append("//<![CDATA[" & vbCrLf)

            setJavascript_Variables(sb, Me.Module.ModuleId)
            setJavascript_Icons(sb, Me.Module.ModuleId)
            setJavascript_MapControls(sb, Me.Module.ModuleId)
            setJavascript_Startup(sb, Me.Module.ModuleId)

            sb.Append("//]]>" & vbCrLf)
            sb.Append("</script>" & vbCrLf)

            ltlJavascript.Text = sb.ToString

            sb = Nothing
        End Sub
#End Region
    End Class
End Namespace

