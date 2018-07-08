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
Imports DotNetNuke.UI.Utilities
Imports DotNetNuke.Modules.Map.Components
Imports DotNetNuke.Modules.Map.Data
Namespace DotNetNuke.Modules.Map.Controls
    Public Class MapData
        Inherits ControlPanelBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ltlAjaxDataWizard As System.Web.UI.WebControls.Literal
        Protected WithEvents ltlJavascript As System.Web.UI.WebControls.Literal
        Protected WithEvents lblPanelName As System.Web.UI.WebControls.Label
        Protected WithEvents lblInstructions As System.Web.UI.WebControls.Label

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region



        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try

                setJavascript()
                Localize(CType(lblPanelName, Object), "Text")
                Localize(CType(lblInstructions, Object), "Text")

            Catch ex As Exception
                DotNetNuke.Services.Exceptions.LogException(New DotNetNuke.Services.Exceptions.ModuleLoadException("Unable to add custom Data provider to output stack.", ex, MyBase.ControlPanelModule.ModuleConfiguration))
            End Try
        End Sub

        Private Sub setJavascript()
            Dim sb As New Text.StringBuilder
            sb.Append("<script language=""javascript"">" & vbCrLf)
            sb.Append("//<![CDATA[" & vbCrLf)

            Javascript_BuildStartup(sb, MyBase.ControlPanelModule.ModuleId)

            sb.Append("//]]>" & vbCrLf)
            sb.Append("</script>" & vbCrLf)

            ltlJavascript.Text = sb.ToString

            sb = Nothing
        End Sub

        Private Function Javascript_BuildStartup(ByRef sb As Text.StringBuilder, ByVal tid As Integer) As String
            sb.Append("function startup() {" & vbCrLf)
            sb.Append("if (window.startDataWizard) {" & vbCrLf)
            sb.Append("TMID = " & tid & ";" & vbCrLf)
            sb.Append("WURL = '" & Me.ModulePath & "';" & vbCrLf)
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
            sb.Append("LOCALE_DSEP          = '" & System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator & "';" & vbCrLf)

            sb.Append("startDataWizard(); " & vbCrLf)
            sb.Append("} else {" & vbCrLf)
            sb.Append("window.setTimeout('startup();',250);" & vbCrLf)
            sb.Append("}" & vbCrLf)
            sb.Append("}" & vbCrLf)
            sb.Append("startup();" & vbCrLf)
            Return sb.ToString
        End Function

    End Class
End Namespace
