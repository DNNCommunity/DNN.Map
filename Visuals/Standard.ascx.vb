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
Namespace DotNetNuke.Modules.Map.Visuals

    Public Class Standard
        Inherits DotNetNuke.Modules.Map.Components.VisualMapControlBase
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents lblInstructions As System.Web.UI.WebControls.Label
        Protected WithEvents btnTest As System.Web.UI.WebControls.Button
        Protected WithEvents lblButtonTest As System.Web.UI.WebControls.Label

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
            Localize(CType(lblInstructions, Object), "Text")
        End Sub



        Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
            lblButtonTest.Text = Me.Module.TabId & ":" & Me.Module.ModuleId & "-" & Me.Module.ModulePath
        End Sub

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


    End Class
End Namespace

