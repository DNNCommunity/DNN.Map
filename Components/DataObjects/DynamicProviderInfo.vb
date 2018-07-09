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
    Public Class DynamicProviderInfo
        Public Const DataType_Data As String = "D"
        Public Const DataType_GeoLocator As String = "G"
        Public Const DataType_Visual As String = "V"

        Private _ProviderID As Integer
        Private _Name As String
        Private _ProviderType As String
        Private _Path As String
        Private _AdminPath As String
        Public Property ProviderID() As Integer
            Get
                Return _ProviderID
            End Get
            Set(ByVal Value As Integer)
                _ProviderID = Value
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
        Public Property ProviderType() As String
            Get
                Return _ProviderType
            End Get
            Set(ByVal Value As String)
                _ProviderType = Value
            End Set
        End Property
        Public Property Path() As String
            Get
                Return _Path
            End Get
            Set(ByVal Value As String)
                _Path = Value
            End Set
        End Property
        Public Property AdminPath() As String
            Get
                Return _AdminPath
            End Get
            Set(ByVal Value As String)
                _AdminPath = Value
            End Set
        End Property
    End Class
End Namespace