@ModelType ProntoEntries.RaceEvent
@Code
    ViewData("Title") = "Create"
End Code

<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Create</h2>
        @Html.ActionLink("Back to List", "Index")
    </div>
    <hr />
    @Using (Html.BeginForm("Create", "RaceEvents", FormMethod.Post, New With {.enctype = "multipart/form-data"}))
        @Html.AntiForgeryToken()

        @<div class="form-horizontal1">
            @*<h4>RaceEvent</h4>*@

            <div class="form--el1">
                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                <div class="form-group">
                    @*@Html.LabelFor(Function(model) model.RaceName, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.RaceName, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.RaceName)}})
                        @Html.ValidationMessageFor(Function(model) model.RaceName, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @*@Html.LabelFor(Function(model) model.RaceDescription, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.RaceDescription, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.RaceDescription)}})
                        @Html.ValidationMessageFor(Function(model) model.RaceDescription, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @*@Html.LabelFor(Function(model) model.RaceDate, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.RaceDate, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.RaceDate)}})
                        @Html.ValidationMessageFor(Function(model) model.RaceDate, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @*@Html.LabelFor(Function(model) model.RaceType, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.RaceType, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.RaceType)}})
                        @Html.ValidationMessageFor(Function(model) model.RaceType, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @*@Html.LabelFor(Function(model) model.Coordinates, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.Coordinates, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Coordinates)}})
                        @Html.ValidationMessageFor(Function(model) model.Coordinates, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @*@Html.LabelFor(Function(model) model.Address, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.Address, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Address)}})
                        @Html.ValidationMessageFor(Function(model) model.Address, "", New With {.class = "text-danger"})
                    </div>
                </div>
            </div>
            <div class="form--el2">
                <div class="form-group">
                    @*@Html.LabelFor(Function(model) model.City, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.City, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.City)}})
                        @Html.ValidationMessageFor(Function(model) model.City, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @*@Html.LabelFor(Function(model) model.Province, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.Province, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Province)}})
                        @Html.ValidationMessageFor(Function(model) model.Province, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @*@Html.LabelFor(Function(model) model.AdminCharge, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.AdminCharge, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.AdminCharge)}})
                        @Html.ValidationMessageFor(Function(model) model.AdminCharge, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @*@Html.LabelFor(Function(model) model.OrgID, htmlAttributes:=New With {.class = "control-label col-md-2"})*@
                    <div class="col-md-10">

                        @*@Html.EditorFor(Function(model) model.OrgID, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.OrgID)}})*@
                        @Html.DropDownList("OrgID", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="")
                        @Html.ValidationMessageFor(Function(model) model.OrgID, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.Image, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @*@Html.EditorFor(Function(model) model.Image, New With {.htmlAttributes = New With {.class = "form-control"}})*@
                        <input type="file" id="imgFile2" name="imgFile2" class="form-control" placeholder="Race Logo" />
                        @Html.ValidationMessageFor(Function(model) model.Image, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.Background, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @*@Html.EditorFor(Function(model) model.Background, New With {.htmlAttributes = New With {.class = "form-control"}})*@
                        <input type="file" id="imgBackground" name="imgBackground" class="form-control" placeholder="Race Background" />
                        @Html.ValidationMessageFor(Function(model) model.Background, "", New With {.class = "text-danger"})
                    </div>
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
