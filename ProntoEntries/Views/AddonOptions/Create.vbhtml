@ModelType ProntoEntries.AddonOption
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
    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
    @*<div class="form-group">
            @Html.LabelFor(Function(model) model.ItemID, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.ItemID, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.ItemID, "", New With { .class = "text-danger" })
            </div>
        </div>*@

    @*<input type="hidden" name="Id" value="@ViewBag.ItemID" />*@

    <div class="form-group">
        @*@Html.LabelFor(Function(model) model.Size, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.Size, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Size)}})
            @Html.ValidationMessageFor(Function(model) model.Size, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @*@Html.LabelFor(Function(model) model.Amount, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.Amount, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Amount)}})
            @Html.ValidationMessageFor(Function(model) model.Amount, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" value="Create" class="btn btn-default" />
        </div>
    </div>
</div>
    End Using
</div>
