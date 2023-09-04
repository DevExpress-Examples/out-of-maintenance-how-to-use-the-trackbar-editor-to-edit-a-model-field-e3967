Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Xml.Linq
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc

Namespace CS.Controllers

    Public Class HomeController
        Inherits Controller

        Public Function Index() As ActionResult
            Dim model As MyModel = New MyModel() With {.Name = "DevExpress User", .Day = 5}
            ViewData("Items") = GetItems()
            Return View(model)
        End Function

        <HttpPost>
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
            Next

            Return items
        End Function
    End Class

    Public Class MyModel

        Public Property Name As String

        Public Property Day As Integer
    End Class

    Public Class TrackBarItemObject

        Public Property DayNum As Integer

        Public Property ToolTip As String

        Public Property Text As String
    End Class
End Namespace
