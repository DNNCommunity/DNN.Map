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
    Public Class Map
        Inherits MapModuleBase
        Implements DotNetNuke.UI.Utilities.IClientAPICallbackEventHandler

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()

            'ADD THE VISUAL MODULE AND DATA MODULE
            If Not _VisualControl Is Nothing AndAlso Not _DataControl Is Nothing Then
                Me.Controls.Add(_VisualControl)
                Me.Controls.Add(_DataControl)
            End If
            If _ResourceFileName Is Nothing Then
                _ResourceFileName = DotNetNuke.Services.Localization.Localization.GetResourceFile(Me, Me.GetType().Name.Remove(Me.GetType.Name.Length - 5, 5) & ".ascx")
            End If

            Me.EnsureChildControls()
        End Sub

#End Region

        Private m_VisualControl As VisualMapControlBase
        Private ReadOnly Property _VisualControl() As VisualMapControlBase
            Get
                If m_VisualControl Is Nothing AndAlso Not Me.VisualProviderSrc Is Nothing AndAlso Me.VisualProviderSrc.Length > 0 Then
                    m_VisualControl = CType(Me.LoadControl(Me.VisualProviderSrc), VisualMapControlBase)
                    If Not m_VisualControl Is Nothing Then
                        m_VisualControl.Module = Me
                    End If
                End If
                Return m_VisualControl
            End Get
        End Property
        Private m_DataControl As DataMapControlBase
        Private ReadOnly Property _DataControl() As DataMapControlBase
            Get
                If m_DataControl Is Nothing AndAlso Not Me.SourceProviderSrc Is Nothing AndAlso Me.SourceProviderSrc.Length > 0 Then
                    Try
                        m_DataControl = CType(Me.LoadControl(Me.SourceProviderSrc), DataMapControlBase)
                        If Not m_DataControl Is Nothing Then
                            m_DataControl.Module = Me
                        End If
                    Catch ex As Exception

                    End Try
                End If
                Return m_DataControl
            End Get
        End Property

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                'REGISTER THE CLIENT API CALLBACK
                If Not _DataControl Is Nothing AndAlso Not _VisualControl Is Nothing Then
                    'these won't be necessary in next release after 3.2.0
                    If ClientAPI.BrowserSupportsFunctionality(ClientAPI.ClientFunctionality.XMLHTTP) _
                      AndAlso ClientAPI.BrowserSupportsFunctionality(ClientAPI.ClientFunctionality.XML) Then
                        ClientAPI.RegisterClientReference(Me.Page, ClientAPI.ClientNamespaceReferences.dnn_xml)
                        ClientAPI.RegisterClientReference(Me.Page, ClientAPI.ClientNamespaceReferences.dnn_xmlhttp)

                        'Only this line will be necessary after 3.2
                        Dim readFunc As String = "function " & "MapRead" & ModuleId & "() {" & ClientAPI.GetCallbackEventReference(Me, _DataControl.JavascriptReadFunctionInit, _VisualControl.JavascriptReadFunctionComplete, "'" & Me.ClientID & "'", _VisualControl.JavascriptReadFunctionFailure) & "}"
                        Dim writeFunc As String = "function " & "MapWrite" & ModuleId & "() {" & ClientAPI.GetCallbackEventReference(Me, _DataControl.JavascriptWriteFunctionInit, _VisualControl.JavascriptWriteFunctionComplete, "'" & Me.ClientID & "'", _VisualControl.JavascriptWriteFunctionFailure) & "}"
                        Dim servFunc As String = "function " & "MapService" & ModuleId & "() {" & ClientAPI.GetCallbackEventReference(Me, _DataControl.JavascriptServiceFunctionInit, _DataControl.JavascriptServiceFunctionComplete, "'" & Me.ClientID & "'", _DataControl.JavascriptServiceFunctionFailure) & "}"
                        Dim formatVar As String = "var MapPointFormat" & ModuleId & " = '" & _DataControl.JavascriptPointFormatValue & "';"

                        'CHECK TO SEE IF THE SERVICE SHOULD BE RUN
                        'THE SERVICE WILL BE REQUESTED TO RUN EVERY 5 Minutes, BUT
                        'WILL ONLY EXECUTE IF THE USER STAYS ON THE PAGE FOR 30 Seconds or more.
                        Dim runService As Boolean = False
                        If Application.Item("MAPLASTSERVICE") Is Nothing Then
                            runService = True
                        Else
                            If CType(Application.Item("MAPLASTSERVICE"), DateTime) < Now.AddMinutes(5) Then
                                runService = True
                            End If
                        End If
                        If runService Then
                            Application.Item("MAPLASTSERVICE") = Now
                            servFunc &= "window.setTimeout('MapService" & ModuleId & "();',10000);"
                        End If

                        If Me.Page.ClientScript.IsClientScriptBlockRegistered(_DataControl.JavascriptLibrary) = False Then
                            Me.Page.ClientScript.RegisterClientScriptBlock(GetType(String), _DataControl.JavascriptLibrary, "<script language=javascript src=""" & Me.ModulePath & "Sources/" & _DataControl.JavascriptLibrary & """></script>", False)
                        End If
                        If Me.Page.ClientScript.IsClientScriptBlockRegistered("MapCallback" & Me.ModuleId) = False Then
                            Me.Page.ClientScript.RegisterClientScriptBlock(GetType(String), "MapCallback" & Me.ModuleId, "<script language=javascript>" & readFunc & writeFunc & servFunc & formatVar & "</script>", False)
                        End If


                    End If
                Else
                    If Me.IsEditable Then
                        Dim ltlOutput As New Web.UI.LiteralControl
                        Dim output As String = Locale("Default")
                        If Not output Is Nothing Then
                            ltlOutput.Text = output
                            Me.Controls.Add(ltlOutput)
                            Me.EnsureChildControls()
                        End If
                    End If
                End If
            Catch ex As Exception
                DotNetNuke.Services.Exceptions.LogException(New DotNetNuke.Services.Exceptions.ModuleLoadException("Unable to add custom Visual provider to output stack.", ex, MyBase.ModuleConfiguration))
            End Try
        End Sub

        Public Function RaiseClientAPICallbackEvent(ByVal eventArgument As String) As String Implements UI.Utilities.IClientAPICallbackEventHandler.RaiseClientAPICallbackEvent
            Dim result As String = "No Source is available to return the data points"
            If Not _DataControl Is Nothing Then
                result = "Unknown Command, GET or SET anticipated - received: " & eventArgument
                If eventArgument.Length > 5 Then
                    Dim command As String = eventArgument.Substring(0, 4).ToUpper
                    eventArgument = eventArgument.Substring(4)
                    Select Case command
                        Case "GET:"
                            Return _DataControl.GetData(eventArgument)
                        Case "SET:"
                            Return _DataControl.SetData(eventArgument)
                        Case "GEO:"
                            Return _DataControl.ServiceData(eventArgument)
                    End Select
                End If
            End If
            Return result
        End Function

        Private _ResourceFileName As String
        Protected Function Locale(ByVal Name As String) As String
            Return DotNetNuke.Services.Localization.Localization.GetString(Name, _ResourceFileName)
        End Function
    End Class
End Namespace
