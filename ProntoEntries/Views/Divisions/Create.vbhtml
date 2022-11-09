@ModelType ProntoEntries.Division
@Code
    ViewData("Title") = "Create"
End Code

<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Create</h2>
        @Html.ActionLink("Back to List", "Index")
    </div>
    <hr />
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-horizontal">
    @*<h4>Division</h4>*@
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    <div class="form-group">
        @*@Html.LabelFor(Function(model) model.Distance, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.Distance, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Distance)}})
            @Html.ValidationMessageFor(Function(model) model.Distance, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @*@Html.LabelFor(Function(model) model.Category, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.Category, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Category)}})
            @Html.ValidationMessageFor(Function(model) model.Category, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @*@Html.LabelFor(Function(model) model.Description, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.Description, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Description)}})
            @Html.ValidationMessageFor(Function(model) model.Description, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @*@Html.LabelFor(Function(model) model.Price, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.MinAge, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.MinAge)}})
            @Html.ValidationMessageFor(Function(model) model.MinAge, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @*@Html.LabelFor(Function(model) model.Price, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.MaxAge, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.MaxAge)}})
            @Html.ValidationMessageFor(Function(model) model.MaxAge, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @*@Html.LabelFor(Function(model) model.StartTime, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.StartTime, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.StartTime)}})
            @Html.ValidationMessageFor(Function(model) model.StartTime, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @*@Html.LabelFor(Function(model) model.Price, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.Price, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Price)}})
            @Html.ValidationMessageFor(Function(model) model.Price, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @*@Html.LabelFor(Function(model) model.RaceID, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
        <div class="col-md-10">
            @*@Html.EditorFor(Function(model) model.RaceID, New With {.htmlAttributes = New With {.class = "form-control"}})*@
            @Html.DropDownList("RaceID", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="Select Event")
            @Html.ValidationMessageFor(Function(model) model.RaceID, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="button-group">
        <input type="submit" value="Create" class="btn btn-default" />
    </div>
</div>
    End Using
</div>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
