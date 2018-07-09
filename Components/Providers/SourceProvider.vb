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
Namespace DotNetNuke.Modules.Map.Components
    Public MustInherit Class SourceProvider
        Private _Settings As Object
        Public Property Settings() As Object
            Get
                Return _Settings
            End Get
            Set(ByVal Value As Object)
                _Settings = Value
            End Set
        End Property
        Public Sub New()
        End Sub
        Public Sub New(ByVal Settings As Object)
            _Settings = Settings
        End Sub
#Region "Map_Points"
        Public MustOverride Function AddPoint(ByVal SourceID As Integer, ByVal Address As String, ByVal Description As String, ByVal GUID As String, ByVal IconIndex As Integer, ByVal Longitude As Double, ByVal Latitude As Double, ByVal SequenceNumber As Integer, ByVal SequenceInfo As String, ByVal ZoomShow As Integer, ByVal ZoomHide As Integer, ByVal ActionPlot As String, ByVal ActionClick As String, ByVal ActionOpen As String, ByVal SummaryOnly As Boolean) As Integer
        Public MustOverride Sub UpdatePoint(ByVal PointID As Integer, ByVal SourceID As Integer, ByVal Address As String, ByVal Description As String, ByVal GUID As String, ByVal IconIndex As Integer, ByVal Longitude As Double, ByVal Latitude As Double, ByVal SequenceNumber As Integer, ByVal SequenceInfo As String, ByVal ZoomShow As Integer, ByVal ZoomHide As Integer, ByVal ActionPlot As String, ByVal ActionClick As String, ByVal ActionOpen As String)
        Public MustOverride Sub DeletePoint(ByVal PointID As Integer, ByVal SourceID As Integer)
        Public MustOverride Sub DeletePoints(ByVal SourceID As Integer)
        Public MustOverride Function GetPoint(ByVal PointID As Integer) As Data.DataPoint
        Public MustOverride Function GetPoints(ByVal PointFilter As DotNetNuke.Modules.Map.Data.PointFilterInformation) As ArrayList
        Public MustOverride Function GetPoints_ByProviderName_NoGeo(ByVal ProviderName As String, ByVal IsUser As Boolean, Optional ByVal SourceID As Integer = -1) As IDataReader
#End Region
    End Class
End Namespace