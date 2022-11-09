@ModelType ProntoEntries.AddonItem
@Code
    ViewData("Title") = "Create"
End Code


<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Create Addon Item</h2>
        @Html.ActionLink("Back to List", "Index")
    </div>
    <hr />

    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-horizontal">
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})


            <div class="form-group">
                @*@Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Name)}})
                    @Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
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
                @*@Html.LabelFor(Function(model) model.RaceID, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                <div class="col-md-10">
                    @Html.DropDownList("RaceID", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="Select Event")
                    @*@Html.EditorFor(Function(model) model.RaceID, New With {.htmlAttributes = New With {.class = "form-control"}})*@
                    @Html.ValidationMessageFor(Function(model) model.RaceID, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="button-group">
                <input type="submit" value="Create" class="btn btn-default" />
            </div>
        </div>
    End Using

</div>

