Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports System.Xml.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc

Public Class HomeController
    Inherits Controller
    Public Function Index() As ActionResult
        Dim model As New MyModel() With {.Name = "DevExpress User", .Day = 5}

        ViewData("Items") = GetItems()

        Return View(model)
    End Function

    <HttpPost()> _
    Public Function Index(<ModelBinder(GetType(DevExpressEditorsBinder))> ByVal model As MyModel) As ActionResult
        TempData("PostedModel") = model
        Return RedirectToAction("Success")
    End Function

    Public Function Success() As ActionResult
        Return View(TempData("PostedModel"))
    End Function

    Private Function GetItems() As IList
        Dim items As List(Of TrackBarItemObject) = New List(Of TrackBarItemObject)()
        Dim document As XDocument = XDocument.Load(Server.MapPath("~/App_Data/TimeLapse.xml"))
        For Each item In document.Root.Elements("Day")
            items.Add(New TrackBarItemObject() With {.DayNum = Convert.ToInt32(item.Attribute("DayNum").Value), .ToolTip = CStr(item.Attribute("ToolTip").Value), .Text = CStr(item.Attribute("Text").Value)})
        Next item

        Return items
    End Function

End Class

Public Class MyModel
    Private privateName As String
    Public Property Name() As String
        Get
            Return privateName
        End Get
        Set(ByVal value As String)
            privateName = value
        End Set
    End Property
    Private privateDay As Integer
    Public Property Day() As Integer
        Get
            Return privateDay
        End Get
        Set(ByVal value As Integer)
            privateDay = value
        End Set
    End Property
End Class

Public Class TrackBarItemObject
    Private privateDayNum As Integer
    Public Property DayNum() As Integer
        Get
            Return privateDayNum
        End Get
        Set(ByVal value As Integer)
            privateDayNum = value
        End Set
    End Property
    Private privateToolTip As String
    Public Property ToolTip() As String
        Get
            Return privateToolTip
        End Get
        Set(ByVal value As String)
            privateToolTip = value
        End Set
    End Property
    Private privateText As String
    Public Property Text() As String
        Get
            Return privateText
        End Get
        Set(ByVal value As String)
            privateText = value
        End Set
    End Property
End Class