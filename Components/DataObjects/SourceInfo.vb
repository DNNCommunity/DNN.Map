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
    Public Class SourceInfo
        Private _PortalID As Integer
        Private _ProviderID As Integer
        Private _SourceID As Integer
        Private _ServiceFlag As Integer
        Private _GeoLocatorProviderID As Integer
        Private _Name As String
        Private _Description As String
        Private _Settings As String
        Private _GeoLocatorSettings As String
        Public Property PortalID() As Integer
            Get
                Return _PortalID
            End Get
            Set(ByVal Value As Integer)
                _PortalID = Value
            End Set
        End Property
        Public Property SourceID() As Integer
            Get
                Return _SourceID
            End Get
            Set(ByVal Value As Integer)
                _SourceID = Value
            End Set
        End Property
        Public Property ServiceFlag() As Integer
            Get
                Return _ServiceFlag
            End Get
            Set(ByVal value As Integer)
                _ServiceFlag = value
            End Set
        End Property
        Public Property ProviderID() As Integer
            Get
                Return _ProviderID
            End Get
            Set(ByVal Value As Integer)
                _ProviderID = Value
            End Set
        End Property
        Public Property GeoLocatorProviderID() As Integer
            Get
                Return _GeoLocatorProviderID
            End Get
            Set(ByVal Value As Integer)
                _GeoLocatorProviderID = Value
            End Set
        End Property
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
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
        Public Property Settings() As String
            Get
                Return _Settings
            End Get
            Set(ByVal Value As String)
                _Settings = Value
            End Set
        End Property
        Public Property GeoLocatorSettings() As String
            Get
                Return _GeoLocatorSettings
            End Get
            Set(ByVal Value As String)
                _GeoLocatorSettings = Value
            End Set
        End Property
    End Class
End Namespace