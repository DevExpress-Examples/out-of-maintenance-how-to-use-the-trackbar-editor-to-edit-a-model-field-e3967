@ModelType VB.MyModel

@Using (Html.BeginForm())

    @Html.DevExpress().TextBox( _
        Sub(settings)
            settings.Name = "Name"
        End Sub).Bind(Model.Name).GetHtml()
    
    @<br />
    
    @Html.DevExpress().TrackBar( _
        Sub(settings)
            settings.Name = "Day"

            settings.Properties.ValueField = "DayNum"
            settings.Properties.ToolTipField = "ToolTip"

            settings.Properties.ValueType = GetType(System.Int32)

            settings.Properties.ScalePosition = ScalePosition.Both
            settings.Width = System.Web.UI.WebControls.Unit.Pixel(300)
        End Sub).BindList(ViewData("Items")).Bind(Model.Day).GetHtml()
    
    @<br />
    
    @Html.DevExpress().Button( _
         Sub(settings)
             settings.Name = "btnSubmit"
             settings.UseSubmitBehavior = True
             settings.Text = "Submit"
         End Sub).GetHtml()

End Using