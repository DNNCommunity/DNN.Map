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
Namespace DotNetNuke.Modules.Map.Geolocator.Google
    Public Class GoogleAdmin
        Inherits DotNetNuke.Modules.Map.Components.ControlPanelBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents lblPanelName As System.Web.UI.WebControls.Label
        Protected WithEvents lblAPIKey As System.Web.UI.WebControls.Label
        Protected WithEvents lblInstructions As System.Web.UI.WebControls.Label
        Protected WithEvents txtAPIKey As System.Web.UI.WebControls.TextBox
        Protected WithEvents chkUseCountry As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkUseRegion As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkUsePostalCode As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkUseCity As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkUseStreet As System.Web.UI.WebControls.CheckBox
        Protected WithEvents chkUseUnit As System.Web.UI.WebControls.CheckBox
        Protected WithEvents lnkSave As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkCancel As System.Web.UI.WebControls.LinkButton

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
            Localize(CType(lblPanelName, Object), "Text")
            Localize(CType(lblAPIKey, Object), "Text")
            Localize(CType(lblInstructions, Object), "Text")

            Localize(CType(chkUseCity, Object), "Text")
            Localize(CType(chkUseCountry, Object), "Text")
            Localize(CType(chkUsePostalCode, Object), "Text")
            Localize(CType(chkUseRegion, Object), "Text")
            Localize(CType(chkUseStreet, Object), "Text")
            Localize(CType(chkUseUnit, Object), "Text")

            Localize(CType(lnkCancel, Object), "Text")
            Localize(CType(lnkSave, Object), "Text")
        End Sub



        Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
            If Not isLoaded Then
                If Not SourceConfiguration.Source Is Nothing Then
                    SourceID = SourceConfiguration.Source.SourceID
                    BindData_GeoLocator(SourceConfiguration.Source, True)
                Else
                    BindData_GeoLocator(SourceID, True)
                End If
            End If
        End Sub

        Private Sub BindData_GeoLocator(ByRef sourceinfo As SourceInfo, ByVal LoadInterface As Boolean)
            If Not sourceinfo Is Nothing AndAlso sourceinfo.PortalID = ControlPanelModule.PortalId Then
                'LOAD THE SETTING
                If Not sourceinfo.GeoLocatorSettings Is Nothing Then
                    Try
                        Dim sinfo As Map.Geolocator.Google.Configuration = Map.Geolocator.Google.Configuration.Deserialize(sourceinfo.GeoLocatorSettings)
                        GeolocatorConfiguration.Settings = sinfo
                        If LoadInterface Then
                            txtAPIKey.Text = sinfo.LicenseKey
                            chkUseCity.Checked = sinfo.useCity
                            chkUseCountry.Checked = sinfo.useCountry
                            chkUsePostalCode.Checked = sinfo.usePostalCode
                            chkUseRegion.Checked = sinfo.useRegion
                            chkUseStreet.Checked = sinfo.useStreet
                            chkUseUnit.Checked = sinfo.useUnit
                        End If
                    Catch ex As Exception
                        DotNetNuke.Services.Exceptions.ProcessModuleLoadException("Unable to deserialize the Map Geolocator Configuration.", Me, ex, True)
                    End Try
                Else
                    GeolocatorConfiguration.Settings = New Map.GeoLocator.Google.Configuration
                    If LoadInterface Then
                        txtAPIKey.Text = ""
                    End If
                End If
            End If
        End Sub
        Private Sub BindData_GeoLocator(ByVal SourceID As Integer, ByVal LoadInterface As Boolean)
            If SourceID > 0 Then
                Dim mapDC As New MapController
                Dim sourceInfo As sourceInfo
                sourceInfo = mapDC.GetSource(SourceID)
                BindData_GeoLocator(sourceInfo, LoadInterface)
                sourceInfo = Nothing
                mapDC = Nothing
            Else
                If LoadInterface Then
                    txtAPIKey.Text = ""
                End If
            End If
        End Sub


        Private Sub lnkSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSave.Click
            BindData_GeoLocator(SourceID, False)
            Dim sinfo As Map.GeoLocator.Google.Configuration
            If Me.SourceConfiguration.Settings Is Nothing Then
                sinfo = New Map.GeoLocator.Google.Configuration
            Else
                sinfo = GeolocatorConfiguration.Settings
            End If

            sinfo.LicenseKey = txtAPIKey.Text
            sinfo.useCity = chkUseCity.Checked
            sinfo.useCountry = chkUseCountry.Checked
            sinfo.usePostalCode = chkUsePostalCode.Checked
            sinfo.useRegion = chkUseRegion.Checked
            sinfo.useStreet = chkUseStreet.Checked
            sinfo.useUnit = chkUseUnit.Checked

            GeolocatorConfiguration.Settings = Map.Geolocator.Google.Configuration.Serialize(sinfo)

            GeolocatorConfiguration.Save()
            MyBase.RemoveControl()

        End Sub

        Private Sub lnkCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCancel.Click
            MyBase.RemoveControl()
        End Sub

    End Class
End Namespace
