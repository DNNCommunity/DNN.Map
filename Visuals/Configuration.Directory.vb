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
Imports System.Data
Imports System.Data.SqlClient
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters
Imports DotNetNuke

Namespace DotNetNuke.Modules.Map.Visuals
#Region "Configuration"
    Public Class DirectoryConfiguration
        Inherits Configuration

#Region "Directory Variables and Public Properties"
        Private _DirectoryRecordsPerPage As String = ""
        Private _DirectorySearchTemplate As String = ""
        Private _DirectorySearchNoResults As String = ""
        Private _DirectorySearchTextboxID As String = ""
        Private _DirectorySearchButtonID As String = ""
        Private _DirectoryElementID As String = ""
        Private _DirectoryCSS As String = ""
        Private _DirectoryCSSItem As String = ""
        Private _DirectoryCSSItemAlternate As String = ""
        Private _DirectoryCSSItemHover As String = ""
        Private _DirectoryPagingElementID As String = ""
        Private _DirectoryPagingCSS As String = ""
        Private _DirectoryPagingCSSLink As String = ""
        Private _DirectoryPagingCSSHover As String = ""
        Private _DirectoryItemTemplate As String = ""
        Private _DirectoryOnclickPlot As Boolean
        Private _DirectoryOnclickPan As Boolean
        Private _DirectoryOnclickClear As Boolean
        Private _DirectoryOnclickZoom As String = ""
        Private _DirectoryUseDistanceCalculation As Boolean
        Private _DirectoryRadius As Integer

        Public Property DirectoryRadius() As Integer
            Get
                Return _DirectoryRadius
            End Get
            Set(ByVal Value As Integer)
                _DirectoryRadius = Value
            End Set
        End Property

        Public Property DirectoryRecordsPerPage() As String
            Get
                Return _DirectoryRecordsPerPage
            End Get
            Set(ByVal Value As String)
                _DirectoryRecordsPerPage = Value
            End Set
        End Property
        Public Property DirectorySearchTemplate() As String
            Get
                Return _DirectorySearchTemplate
            End Get
            Set(ByVal Value As String)
                _DirectorySearchTemplate = Value
            End Set
        End Property
        Public Property DirectorySearchNoResults() As String
            Get
                Return _DirectorySearchNoResults
            End Get
            Set(ByVal Value As String)
                _DirectorySearchNoResults = Value
            End Set
        End Property
        Public Property DirectorySearchTextboxID() As String
            Get
                Return _DirectorySearchTextboxID
            End Get
            Set(ByVal Value As String)
                _DirectorySearchTextboxID = Value
            End Set
        End Property
        Public Property DirectorySearchButtonID() As String
            Get
                Return _DirectorySearchButtonID
            End Get
            Set(ByVal Value As String)
                _DirectorySearchButtonID = Value
            End Set
        End Property
        Public Property DirectoryElementID() As String
            Get
                Return _DirectoryElementID
            End Get
            Set(ByVal Value As String)
                _DirectoryElementID = Value
            End Set
        End Property
        Public Property DirectoryCSS() As String
            Get
                Return _DirectoryCSS
            End Get
            Set(ByVal Value As String)
                _DirectoryCSS = Value
            End Set
        End Property
        Public Property DirectoryCSSItem() As String
            Get
                Return _DirectoryCSSItem
            End Get
            Set(ByVal Value As String)
                _DirectoryCSSItem = Value
            End Set
        End Property
        Public Property DirectoryCSSItemAlternate() As String
            Get
                Return _DirectoryCSSItemAlternate
            End Get
            Set(ByVal Value As String)
                _DirectoryCSSItemAlternate = Value
            End Set
        End Property
        Public Property DirectoryCSSItemHover() As String
            Get
                Return _DirectoryCSSItemHover
            End Get
            Set(ByVal Value As String)
                _DirectoryCSSItemHover = Value
            End Set
        End Property
        Public Property DirectoryPagingElementID() As String
            Get
                Return _DirectoryPagingElementID
            End Get
            Set(ByVal Value As String)
                _DirectoryPagingElementID = Value
            End Set
        End Property
        Public Property DirectoryPagingCSS() As String
            Get
                Return _DirectoryPagingCSS
            End Get
            Set(ByVal Value As String)
                _DirectoryPagingCSS = Value
            End Set
        End Property
        Public Property DirectoryPagingCSSLink() As String
            Get
                Return _DirectoryPagingCSSLink
            End Get
            Set(ByVal Value As String)
                _DirectoryPagingCSSLink = Value
            End Set
        End Property
        Public Property DirectoryPagingCSSHover() As String
            Get
                Return _DirectoryPagingCSSHover
            End Get
            Set(ByVal Value As String)
                _DirectoryPagingCSSHover = Value
            End Set
        End Property
        Public Property DirectoryItemTemplate() As String
            Get
                Return _DirectoryItemTemplate
            End Get
            Set(ByVal Value As String)
                _DirectoryItemTemplate = Value
            End Set
        End Property
        Public Property DirectoryOnclickPlot() As Boolean
            Get
                Return _DirectoryOnclickPlot
            End Get
            Set(ByVal Value As Boolean)
                _DirectoryOnclickPlot = Value
            End Set
        End Property
        Public Property DirectoryOnclickPan() As Boolean
            Get
                Return _DirectoryOnclickPan
            End Get
            Set(ByVal Value As Boolean)
                _DirectoryOnclickPan = Value
            End Set
        End Property
        Public Property DirectoryOnclickClear() As Boolean
            Get
                Return _DirectoryOnclickClear
            End Get
            Set(ByVal Value As Boolean)
                _DirectoryOnclickClear = Value
            End Set
        End Property
        Public Property DirectoryOnclickZoom() As String
            Get
                Return _DirectoryOnclickZoom
            End Get
            Set(ByVal Value As String)
                _DirectoryOnclickZoom = Value
            End Set
        End Property
        Public Property DirectoryUseDistanceCalculation() As Boolean
            Get
                Return _DirectoryUseDistanceCalculation
            End Get
            Set(ByVal Value As Boolean)
                _DirectoryUseDistanceCalculation = Value
            End Set
        End Property
