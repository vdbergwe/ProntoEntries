@ModelType ProntoEntries.Voucher

@Code
    Layout = Nothing
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Create</title>
</head>
<body>
    @Using (Html.BeginForm()) 
        @Html.AntiForgeryToken()
        
        @<div class="form-horizontal">
            <h4>Voucher</h4>
            <hr />
            @Html.ValidationSummary(True, "", New With { .class = "text-danger" })
            <div class="form-group">
                @Html.LabelFor(Function(model) model.Code, htmlAttributes:= New With { .class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Code, New With { .htmlAttributes = New With { .class = "form-control" } })
                    @Html.ValidationMessageFor(Function(model) model.Code, "", New With { .class = "text-danger" })
                </div>
            </div>
    
            <div class="form-group">
                @Html.LabelFor(Function(model) model.Value, htmlAttributes:= New With { .class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Value, New With { .htmlAttributes = New With { .class = "form-control" } })
                    @Html.ValidationMessageFor(Function(model) model.Value, "", New With { .class = "text-danger" })
                </div>
            </div>
    
            <div class="form-group">
                @Html.LabelFor(Function(model) model.IssuedBy, htmlAttributes:= New With { .class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.IssuedBy, New With { .htmlAttributes = New With { .class = "form-control" } })
                    @Html.ValidationMessageFor(Function(model) model.IssuedBy, "", New With { .class = "text-danger" })
                </div>
            </div>
    
            <div class="form-group">
                @Html.LabelFor(Function(model) model.Pf_Reference, htmlAttributes:= New With { .class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Pf_Reference, New With { .htmlAttributes = New With { .class = "form-control" } })
                    @Html.ValidationMessageFor(Function(model) model.Pf_Reference, "", New With { .class = "text-danger" })
                </div>
            </div>
    
            <div class="form-group">
                @Html.LabelFor(Function(model) model.M_Reference, htmlAttributes:= New With { .class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.M_Reference, New With { .htmlAttributes = New With { .class = "form-control" } })
                    @Html.ValidationMessageFor(Function(model) model.M_Reference, "", New With { .class = "text-danger" })
                </div>
            </div>
    
            <div class="form-group">
                @Html.LabelFor(Function(model) model.Date, htmlAttributes:= New With { .class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Date, New With { .htmlAttributes = New With { .class = "form-control" } })
                    @Html.ValidationMessageFor(Function(model) model.Date, "", New With { .class = "text-danger" })
                </div>
            </div>
    
            <div class="form-group">
                @Html.LabelFor(Function(model) model.Status, htmlAttributes:= New With { .class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Status, New With { .htmlAttributes = New With { .class = "form-control" } })
                    @Html.ValidationMessageFor(Function(model) model.Status, "", New With { .class = "text-danger" })
                </div>
            </div>
    
            <div class="form-group">
                @Html.LabelFor(Function(model) model.UsedBy, htmlAttributes:= New With { .class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.UsedBy, New With { .htmlAttributes = New With { .class = "form-control" } })
                    @Html.ValidationMessageFor(Function(model) model.UsedBy, "", New With { .class = "text-danger" })
                </div>
            </div>
    
            <div class="form-group">
                @Html.LabelFor(Function(model) model.UsedDate, htmlAttributes:= New With { .class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.UsedDate, New With { .htmlAttributes = New With { .class = "form-control" } })
                    @Html.ValidationMessageFor(Function(model) model.UsedDate, "", New With { .class = "text-danger" })
                </div>
            </div>
    
            <div class="form-group">
                @Html.LabelFor(Function(model) model.UsedM_Reference, htmlAttributes:= New With { .class = "control-label col-md-2" })
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.UsedM_Reference, New With { .htmlAttributes = New With { .class = "form-control" } })
                    @Html.ValidationMessageFor(Function(model) model.UsedM_Reference, "", New With { .class = "text-danger" })
                </div>
            </div>
    
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input type="submit" value="Create" class="btn btn-default" />
                </div>
            </div>
        </div>
    End Using
    
    <div>
        @Html.ActionLink("Back to List", "Index")
    </div>
</body>
</html>
