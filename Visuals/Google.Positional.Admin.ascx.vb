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
    Public Class PositionalAdmin
        Inherits DotNetNuke.Modules.Map.Components.ControlPanelBase
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents txtIcon As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtShadow As System.Web.UI.WebControls.TextBox
        Protected WithEvents lnkSave As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkCancel As System.Web.UI.WebControls.LinkButton
        Protected lnkAddQueryVarable As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lblLiceneKeys As System.Web.UI.WebControls.Label
        Protected WithEvents rptLicenseKeys As System.Web.UI.WebControls.Repeater
        Protected WithEvents pnlLicenseKeyEdit As System.Web.UI.WebControls.Panel
        Protected WithEvents lblKeyEditor As System.Web.UI.WebControls.Label
        Protected WithEvents lblKeyTarget As System.Web.UI.WebControls.Label
        Protected WithEvents lblKeyDomain As System.Web.UI.WebControls.Label
        Protected WithEvents txtKeyDomain As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblKey As System.Web.UI.WebControls.Label
        Protected WithEvents txtKeyLicense As System.Web.UI.WebControls.TextBox
        Protected WithEvents lnkSaveKey As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkCancelKey As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lblMapLatitude As System.Web.UI.WebControls.Label
        Protected WithEvents txtLatitude As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblSet1 As System.Web.UI.WebControls.Label
        Protected WithEvents lblMapLongitude As System.Web.UI.WebControls.Label
        Protected WithEvents txtLongitude As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblPointDescription As System.Web.UI.WebControls.Label
        Protected WithEvents txtPoint_Description As UI.UserControls.TextEditor
        Protected WithEvents lblWidth As System.Web.UI.WebControls.Label
        Protected WithEvents txtWidth As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblHeight As System.Web.UI.WebControls.Label
        Protected WithEvents txtHeight As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblMapZoom As System.Web.UI.WebControls.Label
        Protected WithEvents ddlZoom As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lblSet2 As System.Web.UI.WebControls.Label
        Protected WithEvents lblMapType As System.Web.UI.WebControls.Label
        Protected WithEvents ddlMapType As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lblSet3 As System.Web.UI.WebControls.Label
        Protected WithEvents lblMapControlType As System.Web.UI.WebControls.Label
        Protected WithEvents ddlMapControlType As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lblNavigationControl As System.Web.UI.WebControls.Label
        Protected WithEvents ddlNavigationType As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lblPanelName3 As System.Web.UI.WebControls.Label
        Protected WithEvents lblPanelName As System.Web.UI.WebControls.Label
        Protected WithEvents lblPanelName2 As System.Web.UI.WebControls.Label
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents rptMarkers As System.Web.UI.WebControls.Repeater
        Protected WithEvents lblEditMarker As System.Web.UI.WebControls.Label
        Protected WithEvents lblEditMarkerTarget As System.Web.UI.WebControls.Label
        Protected WithEvents pnlEditMarker As System.Web.UI.WebControls.Panel
        Protected WithEvents lblMarkerIcon As System.Web.UI.WebControls.Label
        Protected WithEvents lblMarkerShadow As System.Web.UI.WebControls.Label
        Protected WithEvents ctlIcon As DotNetNuke.UI.UserControls.UrlControl
        Protected WithEvents ctlShadow As DotNetNuke.UI.UserControls.UrlControl
        Protected WithEvents lblMarkerWH As System.Web.UI.WebControls.Label
        Protected WithEvents txtIWidth As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtIHeight As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblMarkerShadowWH As System.Web.UI.WebControls.Label
        Protected WithEvents txtSWidth As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtSHeight As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblMarkerAnchorXY As System.Web.UI.WebControls.Label
        Protected WithEvents txtAPointX As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtAPointY As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblMarkerInfoXY As System.Web.UI.WebControls.Label
        Protected WithEvents txtIPointX As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtIPointY As System.Web.UI.WebControls.TextBox
        Protected WithEvents lnkSaveMarker As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkCancelMarker As System.Web.UI.WebControls.LinkButton
        Protected WithEvents ltlGoogleAPIScript As System.Web.UI.WebControls.Literal
        Protected WithEvents pnlMap As System.Web.UI.WebControls.Panel
        Protected WithEvents lblPanelName4 As System.Web.UI.WebControls.Label
        Protected WithEvents lblDefaultRadius As System.Web.UI.WebControls.Label
        Protected WithEvents txtDefaultRadius As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblStandardInitialDelay As System.Web.UI.WebControls.Label
        Protected WithEvents txtInitialDelay As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblStandardRetryDelay As System.Web.UI.WebControls.Label
        Protected WithEvents txtRetryDelay As System.Web.UI.WebControls.TextBox
        Protected WithEvents lblInstructions As System.Web.UI.WebControls.Label
        Protected WithEvents lblOverviewMapControl As System.Web.UI.WebControls.Label
        Protected WithEvents ddlOverviewMapType As System.Web.UI.WebControls.DropDownList

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
        Private _mapsettings As PositionalConfiguration
        Private Property MapSettings() As PositionalConfiguration
            Get
                If _mapsettings Is Nothing Then
                    If Me.MapConfiguration.Settings Is Nothing Then
                        _mapsettings = New PositionalConfiguration
                        Me.MapConfiguration.Settings = _mapsettings
                        Me.MapConfiguration.Save()
                    Else
                        _mapsettings = MapConfiguration.Settings
                    End If
                End If
                Return _mapsettings
            End Get
            Set(ByVal Value As PositionalConfiguration)
                _mapsettings = Value
            End Set
        End Property
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Localize(CType(lblPanelName, Object), "Text")
            Localize(CType(lblInstructions, Object), "Text")
            Localize(CType(lblHeight, Object), "Text")
            Localize(CType(lblKey, Object), "Text")
            Localize(CType(lblKeyDomain, Object), "Text")
            Localize(CType(lblKeyEditor, Object), "Text")
            Localize(CType(lblLiceneKeys, Object), "Text")
            Localize(CType(lblMapControlType, Object), "Text")
            Localize(CType(lblMapLatitude, Object), "Text")
            Localize(CType(lblMapLongitude, Object), "Text")
            Localize(CType(lblMapType, Object), "Text")
            Localize(CType(lblMapZoom, Object), "Text")
            Localize(CType(lblNavigationControl, Object), "Text")
            Localize(CType(lblPointDescription, Object), "Text")
            Localize(CType(lblSet1, Object), "Text")
            Localize(CType(lblSet2, Object), "Text")
            Localize(CType(lblSet3, Object), "Text")
            Localize(CType(lblWidth, Object), "Text")

            Localize(CType(ddlMapType.Items(0), Object), "Text", "MapType0")
            Localize(CType(ddlMapType.Items(1), Object), "Text", "MapType1")
            Localize(CType(ddlMapType.Items(2), Object), "Text", "MapType2")
            Localize(CType(ddlNavigationType.Items(0), Object), "Text", "NavigationType0")
            Localize(CType(ddlNavigationType.Items(1), Object), "Text", "NavigationType1")
            Localize(CType(ddlNavigationType.Items(2), Object), "Text", "NavigationType2")
            Localize(CType(ddlMapControlType.Items(0), Object), "Text", "MapControlType0")
            Localize(CType(ddlMapControlType.Items(1), Object), "Text", "MapControlType1")

            Localize(CType(lblOverviewMapControl, Object), "Text")
            Localize(CType(ddlOverviewMapType.Items(0), Object), "Text", "MapControlOverview0")
            Localize(CType(ddlOverviewMapType.Items(1), Object), "Text", "MapControlOverview1")


            Localize(CType(lblEditMarker, Object), "Text")
            Localize(CType(lblMarkerAnchorXY, Object), "Text")
            Localize(CType(lblMarkerIcon, Object), "Text")
            Localize(CType(lblMarkerInfoXY, Object), "Text")
            Localize(CType(lblMarkerShadow, Object), "Text")
            Localize(CType(lblMarkerShadowWH, Object), "Text")
            Localize(CType(lblMarkerWH, Object), "Text")
            Localize(CType(lblPanelName2, Object), "Text")
            Localize(CType(lblPanelName3, Object), "Text")
            Localize(CType(lblPanelName4, Object), "Text")
            Localize(CType(lnkCancelMarker, Object), "Text")
            Localize(CType(lnkSaveMarker, Object), "Text")

            Localize(CType(lblStandardInitialDelay, Object), "Text")
            Localize(CType(lblStandardRetryDelay, Object), "Text")
            Localize(CType(lblDefaultRadius, Object), "Text")

            Try
                Localize(CType(lnkAddQueryVarable, Object), "Text")
            Catch ex As Exception

            End Try

            Localize(CType(lnkCancelKey, Object), "Text")
            Localize(CType(lnkSaveKey, Object), "Text")

            Localize(CType(lnkCancel, Object), "Text")
            Localize(CType(lnkSave, Object), "Text")
        End Sub



        Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
            If Not isLoaded Then
                If Not MapConfiguration.Map Is Nothing Then
                    BindData_Map(MapConfiguration.Map, True)
                Else
                    BindData_Map(MapID, True)
                End If
            End If
            If MapSettings.LicenseKeys.Count > 0 Then
                pnlMap.Visible = True
                BindMap()
            Else
                pnlMap.Visible = False
            End If
        End Sub
        Private Sub BindMap()
            ltlGoogleAPIScript.Text = "<SCRIPT src = ""http://maps.google.com/maps?file=api&amp;v=2&amp;key=" & MapSettings.CurrentLicenseKey(Request.Url.Host & Request.RawUrl) & """ type=text/javascript></SCRIPT>"
            ltlGoogleAPIScript.Text &= BuildRecordingScript() & Page_Ajax_Data_Wizard()
        End Sub
        Private Sub BindData_Map(ByVal MapID As Integer, ByVal LoadInterface As Boolean)
            If MapID > 0 Then
                Dim mapDC As New Data.MapController
                Dim mapInfo As Data.MapInfo
                mapInfo = mapDC.GetMap(MapID)
                BindData_Map(mapInfo, LoadInterface)
                mapInfo = Nothing
                mapDC = Nothing
            Else
                If LoadInterface Then
                    BindData_LicenseKeys(False)
                    BindData_Markers(False)
                End If
            End If
        End Sub
#Region "Javascript Runtime Components"
        Private Function BuildRecordingScript() As String
            Dim sb As New Text.StringBuilder
            sb.Append("<script language=javascript>" & vbCrLf)
            sb.Append("	function Map_LoadIcons()" & vbCrLf)
            sb.Append("	{" & vbCrLf)
            sb.Append("		if (!ICONS)" & vbCrLf)
            sb.Append("		{" & vbCrLf)
            sb.Append("			ICONS = new Array();" & vbCrLf)
            Dim ico As Configuration.Marker
            For Each ico In MapSettings.Markers
                sb.Append("			icon" & ico.Index & " = new GIcon();" & vbCrLf)
                sb.Append("			icon" & ico.Index & ".image = '" & ImageURL(ico.Icon) & "';" & vbCrLf)
                sb.Append("			icon" & ico.Index & ".shadow = '" & ImageURL(ico.Shadow) & "';" & vbCrLf)
                sb.Append("			icon" & ico.Index & ".iconSize = new GSize(" & ico.IconWidth & ", " & ico.IconHeight & ");" & vbCrLf)
                sb.Append("			icon" & ico.Index & ".shadowSize = new GSize(" & ico.ShadowWidth & ", " & ico.ShadowHeight & ");" & vbCrLf)
                sb.Append("			icon" & ico.Index & ".iconAnchor = new GPoint(" & ico.AnchorX & ", " & ico.AnchorY & ");" & vbCrLf)
                sb.Append("			icon" & ico.Index & ".infoWindowAnchor = new GPoint(" & ico.InfoX & ", " & ico.InfoY & ");" & vbCrLf)

                sb.Append("			ICONS[" & ico.Index & "] = icon" & ico.Index & ";" & vbCrLf)
            Next
            sb.Append("		}" & vbCrLf)
            sb.Append("	}" & vbCrLf)

            sb.Append("function currentMapTypeNumber(mp){ " & vbCrLf)
            sb.Append("var type=-1; " & vbCrLf)
            sb.Append("for(var ix=0;ix<mp.getMapTypes().length;ix++){ " & vbCrLf)
            sb.Append("if(mp.getMapTypes()[ix]==mp.getCurrentMapType()) " & vbCrLf)
            sb.Append(" type=ix; " & vbCrLf)
            sb.Append("} " & vbCrLf)
            sb.Append("return type; " & vbCrLf)
            sb.Append("} " & vbCrLf)
            sb.Append("function setRecording(x)" & vbCrLf)
            sb.Append("{" & vbCrLf)
            sb.Append(" if (map)" & vbCrLf)
            sb.Append("	{" & vbCrLf)
            sb.Append("  if (x=='CENTER')" & vbCrLf)
            sb.Append("	 {" & vbCrLf)
            sb.Append("     a = document.getElementById('" & txtLongitude.UniqueID.Replace(":"c, "_").Replace("$"c, "_") & "');" & vbCrLf)
            sb.Append("     b = document.getElementById('" & txtLatitude.UniqueID.Replace(":"c, "_").Replace("$"c, "_") & "');" & vbCrLf)
            sb.Append("     c = map.getCenterLatLng();" & vbCrLf)
            sb.Append("     if (a && b && c) {" & vbCrLf)
            sb.Append("         a.value = Map_Number(c.x);" & vbCrLf)
            sb.Append("         b.value = Map_Number(c.y);" & vbCrLf)
            sb.Append("	    }" & vbCrLf)
            sb.Append("	 }" & vbCrLf)
            sb.Append("  else if (x=='ZOOM')" & vbCrLf)
            sb.Append("	 {" & vbCrLf)
            sb.Append("     a = document.getElementById('" & ddlZoom.UniqueID.Replace(":"c, "_").Replace("$"c, "_") & "');" & vbCrLf)
            sb.Append("     b = map.getZoomLevel();" & vbCrLf)
            sb.Append("     if (a && b) {" & vbCrLf)
            sb.Append("         a.selectedIndex = b-1;" & vbCrLf)
            sb.Append("	    }" & vbCrLf)
            sb.Append("	 }" & vbCrLf)
            sb.Append("  else if (x=='TYPE')" & vbCrLf)
            sb.Append("	 {" & vbCrLf)
            sb.Append("     a = document.getElementById('" & ddlMapType.UniqueID.Replace(":"c, "_").Replace("$"c, "_") & "');" & vbCrLf)
            sb.Append("     b = currentMapTypeNumber(map);" & vbCrLf)
            sb.Append("     if (a) {" & vbCrLf)
            sb.Append("         a.selectedIndex = b;" & vbCrLf)
            sb.Append("	    }" & vbCrLf)
            sb.Append("	 }" & vbCrLf)
            sb.Append("	}" & vbCrLf)
            sb.Append("}" & vbCrLf)
            sb.Append("</script>")
            Return sb.ToString
        End Function

        Private Function Page_Ajax_Data_Wizard() As String
            Dim sb As New Text.StringBuilder
            sb.Append("<SCRIPT language=""javascript"" src=""" & ImageURL("*" & "Visuals/Dotnetnuke.Map.Visuals.js") & """></SCRIPT>" & vbCrLf)
            sb.Append("<script language=javascript>" & vbCrLf)
            sb.Append("//STARTUP" & vbCrLf)
            sb.Append("function startup() {" & vbCrLf)
            sb.Append("if (window.startDataWizard) {" & vbCrLf)
            sb.Append("TMID = " & Me.ControlPanelModule.TabModuleId & ";" & vbCrLf)
            sb.Append("WURL = '" & ImageURL("*") & "';" & vbCrLf)

            sb.Append("LOCALE_ADDADDRESS1	= '" & Locale("SCRIPT_ADDADDRESS1") & "';" & vbCrLf)
            sb.Append("LOCALE_ADDADDRESS2	= '" & Locale("SCRIPT_ADDADDRESS2") & "';" & vbCrLf)
            sb.Append("LOCALE_ADDRESS		= '" & Locale("SCRIPT_ADDRESS") & "';" & vbCrLf)
            sb.Append("LOCALE_CITY			= '" & Locale("SCRIPT_CITY") & "';	" & vbCrLf)
            sb.Append("LOCALE_STATE		    = '" & Locale("SCRIPT_STATE") & "';" & vbCrLf)
            sb.Append("LOCALE_ZIP			= '" & Locale("SCRIPT_ZIP") & "';" & vbCrLf)
            sb.Append("LOCALE_GO			= '" & Locale("SCRIPT_GO") & "';" & vbCrLf)
            sb.Append("LOCALE_SAVE			= '" & Locale("SCRIPT_SAVE") & "';" & vbCrLf)
            sb.Append("LOCALE_DELETE		= '" & Locale("SCRIPT_DELETE") & "';" & vbCrLf)
            sb.Append("LOCALE_CANCEL		= '" & Locale("SCRIPT_CANCEL") & "';" & vbCrLf)
            sb.Append("LOCALE_EDIT          = '" & Locale("SCRIPT_EDIT") & "';" & vbCrLf)
            sb.Append("LOCALE_ICONINDEX	    = '" & Locale("SCRIPT_ICONINDEX") & "';" & vbCrLf)
            sb.Append("LOCALE_INDEX		    = '" & Locale("SCRIPT_INDEX") & "';" & vbCrLf)
            sb.Append("LOCALE_TIMER		    = '" & Locale("SCRIPT_TIMER") & "';" & vbCrLf)
            sb.Append("LOCALE_TIMERINFO	    = '" & Locale("SCRIPT_TIMERINFO") & "';" & vbCrLf)
            sb.Append("LOCALE_LATITUDE		= '" & Locale("SCRIPT_LATITUDE") & "';" & vbCrLf)
            sb.Append("LOCALE_LONGITUDE	    = '" & Locale("SCRIPT_LONGITUDE") & "';" & vbCrLf)
            sb.Append("LOCALE_DESCRIPTION	= '" & Locale("SCRIPT_DESCRIPTION") & "';" & vbCrLf)
            sb.Append("LOCALE_ADDADDRESS	= '" & Locale("SCRIPT_ADDADDRESS") & "';" & vbCrLf)
            sb.Append("LOCALE_NEW			= '" & Locale("SCRIPT_NEW") & "';" & vbCrLf)
            sb.Append("LOCALE_ZOOM			= '" & Locale("SCRIPT_ZOOM") & "';" & vbCrLf)
            sb.Append("LOCALE_PAGEFIRST	    = '" & Locale("SCRIPT_PAGEFIRST") & "';" & vbCrLf)
            sb.Append("LOCALE_PAGELAST		= '" & Locale("SCRIPT_PAGELAST") & "';" & vbCrLf)
            sb.Append("LOCALE_PAGENEXT		= '" & Locale("SCRIPT_PAGENEXT") & "';" & vbCrLf)
            sb.Append("LOCALE_PAGEBACK		= '" & Locale("SCRIPT_PAGEBACK") & "';" & vbCrLf)
            sb.Append("LOCALE_STATUS1		= '" & Locale("SCRIPT_STATUS1") & "';" & vbCrLf)
            sb.Append("LOCALE_STATUS2		= '" & Locale("SCRIPT_STATUS2") & "';" & vbCrLf)
            sb.Append("LOCALE_STATUS3		= '" & Locale("SCRIPT_STATUS3") & "';" & vbCrLf)
            sb.Append("LOCALE_STATUS4		= '" & Locale("SCRIPT_STATUS4") & "';" & vbCrLf)
            sb.Append("LOCALE_STATUS5		= '" & Locale("SCRIPT_STATUS5") & "';" & vbCrLf)
            sb.Append("LOCALE_STATUS6		= '" & Locale("SCRIPT_STATUS6") & "';" & vbCrLf)
            sb.Append("LOCALE_STATUS7		= '" & Locale("SCRIPT_STATUS7") & "';" & vbCrLf)
            sb.Append("LOCALE_STATUS8		= '" & Locale("SCRIPT_STATUS8") & "';" & vbCrLf)
            sb.Append("LOCALE_STATUS9		= '" & Locale("SCRIPT_STATUS9") & "';" & vbCrLf)
            sb.Append("LOCALE_STATUS10		= '" & Locale("SCRIPT_STATUS10") & "';" & vbCrLf)
            If Not MapSettings Is Nothing AndAlso MapSettings.Description.Length > 0 Then
                sb.Append("LOCALE_SDESCRIPTION = '" & MapSettings.Description.Replace(Chr(10), "<br>").Replace(Chr(13), "").Replace("'", "\'") & "';" & vbCrLf)
            Else
                sb.Append("LOCALE_SDESCRIPTION = '" & Locale("LOCALE_SDESCRIPTION").Replace(Chr(10), "<br>").Replace(Chr(13), "").Replace("'", "\'") & "';" & vbCrLf)
            End If
            If Not MapSettings Is Nothing AndAlso MapSettings.Latitude.Length > 0 Then
                sb.Append("LOCALE_SLAT = '" & MapSettings.Latitude & "';" & vbCrLf)
            Else
                sb.Append("LOCALE_SLAT = '" & Locale("LOCALE_SLAT") & "';" & vbCrLf)
            End If
            If Not MapSettings Is Nothing AndAlso MapSettings.Longitude.Length > 0 Then
                sb.Append("LOCALE_SLON = '" & MapSettings.Longitude & "';" & vbCrLf)
            Else
                sb.Append("LOCALE_SLON = '" & Locale("LOCALE_SLON") & "';" & vbCrLf)
            End If
            If Not MapSettings Is Nothing AndAlso MapSettings.Zoom > 0 Then
                sb.Append("LOCALE_SZOOM = '" & MapSettings.Zoom & "';" & vbCrLf)
            Else
                sb.Append("LOCALE_SZOOM = '" & Locale("LOCALE_SZOOM") & "';" & vbCrLf)
            End If
            sb.Append("LOCALE_DSEP          = '" & System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator & "';" & vbCrLf)

            sb.Append("startDataWizard(); " & vbCrLf)
            sb.Append("} else {" & vbCrLf)
            sb.Append("window.setTimeout('startup();',250);" & vbCrLf)
            sb.Append("}" & vbCrLf)
            sb.Append("}" & vbCrLf)
            sb.Append("startup();" & vbCrLf)

            sb.Append("</script>" & vbCrLf)
            Return sb.ToString
        End Function
#End Region

        Private Sub BindData_Map(ByRef mapinfo As Data.MapInfo, ByVal LoadInterface As Boolean)
            If Not mapinfo Is Nothing AndAlso mapinfo.PortalID = ControlPanelModule.PortalId Then
                'LOAD THE SETTING
                If Not mapinfo.Settings Is Nothing Then
                    Try
                        Dim sinfo As PositionalConfiguration
                        Try
                            sinfo = PositionalConfiguration.Deserialize(mapinfo.Settings)
                        Catch ex As Exception
                            sinfo = New PositionalConfiguration
                        End Try
                        MapConfiguration.Settings = sinfo
                        If LoadInterface Then

                            txtHeight.Text = sinfo.Height
                            Dim dblLat As Double = 0
                            Dim dblLon As Double = 0
                            If Double.TryParse(sinfo.Latitude, Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, dblLat) Then
                                txtLatitude.Text = dblLat.ToString
                            Else
                                txtLatitude.Text = sinfo.Latitude
                            End If
                            If Double.TryParse(sinfo.Longitude, Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, dblLon) Then
                                txtLongitude.Text = dblLon.ToString
                            Else
                                txtLongitude.Text = sinfo.Longitude
                            End If
                            txtPoint_Description.Text = sinfo.Description

                            If Not sinfo.DefaultType Is Nothing AndAlso Not ddlMapType.Items.FindByValue(sinfo.DefaultType) Is Nothing Then
                                ddlMapType.SelectedValue = sinfo.DefaultType
                            End If
                            If Not sinfo.NavigationDisplay Is Nothing AndAlso Not ddlNavigationType.Items.FindByValue(sinfo.NavigationDisplay) Is Nothing Then
                                ddlNavigationType.SelectedValue = sinfo.NavigationDisplay
                            End If
                            If Not sinfo.TypeDisplay Is Nothing AndAlso Not ddlMapControlType.Items.FindByValue(sinfo.TypeDisplay) Is Nothing Then
                                ddlMapControlType.SelectedValue = sinfo.TypeDisplay
                            End If
                            If Not ddlZoom.Items.FindByValue(sinfo.Zoom) Is Nothing Then
                                ddlZoom.SelectedValue = sinfo.Zoom
                            End If
                            If Not sinfo.OverviewMapDisplay Is Nothing AndAlso Not ddlOverviewMapType.Items.FindByValue(sinfo.OverviewMapDisplay) Is Nothing Then
                                ddlOverviewMapType.SelectedValue = sinfo.OverviewMapDisplay
                            End If

                            txtWidth.Text = sinfo.Width

                            'POSITIONAL
                            txtInitialDelay.Text = sinfo.delayInitial
                            txtRetryDelay.Text = sinfo.delayRetry
                            txtDefaultRadius.Text = sinfo.PositionalRadius

                            BindData_LicenseKeys(False)
                            BindData_Markers(False)
                        End If
                    Catch ex As Exception
                            DotNetNuke.Services.Exceptions.ProcessModuleLoadException("Unable to deserialize the Map Source Configuration.", Me, ex, True)
                    End Try
                Else
                        SourceConfiguration.Settings = New PositionalConfiguration
                        If LoadInterface Then
                            BindData_LicenseKeys(False)
                            BindData_Markers(False)
                        End If
                End If
            End If
        End Sub

        Private Sub lnkSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSave.Click
            BindData_Map(MapID, False)
            Savesettings()
            MyBase.RemoveControl()

        End Sub

        Private Sub SaveSettings()
            Dim sinfo As PositionalConfiguration
            If Me.MapConfiguration.Settings Is Nothing Then
                sinfo = New PositionalConfiguration
            Else
                sinfo = MapConfiguration.Settings
            End If

            sinfo.MapType = 1
            sinfo.DefaultType = ddlMapType.SelectedValue
            sinfo.Description = txtPoint_Description.Text
            sinfo.Height = txtHeight.Text

            If Not txtLatitude.Text Is Nothing Then
                Try
                    sinfo.Latitude = CDbl(txtLatitude.Text).ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat)
                Catch ex As Exception
                End Try
            Else
            End If
            If Not txtLongitude.Text Is Nothing Then
                Try
                    sinfo.Longitude = CDbl(txtLongitude.Text).ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat)
                Catch ex As Exception
                End Try
            Else
            End If

            sinfo.TypeDisplay = ddlMapControlType.SelectedValue
            sinfo.NavigationDisplay = ddlNavigationType.SelectedValue
            sinfo.OverviewMapDisplay = ddlOverviewMapType.SelectedValue
            sinfo.Width = txtWidth.Text
            sinfo.Zoom = ddlZoom.SelectedValue

            'POSITIONAL
            sinfo.PositionalRadius = FormatNumber(txtDefaultRadius)
            sinfo.delayInitial = FormatNumber(txtInitialDelay)
            sinfo.delayRetry = FormatNumber(txtRetryDelay)

            sinfo.SequenceMarkers()

            MapConfiguration.Map.Settings = PositionalConfiguration.Serialize(sinfo)

            MapConfiguration.Save()
        End Sub

        Private Sub lnkCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCancel.Click
            MyBase.RemoveControl()
        End Sub

        Public Function ImageURL(ByVal Src As String) As String
            Src = IIf(Src = "", "~/images/spacer.gif", Src)
            If Not Src Is Nothing Then
                If Not Src.IndexOf(":") >= 0 Then
                    If Src.StartsWith("~") Then
                        Src = DotNetNuke.Common.ResolveUrl(Src)
                    ElseIf Src.StartsWith("*") Then
                        Src = ModuleImageURL(Src.Remove(0, 1))
                    ElseIf Src.ToLower.StartsWith("fileid=") Then
                        Dim fileId As Integer = Integer.Parse(Src.Substring(7))
                        Dim objFileController As New DotNetNuke.Services.FileSystem.FileController
                        Dim objFileInfo As DotNetNuke.Services.FileSystem.FileInfo = objFileController.GetFileById(fileId, MyBase.ControlPanelModule.PortalId)
                        If Not objFileInfo Is Nothing Then

                            Src = ControlPanelModule.PortalSettings.HomeDirectory & objFileInfo.Folder & objFileInfo.FileName
                        End If
                    Else
                        Src = ControlPanelModule.PortalSettings.HomeDirectory & Src
                    End If
                    Return Src
                Else
                    Return Src
                End If
            Else
                Return ""
            End If
        End Function

        Public Function ModuleImageURL(ByVal Src As String) As String
            If Not ModulePath Is Nothing Then
                If ModulePath.EndsWith("/") Or ModulePath.EndsWith("\") Then
                    Return ModulePath & Src
                Else
                    Return ModulePath & "/" & Src
                End If
            Else
                Return "~/images/" & Src
            End If
        End Function
        Public Function BoolURL(ByVal Value As Boolean) As String
            If Value Then
                Return ModuleImageURL("checked.gif")
            Else
                Return ModuleImageURL("unchecked.gif")
            End If
        End Function

#Region "Markers"
        Private Property EditMarker() As Integer
            Get
                Return viewstate.Item("EditMarker")
            End Get
            Set(ByVal Value As Integer)
                viewstate.Item("EditMarker") = Value
            End Set
        End Property
        Private Function FormatNumber(ByRef obj As System.Web.UI.WebControls.TextBox) As Integer
            If IsNumeric(obj.Text) Then
                Return CType(obj.Text, Integer)
            Else
                Return 0
            End If
        End Function
        Private Function Build_Marker() As Configuration.Marker
            Dim mkobj As New Configuration.Marker

            mkobj.AnchorX = FormatNumber(txtAPointX)
            mkobj.AnchorY = FormatNumber(txtAPointY)
            mkobj.IconHeight = FormatNumber(txtIHeight)
            mkobj.IconWidth = FormatNumber(txtIWidth)
            mkobj.InfoX = FormatNumber(txtIPointX)
            mkobj.InfoY = FormatNumber(txtIPointY)

            mkobj.Icon = MyBase.getUrlControl(ctlIcon, txtIcon)
            mkobj.Shadow = MyBase.getUrlControl(ctlShadow, txtShadow)
            mkobj.ShadowHeight = FormatNumber(txtSHeight)
            mkobj.ShadowWidth = FormatNumber(txtSWidth)

            Return mkobj
        End Function

        Private Sub BindData_Markers(ByVal ForceSave As Boolean)
            Dim map As PositionalConfiguration
            If Me.MapConfiguration Is Nothing Then
                BindData_Map(MapID, False)
            End If

            If Me.MapConfiguration.Settings Is Nothing Then
                map = New PositionalConfiguration
            Else
                map = CType(MapConfiguration.Settings, PositionalConfiguration)
            End If

            map.SequenceMarkers()

            If ForceSave Then
                Me.MapConfiguration.Settings = map
                SaveSettings()
                MyBase.ControlPanelModule.LoadInteraction()
            End If

            rptMarkers.DataSource = map.Markers
            rptMarkers.DataBind()
        End Sub

        Private Sub rptMarkers_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptMarkers.ItemCommand
            BindData_Map(MapID, False)
            Dim src As PositionalConfiguration = CType(MapConfiguration.Settings, PositionalConfiguration)
            If src Is Nothing Then src = New PositionalConfiguration
            src.SequenceMarkers()

            Dim index As Integer
            If Not e.CommandArgument Is Nothing AndAlso IsNumeric(e.CommandArgument) Then
                index = CInt(e.CommandArgument)
                If index >= 0 And index < src.Markers.Count Then
                    Select Case e.CommandName
                        Case "Edit"
                            'Edit the item
                            Dim tbiMe As Configuration.Marker = src.Markers(index)
                            EditMarkerItem(tbiMe)
                            pnlEditMarker.Visible = True
                        Case "Delete"
                            'Delete the item
                            src.Markers.RemoveAt(index)

                            BindData_Markers(True)
                        Case "Up"
                            'Move the Index Up
                            If index > 0 Then
                                src.SwapMarkers(index, index - 1)
                                BindData_Markers(True)
                            End If
                        Case "Down"
                            'Move the Index Down
                            If index >= 0 Then
                                If index < src.Markers.Count - 1 Then
                                    src.SwapMarkers(index, index + 1)
                                    BindData_Markers(True)
                                End If
                            End If
                    End Select
                End If
            Else
                If Not e.CommandName Is Nothing Then
                    Select Case e.CommandName
                        Case "Add"
                            pnlEditMarker.Visible = True
                            MyBase.setUrlControl(ctlIcon, txtIcon, "")
                            MyBase.setUrlControl(ctlShadow, txtShadow, "")

                            txtAPointX.Text = "0"
                            txtAPointY.Text = "0"
                            txtSHeight.Text = "0"
                            txtSWidth.Text = "0"
                            txtIHeight.Text = "0"
                            txtIPointX.Text = "0"
                            txtIPointY.Text = "0"
                            txtIHeight.Text = "0"
                            txtIWidth.Text = "0"


                            BindData_Markers(True)

                            EditMarker = src.Markers.Count + 1
                    End Select
                End If
            End If
        End Sub

        Private Sub EditMarkerItem(ByRef Value As Configuration.Marker)
            Try
                EditMarker = Value.Index + 1
                lblKeyTarget.Text = " - [" & Value.Index.ToString & "]"

                MyBase.setUrlControl(ctlIcon, txtIcon, Value.Icon)
                MyBase.setUrlControl(ctlShadow, txtShadow, Value.Icon)
                
                txtAPointX.Text = Value.AnchorX
                txtAPointY.Text = Value.AnchorY
                txtSHeight.Text = Value.ShadowHeight
                txtSWidth.Text = Value.ShadowWidth
                txtIHeight.Text = Value.IconHeight
                txtIPointX.Text = Value.InfoX
                txtIPointY.Text = Value.InfoY
                txtIHeight.Text = Value.IconHeight
                txtIWidth.Text = Value.IconWidth

            Catch ex As Exception
                DotNetNuke.Services.Exceptions.LogException(ex)
            End Try

        End Sub

        Private Sub lnkSaveMarker_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSaveMarker.Click
            BindData_Map(MapID, False)

            Dim src As PositionalConfiguration = CType(MapConfiguration.Settings, PositionalConfiguration)

            Dim tbi As Configuration.Marker = Build_Marker()
            Dim index As Integer = CInt(EditMarker)
            If index > src.Markers.Count Then
                'NEW ITEM
                tbi.Index = src.Markers.Count
                src.Markers.Add(tbi)
            Else
                'EXISTING ITEM
                tbi.Index = index - 1
                src.Markers(index - 1) = tbi
            End If

            BindData_Markers(True)

            pnlEditMarker.Visible = False
        End Sub

        Private Sub lnkCancelMarker_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCancelMarker.Click
            BindData_Map(MapID, False)
            BindData_Markers(False)

            pnlEditMarker.Visible = False
        End Sub
#End Region
#Region "License Keys"
        Private Property EditItem() As Integer
            Get
                Return viewstate.Item("EditItem")
            End Get
            Set(ByVal Value As Integer)
                viewstate.Item("EditItem") = Value
            End Set
        End Property

        Private Function Build_LicenseKey() As Configuration.LicenseKey
            Dim lkobj As New Configuration.LicenseKey

            lkobj.Domain = txtKeyDomain.Text
            lkobj.Key = txtKeyLicense.Text

            Return lkobj
        End Function

        Private Sub BindData_LicenseKeys(ByVal ForceSave As Boolean)
            Dim map As PositionalConfiguration
            If Me.MapConfiguration Is Nothing Then
                BindData_Map(MapID, False)
            End If

            If Me.MapConfiguration.Settings Is Nothing Then
                map = New PositionalConfiguration
            Else
                map = CType(MapConfiguration.Settings, PositionalConfiguration)
            End If

            map.SequenceKeys()

            If ForceSave Then
                Me.MapConfiguration.Settings = map
                SaveSettings()
                MyBase.ControlPanelModule.LoadInteraction()
            End If

            rptLicenseKeys.DataSource = map.LicenseKeys
            rptLicenseKeys.DataBind()
        End Sub
        Public Function KeyFormat(ByVal Value As String) As String
            Dim key As String = ""
            If Not Value Is Nothing AndAlso Value.Length > 0 Then
                If Value.Length > 10 Then
                    key = Value.Substring(0, 5) & "..."
                    key &= Value.Substring(Value.Length - 5, 5)
                Else
                    key = Value
                End If
            Else
                key = "<i>Not Provided</i>"
            End If
            Return key
        End Function

        Private Sub rptLicenseKeys_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptLicenseKeys.ItemCommand
            BindData_Map(MapID, False)
            Dim src As PositionalConfiguration = CType(MapConfiguration.Settings, PositionalConfiguration)
            If src Is Nothing Then src = New PositionalConfiguration
            src.SequenceKeys()

            Dim index As Integer
            If Not e.CommandArgument Is Nothing AndAlso IsNumeric(e.CommandArgument) Then
                index = CInt(e.CommandArgument)
                If index >= 0 And index < src.LicenseKeys.Count Then
                    Select Case e.CommandName
                        Case "Edit"
                            'Edit the item
                            Dim tbiMe As Configuration.LicenseKey = src.LicenseKeys(index)
                            EditLicenseKeyItem(tbiMe)
                            pnlLicenseKeyEdit.Visible = True
                        Case "Delete"
                            'Delete the item
                            src.LicenseKeys.RemoveAt(index)

                            BindData_LicenseKeys(True)
                    End Select
                End If
            Else
                If Not e.CommandName Is Nothing Then
                    Select Case e.CommandName
                        Case "Add"
                            pnlLicenseKeyEdit.Visible = True

                            txtKeyDomain.Text = ""
                            txtKeyLicense.Text = ""

                            BindData_LicenseKeys(True)

                            EditItem = src.LicenseKeys.Count + 1
                    End Select
                End If
            End If
        End Sub

        Private Sub EditLicenseKeyItem(ByRef Value As Configuration.LicenseKey)
            Try
                EditItem = Value.Index + 1
                lblKeyTarget.Text = " - [" + Value.Domain + "]"
                txtKeyDomain.Text = Value.Domain
                txtKeyLicense.Text = Value.Key

            Catch ex As Exception
                DotNetNuke.Services.Exceptions.LogException(ex)
            End Try

        End Sub

        Private Sub lnkSaveKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSaveKey.Click
            BindData_Map(MapID, False)

            Dim src As PositionalConfiguration = CType(MapConfiguration.Settings, PositionalConfiguration)

            Dim tbi As Configuration.LicenseKey = Build_LicenseKey()
            Dim index As Integer = CInt(EditItem)
            If index > src.LicenseKeys.Count Then
                'NEW ITEM
                tbi.Index = src.LicenseKeys.Count
                src.LicenseKeys.Add(tbi)
            Else
                'EXISTING ITEM
                tbi.Index = index - 1
                src.LicenseKeys(index - 1) = tbi
            End If

            BindData_LicenseKeys(True)

            pnlLicenseKeyEdit.Visible = False
        End Sub

        Private Sub lnkCancelKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCancelKey.Click
            BindData_Map(MapID, False)
            BindData_LicenseKeys(False)

            pnlLicenseKeyEdit.Visible = False
        End Sub
#End Region

    End Class
End Namespace

