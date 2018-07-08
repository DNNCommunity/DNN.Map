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
    Public Class DataPoint
        Private _PointID As Integer
        Private _GUID As String
        Private _Address As String
        Private _Latitude As Double
        Private _Longitude As Double
        Private _IconIndex As Integer
        Private _SequenceNumber As Integer
        Private _SequenceInfo As String
        Private _Description As String
        Private _ZoomShow As Integer
        Private _ZoomHide As Integer
        Private _ActionClick As String
        Private _ActionOpen As String
        Private _ActionPlot As String
        Private _Distance As Double
        Private _SummaryCount As Integer


        Public Property PointID() As Integer
            Get
                Return _PointID
            End Get
            Set(ByVal Value As Integer)
                _PointID = Value
            End Set
        End Property
        Public Property SummaryCount() As Integer
            Get
                Return _SummaryCount
            End Get
            Set(ByVal Value As Integer)
                _SummaryCount = Value
            End Set
        End Property
        Public Property IconIndex() As Integer
            Get
                Return _IconIndex
            End Get
            Set(ByVal Value As Integer)
                _IconIndex = Value
            End Set
        End Property
        Public Property SequenceNumber() As Integer
            Get
                Return _SequenceNumber
            End Get
            Set(ByVal Value As Integer)
                _SequenceNumber = Value
            End Set
        End Property
        Public Property ZoomShow() As Integer
            Get
                Return _ZoomShow
            End Get
            Set(ByVal Value As Integer)
                _ZoomShow = Value
            End Set
        End Property
        Public Property ZoomHide() As Integer
            Get
                Return _ZoomHide
            End Get
            Set(ByVal Value As Integer)
                _ZoomHide = Value
            End Set
        End Property

        Public Property Latitude() As Double
            Get
                Return _Latitude
            End Get
            Set(ByVal Value As Double)
                _Latitude = Value
            End Set
        End Property
        Public Property Longitude() As Double
            Get
                Return _Longitude
            End Get
            Set(ByVal Value As Double)
                _Longitude = Value
            End Set
        End Property
        Public Property Distance() As Double
            Get
                Return _Distance
            End Get
            Set(ByVal Value As Double)
                _Distance = Value
            End Set
        End Property
        Public Property GUID() As String
            Get
                Return _GUID
            End Get
            Set(ByVal Value As String)
                _GUID = Value
            End Set
        End Property
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal Value As String)
                _Description = Value
            End Set
        End Property
        Public Property SequenceInfo() As String
            Get
                Return _SequenceInfo
            End Get
            Set(ByVal Value As String)
                _SequenceInfo = Value
            End Set
        End Property
        Public Property Address() As String
            Get
                Return _Address
            End Get
            Set(ByVal Value As String)
                _Address = Value
            End Set
        End Property
        Public Property ActionClick() As String
            Get
                Return _ActionClick
            End Get
            Set(ByVal Value As String)
                _ActionClick = Value
            End Set
        End Property
        Public Property ActionOpen() As String
            Get
                Return _ActionOpen
            End Get
            Set(ByVal Value As String)
                _ActionOpen = Value
            End Set
        End Property
        Public Property ActionPlot() As String
            Get
                Return _ActionPlot
            End Get
            Set(ByVal Value As String)
                _ActionPlot = Value
            End Set
        End Property

    End Class
    Public Class PointFilterInformation
        Public SourceID As Integer
        Public SourceInformation As Hashtable
    End Class
End Namespace