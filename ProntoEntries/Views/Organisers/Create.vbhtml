@ModelType ProntoEntries.Organiser
@Code
    ViewData("Title") = "Create Organizer"
End Code
<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Create Organizer</h2>
        @Html.ActionLink("Back to List", "Index")
    </div>

    @Using (Html.BeginForm("Create", "Organisers", FormMethod.Post, New With {.enctype = "multipart/form-data"}))
        @Html.AntiForgeryToken()

        @<div class="form-horizontal">
            @*<h4>Organiser</h4>*@
            <hr />
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
            <div class="form-group">
                @*@Html.LabelFor(Function(model) model.OrgName, htmlAttributes:= New With { .class = "control-label col-md-2" })*@
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.OrgName, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = "Organization Name"}})
                    @Html.ValidationMessageFor(Function(model) model.OrgName, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @*@Html.LabelFor(Function(model) model.OrgEmail, htmlAttributes:= New With { .class = "control-label col-md-2" })*@
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.OrgEmail, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = "Email Address"}})
                    @Html.ValidationMessageFor(Function(model) model.OrgEmail, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @*@Html.LabelFor(Function(model) model.OrgTel, htmlAttributes:= New With { .class = "control-label col-md-2" })*@
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.OrgTel, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = "Phone Number"}})
                    @Html.ValidationMessageFor(Function(model) model.OrgTel, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @*@Html.LabelFor(Function(model) model.OrgWebsite, htmlAttributes:= New With { .class = "control-label col-md-2" })*@
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.OrgWebsite, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = "Website Address"}})
                    @Html.ValidationMessageFor(Function(model) model.OrgWebsite, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @*@Html.LabelFor(Function(model) model.OrgVatNumber, htmlAttributes:= New With { .class = "control-label col-md-2" })*@
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.OrgVatNumber, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = "VAT Number"}})
                    @Html.ValidationMessageFor(Function(model) model.OrgVatNumber, "", New With {.class = "text-danger"})
                </div>
            </div>

            @*<div class="form-group">
                    @Html.LabelFor(Function(model) model.AdminUserID, htmlAttributes:= New With { .class = "control-label col-md-2" })
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.AdminUserID, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = "Admin User"}})
                        @Html.ValidationMessageFor(Function(model) model.AdminUserID, "", New With { .class = "text-danger" })
                    </div>
                </div>*@

            <div class="form-group">
                @*@Html.LabelFor(Function(model) model.Image, htmlAttributes:= New With { .class = "control-label col-md-2" })*@
                <div class="col-md-10">
                    @*@Html.EditorFor(Function(model) model.Image, New With {.htmlAttributes = New With {.class = "form-control", .PlaceHolder = "Company Logo"}})*@

                    <input type="file" id="imgFile" name="imgFile" class="form-control" placeholder="Company Logo" />
                    @Html.ValidationMessageFor(Function(model) model.Image, "", New With {.class = "text-danger"})
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
