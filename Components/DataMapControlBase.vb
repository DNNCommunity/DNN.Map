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
Namespace DotNetNuke.Modules.Map.Components
    Public MustInherit Class DataMapControlBase
        Inherits MapControlBase

        Public Overridable Function canRead() As Boolean
            Return DotNetNuke.Security.PortalSecurity.HasNecessaryPermission(Security.SecurityAccessLevel.View, [Module].PortalSettings, [Module].ModuleConfiguration, [Module].UserInfo.Username)
        End Function
        Public Overridable Function canWrite() As Boolean
            Return DotNetNuke.Security.PortalSecurity.HasNecessaryPermission(Security.SecurityAccessLevel.Edit, [Module].PortalSettings, [Module].ModuleConfiguration, [Module].UserInfo.Username)
        End Function

        Public MustOverride Function GetData(ByVal Argument As String) As String
        Public MustOverride Function SetData(ByVal Argument As String) As String
        Public MustOverride Function ServiceData(ByVal Argument As String) As String
        Public MustOverride Function GeoData(ByVal Argument As String) As String

        Public MustOverride ReadOnly Property JavascriptLibrary() As String
        Public MustOverride ReadOnly Property JavascriptReadFunctionInit() As String
        Public MustOverride ReadOnly Property JavascriptWriteFunctionInit() As String
        Public MustOverride ReadOnly Property JavascriptServiceFunctionInit() As String
        Public MustOverride ReadOnly Property JavascriptGeoFunctionInit() As String
        Public MustOverride ReadOnly Property JavascriptPointFormatValue() As String
        Public MustOverride ReadOnly Property JavascriptServiceFunctionComplete() As String
        Public MustOverride ReadOnly Property JavascriptServiceFunctionFailure() As String

        Public Overridable Function RenderPoints(ByRef Source As ArrayList, ByVal PageIndex As Integer, ByVal RecordsPerPage As Integer) As String
            Dim sb As New Text.StringBuilder
            Dim i As Integer = 0
            Dim distanceColumn As Integer = -1
            Dim dp As Data.DataPoint
            Dim isFirst As Boolean = True
            Dim starti As Integer = 0
            Dim stopi As Integer = Source.Count - 1
            If Source.Count > 0 Then
                If RecordsPerPage > 0 Then
                    starti = PageIndex * RecordsPerPage
                    stopi = (PageIndex + 1) * RecordsPerPage - 1
                    If stopi >= Source.Count Then
                        stopi = Source.Count - 1
                    End If
                End If
                For i = starti To stopi
                    dp = Source(i)
                    If Not isFirst Then
                        sb.Append(",")
                    Else
                        isFirst = False
                    End If
                    sb.Append(RenderPoint(dp))
                Next
            End If
            Return sb.ToString
        End Function
        Private Function RenderPoint(ByRef source As Data.DataPoint) As String
            Dim str As String
            str = "new Array("
            str &= "'" & JavascriptFormatString(source.Address) & "',"
            str &= "'" & JavascriptFormatString(source.Description) & "',"
            str &= source.Distance.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat) & ","
            str &= source.PointID & ","
            str &= source.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat) & ","
            str &= source.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat) & ","
            str &= source.IconIndex & ","
            str &= source.SequenceNumber & ","
            str &= "'" & JavascriptFormatString(source.SequenceInfo) & "',"
            str &= source.ZoomShow & ","
            str &= source.ZoomHide & ","
            str &= source.SummaryCount
            str &= ")"
            Return str
        End Function

        Public Function JavascriptFormatString(ByVal value As String) As String
            If value Is Nothing Then
                value = ""
            End If
            Return value.Replace("\", "\\").Replace("'", "\'").Replace(Chr(10), "\n").Replace(Chr(13), "")
        End Function
    End Class
End Namespace