#End Region
#Region "ISerializable Members"
        Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal hsh As Hashtable)
            MyBase.New(hsh)
            _DirectoryRadius = CType(hsh.Item("_PositionalRadius"), Integer)
            _DirectoryOnclickPlot = CType(hsh.Item("_DirectoryOnclickPlot"), Boolean)
            _DirectoryOnclickPan = CType(hsh.Item("_DirectoryOnclickPan"), Boolean)
            _DirectoryOnclickClear = CType(hsh.Item("_DirectoryOnclickClear"), Boolean)
            _DirectoryUseDistanceCalculation = CType(hsh.Item("_DirectoryUseDistanceCalculation"), Boolean)

            _DirectoryRecordsPerPage = CType(hsh.Item("_DirectoryRecordsPerPage"), String)
            _DirectorySearchTemplate = CType(hsh.Item("_DirectorySearchTemplate"), String)
            _DirectorySearchNoResults = CType(hsh.Item("_DirectorySearchNoResults"), String)
            _DirectorySearchTextboxID = CType(hsh.Item("_DirectorySearchTextboxID"), String)
            _DirectorySearchButtonID = CType(hsh.Item("_DirectorySearchButtonID"), String)
            _DirectoryElementID = CType(hsh.Item("_DirectoryElementID"), String)
            _DirectoryCSS = CType(hsh.Item("_DirectoryCSS"), String)
            _DirectoryCSSItem = CType(hsh.Item("_DirectoryCSSItem"), String)
            _DirectoryCSSItemAlternate = CType(hsh.Item("_DirectoryCSSItemAlternate"), String)
            _DirectoryCSSItemHover = CType(hsh.Item("_DirectoryCSSItemHover"), String)
            _DirectoryPagingElementID = CType(hsh.Item("_DirectoryPagingElementID"), String)
            _DirectoryPagingCSS = CType(hsh.Item("_DirectoryPagingCSS"), String)
            _DirectoryPagingCSSLink = CType(hsh.Item("_DirectoryPagingCSSLink"), String)
            _DirectoryPagingCSSHover = CType(hsh.Item("_DirectoryPagingCSSHover"), String)
            _DirectoryItemTemplate = CType(hsh.Item("_DirectoryItemTemplate"), String)
            _DirectoryOnclickZoom = CType(hsh.Item("_DirectoryOnclickZoom"), String)
        End Sub
        Public Overrides Sub Settings(ByRef hsh As Hashtable)
            MyBase.Settings(hsh)

            hsh.Add("_PositionalRadius", CheckNothing(_DirectoryRadius))
            hsh.Add("_DirectoryRecordsPerPage", CheckNothing(_DirectoryRecordsPerPage))
            hsh.Add("_DirectorySearchTemplate", CheckNothing(_DirectorySearchTemplate))
            hsh.Add("_DirectorySearchNoResults", CheckNothing(_DirectorySearchNoResults))
            hsh.Add("_DirectorySearchTextboxID", CheckNothing(_DirectorySearchTextboxID))
            hsh.Add("_DirectorySearchButtonID", CheckNothing(_DirectorySearchButtonID))
            hsh.Add("_DirectoryElementID", CheckNothing(_DirectoryElementID))
            hsh.Add("_DirectoryCSS", CheckNothing(_DirectoryCSS))
            hsh.Add("_DirectoryCSSItem", CheckNothing(_DirectoryCSSItem))
            hsh.Add("_DirectoryCSSItemAlternate", CheckNothing(_DirectoryCSSItemAlternate))
            hsh.Add("_DirectoryCSSItemHover", CheckNothing(_DirectoryCSSItemHover))
            hsh.Add("_DirectoryPagingElementID", CheckNothing(_DirectoryPagingElementID))
            hsh.Add("_DirectoryPagingCSS", CheckNothing(_DirectoryPagingCSS))
            hsh.Add("_DirectoryPagingCSSLink", CheckNothing(_DirectoryPagingCSSLink))
            hsh.Add("_DirectoryPagingCSSHover", CheckNothing(_DirectoryPagingCSSHover))
            hsh.Add("_DirectoryItemTemplate", CheckNothing(_DirectoryItemTemplate))
            hsh.Add("_DirectoryOnclickZoom", CheckNothing(_DirectoryOnclickZoom))
            hsh.Add("_DirectoryOnclickPlot", CheckNothing(_DirectoryOnclickPlot))
            hsh.Add("_DirectoryOnclickPan", CheckNothing(_DirectoryOnclickPan))
            hsh.Add("_DirectoryOnclickClear", CheckNothing(_DirectoryOnclickClear))
            hsh.Add("_DirectoryUseDistanceCalculation", CheckNothing(_DirectoryUseDistanceCalculation))
        End Sub
        Public Shared Function Serialize(ByVal Value As DirectoryConfiguration) As String
            Try
                Dim hsh As New Hashtable
                Value.Settings(hsh)
                Return DotNetNuke.Common.SerializeHashTableXml(hsh)
            Catch
                Return ""
            End Try
        End Function
        Public Shared Function Deserialize(ByVal Value As String) As DirectoryConfiguration
            Dim m As DirectoryConfiguration = Nothing
            Try
                Dim hsh As Hashtable
                hsh = DotNetNuke.Common.DeserializeHashTableXml(Value)
                m = New DirectoryConfiguration(hsh)
            Catch ex As Exception
            End Try
            Return m
        End Function
#End Region
    End Class
#End Region

End Namespace