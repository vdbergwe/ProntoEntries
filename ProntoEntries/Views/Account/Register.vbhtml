@ModelType RegisterViewModel
@Code
    ViewBag.Title = "Register"
End Code


<div class="orgcontainer create">
    <div class="titlediv">
        <h2>@ViewBag.Title</h2>
    </div>
    <hr />

    @Using Html.BeginForm("Register", "Account", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})

        @Html.AntiForgeryToken()

        @<text>

            @Html.ValidationSummary("", New With {.class = "text-danger"})
            <div class="form-group">
                @*@Html.LabelFor(Function(m) m.Email, New With {.class = "col-md-2 control-label"})*@
                <div class="col-md-10">
                    @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Email)})
                </div>
            </div>
            <div class="form-group">
                @*@Html.LabelFor(Function(m) m.Password, New With {.class = "col-md-2 control-label"})*@
                <div class="col-md-10">
                    @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.Password)})
                </div>
            </div>
            <div class="form-group">
                @*@Html.LabelFor(Function(m) m.ConfirmPassword, New With {.class = "col-md-2 control-label"})*@
                <div class="col-md-10">
                    @Html.PasswordFor(Function(m) m.ConfirmPassword, New With {.class = "form-control", .PlaceHolder = Html.DisplayNameFor(Function(model) model.ConfirmPassword)})
                </div>
            </div>
            <div class="button-group">                
                    <input type="submit" class="btn btn-default" value="Register" />               
            </div>
        </text>
    End Using
</div>

        @section Scripts
            @Scripts.Render("~/bundles/jqueryval")
        End Section
